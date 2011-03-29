// <copyright file="ImageProcessQueueItem.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The logic for generating a thumbnail.</summary>
namespace DPXAnswers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Ink;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using DPXCommon;
    using DPXReader.DyKnow;

    /// <summary>
    /// The logic for generating a thumbnail.
    /// </summary>
    internal class ImageProcessQueueItem : QueueItem
    {
        /// <summary>
        /// The answer window to modify.
        /// </summary>
        private AnswerWindow window;

        /// <summary>
        /// The DyKnow file to render.
        /// </summary>
        private DyKnow dyknow;

        /// <summary>
        /// The index of the panel to render
        /// </summary>
        private int index;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageProcessQueueItem"/> class.
        /// </summary>
        /// <param name="window">The answer window.</param>
        /// <param name="dyknow">The dyknow file.</param>
        /// <param name="index">The panel index.</param>
        internal ImageProcessQueueItem(AnswerWindow window, DyKnow dyknow, int index)
        {
            this.window = window;
            this.dyknow = dyknow;
            this.index = index;
        }

        /// <summary>
        /// Executes the action required by this item.
        /// </summary>
        public override void Run()
        {
            Image myImage = this.dyknow.GetThumbnail(
                this.index, 
                this.window.Inky.ActualWidth, 
                this.window.Inky.ActualHeight, 
                0.4);
            Border b = new Border();
            b.Child = myImage;

            if (this.index == 0)
            {
                b.BorderBrush = Brushes.Gold;
                this.window.SelectedPanelId = this.index;
            }
            else
            {
                b.BorderBrush = Brushes.Black;
            }

            b.BorderThickness = new Thickness(1);
            b.Margin = new Thickness(5);
            
            b.MouseDown += this.window.PanelSelected;
            b.Tag = this.index;

            this.window.PanelScrollView.Children.Add(b);

            this.window.IncrementProgressBar();
        }
    }
}
