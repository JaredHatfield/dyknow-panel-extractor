// <copyright file="ThumbnailQueueItem.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The logic required to generate and display a thumbnail.</summary>
namespace DPXGrader
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

    /// <summary>
    /// The logic required to generate and display a thumbnail.
    /// </summary>
    internal class ThumbnailQueueItem : QueueItem
    {
        /// <summary>
        /// The window element that will be manipulated by this item.
        /// </summary>
        private PanelProcessorWindow window;

        /// <summary>
        /// The panel to render.
        /// </summary>
        private int num;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThumbnailQueueItem"/> class.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <param name="num">The panel number to process.</param>
        internal ThumbnailQueueItem(PanelProcessorWindow window, int num)
        {
            this.window = window;
            this.num = num;
        }

        /// <summary>
        /// Executes the action required by this item.
        /// </summary>
        public override void Run()
        {
            Image myImage = this.window.DyKnow.GetThumbnail(
                this.num, 
                this.window.Inky.ActualWidth, 
                this.window.Inky.ActualHeight, 
                0.4);
            Border b = new Border();
            b.Child = myImage;

            if (this.num == 0)
            {
                b.BorderBrush = Brushes.Gold;
                this.window.SelectedPanelId = this.num;
            }
            else
            {
                b.BorderBrush = Brushes.Black;
            }

            b.BorderThickness = new Thickness(1);
            b.Margin = new Thickness(5);
            b.MouseDown += this.window.PanelSelected;
            b.Tag = this.num;

            this.window.PanelScrollView.Children.Add(b);
        }
    }
}
