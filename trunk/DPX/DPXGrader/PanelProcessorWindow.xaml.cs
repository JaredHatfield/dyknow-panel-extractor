// <copyright file="PanelProcessorWindow.xaml.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The interface for processing panels and producing output.</summary>
namespace DPXGrader
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
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
    /// Interaction logic for PanelProcessorWindow.xaml
    /// </summary>
    public partial class PanelProcessorWindow : Window
    {
        /// <summary>
        /// The dyknow file that is read in.
        /// </summary>
        private DyKnow dyknow;

        /// <summary>
        /// The collection of results.
        /// </summary>
        private Collection<object[]> results;

        /// <summary>
        /// The size of the grade box.
        /// </summary>
        private int boxSize;

        /// <summary>
        /// The location of the grade box.
        /// </summary>
        private BoxLocation boxLocation;

        /// <summary>
        /// The selected thumnail object.
        /// Used to remove the border when a new object is selected.
        /// </summary>
        private Border selectedThumbnail;
        
        /// <summary>
        /// The panel number that is selected.
        /// </summary>
        private int selectedPanelId;

        /// <summary>
        /// Initializes a new instance of the <see cref="PanelProcessorWindow"/> class.
        /// </summary>
        public PanelProcessorWindow()
        {
            InitializeComponent();
            this.boxSize = 50;
            this.boxLocation = BoxLocation.TopLeft;
            this.selectedThumbnail = new Border();
            this.selectedPanelId = -1;
            this.results = new Collection<object[]>();
            this.DisableStepTwo();
            this.DisableStepThree();
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
        /// Disables the part of the interface related to step two.
        /// </summary>
        private void DisableStepTwo()
        {
            this.TextBoxPreviewOutput.IsEnabled = false;
            this.TextBoxStudentName.IsEnabled = false;
            this.TextBoxUserName.IsEnabled = false;
            this.ButtonPreview.IsEnabled = false;
            this.ButtonProcess.IsEnabled = false;
        }

        /// <summary>
        /// Enables the part of the interface related to step two.
        /// </summary>
        private void EnableStepTwo()
        {
            this.TextBoxPreviewOutput.IsEnabled = true;
            this.TextBoxStudentName.IsEnabled = true;
            this.TextBoxUserName.IsEnabled = true;
            this.ButtonPreview.IsEnabled = true;
            this.ButtonProcess.IsEnabled = true;
        }

        /// <summary>
        /// Disables the part of the interface related to step three.
        /// </summary>
        private void DisableStepThree()
        {
            this.ButtonSave.IsEnabled = false;
        }

        /// <summary>
        /// Enables the part of the interface related to step three.
        /// </summary>
        private void EnableStepThree()
        {
            this.ButtonSave.IsEnabled = true;
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
        private Rect GetRectangleArea()
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
        /// Event that is fired when a panel in the scroll view is selected.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PanelSelected(object sender, EventArgs e)
        {
            this.selectedThumbnail.BorderBrush = Brushes.Black;
            Border b = sender as Border;
            this.selectedThumbnail = b;
            b.BorderBrush = Brushes.Gold;
            this.DisplayPanel((int)b.Tag);
        }

        /// <summary>
        /// Displays a panel.
        /// </summary>
        /// <param name="n">The panel to display.</param>
        private void DisplayPanel(int n)
        {
            if (this.dyknow != null && n >= 0 && n < this.dyknow.DATA.Count)
            {
                this.selectedPanelId = n;
                this.dyknow.Render(this.Inky, n);
                string oner = (this.dyknow.DATA[n] as DPXReader.DyKnow.Page).ONER;
                string onern = (this.dyknow.DATA[n] as DPXReader.DyKnow.Page).ONERN;
                if (oner != null)
                {
                    this.TextBoxStudentName.Text = onern;
                }

                if (onern != null)
                {
                    this.TextBoxUserName.Text = oner;
                }
            }
        }

        /// <summary>
        /// Loads the dy know file.
        /// </summary>
        /// <param name="file">The file to load.</param>
        private void LoadDyKnowFile(string file)
        {
            // Reset the GUI
            this.EnableStepTwo();
            this.DisableStepThree();
            this.GridResults.Children.Clear();
            this.Inky.Children.Clear();
            this.Inky.Strokes.Clear();
            this.PanelScrollView.Children.Clear();
            this.TextBoxPreviewOutput.Text = string.Empty;
            this.TextBoxStudentName.Text = string.Empty;
            this.TextBoxUserName.Text = string.Empty;
            this.TextBoxFileName.Text = file;
            this.selectedPanelId = -1;

            this.dyknow = DyKnow.DeserializeFromFile(file);
            if (this.dyknow.DATA.Count > 0)
            {
                this.DisplayPanel(0);
                this.selectedPanelId = 0;
            }

            for (int i = 0; i < this.dyknow.DATA.Count; i++)
            {
                InkCanvas ink = new InkCanvas();
                ink.Width = this.Inky.ActualWidth;
                ink.Height = this.Inky.ActualHeight;
                this.dyknow.Render(ink, i);
                RenderTargetBitmap rtb = new RenderTargetBitmap(Convert.ToInt32(ink.Width), Convert.ToInt32(ink.Height), 96d, 96d, PixelFormats.Default);
                rtb.Render(ink);
                TransformedBitmap tb = new TransformedBitmap(rtb, new ScaleTransform(.4, .4));
                Image myImage = new Image();
                myImage.Source = tb;
                Border b = new Border();
                b.Child = myImage;

                if (i == 0)
                {
                    b.BorderBrush = Brushes.Gold;
                    this.selectedThumbnail = b;
                }
                else
                {
                    b.BorderBrush = Brushes.Black;
                }

                b.BorderThickness = new Thickness(1);
                b.Margin = new Thickness(5);
                b.MouseDown += new MouseButtonEventHandler(this.PanelSelected);
                b.Tag = i;

                this.PanelScrollView.Children.Add(b);
            }
        }

        // Methods that are tied directly to the GUI

        /// <summary>
        /// Handles the Checked event of the RadioButtonPosition control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void RadioButtonPosition_Checked(object sender, RoutedEventArgs e)
        {
            if (this.RadioButtonTopLeft.IsChecked.Value)
            {
                this.boxLocation = BoxLocation.TopLeft;
            }
            else if (this.RadioButtonTopRight.IsChecked.Value)
            {
                this.boxLocation = BoxLocation.TopRight;
            }
            else if (this.RadioButtonBottomLeft.IsChecked.Value)
            {
                this.boxLocation = BoxLocation.BottomLeft;
            }
            else if (this.RadioButtonBottomRight.IsChecked.Value)
            {
                this.boxLocation = BoxLocation.BottomRight;
            }

            // Redraw the rectangle based on the new settings
            this.RedrawRectangle();
        }

        /// <summary>
        /// Handles the ValueChanged event of the SliderSize control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedPropertyChangedEventArgs&lt;System.Double&gt;"/> instance containing the event data.</param>
        private void SliderSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.boxSize = (int)this.SliderSize.Value;

            // Redraw the rectangle based on the new settings
            this.RedrawRectangle();
        }

        /// <summary>
        /// Handles the Click event of the ButtonOpen control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonOpen_Click(object sender, RoutedEventArgs e)
        {
            // Let the user choose which file to open
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "DyKnow files (*.dyz)|*.dyz";
            if (openFileDialog.ShowDialog() == true)
            {
                // Open the DyKnow file
                this.LoadDyKnowFile(openFileDialog.FileName);
            }
        }

        /// <summary>
        /// Handles the Click event of the ButtonPreview control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonPreview_Click(object sender, RoutedEventArgs e)
        {
            StrokeCollection strokes = this.Inky.Strokes.Clone();
            strokes.Clip(this.GetRectangleArea());
            if (strokes.Count > 0)
            {
                InkAnalyzer theInkAnalyzer = new InkAnalyzer();
                theInkAnalyzer.AddStrokes(strokes);
                AnalysisStatus status = theInkAnalyzer.Analyze();

                if (status.Successful)
                {
                    this.TextBoxPreviewOutput.Text = theInkAnalyzer.GetRecognizedString();
                }
                else
                {
                    this.TextBoxPreviewOutput.Text = string.Empty;
                }
            }
            else
            {
                this.TextBoxPreviewOutput.Text = string.Empty;
            }
        }

        /// <summary>
        /// Handles the Click event of the ButtonProcess control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonProcess_Click(object sender, RoutedEventArgs e)
        {
            this.results.Clear();
            this.GridResults.Children.Clear();

            for (int i = 0; i < this.dyknow.DATA.Count; i++)
            {
                DPXReader.DyKnow.Page page = this.dyknow.DATA[i] as DPXReader.DyKnow.Page;
                InkCanvas ink = new InkCanvas();
                ink.Width = Inky.Width;
                ink.Height = Inky.Height;
                this.dyknow.Render(ink, i);
                ink.Strokes.Clip(this.GetRectangleArea());
                string val = string.Empty;
                double valDigit = 0;
                if (ink.Strokes.Count > 0)
                {
                    InkAnalyzer theInkAnalyzer = new InkAnalyzer();
                    theInkAnalyzer.AddStrokes(ink.Strokes);
                    AnalysisStatus status = theInkAnalyzer.Analyze();

                    if (status.Successful)
                    {
                        val = theInkAnalyzer.GetRecognizedString();
                        try
                        {
                            valDigit = double.Parse(val);
                        }
                        catch
                        {
                        }
                    }
                }

                RowDefinition rd = new RowDefinition();
                rd.Height = GridLength.Auto;
                this.GridResults.RowDefinitions.Add(rd);

                // Add the panel number
                Label num = new Label();
                num.Content = i.ToString();
                num.BorderBrush = Brushes.DarkGray;
                num.BorderThickness = new Thickness(1);
                Grid.SetRow(num, i);
                Grid.SetColumn(num, 0);
                this.GridResults.Children.Add(num);

                // Add the name
                Label name = new Label();
                name.Content = page.ONERN;
                name.BorderBrush = Brushes.DarkGray;
                name.BorderThickness = new Thickness(1);
                Grid.SetRow(name, i);
                Grid.SetColumn(name, 1);
                this.GridResults.Children.Add(name);

                // Add the username
                Label user = new Label();
                user.Content = page.ONER;
                user.BorderBrush = Brushes.DarkGray;
                user.BorderThickness = new Thickness(1);
                Grid.SetRow(user, i);
                Grid.SetColumn(user, 2);
                this.GridResults.Children.Add(user);

                // Add the recognized text
                Label cont = new Label();
                cont.Content = val;
                cont.BorderBrush = Brushes.DarkGray;
                cont.BorderThickness = new Thickness(1);
                Grid.SetRow(cont, i);
                Grid.SetColumn(cont, 3);
                this.GridResults.Children.Add(cont);

                // Add the recognized text converted to a number
                Label digit = new Label();
                digit.Content = valDigit;
                digit.BorderBrush = Brushes.DarkGray;
                digit.BorderThickness = new Thickness(1);
                Grid.SetRow(digit, i);
                Grid.SetColumn(digit, 4);
                this.GridResults.Children.Add(digit);

                // Add the values to the results collection
                object[] record = new object[5];
                record[0] = i;
                record[1] = page.ONERN;
                record[2] = page.ONER;
                record[3] = val;
                record[4] = valDigit;
                this.results.Add(record);
            }

            this.EnableStepThree();
        }

        /// <summary>
        /// Handles the Click event of the ButtonSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Let the user choose which file to open
                Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
                saveFileDialog.Filter = "CSV (*.csv)|*.csv";
                if (saveFileDialog.ShowDialog() == true)
                {
                    // Save the CSV file
                    string file = saveFileDialog.FileName;
                    StreamWriter sr = new StreamWriter(file);
                    for (int i = 0; i < this.results.Count; i++)
                    {
                        for (int j = 0; j < this.results[i].Length; j++)
                        {
                            // TODO: This code is really sloppy, lots of error checking and escaping action needs to happen here!
                            string val = this.results[i][j].ToString();
                            val = val.Replace("\n", " ");
                            val = val.Replace(',', ' ');
                            sr.Write(val);

                            if (j + i < this.results[i].Length)
                            {
                                sr.Write(',');
                            }
                        }

                        sr.WriteLine();
                    }

                    sr.Close();
                }
                else
                {
                    MessageBox.Show("Error: The file could not be saved.");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("The file was not saved successfully.");
            }
        }
    }
}
