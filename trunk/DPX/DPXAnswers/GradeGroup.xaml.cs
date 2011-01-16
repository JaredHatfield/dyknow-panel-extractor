// <copyright file="GradeGroup.xaml.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The visual element representing a grade.</summary>
namespace DPXAnswers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
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
    using ClusterLibraryCore;
    using GradeLibrary;

    /// <summary>
    /// Interaction logic for GradeGroup.xaml
    /// </summary>
    public partial class GradeGroup : UserControl
    {
        /// <summary>
        /// The group that is being displayed.
        /// </summary>
        private IClusterGroup<IAnswer, GroupData> group;

        /// <summary>
        /// The index of the group.
        /// </summary>
        private int index;

        /// <summary>
        /// Initializes a new instance of the <see cref="GradeGroup"/> class.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <param name="index">The index.</param>
        public GradeGroup(IClusterGroup<IAnswer, GroupData> group, int index)
        {
            InitializeComponent();
            this.group = group;
            group.PropertyChanged += this.GroupPropertyChanged;
            this.UpdateButtons();
            this.index = index;
            this.LabelGroupName.Content = "Group " + this.index;
            for (int i = 0; i < group.Nodes.Count; i++)
            {
                BoxAnalysis ba = group.Nodes[i].Value as BoxAnalysis;

                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;

                Button b = new Button();
                b.Content = "Display";
                b.Tag = ba.PanelIndex + string.Empty;
                b.Click += this.ButtonDisplayPanelClick;
                b.Width = 50;
                sp.Children.Add(b);
                
                Image img = new Image();
                img.Source = ba.Thumb.Source.Clone();
                img.ToolTip = ba.Answer;
                img.Width = 150;
                sp.Children.Add(img);

                this.StackPanelGrades.Children.Add(sp);
            }
        }

        /// <summary>
        /// The delegate responsible for triggering a request to display a specific panel.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        public delegate void PanelDisplayRequestEventHandler(object sender, DisplayPanelEventArgs e);

        /// <summary>
        /// Occurs when a specific panel is requested to be displayed.
        /// </summary>
        public event PanelDisplayRequestEventHandler DisplayPanel;

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        internal void Cleanup()
        {
            this.group.PropertyChanged -= this.GroupPropertyChanged;
            for (int i = 0; i < this.StackPanelGrades.Children.Count; i++)
            {
                StackPanel sp = this.StackPanelGrades.Children[i] as StackPanel;
                sp.Children.Clear();
            }
        }

        /// <summary>
        /// Updates the buttons.
        /// </summary>
        private void UpdateButtons()
        {
            switch (this.group.Label.Grade)
            {
                case Grade.CORRECT:
                    this.ButtonGradeCorrect.IsEnabled = false;
                    this.ButtonGradeIncorrect.IsEnabled = true;
                    break;
                case Grade.INCORRECT:
                    this.ButtonGradeCorrect.IsEnabled = true;
                    this.ButtonGradeIncorrect.IsEnabled = false;
                    break;
                case Grade.INVALID:
                    this.ButtonGradeCorrect.IsEnabled = true;
                    this.ButtonGradeIncorrect.IsEnabled = true;
                    break;
                case Grade.NOTSET:
                    this.ButtonGradeCorrect.IsEnabled = true;
                    this.ButtonGradeIncorrect.IsEnabled = true;
                    break;
            }
        }

        /// <summary>
        /// Groups the property changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void GroupPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Label"))
            {
                this.UpdateButtons();
            }
        }

        /// <summary>
        /// Buttons the display panel click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonDisplayPanelClick(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            int n = int.Parse(b.Tag as string);
            this.DisplayPanel(this, new DisplayPanelEventArgs(n));
        }

        /// <summary>
        /// Buttons the grade correct click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonGradeCorrectClick(object sender, RoutedEventArgs e)
        {
            this.group.Label.Grade = Grade.CORRECT;
            this.group.UpdateLabelValue();
        }

        /// <summary>
        /// Buttons the grade incorrect click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonGradeIncorrectClick(object sender, RoutedEventArgs e)
        {
            this.group.Label.Grade = Grade.INCORRECT;
            this.group.UpdateLabelValue();
        }
    }
}
