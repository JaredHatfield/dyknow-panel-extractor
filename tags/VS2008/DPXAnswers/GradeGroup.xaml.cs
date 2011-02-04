// <copyright file="GradeGroup.xaml.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The visual element representing a grade.</summary>
namespace DPXAnswers
{
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
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
        /// The cluster that this belongs to.
        /// </summary>
        private ICluster<IAnswer, GroupData> cluster;

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
        /// <param name="cluster">The cluster.</param>
        /// <param name="index">The index.</param>
        public GradeGroup(ICluster<IAnswer, GroupData> cluster, int index)
        {
            InitializeComponent();
            this.cluster = cluster;
            this.group = cluster.Groups[index];
            this.group.PropertyChanged += this.GroupPropertyChanged;
            (this.group.Nodes as INotifyCollectionChanged).CollectionChanged += this.GradeGroupCollectionChanged;
            this.UpdateButtons();
            this.index = index;
            this.LabelGroupName.Content = "Group " + this.index;
            this.GradeGroupCollectionChanged(this, null);
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
            (this.group.Nodes as INotifyCollectionChanged).CollectionChanged -= this.GradeGroupCollectionChanged;
            for (int i = 0; i < this.StackPanelGrades.Children.Count; i++)
            {
                StackPanel sp = this.StackPanelGrades.Children[i] as StackPanel;
                sp.Children.Clear();
            }
        }

        /// <summary>
        /// Grades the group collection changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Collections.Specialized.NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        private void GradeGroupCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.StackPanelGrades.Children.Clear();
            for (int i = 0; i < this.group.Nodes.Count; i++)
            {
                BoxAnalysis ba = this.group.Nodes[i].Value as BoxAnalysis;

                StackPanel stackHorizontal = new StackPanel();
                stackHorizontal.Orientation = Orientation.Horizontal;
                if (i + 1 == this.group.Nodes.Count)
                {
                    // Add a margin to only the last element
                    stackHorizontal.Margin = new Thickness(0, 0, 0, 10);
                }

                StackPanel stackVertical = new StackPanel();
                stackVertical.Orientation = Orientation.Vertical;
                stackVertical.Margin = new Thickness(0);
                stackHorizontal.Children.Add(stackVertical);

                // The button for displaying the panel
                Button b = new Button();
                b.Content = "Display";
                b.Tag = ba.PanelIndex + string.Empty;
                b.Click += this.ButtonDisplayPanelClick;
                b.Width = 80;
                stackVertical.Children.Add(b);

                // The combo box for moving an answer between groups
                ComboBox box = new ComboBox();
                box.Width = 80;
                box.Tag = this.group.Nodes[i];
                for (int j = 0; j < this.cluster.Groups.Count; j++)
                {
                    ComboBoxItem cbi = new ComboBoxItem();
                    cbi.Content = "Group " + j;
                    cbi.Tag = j + string.Empty;
                    if (j == this.index)
                    {
                        cbi.IsSelected = true;
                    }

                    box.Items.Add(cbi);
                }

                // The combo box that lets answers to be moved between groups
                ComboBoxItem cbinew = new ComboBoxItem();
                cbinew.Content = "New Group";
                cbinew.Tag = -1 + string.Empty;
                box.Items.Add(cbinew);
                box.SelectionChanged += this.BoxSelectionChanged;
                stackVertical.Children.Add(box);

                // The thumbnail of the answer
                Border imgBorder = new Border();
                imgBorder.Background = Brushes.LightGray;
                imgBorder.Margin = new Thickness(0, 10, 0, 0);
                Image img = new Image();
                img.Source = ba.Thumb.Source.Clone();
                img.ToolTip = ba.Answer;
                img.Width = 150;
                imgBorder.Child = img;
                stackHorizontal.Children.Add(imgBorder);

                this.StackPanelGrades.Children.Add(stackHorizontal);
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
        /// Boxes the selection changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void BoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            IClusterNode<IAnswer> node = combo.Tag as IClusterNode<IAnswer>;
            ComboBoxItem cbi = e.AddedItems[0] as ComboBoxItem;
            int n = int.Parse(cbi.Tag as string);
            if (n >= 0)
            {
                this.cluster.Move(node, this.cluster.Groups[n]);
            }
            else
            {
                IClusterGroup<IAnswer, GroupData> newGroup = this.cluster.AddGroup();
                this.cluster.Move(node, newGroup);
            }

            ComboBoxItem cbiold = e.RemovedItems[0] as ComboBoxItem;
            int r = int.Parse(cbiold.Tag as string);
            if (this.cluster.Groups[r].Nodes.Count == 0 && this.cluster.Groups[r].IsDeletable)
            {
                this.cluster.RemoveGroup(this.cluster.Groups[r]);
            }
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
