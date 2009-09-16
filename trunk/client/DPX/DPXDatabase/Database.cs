using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using QuickReader;

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


        //QUERIES ON THE SECTION TABLE

        public String getSectionName(int id)
        {
            String mySelectQuery = "SELECT S.[sectionName] FROM Sections S WHERE S.[ID] = @parm1";
            OleDbCommand myCommand = new OleDbCommand(mySelectQuery, connection);
            myCommand.Parameters.AddWithValue("@parm1", id);
            this.open();
            OleDbDataReader myReader = myCommand.ExecuteReader();
            String section = "";
            try
            {
                if (myReader.Read())
                {
                    section = myReader.GetString(0);
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
            return section;
        }

        public Boolean addSection(String name)
        {
            this.open();
            string query = "INSERT INTO Sections([sectionName]) VALUES(@parm1)";
            int status;
            OleDbCommand cmdInsert = new OleDbCommand(query, connection);
            cmdInsert.Parameters.Clear();
            try
            {
                cmdInsert.CommandType = System.Data.CommandType.Text; //Type of query
                //Add parameters to the query
                cmdInsert.Parameters.AddWithValue("@parm1", name);

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

        // QUERIES ON THE STUDENT TABLE
        public List<Student> getAllStudents()
        {
            String mySelectQuery = "SELECT S.[ID], S.[username], S.[fullName], S.[firstName], S.[lastName], S.[Section], S.[isEnrolled] FROM Students S";
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
            String mySelectQuery = "SELECT S.[ID], S.[username], S.[fullName], S.[firstName], S.[lastName], ";
            mySelectQuery += "S.[Section], S.[isEnrolled] FROM Students S WHERE S.[ID] = @parm1";
            OleDbCommand myCommand = new OleDbCommand(mySelectQuery, connection);
            myCommand.Parameters.AddWithValue("@parm1", id);
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
            //The section is going to be null
            if (s.Section < 0)
            {
                this.open();
                string query = "INSERT INTO Students([username], [fullName], [firstName], [lastName], [isEnrolled]) VALUES(@parm1, @parm2, @parm3, @parm4, @parm5)";
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
                    cmdInsert.Parameters.AddWithValue("@parm5", s.IsEnrolled);

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
            //We know what section
            else
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
        }

        public Boolean isStudentUsername(String username)
        {
            String mySelectQuery = "SELECT S.[ID], S.[username], S.[fullName], S.[firstName], S.[lastName], ";
            mySelectQuery += "S.[Section], S.[isEnrolled] FROM Students S WHERE S.[username] = @parm1";
            OleDbCommand myCommand = new OleDbCommand(mySelectQuery, connection);
            myCommand.Parameters.AddWithValue("@parm1", username);
            this.open();
            OleDbDataReader myReader = myCommand.ExecuteReader();
            Boolean studentTest = false;
            try
            {
                if (myReader.Read())
                {
                    studentTest = true;
                }
                else
                {
                    throw new Exception("Probelm retreiving student from database");
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                myReader.Close();
                this.close();
            }
            return studentTest;
        }

        //QUERIES ON THE FILE TABLE
        public int addFile(File f)
        {
            this.open();
            string query = "INSERT INTO Files ( [Classdate], [fileName], [meanStrokes], [stdDevStrokes], ";
            query += "[minStrokes], [maxStrokes], [meanDataLength], [stdDevDataLength], [minDataLength], [maxDataLength] ) ";
            query += " VALUES(@parm1, @parm2, @parm3, @parm4, @parm5, @parm6, @parm7, @parm8, @parm9, @parm10)";

            int insertId = -1;

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
                cmdInsert.Parameters.AddWithValue("@parm4", f.StdDevStrokes);
                cmdInsert.Parameters.AddWithValue("@parm5", f.MinStrokes);
                cmdInsert.Parameters.AddWithValue("@parm6", f.MaxStrokes);
                cmdInsert.Parameters.AddWithValue("@parm7", f.MeanDataLength);
                cmdInsert.Parameters.AddWithValue("@parm8", f.StdDevDataLength);
                cmdInsert.Parameters.AddWithValue("@parm9", f.MinDataLength);
                cmdInsert.Parameters.AddWithValue("@parm10", f.MaxDataLength);

                cmdInsert.ExecuteNonQuery();
                cmdInsert.CommandText = "Select @@Identity";
                insertId = (int)cmdInsert.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, "Error"); //Display the error
            }
            finally
            {
                this.close(); //All done
            }
            return insertId;
        }

        public int addFile(DyKnowReader dr, DateTime d)
        {
            int classdateId = -1;
            if (!this.isClassdate(d))//Date is not in the database
            {
                addClassdate(d);
                
            }
            classdateId = this.getClassdateId(d);
            
            File f = new File(classdateId, dr.FileName, dr.MaxStrokeCount, dr.MeanStrokes, dr.MaxStrokeCount,
                dr.MeanStrokeDistance, dr.MeanStrokeDistance, dr.StdDevStrokeDistance, dr.MinStrokeDistance,
                dr.MaxStrokeDistance);
            return this.addFile(f);
        }

        //QUERIES ON THE CLASSDATE TABLE
        public Boolean addClassdate(DateTime d)
        {
            this.open();
            string query = "INSERT INTO Classdates ( classDate ) VALUES (@parm1)";
            int status;
            OleDbCommand cmdInsert = new OleDbCommand(query, connection);
            cmdInsert.Parameters.Clear();
            try
            {
                cmdInsert.CommandType = System.Data.CommandType.Text; //Type of query
                //Add parameters to the query
                cmdInsert.Parameters.AddWithValue("@parm1", d.Date);

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

        public Boolean isClassdate(DateTime d)
        {
            String mySelectQuery = "SELECT C.[ID], C.[classDate] FROM Classdates C WHERE C.[classDate] = @parm1";
            OleDbCommand myCommand = new OleDbCommand(mySelectQuery, connection);
            myCommand.Parameters.AddWithValue("@parm1", d.Date);
            this.open();
            OleDbDataReader myReader = myCommand.ExecuteReader();

            Boolean panelFound = false;
            try
            {
                //If a row was returned the date is in the database
                if (myReader.Read())
                {
                    panelFound = true;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                myReader.Close();
                this.close();
            }
            return panelFound;
        }

        public int getClassdateId(DateTime d)
        {
            String mySelectQuery = "SELECT C.[ID], C.[classDate] FROM Classdates C WHERE C.[classDate] = @parm1";
            OleDbCommand myCommand = new OleDbCommand(mySelectQuery, connection);
            myCommand.Parameters.AddWithValue("@parm1", d.Date);
            this.open();
            OleDbDataReader myReader = myCommand.ExecuteReader();
            int id = -1;
            try
            {
                if (myReader.Read())
                {
                    id = myReader.GetInt32(0);
                }
                else
                {
                    throw new Exception("Probelm retreiving student from database");
                }
            }
            catch
            {
                return -1;
            }
            finally
            {
                myReader.Close();
                this.close();
            }
            return id;
        }

        public List<Classdate> getClassdates()
        {
            String mySelectQuery = "SELECT C.[ID], C.[classDate] FROM Classdates C ORDER BY [classDate] DESC";
            OleDbCommand myCommand = new OleDbCommand(mySelectQuery, connection);
            this.open();
            OleDbDataReader myReader = myCommand.ExecuteReader();
            List<Classdate> dates = new List<Classdate>();
            try
            {
                while (myReader.Read())
                {
                    dates.Add(new Classdate(myReader.GetInt32(0), myReader.GetDateTime(1)));
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
            return dates;
        }

        //QUERIES ON THE PANELS TABLE

        public Boolean addPanel(int fileid, DyKnowPage d)
        {
            this.open();
            string query = "INSERT INTO Panels ( [File], [slideNumber], [username], [totalStrokeCount], ";
            query += "[netStrokeCount], [deletedStrokeCount], [totalDateLength], [netDataLength], [deletedDataLength], ";
            query += "[isBlank], [analysis] ) VALUES(@parm1, @parm2, @parm3, @parm4, @parm5, @parm6, @parm7, @parm8, ";
            query += "@parm9, @parm10, @parm11)";
            int status;
            OleDbCommand cmdInsert = new OleDbCommand(query, connection);
            cmdInsert.Parameters.Clear();
            try
            {
                cmdInsert.CommandType = System.Data.CommandType.Text; //Type of query
                //Add parameters to the query
                cmdInsert.Parameters.AddWithValue("@parm1", fileid);
                cmdInsert.Parameters.AddWithValue("@parm2", d.PageNumber);
                cmdInsert.Parameters.AddWithValue("@parm3", d.UserName);
                cmdInsert.Parameters.AddWithValue("@parm4", d.TotalStrokeCount);
                cmdInsert.Parameters.AddWithValue("@parm5", d.NetStrokeCount);
                cmdInsert.Parameters.AddWithValue("@parm6", d.DeletedStrokeCount);
                cmdInsert.Parameters.AddWithValue("@parm7", d.TotalStrokeDistance);
                cmdInsert.Parameters.AddWithValue("@parm8", d.NetStrokeDistance);
                cmdInsert.Parameters.AddWithValue("@parm9", d.DeletedStrokeDistance);
                cmdInsert.Parameters.AddWithValue("@parm10", d.IsBlank);
                cmdInsert.Parameters.AddWithValue("@parm11", d.Finished);

                status = cmdInsert.ExecuteNonQuery(); // 0 = failed, 1 = success
                if (!(status == 0))
                {
                    return false; //It Failed
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

        //QUERIES ON THE EXCEPTIONS TABLE

        public Boolean addException(Exceptions e)
        {
            this.open();
            string query = "INSERT INTO Exceptions ( [Classdate], [Student], [Reason], [notes] ) ";
            query += "VALUES (@parm1, @parm2, @parm3, @parm4)";
            int status;
            OleDbCommand cmdInsert = new OleDbCommand(query, connection);
            cmdInsert.Parameters.Clear();
            try
            {
                cmdInsert.CommandType = System.Data.CommandType.Text; //Type of query
                //Add parameters to the query
                cmdInsert.Parameters.AddWithValue("@parm1", e.Classdate);
                cmdInsert.Parameters.AddWithValue("@parm2", e.Student);
                cmdInsert.Parameters.AddWithValue("@parm3", e.Reason);
                cmdInsert.Parameters.AddWithValue("@parm4", e.Notes);

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
