// <copyright file="App.xaml.cs" company="DPX on Google Code">
// GNU General Public License v3
// </copyright>
namespace DPX
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Linq;
    using System.Windows;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Create an instance of the controller right away.
        /// </summary>
        private Controller c = Controller.Instance();
    }
}
