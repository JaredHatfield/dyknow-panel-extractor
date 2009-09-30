using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Ink;
using System.IO;
using QuickReader;

namespace Preview
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private DyKnowReader dr;
        private int currentPanelNumber = 0;

        public Window1()
        {
            InitializeComponent();
            currentPanelNumber = 0;
        }

        private void updatePageNumber()
        {
            labelPageNumber.Content = (currentPanelNumber + 1).ToString() + " of " + dr.NumOfPages().ToString();
        }
        private void buttonLoadClick(object sender, RoutedEventArgs e)
        {
            //Let the user choose which file to open
            Microsoft.Win32.OpenFileDialog openFileDialog1 = new Microsoft.Win32.OpenFileDialog();
            openFileDialog1.Filter = "DyKnow files (*.dyz)|*.dyz";
            if (openFileDialog1.ShowDialog() == true)
            {
                //Open the DyKnow file
                dr = new DyKnowReader(openFileDialog1.FileName);
                currentPanelNumber = 0;
                Inky.Strokes.Clear();
                displayPanel(currentPanelNumber);
                updatePageNumber();
                
            }
        }

        private void buttonNextClick(object sender, RoutedEventArgs e)
        {
            if (dr != null)
            {
                if ((currentPanelNumber + 1) < dr.NumOfPages())
                {
                    Inky.Strokes.Clear();
                    displayPanel(++currentPanelNumber);
                    updatePageNumber();
                }
            }
        }

        private void buttonPreviousClick(object sender, RoutedEventArgs e)
        {
            if (dr != null)
            {
                if (currentPanelNumber > 0)
                {
                    Inky.Strokes.Clear();
                    displayPanel(--currentPanelNumber);
                    updatePageNumber();
                }
            }
        }

        private void buttonExportImageClick(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog1 = new Microsoft.Win32.SaveFileDialog();
            //saveFileDialog1.Filter = "DyKnow files (*.dyz)|*.dyz";
            if (saveFileDialog1.ShowDialog() == true)
            {
                FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.Create);

                RenderTargetBitmap rtb = new RenderTargetBitmap(1024 / 2, 768 / 2, 96d, 96d, PixelFormats.Default);
                
                rtb.Render(Inky);
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(rtb));
                
                //encoder.Frames.Add(BitmapFrame.Create(Inky));

                encoder.Save(fs);
                fs.Close();
            }
            
        }

        private void DisplayAboutWindow(object sender, RoutedEventArgs e)
        {
            AboutDPX popupWindow = new AboutDPX();
            popupWindow.Owner = this;
            popupWindow.ShowDialog();
        }

        private void displayPanel(int n)
        {
            //Some error checking to make sure we don't crash
            if (dr != null && n >= 0 && n < dr.NumOfPages())
            {
                currentPanelNumber = n;
                //Read in the panel
                DyKnowPage dp = dr.getDyKnowPage(n);
                //Get that Panel's pen strokes
                List<DyKnowPenStroke> pens = dp.Pens;
                //Loop through all of the pen strokes
                for (int i = 0; i < pens.Count; i++)
                {
                    //Only display the ink if it wasn't deleted
                    if (!pens[i].DELETED)
                    {
                        //The data is encoded as a string
                        String data = pens[i].DATA;
                        //Truncate off the "base64:" from the beginning of the string
                        data = data.Substring(7);
                        //Decode the string
                        byte[] bufferData = Convert.FromBase64String(data);
                        //Turn the string into a stream
                        Stream s = new MemoryStream(bufferData);
                        //Convert the stream into an ink stroke
                        StrokeCollection sc = new StrokeCollection(s);

                        if (pens[i].PH != 768 || pens[i].PW != 1024)
                        {
                            Matrix inkTransform = new Matrix();
                            inkTransform.Scale(1024.0 / (double)pens[i].PW, 768.0 / (double)pens[i].PH);
                            sc.Transform(inkTransform, true);
                        }
                        //Add the ink stroke to the canvas
                        Inky.Strokes.Add(sc);
                    }
                }
            }
        }
    }
}
