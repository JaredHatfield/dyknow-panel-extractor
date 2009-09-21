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
        public Window1()
        {
            InitializeComponent();
        }

        private void buttonLoadClick(object sender, RoutedEventArgs e)
        {
            //Clear what is currently on the screen
            Inky.Strokes.Clear();

            //Let the user choose which file to open
            Microsoft.Win32.OpenFileDialog openFileDialog1 = new Microsoft.Win32.OpenFileDialog();
            openFileDialog1.Filter = "DyKnow files (*.dyz)|*.dyz";
            if (openFileDialog1.ShowDialog() == true)
            {
                //Open the DyKnow file
                DyKnowReader dr = new DyKnowReader(openFileDialog1.FileName);
                //Read in the first panel
                DyKnowPage dp = dr.getDyKnowPage(0);
                //Get that Panel's pen strokes
                List<DyKnowPenStroke> pens = dp.Pens;
                //Loop through all of the pen strokes
                for (int i = 0; i < pens.Count; i++)
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
                    //Add the ink stroke to the canvas
                    Inky.Strokes.Add(sc);
                }
                
            }
        }
    }
}
