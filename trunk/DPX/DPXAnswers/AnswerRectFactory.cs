// <copyright file="AnswerRectFactory.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>The Answer Rectangle Factory.</summary>
namespace DPXAnswers
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Windows;

    /// <summary>
    /// The Answer Rectangle Factory.
    /// </summary>
    internal class AnswerRectFactory
    {
        /// <summary>
        /// The list of rectangles.
        /// </summary>
        private Collection<AnswerRect> rectangles;

        /// <summary>
        /// The current index.
        /// </summary>
        private int index;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnswerRectFactory"/> class.
        /// </summary>
        public AnswerRectFactory()
        {
            this.rectangles = new Collection<AnswerRect>();
            this.index = 0;
        }

        /// <summary>
        /// Gets the answer rectangle.
        /// </summary>
        /// <value>The answer rectangle.</value>
        public ReadOnlyCollection<AnswerRect> AnswerRect
        {
            get { return new ReadOnlyCollection<AnswerRect>(this.rectangles); }
        }

        /// <summary>
        /// Resets the factory.
        /// </summary>
        public void Reset()
        {
            this.rectangles.Clear();
            this.index = 0;
        }

        /// <summary>
        /// Gets the answer rect.
        /// </summary>
        /// <param name="rect">The rectangle.</param>
        /// <returns>The AnswerRect.</returns>
        public AnswerRect GetAnswerRect(Rect rect)
        {
            AnswerRect ar = null;
            lock (this.rectangles)
            {
                for (int i = 0; i < this.rectangles.Count; i++)
                {
                    if (this.rectangles[i].Area.Equals(rect))
                    {
                        ar = this.rectangles[i];
                    }
                }

                if (ar == null)
                {
                    ar = new AnswerRect(rect, this.index++);
                    this.rectangles.Add(ar);
                }
            }

            return ar;
        }
    }
}
