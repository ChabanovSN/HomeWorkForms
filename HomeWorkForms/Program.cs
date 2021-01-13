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

        Label label;

        public Form2(){
                MinimumSize = new Size(600, 600);
                StartPosition = FormStartPosition.CenterScreen;
                ResizeRedraw = true;
            label = new Label
            {
                Location = new Point(300,300),
                Text= $"Поймай меня",
                Width =100,
                Height = 20
            };

            Controls.Add(label);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            int lblLeft = label.Left;
            int lblTop = label.Top;
            int xL = label.Location.X;
            int yL = label.Location.Y;
            int xE = e.Location.X;
            int yE = e.Location.Y;

            Console.WriteLine($" left l{xL}  - e{xE} = {xL - xE} y l{ yL}  -e{ yE} = { yL - yE}");
          //  Console.WriteLine($" right {xE - xL} y { yE - yL}");
            if (xL - xE < 10 && xL - xE >= -15 && yL - yE < 5) /// to right
            {
               
                lblLeft += 10;
              
            }
            else if (xL - xE < 5 && yL - yE < 10 && yL - yE >= 10) // to top
            {

                lblTop -= 10;
            }
            else if (xL - xE < 10 && xL - xE >= -15 && yL - yE < 10 && yL - yE >=-10) // to right and to buttom
            {

                lblLeft += 10;
                lblTop += 10;
            }
            //////////////////////////////////////////

            if (xE - xL-100 <10 && xE - xL-100 >= -15 && yL - yE < 5) /// to left
            {
               
                lblLeft -= 10;

            }
            else if (xL - xE < 5 && yL - yE < 10 && yL - yE >= -10) // to buttom
            {
               
              lblTop += 10;
            }
           

            /////////
            if (lblLeft + label.Width > Width) // right side
                {
                    lblLeft -= 20;
                    lblTop += 20;
                }
                if (lblTop + label.Height > Height-10) // buttom side
                {

                    lblTop -= 60;
                    lblLeft -= 50;
               
                }

            if (lblLeft + label.Width <= 100) // right side
            {
                lblLeft = 120;
                lblTop = 20;
            }
            if (lblTop + label.Height <  10) // buttom side
            {

                lblTop = 220;
                lblLeft = 120;

            }



            label.Location = new Point(lblLeft, lblTop);
              //  Update();
            
           // Console.WriteLine( $" btn{lblLeft} : {  lblTop} form {Width} : {Height}");
        }



    }

}
