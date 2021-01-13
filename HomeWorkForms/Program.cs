using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


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
        public class Form2 : Form{
        List<Rectangle> list;
        List<Color> colors;
        int x1, x2, y1, y2;
        SolidBrush myBrush = new SolidBrush(Color.Red);
        Random random = new Random();

        public Form2(){
            list = new List<Rectangle>();
            colors = new List<Color>();          
                MinimumSize = new Size(600, 600);
                StartPosition = FormStartPosition.CenterScreen;
                ResizeRedraw = true;         
              
               
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            // base.OnMouseDoubleClick(e);
            if (e.Button == MouseButtons.Left)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Contains(e.Location))
                    {
                        list.RemoveAt(i);
                        colors.RemoveAt(i);
                        Invalidate();
                        break;
                    }

                }
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
           if (e.Button == MouseButtons.Left)
            {
               x1 = e.Location.X;
               y1 = e.Location.Y;               
            }
                    
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                x2 = e.Location.X;
                y2 = e.Location.Y;
               
                if (x2 - x1 < 10 && y1 - y2 < 10 )
                {

                     if (x2 != x1)
                    MessageBox.Show("Объект должен буть минимум 10 Х 10");
                }
                else
                {

                    if (x2 !=x1 && y1 != y2){
                    list.Add(new Rectangle(x1, y1, x2 - x1, y2 - y1));
                    colors.Add(Color.FromArgb(random.Next(0, 255),
                              random.Next(0, 255), random.Next(0, 255)));
                    x1 = x2 = y1 = y2 = 0;
                    Invalidate();
                    }
                }

            }
            if (e.Button == MouseButtons.Right)
            {
               
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (list[i].Contains(e.Location))
                    {
                      double sq =  (double)list[i].Width * list[i].Height / 1000;
             Text = $"( x1={list[i].Left} y1={list[i].Top}; x2={list[i].Right} y2={list[i].Bottom}) площадь= {sq} кв.см";
                        break;
                    }
                }

            }

        }
      
       
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;               
                for (int i = 0; i < list.Count; i++)
                {
                    myBrush.Color = colors[i];
                    g.FillRectangle(myBrush, list[i]);
                }

          }

    }

}
