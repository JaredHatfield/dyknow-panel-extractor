// <copyright file="DatabaseManager.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>Provides access to the database.</summary>
namespace HandwritingAccuracy
{
    using System;
    using System.Collections.ObjectModel;
    using System.Data.Odbc;
    using System.Diagnostics;
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
        internal static DatabaseManager Instance()
        {
            if (DatabaseManager.instance == null)
            {
                DatabaseManager.instance = new DatabaseManager();
            }

            return DatabaseManager.instance;
        }

        /// <summary>
        /// Inserts the experiment.
        /// </summary>
        /// <param name="participant">The participant.</param>
        /// <param name="tablet">The tablet.</param>
        /// <returns>The id of the new experiment.</returns>
        internal int InsertExperiment(Participant participant, TabletPC tablet)
        {
            OdbcCommand command = new OdbcCommand("INSERT INTO `experiment` (`participant`, `tablet`, `time`) VALUES(?, ?, NOW());", this.connection);
            OdbcParameter p = new OdbcParameter("participant", participant.PID);
            command.Parameters.Add(p);
            OdbcParameter t = new OdbcParameter("tablet", tablet.PID);
            command.Parameters.Add(t);
            int n = command.ExecuteNonQuery();
            
            // Get the last inserted 
            OdbcCommand command2 = new OdbcCommand("SELECT LAST_INSERT_ID()", this.connection);
            object result = command2.ExecuteScalar();
            int value = Int32.Parse(result.ToString());
            Debug.WriteLine("Inserted Experiment with PID = " + value);
            return value;
        }

        /// <summary>
        /// Gets the participant.
        /// </summary>
        /// <param name="firstname">The firstname.</param>
        /// <param name="lastname">The lastname.</param>
        /// <returns>The participant.</returns>
        internal Participant GetParticipant(string firstname, string lastname)
        {
            OdbcCommand command = new OdbcCommand("SELECT `pid`, `firstname`, `lastname`, `handedness`, `gender`, `own`, `use` FROM `participant` WHERE `firstname` = ? AND `lastname` = ? LIMIT 1;", this.connection);
            OdbcParameter first = new OdbcParameter("firstname", firstname);
            command.Parameters.Add(first);
            OdbcParameter last = new OdbcParameter("lastname", lastname);
            command.Parameters.Add(last);
            OdbcDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Participant p = new Participant(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5), reader.GetInt32(6));
                return p;
            }

            return null;
        }

        /// <summary>
        /// Gets all tablets.
        /// </summary>
        /// <returns>A collection of all of the tablets.</returns>
        internal Collection<TabletPC> GetAllTablets()
        {
            Collection<TabletPC> results = new Collection<TabletPC>();
            OdbcCommand command = new OdbcCommand("SELECT `pid`, `manufacturer`, `model` FROM `tablet`", this.connection);
            try
            {
                this.Connect();
                OdbcDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    TabletPC tablet = new TabletPC(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                    results.Add(tablet);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Could not execute query " + e.Message);
            }

            return results;
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
