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
        RichTextBox richTextBox1;
        float fontSize = 12;
        public Form2()
        {
            //  FontDialog dialog = new FontDialog();
            //  if (dialog.ShowDialog() != DialogResult.Cancel) { }

            Init();
        }

       

        void Init()
        {
            
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.Bounds = Screen.PrimaryScreen.Bounds;
            Font = new Font(Font.Name, fontSize);
          
           
            //  (цвет шрифта, цвет фона, ШРИФт
           
          
            // Create a MenuStrip control with a new window.
            MenuStrip ms = new MenuStrip();
            ToolStripMenuItem windowMenu = new ToolStripMenuItem("Файл");
            ToolStripMenuItem windowOpenMenu = new ToolStripMenuItem("Открыть");
            windowOpenMenu.Click += (s,e) => {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                    openFileDialog.FilterIndex = 2;
                    openFileDialog.RestoreDirectory = true;
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        richTextBox1.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.RichText);
                        Text = openFileDialog.FileName;
                    }
                }



            };
            ToolStripMenuItem windowSavenMenu = new ToolStripMenuItem("Сохранить");
            windowSavenMenu.Click += (s, e) => {

                using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
                {

                    saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                    saveFileDialog1.FilterIndex = 2;
                    saveFileDialog1.RestoreDirectory = true;

                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.RichText);
                        Text = saveFileDialog1.FileName;

                    }
                }
            };
            windowMenu.DropDownItems.Add(windowOpenMenu);
            windowMenu.DropDownItems.Add(windowSavenMenu);
          
            ToolStripMenuItem SettingsMenu = new ToolStripMenuItem("Настройки");

            ToolStripMenuItem ColorMenu = new ToolStripMenuItem("Цвет");          
            ToolStripMenuItem ColorBackMenu = new ToolStripMenuItem("Фон");
            ToolStripMenuItem FontMenu = new ToolStripMenuItem("Шрифт");
            ToolStripMenuItem SizeMenu = new ToolStripMenuItem("Размер");

            ColorMenu.BackColor = Color.White;
            ColorBackMenu.BackColor = Color.White;
            FontMenu.BackColor = Color.White;
            SizeMenu.BackColor = Color.White;
            SettingsMenu.DropDownItems.Add(ColorMenu);
            SettingsMenu.DropDownItems.Add(ColorBackMenu);
            SettingsMenu.DropDownItems.Add(FontMenu);
            SettingsMenu.DropDownItems.Add(SizeMenu);
           
            ColorList(ColorMenu);
            ColorBackList(ColorBackMenu);
            FontList(FontMenu);
             SizeList(SizeMenu);
            // Add the window ToolStripMenuItem to the MenuStrip.
            ms.Items.Add(windowMenu);
            ms.Items.Add(SettingsMenu);
            // Dock the MenuStrip to the top of the form.
            ms.Dock = DockStyle.Top;

            // The Form.MainMenuStrip property determines the merge target.
            this.MainMenuStrip = ms;
            richTextBox1 = new RichTextBox {
                Height = this.Height -100,
                Width = this.Width-20,
             Location  = new Point(5,60)
            
            };

            Resize += (s,e) => {
                richTextBox1.Height = this.Height - 100;
                richTextBox1.Width = this.Width - 20;
            
            };


            this.Controls.AddRange(new Control[] { ms, richTextBox1 });
           
        }

      private  void ColorList(ToolStripMenuItem menu)
        {
            foreach (Color color in new ColorConverter().GetStandardValues())
            {
                   var tsmItem = new  ToolStripMenuItem
                   {
                       BackColor = Color.FromArgb(color.A, color.R, color.G, color.B),
                       ForeColor = Color.FromArgb(color.A, (byte)~color.R, (byte)~color.G, (byte)~color.B),
                       Text = $"{color.Name}"

                   };
                tsmItem.Click += (s, e) =>
                {
                    richTextBox1.SelectionColor = Color.FromArgb(color.A, color.R, color.G, color.B);
                };
                    menu.DropDownItems.Add(tsmItem);
                                     
                }
        }
        private void ColorBackList(ToolStripMenuItem menu)
        {
            foreach (Color color in new ColorConverter().GetStandardValues())
            {
                var tsmItem = new ToolStripMenuItem
                {
                    BackColor = Color.FromArgb(color.A, color.R, color.G, color.B),
                    ForeColor = Color.FromArgb(color.A, (byte)~color.R, (byte)~color.G, (byte)~color.B),
                    Text = $"{color.Name}"

                };
                tsmItem.Click += (s, e) =>
                {
                    richTextBox1.SelectionBackColor = Color.FromArgb(color.A, color.R, color.G, color.B);
                };
                menu.DropDownItems.Add(tsmItem);

            }
        }
        private void SizeList(ToolStripMenuItem menu)
        {
            for (int i = 5; i < 75; i+=2)
            {

           
                var tsmItem = new ToolStripMenuItem
                {
                    BackColor = Color.White,                   
                    Text = $"{i}"

                };
                tsmItem.Click += (s, e) =>
                {
                   fontSize = float.Parse( tsmItem.Text);
                    richTextBox1.SelectionFont = new Font(Font.Name, fontSize, FontStyle.Regular);
                };
                menu.DropDownItems.Add(tsmItem);

            }
        }
        private void FontList(ToolStripMenuItem menu)
        {

         
            foreach (var font in new InstalledFontCollection().Families)
            {
                FontFamily fontFamily = new FontFamily(font.Name);
                var tsmItem = new ToolStripMenuItem
                {
                    BackColor = Color.White,
                    Font = new Font(fontFamily, 12, FontStyle.Regular, GraphicsUnit.Point),
                    Text = font.Name
                };
                tsmItem.Click += (s, e) =>
                {  
                    richTextBox1.SelectionFont= new Font(font.Name, Font.Size, FontStyle.Regular);
                 
                };
                menu.DropDownItems.Add(tsmItem);

            }
        }
    }
}
