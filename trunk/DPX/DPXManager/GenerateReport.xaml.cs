// <copyright file="GenerateReport.xaml.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>Tab that displays the interface for generating a report.</summary>
namespace DPXManager
{
    using System;
    using System.Collections.Generic;
    using System.IO;
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
    using DPXDatabase;

    /// <summary>
    /// Tab that displays the interface for generating a report.
    /// </summary>
    public partial class GenerateReport : Page
    {
        /// <summary>
        /// Creates an instance of the singleton controller.
        /// </summary>
        private Controller c = Controller.Instance();

        /// <summary>
        /// Initializes a new instance of the GenerateReport class.
        /// </summary>
        public GenerateReport()
        {
            InitializeComponent();
            this.c.SetListBoxReportDates(listBoxDates);
        }

        /// <summary>
        /// Gather the information needed to generate the report, create the report and then display it.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ButtonGenerateReport_Click(object sender, RoutedEventArgs e)
        {
            if (this.c.IsDatabaseOpen())
            {
                // Erase everything that is currently in the rich text box
                this.richTextBoxReport.Document.Blocks.Clear();

                // Generate the report and fill it in the rich text box
                List<Classdate> selectedDates = new List<Classdate>();
                for (int i = 0; i < listBoxDates.SelectedItems.Count; i++)
                {
                    selectedDates.Add(listBoxDates.SelectedItems[i] as Classdate);
                }

                Paragraph para = new Paragraph();
                para.Inlines.Add(new Run(this.c.DB.GenerateReport(selectedDates)));
                richTextBoxReport.Document.Blocks.Add(para);
            }
            else
            {
                MessageBox.Show("Database is not open.", "Error");
            }
        }

        /// <summary>
        /// Buttons the save report.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonSaveReport(object sender, RoutedEventArgs e)
        {
            try
            {
                // Configure save file dialog box
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.DefaultExt = ".txt"; // Default file extension
                dlg.Filter = "Text documents (*.txt)|*.txt"; // Filter files by extension

                // Show open file dialog box
                Nullable<bool> result = dlg.ShowDialog();

                // Process open file dialog box results
                if (result == true)
                {
                    // Save the document
                    TextRange range;
                    FileStream fileStream;
                    range = new TextRange(this.richTextBoxReport.Document.ContentStart, this.richTextBoxReport.Document.ContentEnd);
                    fileStream = new FileStream(dlg.FileName, FileMode.Create);
                    range.Save(fileStream, DataFormats.Text);
                    fileStream.Close();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
