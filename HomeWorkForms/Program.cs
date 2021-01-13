using System;
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
        public class Form2 : Form
        {
        Rectangle rctIn; 
        Rectangle rctOut;
        bool triger = false; 
        public Form2()
            {
                ClientSize = new Size(220, 220);
                MinimumSize = new Size(260, 260);
                StartPosition = FormStartPosition.CenterScreen;
            ResizeRedraw = true;
           
               rctIn = new Rectangle(15, 15, 190, 190);          
               rctOut = new Rectangle(10, 10, 200, 200);
              
               
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            int x = e.Location.X;
            int y = e.Location.Y;


            if (e.Button == MouseButtons.Left)
            {
                triger = true;

                if (Control.ModifierKeys == Keys.Control)                
                    System.Environment.Exit(1);
               

                if (rctIn.Contains(e.Location))
                    MessageBox.Show("Внутри");
                else if (rctOut.Contains(e.Location) && !rctIn.Contains(e.Location))
                    MessageBox.Show("На границе");
                else
                    MessageBox.Show("Снаружи");
            }

           if (e.Button == MouseButtons.Right)
            {
                triger = false;
                Text = $"Ширина= {Width} Высота={Height}";
               
            }
         
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
                 if(triger)
                 Text = $" x = {e.X} y= {e.Y}";

        }
        protected override void OnPaint(PaintEventArgs e)
        {
           
            rctIn.Width = this.Width-35;
            rctIn.Height = this.Height-55;
            rctOut.Width = this.Width-25;
            rctOut.Height = this.Height-45;
            Graphics g = e.Graphics;          
       
            using (SolidBrush myBrush = new SolidBrush(Color.Red))
            {
                g.FillRectangle(myBrush, rctOut);
                myBrush.Color = Color.Purple;
                g.FillRectangle(myBrush, rctIn);
          
            }

          
        }

    }

}
