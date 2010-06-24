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
    using System.Windows.Threading;
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
        private Collection<string[]> results;

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
        /// The name of the file that is being opened.
        /// </summary>
        private string fileName;

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
            this.results = new Collection<string[]>();
            this.DisableStepTwo();
            this.DisableStepThree();
        }

        /// <summary>
        /// The delegate for call with no arguments.
        /// </summary>
        private delegate void NoArgsDelegate();

        /// <summary>
        /// The delegate for updating the progress bar.
        /// </summary>
        /// <param name="progress">The current progress for the bar.</param>
        /// <param name="goal">The goal for the progress bar. </param>
        private delegate void UpdateProgressBarDelegate(int progress, int goal);

        /// <summary>
        /// The delegate for displaying a panel.
        /// </summary>
        /// <param name="number">The panel number.</param>
        private delegate void DisplayPanelDelegate(int number);

        /// <summary>
        /// The delegate for creating and adding a thumbnail to the timeline.
        /// </summary>
        /// <param name="number">The panel number.</param>
        private delegate void AddThumnailDelegate(int number);

        /// <summary>
        /// The delegate for adding a row to the results table.
        /// </summary>
        /// <param name="number">The panel number.</param>
        /// <param name="onern">The name on the panel.</param>
        /// <param name="oner">The username on the panel.</param>
        /// <param name="text">The recognized text.</param>
        /// <param name="value">The numeric version of the recognized text.</param>
        private delegate void AddRowToResultsDelegate(int number, string onern, string oner, string text, double value);

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
                return new Rect(400 - this.boxSize, 0, this.boxSize, this.boxSize);
            }
            else if (this.boxLocation.Equals(BoxLocation.BottomLeft))
            {
                return new Rect(0, 300 - this.boxSize, this.boxSize, this.boxSize);
            }
            else if (this.boxLocation.Equals(BoxLocation.BottomRight))
            {
                return new Rect(400 - this.boxSize, 300 - this.boxSize, this.boxSize, this.boxSize);
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
        /// Loads the dy know file.
        /// </summary>
        private void LoadDyKnowFile()
        {
            string file = this.fileName;
            Dispatcher.Invoke(new UpdateProgressBarDelegate(this.UpdateOpenProgressBar), DispatcherPriority.Normal, 0, 1);
            Dispatcher.Invoke(new UpdateProgressBarDelegate(this.UpdateProcessProgressBar), DispatcherPriority.Input, 0, 1);
            
            // Reset the GUI
            Dispatcher.Invoke(new NoArgsDelegate(this.ClearInterface), DispatcherPriority.Normal);
            this.selectedPanelId = -1;

            // Read in the file
            this.dyknow = DyKnow.DeserializeFromFile(file);
            int goal = this.dyknow.DATA.Count + 1;

            // Set up the progress bar.
            Dispatcher.Invoke(new UpdateProgressBarDelegate(this.UpdateOpenProgressBar), DispatcherPriority.Normal, 1, goal);

            // Display the first panel
            if (this.dyknow.DATA.Count > 0)
            {
                Dispatcher.Invoke(new DisplayPanelDelegate(this.DisplayPanel), DispatcherPriority.Input, 0);
                this.selectedPanelId = 0;
            }

            // Render and add all of the thumbnails
            for (int i = 0; i < this.dyknow.DATA.Count; i++)
            {
                Dispatcher.Invoke(new AddThumnailDelegate(this.AddThumbnail), DispatcherPriority.Input, i);
                Dispatcher.BeginInvoke(new UpdateProgressBarDelegate(this.UpdateOpenProgressBar), DispatcherPriority.Input, i, goal);
            }

            Dispatcher.BeginInvoke(new NoArgsDelegate(this.EnableStepTwo), DispatcherPriority.Input);
            Dispatcher.Invoke(new UpdateProgressBarDelegate(this.UpdateOpenProgressBar), DispatcherPriority.Input, goal, goal);
            Dispatcher.BeginInvoke(new NoArgsDelegate(this.ReEnableOpen), DispatcherPriority.Input);
        }

        // Methods that are dispatched to update the GUI
        
        /// <summary>
        /// Clears the interface.
        /// </summary>
        private void ClearInterface()
        {
            this.DisableStepTwo();
            this.DisableStepThree();
            this.GridResults.Children.Clear();
            this.Inky.Children.Clear();
            this.Inky.Strokes.Clear();
            this.PanelScrollView.Children.Clear();
            this.TextBoxPreviewOutput.Text = string.Empty;
            this.TextBoxStudentName.Text = string.Empty;
            this.TextBoxUserName.Text = string.Empty;
            this.TextBoxFileName.Text = this.fileName;
        }

        /// <summary>
        /// Updates the open progress bar.
        /// </summary>
        /// <param name="progress">The current progress.</param>
        /// <param name="goal">The goal for the bar.</param>
        private void UpdateOpenProgressBar(int progress, int goal)
        {
            this.ProgressBarOpen.Maximum = goal;
            this.ProgressBarOpen.Value = progress;
        }

        /// <summary>
        /// Updates the process progress bar.
        /// </summary>
        /// <param name="progress">The current progress.</param>
        /// <param name="goal">The goal for the bar.</param>
        private void UpdateProcessProgressBar(int progress, int goal)
        {
            this.ProgressBarProcess.Maximum = goal;
            this.ProgressBarProcess.Value = progress;
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
        /// Adds the thumbnail.
        /// </summary>
        /// <param name="number">The panel number.</param>
        private void AddThumbnail(int number)
        {
            InkCanvas ink = new InkCanvas();
            ink.Width = this.Inky.ActualWidth;
            ink.Height = this.Inky.ActualHeight;
            this.dyknow.Render(ink, number);
            RenderTargetBitmap rtb = new RenderTargetBitmap(Convert.ToInt32(ink.Width), Convert.ToInt32(ink.Height), 96d, 96d, PixelFormats.Default);
            rtb.Render(ink);
            TransformedBitmap tb = new TransformedBitmap(rtb, new ScaleTransform(.4, .4));
            Image myImage = new Image();
            myImage.Source = tb;
            Border b = new Border();
            b.Child = myImage;

            if (number == 0)
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
            b.Tag = number;

            this.PanelScrollView.Children.Add(b);
        }

        /// <summary>
        /// Re-Enable the open button.
        /// </summary>
        private void ReEnableOpen()
        {
            this.ButtonOpen.IsEnabled = true;
        }

        /// <summary>
        /// Re-Enable the process button.
        /// </summary>
        private void ReEnableProcess()
        {
            this.ButtonOpen.IsEnabled = true;
            this.ButtonPreview.IsEnabled = true;
            this.ButtonProcess.IsEnabled = true;
        }

        /// <summary>
        /// Clears the results.
        /// </summary>
        private void ClearResults()
        {
            this.results.Clear();
            this.GridResults.Children.Clear();
            this.DisableStepThree();
        }

        /// <summary>
        /// Disables the box controls for the slider and position.
        /// </summary>
        private void DisableBoxControls()
        {
            this.SliderSize.IsEnabled = false;
            this.RadioButtonBottomLeft.IsEnabled = false;
            this.RadioButtonBottomRight.IsEnabled = false;
            this.RadioButtonTopLeft.IsEnabled = false;
            this.RadioButtonTopRight.IsEnabled = false;
        }

        /// <summary>
        /// Enables the box controls for the slider and position.
        /// </summary>
        private void EnableBoxControls()
        {
            this.SliderSize.IsEnabled = true;
            this.RadioButtonBottomLeft.IsEnabled = true;
            this.RadioButtonBottomRight.IsEnabled = true;
            this.RadioButtonTopLeft.IsEnabled = true;
            this.RadioButtonTopRight.IsEnabled = true;
        }

        /// <summary>
        /// Adds the row to results.
        /// </summary>
        /// <param name="i">The panel number.</param>
        /// <param name="onern">The onern value.</param>
        /// <param name="oner">The oner value.</param>
        /// <param name="text">The recognized text.</param>
        /// <param name="value">The numeric value.</param>
        private void AddRowToResults(int i, string onern, string oner, string text, double value)
        {
            RowDefinition rd = new RowDefinition();
            rd.Height = GridLength.Auto;
            this.GridResults.RowDefinitions.Add(rd);

            // Add the panel number
            Label num = new Label();
            num.Content = (i + 1).ToString();
            num.BorderBrush = Brushes.DarkGray;
            num.BorderThickness = new Thickness(1);
            Grid.SetRow(num, i);
            Grid.SetColumn(num, 0);
            this.GridResults.Children.Add(num);

            // Add the name
            Label name = new Label();
            name.Content = onern;
            name.BorderBrush = Brushes.DarkGray;
            name.BorderThickness = new Thickness(1);
            Grid.SetRow(name, i);
            Grid.SetColumn(name, 1);
            this.GridResults.Children.Add(name);

            // Add the username
            Label user = new Label();
            user.Content = oner;
            user.BorderBrush = Brushes.DarkGray;
            user.BorderThickness = new Thickness(1);
            Grid.SetRow(user, i);
            Grid.SetColumn(user, 2);
            this.GridResults.Children.Add(user);

            // Add the recognized text
            Label cont = new Label();
            cont.Content = text;
            cont.BorderBrush = Brushes.DarkGray;
            cont.BorderThickness = new Thickness(1);
            Grid.SetRow(cont, i);
            Grid.SetColumn(cont, 3);
            this.GridResults.Children.Add(cont);

            // Add the recognized text converted to a number
            Label digit = new Label();
            digit.Content = value;
            digit.BorderBrush = Brushes.DarkGray;
            digit.BorderThickness = new Thickness(1);
            Grid.SetRow(digit, i);
            Grid.SetColumn(digit, 4);
            this.GridResults.Children.Add(digit);
        }

        /// <summary>
        /// Do all of the crunching for the processes step.
        /// </summary>
        private void ProcessStep()
        {
            Dispatcher.Invoke(new NoArgsDelegate(this.DisableBoxControls), DispatcherPriority.Normal);
            Dispatcher.Invoke(new NoArgsDelegate(this.ClearResults), DispatcherPriority.Normal);
            int goal = this.dyknow.DATA.Count;
            Dispatcher.Invoke(new UpdateProgressBarDelegate(this.UpdateProcessProgressBar), DispatcherPriority.Input, 0, goal);

            for (int i = 0; i < this.dyknow.DATA.Count; i++)
            {
                DPXReader.DyKnow.Page page = this.dyknow.DATA[i] as DPXReader.DyKnow.Page;
                InkCanvas ink = new InkCanvas();
                ink.Width = 400;
                ink.Height = 300;
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
                
                // Add the new record to the results table 
                Dispatcher.Invoke(new AddRowToResultsDelegate(this.AddRowToResults), DispatcherPriority.Input, i, page.ONERN, page.ONER, val, valDigit);

                // Add the values to the results collection
                string[] record = new string[5];
                record[0] = (i + 1).ToString();
                record[1] = page.ONERN;
                record[2] = page.ONER;
                record[3] = val;
                record[4] = string.Empty + valDigit;
                this.results.Add(record);

                // Update the progress bar
                Dispatcher.Invoke(new UpdateProgressBarDelegate(this.UpdateProcessProgressBar), DispatcherPriority.Input, i, goal);
            }

            Dispatcher.BeginInvoke(new NoArgsDelegate(this.EnableBoxControls), DispatcherPriority.Input);
            Dispatcher.BeginInvoke(new NoArgsDelegate(this.ReEnableProcess), DispatcherPriority.Input);
            Dispatcher.BeginInvoke(new NoArgsDelegate(this.EnableStepThree), DispatcherPriority.Input);
            Dispatcher.BeginInvoke(new UpdateProgressBarDelegate(this.UpdateProcessProgressBar), DispatcherPriority.Input, goal, goal);
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
                this.ButtonOpen.IsEnabled = false;

                // Open the DyKnow file
                this.fileName = openFileDialog.FileName;
                Thread t = new Thread(new ThreadStart(this.LoadDyKnowFile));
                t.Name = "OpenFileThread";
                t.Start();
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
            this.ButtonOpen.IsEnabled = false;
            this.ButtonPreview.IsEnabled = false;
            this.ButtonProcess.IsEnabled = false;

            Thread t = new Thread(new ThreadStart(this.ProcessStep));
            t.Name = "ProcessThread";
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
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
                        string[] val = this.results[i];
                        for (int j = 0; j < val.Length; j++)
                        {
                            // Remove any line breaks that may be part of the elements and replace them with spaces
                            if (val[j].Contains(Environment.NewLine))
                            {
                                val[j] = val[j].Replace(Environment.NewLine, " ");
                            }

                            // Any of the strings that contain a comma need to be surrounded with double quotes
                            if (val[j].Contains(','))
                            {
                                // TODO: What if there are double quotes contained within the string, these should be escaped.
                                val[j] = "\"" + val[j] + "\"";
                            }
                        }

                        string output = string.Join(",", val);
                        sr.WriteLine(output);
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
