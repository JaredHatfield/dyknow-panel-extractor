using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;


namespace DPXDatabase
{
    public class Database
    {
        private OleDbConnection connection;

        public Database(String filename)
        {
            connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filename + ";Persist Security Info=False");
            this.open();
            this.close();
        }

        private void open()
        {
            connection.Open();
        }
        private void close()
        {
            connection.Close();
        }

        public List<Student> getAllStudents()
        {
            String mySelectQuery = "SELECT S.[ID], S.[username], S.[fullName], S.[firstName], S.[lastName], S.[Section], S.[isEnrolled] FROM Students S";
            Console.WriteLine(mySelectQuery);
            OleDbCommand myCommand = new OleDbCommand(mySelectQuery, connection);
            this.open();
            OleDbDataReader myReader = myCommand.ExecuteReader();
            List<Student> students = new List<Student>();
            try
            {
                while (myReader.Read())
                {
                    students.Add(new Student(myReader.GetInt32(0), myReader.GetString(1), myReader.GetString(2),
                        myReader.GetString(3), myReader.GetString(4), myReader.GetInt32(5),
                        myReader.GetBoolean(6)));
                }
            }
            catch
            {
                return null;
            }
            finally
            {
                myReader.Close();
                this.close();
            }
            return students;
        }

        public Student getStudent(int id)
        {
            String mySelectQuery = "SELECT S.[ID], S.[username], S.[fullName], S.[firstName], S.[lastName], S.[Section], S.[isEnrolled] FROM Students S WHERE S.[ID] = " + id.ToString();
            Console.WriteLine(mySelectQuery);
            OleDbCommand myCommand = new OleDbCommand(mySelectQuery, connection);
            this.open();
            OleDbDataReader myReader = myCommand.ExecuteReader();
            Student student = new Student();
            try
            {
                if (myReader.Read())
                {
                    student = new Student(id, myReader.GetString(1), myReader.GetString(2),
                        myReader.GetString(3), myReader.GetString(4), myReader.GetInt32(5),
                        myReader.GetBoolean(6));
                }
                else
                {
                    throw new Exception("Probelm retreiving student from database");
                }
            }
            catch
            {
                return null;
            }
            finally
            {
                myReader.Close();
                this.close();
            }
            return student;
        }

        public Boolean addStudent(Student s)
        {
            this.open();
            string query = "INSERT INTO Students([username], [fullName], [firstName], [lastName], [Section], [isEnrolled]) VALUES(@parm1, @parm2, @parm3, @parm4, @parm5, @parm6)";
            int status;
            OleDbCommand cmdInsert = new OleDbCommand(query, connection);
            cmdInsert.Parameters.Clear();
            try
            {
                cmdInsert.CommandType = System.Data.CommandType.Text; //Type of query
                //Add parameters to the query
                cmdInsert.Parameters.AddWithValue("@parm1", s.Username);
                cmdInsert.Parameters.AddWithValue("@parm2", s.FirstName);
                cmdInsert.Parameters.AddWithValue("@parm3", s.FirstName);
                cmdInsert.Parameters.AddWithValue("@parm4", s.LastName);
                cmdInsert.Parameters.AddWithValue("@parm5", s.Section);
                cmdInsert.Parameters.AddWithValue("@parm6", s.IsEnrolled);

                status = cmdInsert.ExecuteNonQuery(); // 0 = failed, 1 = success
                if (!(status == 0))
                {
                    return false; //ItFailed
                }
                else
                {
                    return true; //It Worked!
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, "Error"); //Display the error
            }
            finally
            {
                this.close(); //All done
            }
            return true;
        }

        public Boolean addFile(File f)
        {
            this.open();
            string query = "INSERT INTO Files ( [Classdate], [fileName], [meanStrokes], [minStrokes], [maxStrokes], [meanDataLength], [minDataLength], [maxDataLength] ) ";
            query += " VALUES(@parm1, @parm2, @parm3, @parm4, @parm5, @parm6, @parm7, @parm8)";

            int status;
            OleDbCommand cmdInsert = new OleDbCommand(query, connection);
            cmdInsert.Parameters.Clear();
            try
            {
                cmdInsert.CommandType = System.Data.CommandType.Text; //Type of query
                //Add parameters to the query
                cmdInsert.Parameters.AddWithValue("@parm1", f.Classdate);
                cmdInsert.Parameters.AddWithValue("@parm2", f.FileName);
                cmdInsert.Parameters.AddWithValue("@parm3", f.MeanStrokes);
                cmdInsert.Parameters.AddWithValue("@parm4", f.MinStrokes);
                cmdInsert.Parameters.AddWithValue("@parm5", f.MaxStrokes);
                cmdInsert.Parameters.AddWithValue("@parm6", f.MinDataLength);
                cmdInsert.Parameters.AddWithValue("@parm7", f.MinDataLength);
                cmdInsert.Parameters.AddWithValue("@parm8", f.MaxDataLength);

                status = cmdInsert.ExecuteNonQuery(); // 0 = failed, 1 = success
                if (!(status == 0))
                {
                    return false; //ItFailed
                }
                else
                {
                    return true; //It Worked!
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, "Error"); //Display the error
            }
            finally
            {
                this.close(); //All done
            }
            return true;
        }

    }
}
