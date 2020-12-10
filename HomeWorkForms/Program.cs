using System;
using System.Windows.Forms;
namespace HomeWorkForms
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Application.Run(new Form1());
        }
    }
    public class Form1 : Form
    {
        private int leftBtn, midBtn,rightBtn;

        public Form1() {
            Width = 600;
            Click += Control1_MouseClick;
            ShowCount();
           }
        private void Control1_MouseClick(Object sender,EventArgs e)
        {

            if( e is MouseEventArgs arg) {
                if (arg.Button == MouseButtons.Left)
                    leftBtn++;
               if (arg.Button == MouseButtons.Right)
                    rightBtn++;
                if (arg.Button == MouseButtons.Middle)
                    midBtn++;

                ShowCount();
            }
        }
        private void ShowCount() {
            Text = $"левая кнопка: {leftBtn}, средняя{midBtn}, правая {rightBtn}";
        }


    }
}
