// <copyright file="GradeRow.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The row for the grade results.</summary>
namespace DPXAnswers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using ClusterLibraryCore;
    using GradeLibrary;

    /// <summary>
    /// The row for the grade results.
    /// </summary>
    internal class GradeRow
    {
        /// <summary>
        /// The rectangle
        /// </summary>
        private AnswerRect rect;

        /// <summary>
        /// The box analysis.
        /// </summary>
        private BoxAnalysis boxAnalysis;

        /// <summary>
        /// The group for the GradeRow.
        /// </summary>
        private IClusterGroup<IAnswer, GroupData> group;

        /// <summary>
        /// The index label.
        /// </summary>
        private Label index;

        /// <summary>
        /// The number label.
        /// </summary>
        private Label num;

        /// <summary>
        /// The answer label.
        /// </summary>
        private Label ans;

        /// <summary>
        /// The answer window.
        /// </summary>
        private AnswerWindow answerWindow;

        /// <summary>
        /// Initializes a new instance of the <see cref="GradeRow"/> class.
        /// </summary>
        /// <param name="g">The grid to add to.</param>
        /// <param name="answerWindow">The answer window.</param>
        /// <param name="panel">The panel.</param>
        /// <param name="i">The panel index.</param>
        internal GradeRow(Grid g, AnswerWindow answerWindow, PanelAnswer panel, int i)
        {
            this.answerWindow = answerWindow;
            this.rect = panel.Keys[i];
            this.boxAnalysis = panel.GetBoxAnalysis(panel.Keys[i]);
            this.group = this.rect.Cluster.GetGroup(this.boxAnalysis);
            this.group.PropertyChanged += this.GradeRowPropertyChanged;

            // Add the panel index
            this.index = new Label();
            this.index.Content = "Box " + panel.Keys[i].Index;
            this.index.BorderBrush = Brushes.DarkGray;
            this.index.BorderThickness = new Thickness(1);
            this.index.Tag = this;
            this.index.MouseEnter += answerWindow.AnswerMouseEnter;
            this.index.MouseLeave += answerWindow.AnswerMouseLeave;
            Grid.SetRow(this.index, i);
            Grid.SetColumn(this.index, 0);
            g.Children.Add(this.index);

            // Add the panel number
            this.num = new Label();
            this.num.Content = panel.GetRecognizedString(panel.Keys[i]);
            this.num.BorderBrush = Brushes.DarkGray;
            this.num.BorderThickness = new Thickness(1);
            this.num.Tag = this;
            this.num.MouseEnter += answerWindow.AnswerMouseEnter;
            this.num.MouseLeave += answerWindow.AnswerMouseLeave;
            Grid.SetRow(this.num, i);
            Grid.SetColumn(this.num, 1);
            g.Children.Add(this.num);

            // Add the answer
            this.ans = new Label();
            this.SetStatusLabel(this.rect.Cluster.GetGroup(this.boxAnalysis).Label.Grade);
            this.ans.BorderBrush = Brushes.DarkGray;
            this.ans.BorderThickness = new Thickness(1);
            this.ans.Tag = this;
            this.ans.MouseEnter += answerWindow.AnswerMouseEnter;
            this.ans.MouseLeave += answerWindow.AnswerMouseLeave;
            Grid.SetRow(this.ans, i);
            Grid.SetColumn(this.ans, 2);
            g.Children.Add(this.ans);
        }

        /// <summary>
        /// Gets the rect.
        /// </summary>
        /// <value>The rectangle.</value>
        public Rect Rect
        {
            get { return this.rect.Area; }
        }

        /// <summary>
        /// Occurs when the mouse moves into the region.
        /// </summary>
        public void MouseIn()
        {
            this.index.Background = Brushes.LightYellow;
            this.num.Background = Brushes.LightYellow;
            this.ans.Background = Brushes.LightYellow;
        }

        /// <summary>
        /// Occurs when the mouse moves out of the region.
        /// </summary>
        public void MouseOut()
        {
            this.index.Background = Brushes.White;
            this.num.Background = Brushes.White;
            this.ans.Background = Brushes.White;
        }

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        internal void Cleanup()
        {
            this.group.PropertyChanged -= this.GradeRowPropertyChanged;
            this.index.MouseEnter -= this.answerWindow.AnswerMouseEnter;
            this.index.MouseLeave -= this.answerWindow.AnswerMouseLeave;
            this.num.MouseEnter -= this.answerWindow.AnswerMouseEnter;
            this.num.MouseLeave -= this.answerWindow.AnswerMouseLeave;
            this.ans.MouseEnter -= this.answerWindow.AnswerMouseEnter;
            this.ans.MouseLeave -= this.answerWindow.AnswerMouseLeave;
        }

        /// <summary>
        /// Sets the status of the answer for the specific answer box..
        /// </summary>
        /// <param name="grade">The grade of the panel.</param>
        private void SetStatusLabel(Grade grade)
        {
            switch (grade)
            {
                case Grade.NOTSET:
                    this.ans.Content = BoxAnalysis.BoxGradeString(grade);
                    this.ans.Foreground = Brushes.Black;
                    this.ans.FontWeight = FontWeights.Normal;
                    break;
                case Grade.CORRECT:
                    this.ans.Content = BoxAnalysis.BoxGradeString(grade);
                    this.ans.Foreground = Brushes.DarkGreen;
                    this.ans.FontWeight = FontWeights.Bold;
                    break;
                case Grade.INCORRECT:
                    this.ans.Content = BoxAnalysis.BoxGradeString(grade);
                    this.ans.Foreground = Brushes.DarkRed;
                    this.ans.FontWeight = FontWeights.Bold;
                    break;
                case Grade.INVALID:
                    this.ans.Content = BoxAnalysis.BoxGradeString(grade);
                    this.ans.Foreground = Brushes.Black;
                    this.ans.FontWeight = FontWeights.Normal;
                    break;
            }
        }

        /// <summary>
        /// Grades the row property changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void GradeRowPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Label"))
            {
                this.SetStatusLabel(this.group.Label.Grade);
            }
        }
    }
}
