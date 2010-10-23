// <copyright file="QueueItem.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>A queued object that is able to be processed.</summary>
namespace DPXCommon
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// A queued object that is able to be processed.
    /// </summary>
    public abstract class QueueItem
    {
        /// <summary>
        /// Executes the action required by this item.
        /// </summary>
        public abstract void Run();
    }
}
