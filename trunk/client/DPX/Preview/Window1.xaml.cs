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
            menuUserInformation.IsEnabled = false;
            menuStatistics.IsEnabled = false;
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
                menuUserInformation.IsEnabled = true;
                menuStatistics.IsEnabled = true;
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

        private void updatePageNumber()
        {
            labelPageNumber.Content = (currentPanelNumber + 1).ToString() + " of " + dr.NumOfPages().ToString();
        }

        private void buttonExportImageClick(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog1 = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog1.Filter = "JPEG (*.jpg)|*.jpg";
            if (saveFileDialog1.ShowDialog() == true)
            {
                FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.Create);
                RenderTargetBitmap rtb = new RenderTargetBitmap(Convert.ToInt32( Inky.Width / 2.0), Convert.ToInt32( Inky.Height / 2.0), 96d, 96d, PixelFormats.Default);
               
                rtb.Render(Inky);
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(rtb));
                encoder.Save(fs);
                fs.Close();
            }
        }

        private void displayPanel(int n)
        {
            
            
            
            //Some error checking to make sure we don't crash
            if (dr != null && n >= 0 && n < dr.NumOfPages())
            {
                currentPanelNumber = n;
                //Read in the panel
                DyKnowPage dp = dr.getDyKnowPage(n);


                //Display all of the images
                Inky.Children.Clear();
                List<DyKnowImage> dki = dp.Images;
                //Add all of the images as children (there should only be 1, but this works for now)
                for (int i = 0; i < dki.Count; i++)
                {
                    //Get the actual image
                    ImageData id = dr.getImageData(dki[i].Id);
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    bi.StreamSource = new MemoryStream(System.Convert.FromBase64String(id.Img));
                    bi.EndInit();
                    //Resize the image if it is not the correct size
                    if (dki[i].Ph != Inky.Height || dki[i].Pw != Inky.Width)
                    {
                        
                        TransformedBitmap tb = new TransformedBitmap();
                        tb.BeginInit();
                        tb.Source = bi;
                        //ScaleTransform sc = new ScaleTransform(1024.0 / (double)dki[i].Pw, 768.0 / (double)dki[i].Ph);
                        ScaleTransform sc = new ScaleTransform(Inky.Width / bi.Width, Inky.Height/ bi.Height);
                        tb.Transform = sc;
                        tb.EndInit();
                        //Add the image to the canvas
                        Image im = new Image();
                        im.Source = tb;
                        Inky.Children.Add(im);
                    }
                    //The image is the correct size
                    else{
                        //Add the image to the canvas
                        Image im = new Image();
                        im.Source = bi;
                        Inky.Children.Add(im);
                    }
                }
                


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
                        //Resize the panel if it is not the default resolution
                        if (pens[i].PH != Inky.Height || pens[i].PW != Inky.Width)
                        {
                            Matrix inkTransform = new Matrix();
                            inkTransform.Scale(Inky.Width / (double)pens[i].PW, Inky.Height / (double)pens[i].PH);
                            sc.Transform(inkTransform, true);
                        }
                        //Add the ink stroke to the canvas
                        Inky.Strokes.Add(sc);
                    }
                }
            }
        }

        private void displayAboutWindow(object sender, RoutedEventArgs e)
        {
            AboutDPX popupWindow = new AboutDPX();
            popupWindow.Owner = this;
            popupWindow.ShowDialog();
        }

        private void displayUserInformationWindow(object sender, RoutedEventArgs e)
        {
            FlowDocument fd = new FlowDocument();
            fd.Blocks.Add(new Paragraph(new Run("Students")));
            if (fd != null)
            {
                for (int i = 0; i < dr.NumOfPages(); i++)
                {
                    DyKnowPage d = dr.getDyKnowPage(i);
                    String s = (i + 1).ToString() + ") " + d.FullName;
                    Paragraph p = new Paragraph(new Run(s));
                    p.LineHeight = 5.0;
                    fd.Blocks.Add(p);
                }
            }
            UserInformation popupWindow = new UserInformation(fd);
            popupWindow.Owner = this;
            popupWindow.ShowDialog();
        }

        private void displayStatisticsWindow(object sender, RoutedEventArgs e)
        {
            Statistics popupWindow = new Statistics(dr);
            popupWindow.Owner = this;
            popupWindow.ShowDialog();
        }
    }
}
