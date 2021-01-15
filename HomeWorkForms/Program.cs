using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;


namespace HomeWorkForms
{

    class MainClass
    {

        [STAThread]
        public static void Main()
        {
            Console.WriteLine("CurrentCulture is {0}.", CultureInfo.CurrentCulture.Name);
            Application.Run(new Form2());
        }

    }
        public class Form2 : Form{

        public Form2(){
               ClientSize = new Size(200, 100);
                StartPosition = FormStartPosition.CenterScreen;

            Label label = new Label
            {
                Location = new Point(50,5)
            };
            TextBox text = new TextBox
            {
                Location = new Point(50,25)
            };

            text.KeyUp += ( object sender, KeyEventArgs e) =>
             {
                 if (e.KeyCode == Keys.Enter)
                 {
                     try
                     {
                        
                         DateTime dt = DateTime.Parse(text.Text, CultureInfo.CurrentCulture);
                         label.Text = dt.DayOfWeek.ToString();
                         e.Handled = true;
                     }
                     catch
                     {
                         MessageBox.Show("Неверный формат даты");
                     }
                 }
             };
            Controls.Add(label); Controls.Add(text);
        }

    }

}
