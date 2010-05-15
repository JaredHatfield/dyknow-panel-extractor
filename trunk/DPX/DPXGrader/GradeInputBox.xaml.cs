// <copyright file="GradeInputBox.xaml.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The user control that allows for ink input of a grade.</summary>
namespace DPXGrader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Ink;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;

    /// <summary>
    /// Interaction logic for GradeInputBox.xaml
    /// </summary>
    public partial class GradeInputBox : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GradeInputBox"/> class.
        /// </summary>
        public GradeInputBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets the recognized value.
        /// </summary>
        /// <value>The recognized value.</value>
        internal string Value
        {
            get { return this.textBox.Text; }
        }

        /// <summary>
        /// Buttons the click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            InkAnalyzer theInkAnalyzer = new InkAnalyzer();
            theInkAnalyzer.AddStrokes(theInkCanvas.Strokes);
            AnalysisStatus status = theInkAnalyzer.Analyze();

            if (status.Successful)
            {
                textBox.Text = theInkAnalyzer.GetRecognizedString();
            }
            else
            {
                textBox.Text = string.Empty;
            }
        }
    }
}
