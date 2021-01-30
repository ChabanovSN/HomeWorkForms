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
using System.Drawing.Text;
using System.Data.Common;
using MySql.Data.MySqlClient;
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
        DbConnection conn = null;
        DbProviderFactory fact = null;
        DbDataAdapter adapter = null;
        DataTable table = null;
        DataGridView dataGridView1;
        ComboBox comboBox1;
        string SQL = "select * from Saler";
        public Form2()
        {
           
       
            Init();
        }

        void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            foreach (ConnectionStringSettings cs in ConfigurationManager.ConnectionStrings)
            {
                

                if (cs.Name.StartsWith("sales") && cs.ProviderName == comboBox1.SelectedItem.ToString())
                {
                    fact = DbProviderFactories.GetFactory(cs.ProviderName);

               
                    conn = fact.CreateConnection();                  
                  
                    conn.ConnectionString = cs.ConnectionString;
                    adapter = fact.CreateDataAdapter();
                    adapter.SelectCommand = conn.CreateCommand();

                    adapter.SelectCommand.CommandText = SQL;
                    table = new DataTable();
                    adapter.Fill(table);
                    // выводим результаты запроса
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = table;

                    //    Console.WriteLine(cs.ProviderName);
                    break;
                }

            }


        }

        void Init()
        {

            ClientSize = new Size(700, 400);
            StartPosition = FormStartPosition.CenterScreen;
           


            comboBox1 = new ComboBox
            {
                Location = new Point(5, 5),
                Width = 200
            };
            dataGridView1 = new DataGridView
            {
                Location = new Point(5, 55),
                Width = 500
            };
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;

            GroupBox groupBox = new GroupBox
            {
                Text = "Запрос",
                Location = new Point(520, 25),
                Width =120
            };
            RadioButton radioS = new RadioButton
            {
                Text = "Продовец",
                Location = new Point(5, 25)
            };
            radioS.CheckedChanged += (s, e) => {
                SQL = "select * from Saler";
                ComboBox1_SelectedIndexChanged(null, null);
            };
            RadioButton radioB = new RadioButton
            { Checked = true,
                Text = "Покупатель",
                Location = new Point(5, 50)
            };

            radioB.CheckedChanged += (s, e) => {
                SQL = "select * from Buyer";
                ComboBox1_SelectedIndexChanged(null, null);
            };
            RadioButton radioSale = new RadioButton
            {
                Text = "Продажи",
                Location = new Point(5, 75)
            };
            radioSale.CheckedChanged += (s, e) => {
                SQL = @"select b.FirstName, b.LastName, s.FirstName, s.LastName, dil.summa , dil.dateDiler
                 from Saler as s
     join Sale as dil on dil.id_saler = s.Id
     join Buyer as b  on dil.id_buyer = b.Id;";
                ComboBox1_SelectedIndexChanged(null, null);
            };
            groupBox.Controls.AddRange(new Control[] { radioB, radioS, radioSale});
            Controls.AddRange(new Control[] { comboBox1, dataGridView1, groupBox });
            try
            {
               
             
                  DataTable t = DbProviderFactories.GetFactoryClasses();             
                 comboBox1.Items.Clear();
             

            
                foreach (DataRow dr in t.Rows)
                {
                    comboBox1.Items.Add(dr["InvariantName"]);
                }




            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }



        }

    }
}
