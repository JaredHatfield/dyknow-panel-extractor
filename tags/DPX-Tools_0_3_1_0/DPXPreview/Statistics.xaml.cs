// <copyright file="Statistics.xaml.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The statistics information window.</summary>
namespace DPXPreview
{
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
    using DPXReader;

    /// <summary>
    /// The statistics information window.
    /// </summary>
    public partial class Statistics : Window
    {
        /// <summary>
        /// Instance of DyKnowReader.
        /// </summary>
        private DyKnowReader dr;

        /// <summary>
        /// Initializes a new instance of the Statistics class.
        /// </summary>
        /// <param name="dr">Instance of DyKnowReader.</param>
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
