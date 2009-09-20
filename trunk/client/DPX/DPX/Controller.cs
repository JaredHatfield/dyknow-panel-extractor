using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DPXDatabase;

namespace DPX
{
    class Controller
    {
        //Singleton instance of the class
        private static Controller c;

        //Instance of the database
        Database db;

        private Controller()
        {
            db = null;
        }

        public static Controller Instance()
        {
            if (c == null)
            {
                c = new Controller();
            }
            return c;
        }

        public Database DB
        {
            get { return db; }
        }


        public Boolean isDatabaseOpen()
        {
            if (db == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void openDatabaseFile(String filename)
        {
            db = new Database(filename);
        }

        public void closeDatabase()
        {
            if (db != null)
            {
                db.Connection.Dispose();
                db = null;
            }
        }
    }
}
