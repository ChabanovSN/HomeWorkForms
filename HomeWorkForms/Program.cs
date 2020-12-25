using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using System.Data;
using System.Diagnostics;

namespace HomeWorkForms
{
    /*
    Создайте базу планировщик и в приложении, через меню реализуйте возможность 
просмотра списка дел на текущую дату, на неделю, добавление/удаление задач
    */
    class MainClass
    {
          private static readonly string StrConnMaster = ConfigurationManager.ConnectionStrings["master"].ConnectionString;
          private static readonly string Script1 = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "script1.sql"));
        private static readonly string Script2 = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "script2.sql"));
        private static void ExecuteCommand(string Script, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(Script, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                    
        }

        public static void Main()
        {
           ExecuteCommand(Script1, StrConnMaster);
            ExecuteCommand(Script2, StrConnMaster);
            Application.Run(new Form2());
        }


        public class Form2 : Form
        {
            private SqlConnection conn = null;
            SqlDataAdapter da = null;
            DataSet set = null;
            SqlCommandBuilder cmd = null;
            private readonly string StrConnScheduler;
            private string nameTable = "TaskList";
            private DataGridView dataGridView1;
            private Button fillBtn, updateBtn;
       
            private Label LabelStart, LabelEnd;
            private DateTimePicker dateStart, dateEnd;
            private  string SQLSelect = "SELECT * FROM TaskList;";
              public Form2()
            {
                StrConnScheduler = ConfigurationManager.ConnectionStrings["Scheduler"].ConnectionString;
                conn = new SqlConnection(StrConnScheduler);
                Init();
               

            }

            void FillBtn_Click(object sender, EventArgs e)
            {
                try
                {
                  

                    set = new DataSet();                   
                    da = new SqlDataAdapter(SQLSelect, conn);


                    cmd = new SqlCommandBuilder(da);  
                    if(dateStart.Value.Date < dateEnd.Value.Date)                 
                          da.SelectCommand = SelectCMD(dateStart.Value.Date, dateEnd.Value.Date);
                    da.Fill(set, nameTable);
                    dataGridView1.DataSource = set.Tables[nameTable];
                    dataGridView1.Columns[0].MinimumWidth = 2;
                    dataGridView1.Columns[0].Width = 2;
                    dataGridView1.Columns[1].Width = 255;
                    dataGridView1.Columns[2].Width = 102;


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                }
            }

           
            void UpdateBtn_Click(object sender, EventArgs e)
            {
                try
                {
                           da.Update(set, nameTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                }


            }
             
            private SqlCommand InserComd()
            {

             SqlCommand InserCMD = new SqlCommand(" INSERT INTO TaskList VALUES (@pName,@pDate)", conn);

            InserCMD.Parameters.Add(new SqlParameter("@pName", SqlDbType.NVarChar));
            InserCMD.Parameters["@pName"].SourceVersion =DataRowVersion.Current;
            InserCMD.Parameters["@pName"].SourceColumn ="Name";

                InserCMD.Parameters.Add(new SqlParameter("@pDate", SqlDbType.Date));
                InserCMD.Parameters["@pDate"].SourceVersion = DataRowVersion.Current;
                InserCMD.Parameters["@pDate"].SourceColumn = "Date";
                return InserCMD;
            }
            private SqlCommand DeleteEmptyComd()
            {

                SqlCommand DEeleteCMD = new SqlCommand("DELETE FROM TaskList WHERE (Name IS NULL) OR (Date IS NULL)", conn);
                return DEeleteCMD;
            }

            private SqlCommand SelectCMD( DateTime start,DateTime end)
            {
                Console.WriteLine("SelectCMD");

                Console.WriteLine(start);
                Console.WriteLine(end);

                SqlCommand cmdSelect = new SqlCommand("SELECT * FROM TaskList WHERE Date>= @pStart AND Date <= @pEnd", conn);
                cmdSelect.Parameters.Add("@pStart", SqlDbType.DateTime).Value=start;
                cmdSelect.Parameters.Add("@pEnd", SqlDbType.DateTime).Value = end;
                Console.WriteLine(cmdSelect.CommandText);

                return cmdSelect;
            }


            void Init()
            {
                ClientSize = new Size(420, 410);
                StartPosition = FormStartPosition.CenterScreen;
                MaximizeBox = false;
                MinimizeBox = false;
                LabelStart = new Label
                {
                    Location = new Point(10, 5),
                    Text = "От",
                    Width = 20
                };
                dateStart = new DateTimePicker
                {
                    Location = new Point(30, 5),
                    Width = 160,
                    Name ="Start"
                };
                LabelEnd = new Label
                {
                    Location = new Point(230, 5),
                    Text = "До",
                    Width = 20
                };
                dateEnd = new DateTimePicker
                {
                    Location = new Point(250, 5),
                    Width = 160,
                    Name = "End"
                };

                fillBtn = new Button
                {
                    Location = new Point(10, 35),
                    Text = "Искать"
                };
                fillBtn.Click += FillBtn_Click;
                updateBtn = new Button
                {
                    Location = new Point(333, 35),
                    Text = "Обновить"
                };
                updateBtn.Click += UpdateBtn_Click;
                dataGridView1 = new DataGridView
                {
                    Location = new Point(10, 60),
                    Width = 400,
                    Height= 340,
                    
                 
            };
               
                Controls.AddRange(new Control[] {LabelStart,dateStart,LabelEnd,dateEnd, dataGridView1, fillBtn, updateBtn,});

            }

        }
    }
}
