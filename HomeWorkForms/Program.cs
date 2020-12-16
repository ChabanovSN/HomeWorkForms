using System;
using System.Drawing;
using System.Windows.Forms;
namespace HomeWorkForms
{
    class MainClass
    {
        /*
        Задание 1
Вывести на экран свое (краткое!!!) резюме с помощью последовательности 
MessageBox’ов (числом не менее трех). Причем на заголовке
последнего должно отобразиться среднее число символов на странице
(общее количество символов в резюме / количество MessageBox’ов).
        */
        static private string Resume = "Я Сергей Чабанов\n" +
                                       "Доктор по профессии,\n" +
                                        "но в душе программист :-)";
        static void Task1() {
            var arrStrings = Resume.Split('\n');
            for (int i = 0; i < arrStrings.Length; i++)
            {
                if (i < arrStrings.Length - 1)
                    MessageBox.Show(arrStrings[i]);
                else
                   MessageBox.Show(arrStrings[i], $"{Resume.Length/ i}");

            }
        }

        /*
        Задание 2
Написать функцию, которая «угадывает» задуманное пользователем число от 
1 до 2000. Для запроса к пользователю использовать
MessageBox. После того, как число отгадано, необходимо вывести
количество запросов, потребовавшихся для этого, и предоставить
пользователю возможность сыграть еще раз, не выходя из программы
(MessageBox’ы оформляются кнопками и значками соответственно
ситуации).
        */
        static void Task2()
        {
            Random random = new Random();
            int comInt,count = 0;
            int value =1;
            if (InputBox( ref  value) == DialogResult.OK)
            {
                while (true)
                {
                    comInt = random.Next(0, 2000);
                    count++;
                    if (value == comInt)
                    {
                        MessageBox.Show($"Вы загадали {comInt} число попыток {count}");
                        break;
                    }
                    else
                    {
                        if (MessageBox.Show($"Вы загадали {value} комп предположил {comInt}", "Если устали нажмите Cancel",
                              MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                            break;
                    
                    }
                }

                if (MessageBox.Show("Повторить","Игра закончена", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    Task2();
            }
        }
        public static DialogResult InputBox(ref int value)
        {
            Form form = new Form()
            {
         StartPosition = FormStartPosition.CenterScreen,
            MinimizeBox = false,
            MaximizeBox = false,
           Text = "Загадайте число",
           ClientSize = new Size(300, 100)
        };

            Label label = new Label() {
                Location = new Point(10, 10),
                Text = "Введите число от 1 до 2000",
         AutoSize = true
            };

            TextBox textBox = new TextBox() {
                Location = new Point(10, 40),
                Text = value.ToString()
        };

            Button buttonOk = new Button() {
                Location = new Point(10, 70),
                Text = "OK",
                DialogResult = DialogResult.OK
            };

            Button buttonCancel = new Button() {
                Location = new Point(150, 70),
                Text = "Cancel",
                DialogResult = DialogResult.Cancel
            };

   

           
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });

            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            try
            {
                Int32.TryParse(textBox.Text, out int rezult);

              
                if (rezult < 1 || rezult > 2000)
                    throw new Exception();
                else
                    value = rezult;
            
            }catch {
                MessageBox.Show("Нужно указать цифру от 1 до 2000");
                InputBox(ref value);
               }

           
            return dialogResult;
        }
            public static void Main(string[] args)
        {
            Task1();
            Task2();
        }
    }
 }
