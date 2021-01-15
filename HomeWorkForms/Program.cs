using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;


namespace HomeWorkForms
{

    class MainClass
    {

        [STAThread]
        public static void Main()
        {
            Console.WriteLine("CurrentCulture is {0}.", CultureInfo.CurrentCulture.NativeName);
            Application.Run(new Form2());
        }

    }
    public class Form2 : Form
    {

        public Form2()
        {
            ClientSize = new Size(200, 300);
            StartPosition = FormStartPosition.CenterScreen;

            Label label = new Label
            {
                Location = new Point(50, 5)
            };
            TextBox text = new TextBox
            {
                Location = new Point(50, 25)
            };



            GroupBox groupBox1 = new GroupBox
            {
                Location = new Point(50, 60),
                Size = new Size(100, 190),
                Text = "Интервал"
            };
            RadioButton radioButton1 = new RadioButton
            {
                Location = new Point(5, 20),
                Size = new Size(60, 15),
                Text = "Год"
            };
            RadioButton radioButton2 = new RadioButton
            {
                Location = new Point(5, 50),
                Size = new Size(60, 15),
                Text = "Месяц"
            };
            RadioButton radioButton3 = new RadioButton
            {
                Location = new Point(5, 80),
                Size = new Size(60, 15),
                Text = "День"
            };
            RadioButton radioButton4 = new RadioButton
            {
                Location = new Point(5, 110),
                Size = new Size(60, 15),
                Text = "Час",
                Checked = true
            };
            RadioButton radioButton5 = new RadioButton
            {
                Location = new Point(5, 140),
                Size = new Size(60, 15),
                Text = "Минута"
            };
            RadioButton radioButton6 = new RadioButton
            {
                Location = new Point(5, 170),
                Size = new Size(60, 15),
                Text = "Секунда"
            };


            text.KeyUp += (object sender, KeyEventArgs e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    try
                    {

                        DateTime dt = DateTime.Parse(text.Text, CultureInfo.CurrentCulture);
                        TimeSpan diff1 = dt.Subtract(DateTime.Now);

                        RadioButton radioBtn = groupBox1.Controls
                                              .OfType<RadioButton>()
                                              .FirstOrDefault(x => x.Checked);
                   
                        switch (radioBtn.Text)
                        {
                            case "Год":
                                label.Text = (diff1.Days/ (double)354).ToString("0.##");
                                break;
                            case "Месяц":
                                label.Text = ( diff1.Days/ (double) 30).ToString("0.##");
                                break;
                            case "День":
                                label.Text =  diff1.Days.ToString("0.##");
                                break;
                            case "Час":
                                label.Text = diff1.TotalHours.ToString("0.##");
                                break;
                            case "Минута":
                                label.Text = diff1.TotalMinutes.ToString("0.##");
                                break;
                            case "Секунда":
                                label.Text = diff1.TotalSeconds.ToString("0.##");
                                break;
                            default:
                                label.Text = "Error";
                                break;
                        }

                      e.Handled = true;
                    }
                    catch
                    {
                        MessageBox.Show("Неверный формат даты");
                    }
                }
            };

            groupBox1.Controls.AddRange(new Control[] { radioButton1, radioButton2, radioButton3,
                radioButton4,radioButton5,radioButton6 });
            Controls.AddRange(new Control[] { groupBox1, text, label });

        }

    }

}
