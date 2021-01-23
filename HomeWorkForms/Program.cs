using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace HomeWorkForms
{

    class MainClass
    {

        [STAThread]
        public static void Main()
        {
          
            Application.Run(new Form2());
        }

    }
    public class Form2 : Form
    {
        private SqlConnection conn = null;
        SqlDataAdapter da = null;
        DataSet set = null;
        //SqlCommandBuilder cmd = null;
        string cs = "";
        bool Auth = false;
        private DataGridView dataGridView1;
        private Button fillBtn, updateBtn;
        private GroupBox groupBox;
        private RadioButton showRbtn, editRbtn;
        private ComboBox comboBox;

        public Form2()
        {
            cs = ConfigurationManager.ConnectionStrings["diary"].ConnectionString;
            Init();
        }

        void FillBtn_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(cs);
                set = new DataSet();               
               
                string where = " ";
                 if ( comboBox.SelectedIndex != -1)
                    where = $"where sb.Name='{comboBox.Items[comboBox.SelectedIndex]}'";
                string select = "select sb.Name as Subject ,s.FirstName,s.LastName,l.Estimate as Estimate,l.[Date] " +
                    " from lessons as l " +
                    " JOIN Students as s ON s.Id = l.Student " +
                    " JOIN Subjects as sb ON sb.Id = l.Subject" +
                    $" {where} ;" +
                    "\n select Name from Subjects;" +
                    "\n select FirstName, LastName from Students;";



                da = new SqlDataAdapter(select, conn);
               

                if (Auth)
                {
                    da.UpdateCommand = UpdateCmd();
                    da.DeleteCommand = DeleteCmd();
                    da.InsertCommand = InsetCmd();
                }
                dataGridView1.DataSource = null;
                da.TableMappings.Add("Table", "diary");
                da.TableMappings.Add("Table1", "subjects");            
                da.Fill(set);
                dataGridView1.DataSource = set.Tables["diary"];
              
                PrintValues(set.Tables["subjects"]);
              
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
            }
        }
        private void PrintValues(DataTable table)
        {
            comboBox.Items.Clear();
            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn column in table.Columns)
                {
                    comboBox.Items.Add(row[column]);
                   
                }
            }
          
        }


        void UpdateBtn_Click(object sender, EventArgs e)
        {
            try
            {
               
                 
                da.Update(set, "diary");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
            }


        }

         SqlCommand InsetCmd()
        {
            string sql = @"INSERT into Lessons(Subject,Student,Estimate,[Date]) 
              VALUES ((select TOP (1) Id from Subjects where Name =@Subject),
                     (select TOP (1) Id from Students where FirstName =@FirstName AND LastName = @LastName),
                      @Estimate,@Date)";
            SqlCommand insetCmd = new SqlCommand(sql, conn);

            insetCmd.Parameters.Add(new SqlParameter("@Estimate", SqlDbType.VarChar));
            insetCmd.Parameters["@Estimate"].SourceVersion = DataRowVersion.Current;
            insetCmd.Parameters["@Estimate"].SourceColumn = "Estimate";

            insetCmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.VarChar));
            insetCmd.Parameters["@FirstName"].SourceVersion = DataRowVersion.Current;
            insetCmd.Parameters["@FirstName"].SourceColumn = "FirstName";

            insetCmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.VarChar));
            insetCmd.Parameters["@LastName"].SourceVersion = DataRowVersion.Current;
            insetCmd.Parameters["@LastName"].SourceColumn = "LastName";

            insetCmd.Parameters.Add(new SqlParameter("@Subject", SqlDbType.VarChar));
             insetCmd.Parameters["@Subject"].SourceVersion = DataRowVersion.Current;
            insetCmd.Parameters["@Subject"].SourceColumn = "Subject";

            insetCmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.Date));
            insetCmd.Parameters["@Date"].SourceVersion = DataRowVersion.Original;
            insetCmd.Parameters["@Date"].SourceColumn = "Date";

            //вставляем созданный объект SqlCommand в свойство
            //UpdateCommand SqlDataAdapter
            return insetCmd;
        }
        SqlCommand DeleteCmd()
        {
            string sql = @"DELETE Lessons   where Student =(select TOP (1) Id from Students where FirstName =@FirstName AND LastName = @LastName)                                
               AND Subject = (select TOP (1) Id from Subjects where Name =@Subject) 
                                              AND  Date   = @Date";
            SqlCommand deleteCmd = new SqlCommand(sql, conn);

            deleteCmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.VarChar));
            deleteCmd.Parameters["@FirstName"].SourceVersion = DataRowVersion.Original;
            deleteCmd.Parameters["@FirstName"].SourceColumn = "FirstName";

            deleteCmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.VarChar));
            deleteCmd.Parameters["@LastName"].SourceVersion = DataRowVersion.Original;
            deleteCmd.Parameters["@LastName"].SourceColumn = "LastName";

            deleteCmd.Parameters.Add(new SqlParameter("@Subject", SqlDbType.VarChar));
            deleteCmd.Parameters["@Subject"].SourceVersion = DataRowVersion.Original;
            deleteCmd.Parameters["@Subject"].SourceColumn = "Subject";

            deleteCmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.Date));
            deleteCmd.Parameters["@Date"].SourceVersion = DataRowVersion.Original;
            deleteCmd.Parameters["@Date"].SourceColumn = "Date";

            //вставляем созданный объект SqlCommand в свойство
            //UpdateCommand SqlDataAdapter
            return deleteCmd;

        }

        SqlCommand UpdateCmd()
        {
            string sql = @"Update Lessons set Estimate =@pEstimate  where Student =(select TOP (1) Id from Students where FirstName =@FirstName AND LastName = @LastName)                                
               AND Subject = (select TOP (1) Id from Subjects where Name =@Subject) 
                                              AND  Date   = @Date";
            SqlCommand UpdateCmd = new SqlCommand(sql, conn);
//создаем параметры для запроса Update
            UpdateCmd.Parameters.Add(new SqlParameter("@pEstimate", SqlDbType.VarChar));
            UpdateCmd.Parameters["@pEstimate"].SourceVersion =DataRowVersion.Current;
            UpdateCmd.Parameters["@pEstimate"].SourceColumn = "Estimate";

            UpdateCmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.VarChar));
            UpdateCmd.Parameters["@FirstName"].SourceVersion = DataRowVersion.Original;
            UpdateCmd.Parameters["@FirstName"].SourceColumn = "FirstName";

            UpdateCmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.VarChar));
            UpdateCmd.Parameters["@LastName"].SourceVersion = DataRowVersion.Original;
            UpdateCmd.Parameters["@LastName"].SourceColumn = "LastName";
           
            UpdateCmd.Parameters.Add(new SqlParameter("@Subject", SqlDbType.VarChar));
            UpdateCmd.Parameters["@Subject"].SourceVersion = DataRowVersion.Original;
            UpdateCmd.Parameters["@Subject"].SourceColumn = "Subject";
          
            UpdateCmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.Date));
            UpdateCmd.Parameters["@Date"].SourceVersion = DataRowVersion.Original;
            UpdateCmd.Parameters["@Date"].SourceColumn = "Date";
                      
            //вставляем созданный объект SqlCommand в свойство
            //UpdateCommand SqlDataAdapter
          return UpdateCmd;
        }

       

        void Init()
        {
            ClientSize = new Size(620, 250);
            StartPosition = FormStartPosition.CenterScreen;

         
            fillBtn = new Button
            {
                Location = new Point(10, 35),
                Text = "Искать"
            };
            fillBtn.Click += FillBtn_Click;

            comboBox = new ComboBox
            {
                Location = new Point(90, 35)
            };
            updateBtn = new Button
            {
                Location = new Point(290, 35),
                Text = "Редоктировать",
                Width= 100
            };
            updateBtn.Hide();
            updateBtn.Click += UpdateBtn_Click;

            groupBox = new GroupBox
            {
                Text= "Авторизация",
                Location = new Point(400,10),
                Height = 65

            };
            showRbtn = new RadioButton
            {
                Text= "Просмотр",
                Location = new Point (5,15)
            };
            showRbtn.CheckedChanged += (s, e) =>
             {
                 Auth = false;
                 updateBtn.Hide();
             };
            editRbtn = new RadioButton
            {
                Text = "Правка",
                 Location = new Point(5, 35)
            };
            editRbtn.CheckedChanged += (s, e) =>
            {
                Auth = true;
                updateBtn.Show();
            };
            groupBox.Controls.Add(showRbtn);
            groupBox.Controls.Add(editRbtn);
            dataGridView1 = new DataGridView
            {
                Location = new Point(10, 80),
                Width = 600
            };
     

            Controls.AddRange(new Control[] { dataGridView1, fillBtn, updateBtn, groupBox, comboBox});

        }

    }
}
