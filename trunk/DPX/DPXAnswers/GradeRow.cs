// <copyright file="GradeRow.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The row for the grade results.</summary>
namespace DPXAnswers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

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
        /// The Correct answer button.
        /// </summary>
        private Button cor;

        /// <summary>
        /// The incorrect answer button.
        /// </summary>
        private Button incor;

        /// <summary>
        /// Initializes a new instance of the <see cref="GradeRow"/> class.
        /// </summary>
        /// <param name="g">The grid to add to.</param>
        /// <param name="answerWindow">The answer window.</param>
        /// <param name="panel">The panel.</param>
        /// <param name="i">The panel index.</param>
        public GradeRow(Grid g, AnswerWindow answerWindow, PanelAnswer panel, int i)
        {
            this.rect = panel.Keys[i];
            this.boxAnalysis = panel.GetBoxAnalysis(panel.Keys[i]);

            // Add the panel index
            this.index = new Label();
            this.index.Content = "Box " + panel.Keys[i].Index;
            this.index.BorderBrush = Brushes.DarkGray;
            this.index.BorderThickness = new Thickness(1);
            this.index.Tag = this;
            this.index.MouseEnter += new System.Windows.Input.MouseEventHandler(answerWindow.AnswerMouseEnter);
            this.index.MouseLeave += new System.Windows.Input.MouseEventHandler(answerWindow.AnswerMouseLeave);
            Grid.SetRow(this.index, i);
            Grid.SetColumn(this.index, 0);
            g.Children.Add(this.index);

            // Add the panel number
            this.num = new Label();
            this.num.Content = panel.GetRecognizedString(panel.Keys[i]);
            this.num.BorderBrush = Brushes.DarkGray;
            this.num.BorderThickness = new Thickness(1);
            this.num.Tag = this;
            this.num.MouseEnter += new System.Windows.Input.MouseEventHandler(answerWindow.AnswerMouseEnter);
            this.num.MouseLeave += new System.Windows.Input.MouseEventHandler(answerWindow.AnswerMouseLeave);
            Grid.SetRow(this.num, i);
            Grid.SetColumn(this.num, 1);
            g.Children.Add(this.num);

            // Add the answer
            this.ans = new Label();
            this.ans.Content = panel.GetAnswer(panel.Keys[i]);
            this.ans.BorderBrush = Brushes.DarkGray;
            this.ans.BorderThickness = new Thickness(1);
            this.ans.Tag = this;
            this.ans.MouseEnter += new System.Windows.Input.MouseEventHandler(answerWindow.AnswerMouseEnter);
            this.ans.MouseLeave += new System.Windows.Input.MouseEventHandler(answerWindow.AnswerMouseLeave);
            Grid.SetRow(this.ans, i);
            Grid.SetColumn(this.ans, 2);
            g.Children.Add(this.ans);

            // Button to mark the answer as correct
            this.cor = new Button();
            this.cor.Content = "C";
            this.cor.Tag = this;
            this.cor.Click += new RoutedEventHandler(this.AnswerSetCorrect);
            this.cor.BorderBrush = Brushes.DarkGray;
            this.cor.BorderThickness = new Thickness(1);
            Grid.SetRow(this.cor, i);
            Grid.SetColumn(this.cor, 3);
            g.Children.Add(this.cor);

            // Button to mark the answer as incorrect
            this.incor = new Button();
            this.incor.Content = "I";
            this.incor.Tag = this;
            this.incor.Click += new RoutedEventHandler(this.AnswerSetIncorrect);
            this.incor.BorderBrush = Brushes.DarkGray;
            this.incor.BorderThickness = new Thickness(1);
            Grid.SetRow(this.incor, i);
            Grid.SetColumn(this.incor, 4);
            g.Children.Add(this.incor);
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
        /// Answers the set correct.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void AnswerSetCorrect(object sender, RoutedEventArgs e)
        {
            this.boxAnalysis.BoxGrade = BoxAnalysis.Grade.SETCORRECT;
            this.ans.Content = this.boxAnalysis.BoxGrade;
        }

        /// <summary>
        /// Answers the set incorrect.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void AnswerSetIncorrect(object sender, RoutedEventArgs e)
        {
            this.boxAnalysis.BoxGrade = BoxAnalysis.Grade.SETINCORRECT;
            this.ans.Content = this.boxAnalysis.BoxGrade;
        }
    }
}
