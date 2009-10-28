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
using DPXDatabase;

namespace DPX
{
    /// <summary>
    /// Interaction logic for GenerateReport.xaml
    /// </summary>
    public partial class GenerateReport : Page
    {
        Controller c = Controller.Instance();

        public GenerateReport()
        {
            InitializeComponent();
            c.setListBoxReportDates(listBoxDates);
        }

        private void buttonGenerateReport_Click(object sender, RoutedEventArgs e)
        {
            if (c.isDatabaseOpen())
            {
                List<Classdate> selectedDates = new List<Classdate>();
                for (int i = 0; i < listBoxDates.SelectedItems.Count; i++)
                {
                    selectedDates.Add(listBoxDates.SelectedItems[i] as Classdate);
                }

                Paragraph para = new Paragraph();
                para.Inlines.Add(new Run(c.DB.GenerateReport(selectedDates)));
                richTextBoxReport.Document.Blocks.Add(para);
            }
            else
            {
                MessageBox.Show("Database is not open.", "Error");
            }
        }
    }
}
