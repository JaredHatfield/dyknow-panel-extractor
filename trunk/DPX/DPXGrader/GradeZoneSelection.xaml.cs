// <copyright file="GradeZoneSelection.xaml.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The interface for selecting the region of the panel that will contain the grade.</summary>
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
    using System.Windows.Shapes;
    using DPXReader.DyKnow;

    /// <summary>
    /// Interaction logic for GradeZoneSelection.xaml
    /// </summary>
    public partial class GradeZoneSelection : Window
    {
        /// <summary>
        /// The DyKnow reader that contains a file.
        /// </summary>
        private DyKnow dr;

        /// <summary>
        /// The current page.
        /// </summary>
        private int currentPage;

        /// <summary>
        /// The size of the grade box.
        /// </summary>
        private int boxSize;

        /// <summary>
        /// The location of the grade box.
        /// </summary>
        private BoxLocation boxLocation;

        /// <summary>
        /// Initializes a new instance of the <see cref="GradeZoneSelection"/> class.
        /// </summary>
        /// <param name="dr">The DyKnowReader.</param>
        public GradeZoneSelection(DyKnow dr)
        {
            InitializeComponent();
            this.boxSize = 50;
            this.boxLocation = BoxLocation.TopLeft;
            this.dr = dr;
            this.currentPage = 0;
            if (this.dr != null && this.dr.DATA.Count > 0)
            {
                this.dr.Render(this.Inky, 0);
                this.LabelPanelNumber.Content = (this.currentPage + 1) + " of " + dr.DATA.Count;
            }
        }

        /// <summary>
        /// The possible locations of the grade box
        /// </summary>
        private enum BoxLocation
        {
            /// <summary>
            /// Location represented by the top left corner.
            /// </summary>
            TopLeft,

            /// <summary>
            /// Location represented by the top right corner.
            /// </summary>
            TopRight,

            /// <summary>
            /// Location represented by the bottom left corner.
            /// </summary>
            BottomLeft,

            /// <summary>
            /// Location represented by the bottom right corner.
            /// </summary>
            BottomRight
        }

        /// <summary>
        /// Processes a change in the location of the box.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void RadioLocationChanged(object sender, RoutedEventArgs e)
        {
            if (this.RadioTopLeft.IsChecked.Value)
            {
                this.boxLocation = BoxLocation.TopLeft;
            }
            else if (this.RadioTopRight.IsChecked.Value)
            {
                this.boxLocation = BoxLocation.TopRight;
            }
            else if (this.RadioBottomLeft.IsChecked.Value)
            {
                this.boxLocation = BoxLocation.BottomLeft;
            }
            else if (this.RadioBottomRight.IsChecked.Value)
            {
                this.boxLocation = BoxLocation.BottomRight;
            }

            // Redraw the rectangle based on the new settings
            this.RedrawRectangle();
        }

        /// <summary>
        /// Sliders the value changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedPropertyChangedEventArgs&lt;System.Double&gt;"/> instance containing the event data.</param>
        private void SliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.boxSize = (int)this.BoxSizeSlider.Value;

            // Redraw the rectangle based on the new settings
            this.RedrawRectangle();
        }

        /// <summary>
        /// Redraws the rectangle on the canvas based on the new location and size.
        /// </summary>
        private void RedrawRectangle()
        {
            // Set the size of the box
            this.Boxy.Width = this.boxSize;
            this.Boxy.Height = this.boxSize;

            // Set the relative position of the box
            if (this.boxLocation.Equals(BoxLocation.TopLeft))
            {
                Canvas.SetLeft(this.Boxy, 0);
                Canvas.SetTop(this.Boxy, 0);
            }
            else if (this.boxLocation.Equals(BoxLocation.TopRight))
            {
                Canvas.SetLeft(this.Boxy, this.Inky.Width - this.boxSize);
                Canvas.SetTop(this.Boxy, 0);
            }
            else if (this.boxLocation.Equals(BoxLocation.BottomLeft))
            {
                Canvas.SetLeft(this.Boxy, 0);
                Canvas.SetTop(this.Boxy, this.Inky.Height - this.boxSize);
            }
            else if (this.boxLocation.Equals(BoxLocation.BottomRight))
            {
                Canvas.SetLeft(this.Boxy, this.Inky.Width - this.boxSize);
                Canvas.SetTop(this.Boxy, this.Inky.Height - this.boxSize);
            }
        }

        /// <summary>
        /// Gets the selected area.
        /// </summary>
        /// <returns>The rectangle that represents the area.</returns>
        private Rect GetSelectedArea()
        {
            if (this.boxLocation.Equals(BoxLocation.TopLeft))
            {
                return new Rect(0, 0, this.boxSize, this.boxSize);
            }
            else if (this.boxLocation.Equals(BoxLocation.TopRight))
            {
                return new Rect(this.Inky.Width - this.boxSize, 0, this.boxSize, this.boxSize);
            }
            else if (this.boxLocation.Equals(BoxLocation.BottomLeft))
            {
                return new Rect(0, this.Inky.Height - this.boxSize, this.boxSize, this.boxSize);
            }
            else if (this.boxLocation.Equals(BoxLocation.BottomRight))
            {
                return new Rect(this.Inky.Width - this.boxSize, this.Inky.Height - this.boxSize, this.boxSize, this.boxSize);
            }

            throw new Exception("Rectangle could not be generated");
        }

        /// <summary>
        /// Handles the Click event of the ButtonAnalyze control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonAnalyze_Click(object sender, RoutedEventArgs e)
        {
            StrokeCollection strokes = this.Inky.Strokes.Clone();
            strokes.Clip(this.GetSelectedArea());
            InkAnalyzer theInkAnalyzer = new InkAnalyzer();
            theInkAnalyzer.AddStrokes(strokes);
            AnalysisStatus status = theInkAnalyzer.Analyze();

            if (status.Successful)
            {
                MessageBox.Show(theInkAnalyzer.GetRecognizedString(), "Converted Text Preview");
            }
        }

        /// <summary>
        /// Handles the Click event of the ButtonPrevious control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (this.dr != null && this.currentPage > 0)
            {
                this.Inky.Strokes.Clear();
                this.Inky.Children.Clear();
                this.dr.Render(this.Inky, --this.currentPage);
                this.LabelPanelNumber.Content = (this.currentPage + 1) + " of " + this.dr.DATA.Count;
            }
        }

        /// <summary>
        /// Handles the Click event of the ButtonNext control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            if (this.dr != null && this.currentPage + 1 < this.dr.DATA.Count)
            {
                this.Inky.Strokes.Clear();
                this.Inky.Children.Clear();
                this.dr.Render(this.Inky, ++this.currentPage);
                this.LabelPanelNumber.Content = (this.currentPage + 1) + " of " + this.dr.DATA.Count;
            }
        }

        /// <summary>
        /// Handles the Click event of the ProcessFile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ProcessFile_Click(object sender, RoutedEventArgs e)
        {
            this.TextBoxResults.Text = string.Empty;
            Rect r = this.GetSelectedArea();
            List<double> numbers = new List<double>();

            if (this.dr != null)
            {
                for (int i = 0; i < this.dr.DATA.Count; i++)
                {
                    // Get all of the ink that we are concerned about
                    InkCanvas ic = new InkCanvas();
                    ic.Width = this.Inky.Width;
                    ic.Height = this.Inky.Height;
                    this.dr.Render(ic, i);
                    ic.Strokes.Clip(r);

                    // Perform the analysis
                    if (ic.Strokes.Count > 0)
                    {
                        InkAnalyzer theInkAnalyzer = new InkAnalyzer();
                        theInkAnalyzer.AddStrokes(ic.Strokes);
                        AnalysisStatus status = theInkAnalyzer.Analyze();
                        if (status.Successful)
                        {
                            this.TextBoxResults.Text += theInkAnalyzer.GetRecognizedString() + "\n";

                            if (this.CheckBoxAlternate.IsChecked.Value)
                            {
                                AnalysisAlternateCollection a = theInkAnalyzer.GetAlternates();
                                for (int j = 0; j < a.Count; j++)
                                {
                                    this.TextBoxResults.Text += (j + 1) + ") " + a[j].RecognizedString + "\n";
                                }

                                this.TextBoxResults.Text += "----------";
                            }

                            try
                            {
                                double score = double.Parse(theInkAnalyzer.GetRecognizedString());
                                numbers.Add(score);
                            }
                            catch
                            {
                            }
                        }
                    }
                }

                this.TextBoxResults.Text += "Total: " + numbers.Sum();
            }
        }
    }
}
