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
        /// The stack panel that contains the buttons.
        /// </summary>
        private StackPanel sp;

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

            // The stack panel for the buttons
            Border b = new Border();
            b.BorderBrush = Brushes.DarkGray;
            b.BorderThickness = new Thickness(1);
            Grid.SetRow(b, i);
            Grid.SetColumn(b, 3);
            g.Children.Add(b);
            this.sp = new StackPanel();
            this.sp.Orientation = Orientation.Vertical;
            b.Child = this.sp;

            // Button to mark the answer as correct
            this.cor = new Button();
            this.cor.Content = "Correct";
            this.cor.Tag = this;
            this.cor.Click += new RoutedEventHandler(this.AnswerSetCorrect);
            this.cor.Margin = new Thickness(5, 5, 5, 0);
            this.sp.Children.Add(this.cor);

            // Button to mark the answer as incorrect
            this.incor = new Button();
            this.incor.Content = "Incorrect";
            this.incor.Tag = this;
            this.incor.Click += new RoutedEventHandler(this.AnswerSetIncorrect);
            this.incor.Margin = new Thickness(5);
            this.sp.Children.Add(this.incor);

            // Add the answer
            this.ans = new Label();
            this.SetStatusLabel(this.rect.Panels.GetGroup(this.boxAnalysis).Label);
            this.ans.BorderBrush = Brushes.DarkGray;
            this.ans.BorderThickness = new Thickness(1);
            this.ans.Tag = this;
            this.ans.MouseEnter += new System.Windows.Input.MouseEventHandler(answerWindow.AnswerMouseEnter);
            this.ans.MouseLeave += new System.Windows.Input.MouseEventHandler(answerWindow.AnswerMouseLeave);
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
            this.sp.Background = Brushes.LightYellow;
        }

        /// <summary>
        /// Occurs when the mouse moves out of the region.
        /// </summary>
        public void MouseOut()
        {
            this.index.Background = Brushes.White;
            this.num.Background = Brushes.White;
            this.ans.Background = Brushes.White;
            this.sp.Background = Brushes.White;
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
                    this.cor.IsEnabled = true;
                    this.incor.IsEnabled = true;
                    break;
                case Grade.AUTOCORRECT:
                    this.ans.Content = BoxAnalysis.BoxGradeString(grade);
                    this.ans.Foreground = Brushes.DarkGreen;
                    this.ans.FontWeight = FontWeights.Normal;
                    this.cor.IsEnabled = false;
                    this.incor.IsEnabled = true;
                    break;
                case Grade.SETCORRECT:
                    this.ans.Content = BoxAnalysis.BoxGradeString(grade);
                    this.ans.Foreground = Brushes.DarkGreen;
                    this.ans.FontWeight = FontWeights.Bold;
                    this.cor.IsEnabled = false;
                    this.incor.IsEnabled = true;
                    break;
                case Grade.AUTOINCORRECT:
                    this.ans.Content = BoxAnalysis.BoxGradeString(grade);
                    this.ans.Foreground = Brushes.DarkRed;
                    this.ans.FontWeight = FontWeights.Normal;
                    this.cor.IsEnabled = true;
                    this.incor.IsEnabled = false;
                    break;
                case Grade.SETINCORRECT:
                    this.ans.Content = BoxAnalysis.BoxGradeString(grade);
                    this.ans.Foreground = Brushes.DarkRed;
                    this.ans.FontWeight = FontWeights.Bold;
                    this.cor.IsEnabled = true;
                    this.incor.IsEnabled = false;
                    break;
                case Grade.INVALID:
                    this.ans.Content = BoxAnalysis.BoxGradeString(grade);
                    this.ans.Foreground = Brushes.Black;
                    this.ans.FontWeight = FontWeights.Normal;
                    this.cor.IsEnabled = false;
                    this.incor.IsEnabled = false;
                    break;
            }
        }

        /// <summary>
        /// Sets the group that contains this box as correct.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void AnswerSetCorrect(object sender, RoutedEventArgs e)
        {
            this.rect.Panels.GetGroup(this.boxAnalysis).Label = Grade.SETCORRECT;
            this.SetStatusLabel(Grade.SETCORRECT);
        }

        /// <summary>
        /// Sets the group that contains this box as incorrect.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void AnswerSetIncorrect(object sender, RoutedEventArgs e)
        {
            this.rect.Panels.GetGroup(this.boxAnalysis).Label = Grade.SETINCORRECT;
            this.SetStatusLabel(Grade.SETINCORRECT);
        }
    }
}
