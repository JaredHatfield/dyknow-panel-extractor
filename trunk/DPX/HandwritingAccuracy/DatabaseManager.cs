// <copyright file="DatabaseManager.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>Provides access to the database.</summary>
namespace HandwritingAccuracy
{
    using System;
    using System.Collections.Generic;
    using System.Data.Odbc;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// The database manager class.
    /// </summary>
    internal class DatabaseManager
    {
        /// <summary>
        /// The singleton instance.
        /// </summary>
        private static DatabaseManager instance;

        /// <summary>
        /// The ODBC connection to the MySQL database
        /// </summary>
        private OdbcConnection connection;

        /// <summary>
        /// Prevents a default instance of the DatabaseManager class from being created.
        /// </summary>
        private DatabaseManager()
        {
            string connectionString = this.ConstructConnectionString();
            this.connection = new OdbcConnection(connectionString);
        }

        /// <summary>
        /// Instances this instance.
        /// </summary>
        /// <returns>The singleton instance.</returns>
        private static DatabaseManager Instance()
        {
            if (DatabaseManager.instance == null)
            {
                DatabaseManager.instance = new DatabaseManager();
            }

            return DatabaseManager.instance;
        }

        /// <summary>
        /// Connects this instance.
        /// </summary>
        private void Connect()
        {
            if (this.connection.State != System.Data.ConnectionState.Open)
            {
                this.connection.Open();
            }
        }

        /// <summary>
        /// Constructs the connection string.
        /// </summary>
        /// <returns>The constructed connection string</returns>
        private string ConstructConnectionString()
        {
            string connectionString = string.Empty;
            connectionString += "DRIVER={" + HandwritingAccuracy.Properties.Settings.Default.DBDriverId + "};";
            connectionString += "SERVER=" + HandwritingAccuracy.Properties.Settings.Default.DBHost + ";";
            connectionString += "PORT=" + HandwritingAccuracy.Properties.Settings.Default.DBPort + ";";
            connectionString += "DATABASE=" + HandwritingAccuracy.Properties.Settings.Default.DBName + ";";
            connectionString += "UID=" + HandwritingAccuracy.Properties.Settings.Default.DBUid + ";";
            connectionString += "PASSWORD=" + HandwritingAccuracy.Properties.Settings.Default.DBPass + ";";
            connectionString += "OPTION=" + HandwritingAccuracy.Properties.Settings.Default.DBOptions + ";";
            return connectionString;
        }
    }
}
