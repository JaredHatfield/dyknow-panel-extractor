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
using System.Windows.Shapes;
using QuickReader;

namespace Preview
{
    /// <summary>
    /// Interaction logic for Statistics.xaml
    /// </summary>
    public partial class Statistics : Window
    {
        DyKnowReader dr;
        public Statistics(DyKnowReader dr)
        {
            InitializeComponent();
            this.dr = dr;

            textBoxMeanLengthOfData.Text = dr.MeanStrokeDistance.ToString();
            textBoxMeanNumberOfStrokes.Text = dr.MeanStrokes.ToString();
            textBoxNumberOfPanels.Text = dr.NumOfPages().ToString();
            textBoxStandardDeviationOfDataLength.Text = dr.StdDevStrokeDistance.ToString();
            textBoxStandardDeviationOfStrokes.Text = dr.StdDevStrokes.ToString();
        }

    }
}
