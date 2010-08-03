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
            InkCanvas ink = new InkCanvas();
            ink.Width = this.window.Inky.ActualWidth;
            ink.Height = this.window.Inky.ActualHeight;
            lock (this.window.DyKnow)
            {
                this.window.DyKnow.Render(ink, this.num);
            }

            RenderTargetBitmap rtb = new RenderTargetBitmap(Convert.ToInt32(ink.Width), Convert.ToInt32(ink.Height), 96d, 96d, PixelFormats.Default);
            rtb.Render(ink);
            TransformedBitmap tb = new TransformedBitmap(rtb, new ScaleTransform(.4, .4));
            Image myImage = new Image();
            myImage.Source = tb;
            Border b = new Border();
            b.Child = myImage;

            if (this.num == 0)
            {
                b.BorderBrush = Brushes.Gold;
                this.window.SelectedThumbnail = b;
            }
            else
            {
                b.BorderBrush = Brushes.Black;
            }

            b.BorderThickness = new Thickness(1);
            b.Margin = new Thickness(5);
            b.MouseDown += new MouseButtonEventHandler(this.window.PanelSelected);
            b.Tag = this.num;

            this.window.PanelScrollView.Children.Add(b);
        }
    }
}
