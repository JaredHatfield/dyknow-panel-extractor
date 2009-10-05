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

        public OleDbConnection Connection
        {
            get { return connection; }
        }


        // QUERIES ON THE SECTION TABLE
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
        public List<Section> getSections()
        {
            String mySelectQuery = "SELECT S.[ID], S.[sectionName] FROM Sections S ORDER BY [sectionName] ASC";
            OleDbCommand myCommand = new OleDbCommand(mySelectQuery, connection);
            this.open();
            OleDbDataReader myReader = myCommand.ExecuteReader();
            List<Section> sections = new List<Section>();
            sections.Add(new Section(-1, "No Section"));
            try
            {
                while (myReader.Read())
                {
                    sections.Add(new Section(myReader.GetInt32(0), myReader.GetString(1)));
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
            return sections;
        }


        // QUERIES ON THE STUDENT TABLE
        public List<Student> getAllStudents()
        {
            String mySelectQuery = "SELECT S.[ID], S.[username], S.[fullName], S.[firstName], S.[lastName], S.[Section], S.[isEnrolled] FROM Students S ORDER BY S.[fullName]";
            this.open();
            OleDbCommand myCommand = new OleDbCommand(mySelectQuery, connection);
            OleDbDataReader myReader = myCommand.ExecuteReader();
            List<Student> students = new List<Student>();
            try
            {
                while (myReader.Read())
                {
                    if (myReader.IsDBNull(5))
                    {
                        students.Add(new Student(myReader.GetInt32(0), myReader.GetString(1), myReader.GetString(2),
                            myReader.GetString(3), myReader.GetString(4), -1,
                            myReader.GetBoolean(6)));
                    }
                    else
                    {
                        students.Add(new Student(myReader.GetInt32(0), myReader.GetString(1), myReader.GetString(2),
                            myReader.GetString(3), myReader.GetString(4), myReader.GetInt32(5),
                            myReader.GetBoolean(6)));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
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
                    if (myReader.IsDBNull(5))
                    {
                        student = new Student(myReader.GetInt32(0), myReader.GetString(1), myReader.GetString(2),
                            myReader.GetString(3), myReader.GetString(4), -1,
                            myReader.GetBoolean(6));
                    }
                    else
                    {
                        student = new Student(myReader.GetInt32(0), myReader.GetString(1), myReader.GetString(2),
                            myReader.GetString(3), myReader.GetString(4), myReader.GetInt32(5),
                            myReader.GetBoolean(6));
                    }
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
                    cmdInsert.Parameters.AddWithValue("@parm2", s.FullName);
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
        public Boolean updateStudentSection(int section, int studentId)
        {
            if (section == -1)
            {
                this.open();
                string query = "UPDATE Students AS S SET S.Section = Null WHERE S.ID = @parm2;";
                int status;
                OleDbCommand cmdInsert = new OleDbCommand(query, connection);
                cmdInsert.Parameters.Clear();
                try
                {
                    cmdInsert.CommandType = System.Data.CommandType.Text; //Type of query
                    //Add parameters to the query
                    cmdInsert.Parameters.AddWithValue("@parm2", studentId);
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
            }
            else
            {
                this.open();
                string query = "UPDATE Students AS S SET S.Section = @parm1 WHERE S.ID = @parm2;";
                int status;
                OleDbCommand cmdInsert = new OleDbCommand(query, connection);
                cmdInsert.Parameters.Clear();
                try
                {
                    cmdInsert.CommandType = System.Data.CommandType.Text; //Type of query
                    //Add parameters to the query
                    cmdInsert.Parameters.AddWithValue("@parm1", section);
                    cmdInsert.Parameters.AddWithValue("@parm2", studentId);

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
            }
            return true;
        }
        public Boolean updateStudentSetEnrolled(Boolean val, int studentId)
        {
            this.open();
            string query = "UPDATE Students AS S SET S.isEnrolled = @parm1 WHERE S.ID = @parm2;";
            int status;
            OleDbCommand cmdInsert = new OleDbCommand(query, connection);
            cmdInsert.Parameters.Clear();
            try
            {
                cmdInsert.CommandType = System.Data.CommandType.Text; //Type of query
                //Add parameters to the query
                if (val == true)
                {
                    cmdInsert.Parameters.AddWithValue("@parm1", true);
                }
                else
                {
                    cmdInsert.Parameters.AddWithValue("@parm1", false);
                }
                cmdInsert.Parameters.AddWithValue("@parm2", studentId);

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


        // QUERIES ON THE FILE TABLE
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


        // QUERIES ON THE CLASSDATE TABLE
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


        // QUERIES ON THE PANELS TABLE
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
        public List<DisplayPanelInfo> getPanelsForStudent(int studentid)
        {
            String mySelectQuery = "SELECT Classdates.classDate, Files.fileName, Panels.slideNumber, Panels.totalStrokeCount, Panels.netStrokeCount, Panels.isBlank, Panels.analysis FROM (Classdates INNER JOIN Files ON Classdates.ID = Files.Classdate) INNER JOIN (Students INNER JOIN Panels ON Students.username = Panels.username) ON Files.ID = Panels.File WHERE (((Students.ID)= @parm1)) ORDER BY Classdates.classDate DESC;";
            OleDbCommand myCommand = new OleDbCommand(mySelectQuery, connection);
            myCommand.Parameters.AddWithValue("@parm1", studentid);
            this.open();
            OleDbDataReader myReader = myCommand.ExecuteReader();
            List<DisplayPanelInfo> panelinfo = new List<DisplayPanelInfo>();
            try
            {
                while (myReader.Read())
                {
                    panelinfo.Add(new DisplayPanelInfo(myReader.GetDateTime(0), myReader.GetString(1),
                        myReader.GetInt32(2), myReader.GetInt32(3), myReader.GetInt32(4),
                        myReader.GetBoolean(5), myReader.GetString(6)));
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
            return panelinfo;
        }

        // QUERIES ON THE EXCEPTIONS TABLE
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
        public List<DisplayExceptionInfo> getExceptionsForStudent(int studentid)
        {
            String mySelectQuery = "SELECT Classdates.classDate, Reasons.credit, Reasons.description, Exceptions.notes FROM Classdates INNER JOIN (Reasons INNER JOIN (Students INNER JOIN Exceptions ON Students.ID = Exceptions.Student) ON Reasons.ID = Exceptions.Reason) ON Classdates.ID = Exceptions.Classdate WHERE Students.ID = @parm1 ORDER BY Classdates.classDate DESC;";
            OleDbCommand myCommand = new OleDbCommand(mySelectQuery, connection);
            myCommand.Parameters.AddWithValue("@parm1", studentid);
            this.open();
            OleDbDataReader myReader = myCommand.ExecuteReader();
            List<DisplayExceptionInfo> exceptionlist = new List<DisplayExceptionInfo>();
            try
            {
                while (myReader.Read())
                {
                    exceptionlist.Add(new DisplayExceptionInfo(myReader.GetDateTime(0), myReader.GetBoolean(1),
                        myReader.GetString(2), myReader.GetString(3)));
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
            return exceptionlist;
        }


        // QUERIES ON THE REASON TABLE
        public List<Reason> getReasons()
        {
            String mySelectQuery = "SELECT R.[ID], R.[credit], R.[description] FROM Reasons R ORDER BY [ID] ASC";
            OleDbCommand myCommand = new OleDbCommand(mySelectQuery, connection);
            this.open();
            OleDbDataReader myReader = myCommand.ExecuteReader();
            List<Reason> reasons = new List<Reason>();
            try
            {
                while (myReader.Read())
                {
                    reasons.Add(new Reason(myReader.GetInt32(0), myReader.GetBoolean(1),
                        myReader.GetString(2)));
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
            return reasons;
        }


    }
}
