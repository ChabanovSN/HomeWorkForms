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

              public Form2()
            {
                ClientSize = new Size(420, 60);
                StartPosition = FormStartPosition.CenterScreen;
                MaximizeBox = false; MinimizeBox = false;
             
                TrackBar trackBar1 =  new TrackBar
                {
                  Location = new Point(8, 8),
                  Size = new Size(400, 45),
                      Maximum = 200,
                    TickFrequency = 5
                };

                System.Timers.Timer timer = new System.Timers.Timer(1000);
                timer.Elapsed += (sender, e) => {
                      if(trackBar1.Value>0)
                            trackBar1.Value -= 1;
                    Text = trackBar1.Value.ToString();
                             
                };
                trackBar1.Scroll += (sender, e) => {
                    timer.Start();
                };

                Controls.AddRange(new Control[] { trackBar1 });

                }

        }

}
