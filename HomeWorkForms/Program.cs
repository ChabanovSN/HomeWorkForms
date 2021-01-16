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
           
            Application.Run(new Form2());
        }

    }
    public class Form2 : Form
    {
        private double daySum = 0;
        Timer myTimer = new Timer();
        bool newClient = false;
        public Form2()
        {
            myTimer.Interval = 10000;
            Init();
                

        }
        private static void TimerEventProcessor(Object myObject,EventArgs myEventArgs)
        {

        }
        private void Init() {
            Font = new Font(Font.Name, 10.0F);
            Text = "Best Oil";

            Size size = new Size(605, 600);
            ClientSize = size;
            StartPosition = FormStartPosition.CenterScreen;
            MaximumSize = size; MinimumSize = size;
            ///////OIL///////////

            double[] priceOil = { 82.2, 95.4, 75.99 };

            GroupBox groupOil = new GroupBox
            {
                Text = "Автозаправка",
                Location = new Point(10, 20),
                Size = new Size(280, 340)
            };

            Label labelFuel = new Label
            {
                Text = "Топливо",
                Location = new Point(25, 40)
            };
            ComboBox comboBoxOil = new ComboBox {
                Location = new Point(120, 40),

            };

            comboBoxOil.Items.AddRange(new object[] { "Аи-92", "Аи-95", "Дизель", });
            comboBoxOil.SelectedIndex = 1;




            Label labelPriceTitle = new Label
            {
                Text = "Цена",
                Location = new Point(25, 80),
                Width = 80
            };
            Label labelPrice = new Label
            {
                Text = priceOil[comboBoxOil.SelectedIndex].ToString(),
                Location = new Point(120, 80),
                Width = 40
            };
            comboBoxOil.SelectedIndexChanged += (object sender, EventArgs e) => {
                labelPrice.Text = priceOil[comboBoxOil.SelectedIndex].ToString();
            };
            Label labelRub1 = new Label
            {
                Text = "руб.",
                Location = new Point(240, 80),
                Width = 30
            };

            RadioButton quantuty = new RadioButton {
                Checked = true,
                Text = "Кол-во",
                Location = new Point(5, 20),
                Width = 70
            };

            RadioButton money = new RadioButton {
                Text = "Сумма",
                Location = new Point(5, 50),
                Width = 70
            };
            TextBox textLiters = new TextBox
            {
                Location = new Point(140, 140),
                Width = 70
            };
            Label labelLitr = new Label
            {
                Text = "лтр.",
                Location = new Point(240, 140),
                Width = 30
            };

            TextBox textSum = new TextBox
            {
                Location = new Point(140, 170),
                Width = 70,
                Enabled = false
            };
            Label labelRub2 = new Label
            {
                Text = "руб.",
                Location = new Point(240, 170),
                Width = 30
            };
            quantuty.CheckedChanged += (s, e) => {
                textSum.Text = textLiters.Text = "";
                textSum.Enabled = false;
                textLiters.Enabled = true;
            };
           money.CheckedChanged += (s, e) => {
               textSum.Text = textLiters.Text = "";
               textSum.Enabled = true;
                textLiters.Enabled =false;
            };
            GroupBox groupSumKol = new GroupBox
            {
                Location = new Point(25,120),
                Size = new Size(100,80)
            };

            groupSumKol.Controls.AddRange(new Control[] {quantuty,money });

            Label labelSumOIl = new Label
            {
                Font = new Font(Font.Name, 16.0F),
                Location = new Point(100,40)
        };
            Label labelRub3 = new Label
            {
                Text = "руб.",
                Location = new Point(180, 60),
                Width = 40
            };
            textLiters.KeyUp += (object sender, KeyEventArgs e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    try
                    {
                        double.TryParse(textLiters.Text, out double litter);
                        double sum = litter * priceOil[comboBoxOil.SelectedIndex];
                        labelSumOIl.Text = sum.ToString("0.##");

                    }
                    catch
                    {
                        MessageBox.Show("Ошибка формата ввода");
                    }
                }
            };
            textSum.KeyUp += (object sender, KeyEventArgs e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    try
                    {
                        double.TryParse(textSum.Text, out double sum);
                        double litters = sum/ priceOil[comboBoxOil.SelectedIndex];
                        labelSumOIl.Text = sum.ToString("0.##");
                        textLiters.Text = litters.ToString("0.##");
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка формата ввода");
                    }
                }
            };
            GroupBox groupAllSumOil = new GroupBox
            {
                Location = new Point(25, 220),
                Size = new Size(240, 90),
                Text = "К оплате"
            };
            groupAllSumOil.Controls.AddRange(new Control[] { labelSumOIl, labelRub3 });
            groupOil.Controls.AddRange(new Control[] { labelFuel, comboBoxOil, labelPriceTitle, labelPrice, labelRub1,
                groupSumKol,  groupAllSumOil,textLiters,textSum,labelRub1,labelRub2,labelLitr });



            /////////////COFE////////////////
            /// 
            double[] priceFood = { 40.0, 50.4, 75.2,40.4 };
            Label labelPriceFood = new Label
            {
                Location = new Point(130,40),
                Text ="Цена",
                Width = 50
            };
            Label labelKolFood = new Label
            {
                Location = new Point(190, 40),
                Text = "Количество",
                Width = 85
            };
            CheckBox checkBoxHotDog = new CheckBox
            {
                Location = new Point(10, 70),
                Text = "Хот-дог"
            };
            TextBox textHotDogPrice = new TextBox
            {
                Text = priceFood[0].ToString("0.##"),
                Enabled = false,
                Location = new Point(120, 70),
                Width = 50
            };
            TextBox textHotDogKol = new TextBox
            {
                Name = "Kol1",
                Enabled = false,
                Location = new Point(190, 70),
                Width = 85
            };
           

            CheckBox checkBoxHumberg = new CheckBox
            {
                Location = new Point(10, 100),
                Text = "Гамбургер"
            };
            TextBox textHumbergPrice = new TextBox
            {
                Text = priceFood[1].ToString("0.##"),
                Enabled = false,
                Location = new Point(120, 100),
                Width = 50
            };
            TextBox textHumbergKol = new TextBox
            {
                Name = "Kol2",
                Enabled = false,
                Location = new Point(190, 100),
                Width = 85
            };


            CheckBox checkBoxPatetto = new CheckBox
            {
                Location = new Point(10, 130),
                Text = "Фри"
            };
            TextBox textPatettoPrice = new TextBox
            {
                Text = priceFood[2].ToString("0.##"),
                Enabled = false,
                Location = new Point(120, 130),
                Width = 50
            };
            TextBox textPatettoKol = new TextBox
            {
                Name = "Kol3",
                Enabled = false,
                Location = new Point(190, 130),
                Width = 85
            };



            CheckBox checkBoxKola = new CheckBox
            {
                Location = new Point(10, 160),
                Text = "Кока-кола"
            };
            TextBox textKolaPrice = new TextBox
            {
                Text = priceFood[3].ToString("0.##"),
                Enabled = false,
                Location = new Point(120, 160),
                Width = 50
            };
            TextBox textKolaKol = new TextBox
            {
                Name = "Kol4",
                Enabled = false,
                Location = new Point(190, 160),
                Width = 85
            };
           


            Label labelSumFood = new Label
            {
                Font = new Font(Font.Name, 16.0F),
                Location = new Point(100, 40)
            };
            Label labelRub4 = new Label
            {
                Text = "руб.",
                Location = new Point(180, 60),
                Width = 40
            };
            GroupBox groupAllSumFood = new GroupBox
            {
                Location = new Point(25, 220),
                Size = new Size(240, 90),
                Text = "К оплате"
            };

           


            groupAllSumFood.Controls.AddRange(new Control[] { labelSumFood , labelRub4 });

            GroupBox groupCofe = new GroupBox
            {
                Text = "Мини-Кафе",
                Location = new Point(308, 20),
                Size = new Size(280, 340)
            };
            checkBoxHotDog.CheckedChanged += (s, e) => {
                if (checkBoxHotDog.Checked)
                    textHotDogKol.Enabled = true;
                else
                {
                    textHotDogKol.Enabled = false;
                    textHotDogKol.Text = "";
                }

                labelSumFood.Text = countSumFood(groupCofe, priceFood);
            };
            textHotDogKol.KeyUp += (object sender, KeyEventArgs e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    labelSumFood.Text = countSumFood(groupCofe, priceFood);
                }
            };

        
           

            checkBoxHumberg.CheckedChanged += (s, e) => {
                if (checkBoxHumberg.Checked)
                    textHumbergKol.Enabled = true;
                else
                {
                    textHumbergKol.Enabled = false;
                    textHumbergKol.Text = "";
                }
                labelSumFood.Text = countSumFood(groupCofe, priceFood);
            };
            textHumbergKol.KeyUp += (object sender, KeyEventArgs e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    labelSumFood.Text = countSumFood(groupCofe, priceFood);
                }
            };
            checkBoxPatetto.CheckedChanged += (s, e) => {
                if (checkBoxPatetto.Checked)
                    textPatettoKol.Enabled = true;
                else
                {
                    textPatettoKol.Enabled = false;
                    textPatettoKol.Text = "";
                }
                labelSumFood.Text = countSumFood(groupCofe, priceFood);
            };
            textPatettoKol.KeyUp += (object sender, KeyEventArgs e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    labelSumFood.Text = countSumFood(groupCofe, priceFood);
                }
            };
            checkBoxKola.CheckedChanged += (s, e) => {
                if (checkBoxKola.Checked)
                    textKolaKol.Enabled = true;
                else
                {
                    textKolaKol.Enabled = false;
                    textKolaKol.Text = "";
                }
                labelSumFood.Text = countSumFood(groupCofe, priceFood);
            };
            textKolaKol.KeyUp += (object sender, KeyEventArgs e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    labelSumFood.Text = countSumFood(groupCofe, priceFood);
                }
            };
            groupCofe.Controls.AddRange(new Control[] {labelPriceFood,labelKolFood,
                checkBoxHotDog,textHotDogPrice,textHotDogKol,
                    checkBoxHumberg,textHumbergPrice,textHumbergKol,
                        checkBoxPatetto,textPatettoPrice,textPatettoKol,
                            checkBoxKola,textKolaPrice,textKolaKol,groupAllSumFood });

            ///////COMMON/////
            /// 
            Button OilFoodSumBtn = new Button {

                Text = "ВСЕГО",
                Location = new Point(110,30),             
                Size = new Size(120,50)
                 };
        
            Label labelSumAll = new Label
            {
                Font = new Font(Font.Name, 16.0F),
                Location = new Point(300, 90)
            };
            Label labelRub5 = new Label
            {
                Text = "руб.",
                Location = new Point(400, 100),
                Width = 40
            };
            OilFoodSumBtn.Click += (s, e) => {

                double.TryParse(labelSumOIl.Text, out double oil);
                double.TryParse(labelSumFood.Text, out double food);
                labelSumAll.Text = (oil + food).ToString("0.##");
                daySum += oil + food;
                Text = $"Best Oil. Выручка за день = {daySum} руб.";
                newClient = true;
                myTimer.Start();
                
            };
            GroupBox groupCommon = new GroupBox
            {
                Text = "Всего к оплате",
                Location = new Point(10, 380),
                Size = new Size(580, 150)
            };
            groupCommon.Controls.AddRange(new Control[] { OilFoodSumBtn, labelSumAll, labelRub5 });
            //
            Controls.AddRange(new Control[] { groupOil,groupCofe,groupCommon });
            myTimer.Tick += (s, e) => {
                if (newClient)
                {
                    myTimer.Stop();
                    if (MessageBox.Show("Новый клиент?", "Обновить форму ",
                       MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        myTimer.Enabled = true;
                        checkBoxKola.Checked =
                        checkBoxHotDog.Checked =
                        checkBoxHumberg.Checked =
                        checkBoxPatetto.Checked = false;
                        comboBoxOil.SelectedIndex = 1;
                        textLiters.Text = textLiters.Text 
                        =labelSumAll.Text= labelSumOIl.Text = "";
                        newClient = false;
                    }
                    else
                    {
                        myTimer.Start();
                    }

                }
            };
        }

        private string countSumFood(GroupBox groupBox, double[] prices) {

            TextBox[] texts = groupBox.Controls.OfType<TextBox>()
            .Where(e => e.Name.StartsWith("Kol", StringComparison.Ordinal))
                .ToArray();
            double totalSum = 0;
            for (int i = 0; i <prices.Length; i++)
            {
                try
                {
                  int kol=0;
                    if (texts[i].Text.Length > 0)
                        int.TryParse(texts[i].Text, out  kol);                   
                    double sum = kol * prices[i];
                    totalSum += sum;                 

                }
                catch
                {
                    MessageBox.Show("Ошибка формата ввода");
                }
            }

            return totalSum.ToString("0.##");

        }


    }
     
}
