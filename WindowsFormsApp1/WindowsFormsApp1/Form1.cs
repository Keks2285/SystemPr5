using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
//Не забудьте подключить 

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        byte[] buffer={0};
        bool ArduinoConnected = false;
        public Form1()
        {
            InitializeComponent();
           

        }
        
        private void button1_Click(object sender, EventArgs e) //найти
        {
            comboBox1.Items.Clear();
            //Получаем список COM портов доступных в системе
            string[] portnames = SerialPort.GetPortNames();
            //Проверяем, есть ли доступные
            if (portnames.Length == 0)
            {
                MessageBox.Show("Com-port Arduino не найден");
            }
           
            foreach (string portname in portnames)
            {
                //Добавляем доступные COM порты в список
                comboBox1.Items.Add(portname);
                Console.WriteLine(portnames.Length);
                if (portnames[0] != null)
                {
                    comboBox1.SelectedItem = portnames[0];
                }
            }
        }

        private void connectToArduino()
        {
            ArduinoConnected = true;
            string selectedPort = comboBox1.GetItemText(comboBox1.SelectedItem);
            serialPort1.PortName = selectedPort;
            serialPort1.Open();
        }


        private void disconnectFromArduino()
        {
            ArduinoConnected = false;
            serialPort1.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // При закрытии программы, закрываем порт
            if (serialPort1.IsOpen) serialPort1.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (ArduinoConnected)
            {
                serialPort1.Write("1"); //выключить п1
            }
        }

        private void button4_Click(object sender, EventArgs e) // включить п1
        {
            if (ArduinoConnected)
            {
                serialPort1.Write("0");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!ArduinoConnected)
            {
                connectToArduino();
                button2.BackColor = Color.Green;
                button2.Text = "Arduino подключен";
            }
            else
            {
                disconnectFromArduino();
                button2.BackColor = Color.Red;
                button2.Text = "Arduino отключен";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (ArduinoConnected)
            {
                serialPort1.Write("10"); // включить п2
            }
        }

        private void button6_Click(object sender, EventArgs e) // выключить п2
        {
            if (ArduinoConnected)
            {
                serialPort1.Write("11");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (ArduinoConnected)
            {
                serialPort1.Write("110");
            }
        }

        public void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string a = serialPort1.ReadLine();
            if (a!=null)
            {
                DialogResult result = MessageBox.Show
                    ("Кажется у нас гости",
                      "Включить сигнализцию?",
                      MessageBoxButtons.YesNo,
                      MessageBoxIcon.Question,
                      MessageBoxDefaultButton.Button1,
                      MessageBoxOptions.DefaultDesktopOnly
                      );

                if (result == DialogResult.Yes)
                {
                    if (ArduinoConnected)
                    {
                        serialPort1.Write("111");
                    }
                }

            }
        }
    }
}
