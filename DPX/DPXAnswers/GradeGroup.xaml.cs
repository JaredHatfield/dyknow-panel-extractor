﻿// <copyright file="GradeGroup.xaml.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The visual element representing a grade.</summary>
namespace DPXAnswers
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
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using ClusterLibraryCore;

    /// <summary>
    /// Interaction logic for GradeGroup.xaml
    /// </summary>
    public partial class GradeGroup : UserControl
    {
        /// <summary>
        /// The group that is being displayed.
        /// </summary>
        private IClusterGroup<BoxAnalysis, Grade> group;

        /// <summary>
        /// The index of the group.
        /// </summary>
        private int index;

        /// <summary>
        /// Initializes a new instance of the <see cref="GradeGroup"/> class.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <param name="index">The index.</param>
        public GradeGroup(IClusterGroup<BoxAnalysis, Grade> group, int index)
        {
            InitializeComponent();
            this.group = group;
            group.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(this.GroupPropertyChanged);
            this.UpdateButtons();
            this.index = index;
            this.LabelGroupName.Content = "Group " + this.index;
            for (int i = 0; i < group.Nodes.Count; i++)
            {
                Label l = new Label();
                l.Content = group.Nodes[i].Value.Answer;
                l.Tag = group.Nodes[i].Value;
                this.StackPanelGrades.Children.Add(l);
            }
        }

        /// <summary>
        /// Updates the buttons.
        /// </summary>
        private void UpdateButtons()
        {
            switch (this.group.Label)
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
        /// Buttons the grade correct click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonGradeCorrectClick(object sender, RoutedEventArgs e)
        {
            this.group.Label = Grade.CORRECT;
        }

        /// <summary>
        /// Buttons the grade incorrect click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonGradeIncorrectClick(object sender, RoutedEventArgs e)
        {
            this.group.Label = Grade.INCORRECT;
        }
    }
}