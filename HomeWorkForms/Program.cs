using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Text;

namespace HomeWorkForms
{

    class MainClass
    {

        [STAThread]
        public static void Main()
        {

            Application.Run(new Form1());
        }

    }
    public class Form1 : Form
    {
        RichTextBox textBox;

        public Form1()
        {
           
       
            Init();
        }

       public void setText(string text)
        {
            textBox.Rtf = text;
        }
        void Init()
        {

            ClientSize = new Size(600, 400);
            StartPosition = FormStartPosition.CenterScreen;

            textBox = new RichTextBox
            {
                Location = new Point(10, 50),
                Multiline = true,
                Width = 580,
                Height = 340,
                Enabled = false
            };
            Button loadBTn = new Button
            {
                Location = new Point(10, 20),
                Text = "загрузить файл",
                Width = 100
            };
            Button editBTn = new Button
            {
                Location = new Point(140, 20),
                Text = "редактировать",
                Width = 100,
                Enabled = false
            };

            loadBTn.Click += (s, e) =>
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                    openFileDialog.FilterIndex = 2;
                    openFileDialog.RestoreDirectory = true;
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        textBox.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.RichText);
                        editBTn.Enabled = true;
                    }
                }
            };

            editBTn.Click += (s, e) => {
                Form2 form2 = new Form2();
                form2.setText(textBox.Rtf);
               this.Hide();
                form2.Show();
            
            };
                Resize += (s, e) => {
                textBox.Height = this.Height - 100;
                textBox.Width = this.Width - 20;

            };
            Controls.AddRange(new Control[] { textBox, loadBTn , editBTn });

        }

    }
    public class Form2 : Form
    {
        RichTextBox textBox;
        public Form2()
        {


            Init();
        }

       public void setText(string text)
        {
            textBox.Rtf = text;
        }
        void Init()
        {

            ClientSize = new Size(600, 400);
            StartPosition = FormStartPosition.CenterScreen;

              textBox = new RichTextBox
            {
                Location = new Point(10, 50),
                Multiline = true,
                Width = 580,
                Height = 340,
               
            };
            Button saveBTn = new Button
            {
                Location = new Point(10, 20),
                Text = "Сохранить",
                Width = 100
            };
            Button cancelBTn = new Button
            {
                Location = new Point(140, 20),
                Text = "Отменить",
                Width = 100,
               
            };

            saveBTn.Click += (s, e) =>
            {
                Form1 ifrm = (Form1)Application.OpenForms[0];
                if (ifrm is Form1 form1)
                {
                    form1.setText(textBox.Rtf);
                    form1.Show();
                    this.Close();
                }
            };
            Resize += (s, e) => {
                textBox.Height = this.Height - 100;
                textBox.Width = this.Width - 20;

            };
            cancelBTn.Click += (s, e) => {

                Form1 ifrm = (Form1)Application.OpenForms[0];
                if (ifrm is Form1 form1)
                {
                  
                    form1.Show();
                    this.Close();
                }
            };
            Controls.AddRange(new Control[] { textBox, saveBTn, cancelBTn });

        }

    }
}
