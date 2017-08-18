using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public string[][] data;
        public int randomNumber;
   
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string filePath = Application.StartupPath+"\\tombola.csv";
            StreamReader sr = new StreamReader(filePath);
            var lines = new List<string[]>();
            int Row = 0;
            while (!sr.EndOfStream)
            {
                string[] Line = sr.ReadLine().Split(',');
                lines.Add(Line);
                Row++;
            }
            data = lines.ToArray();
            labelHeader.Text = "В томболата участват "+data.Length+" потребители на Networx-BG !";

        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            buttonStart.Enabled = false;
            buttonStop.Enabled = true;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (data.Length == 0)
            {
                timer1.Enabled = false;
                buttonStop.Enabled = false;
                MessageBox.Show("Няма повече потребители за изтегляне!");
                return;
            }
            Random random = new Random();
            randomNumber  = random.Next(0, data.Length);
            textUsername.Text = data[randomNumber][0];
            textRealName.Text = data[randomNumber][1];
            int found = 0;
            found = data[randomNumber][2].LastIndexOf("@");
            int zvezdichki = found;
            if (found > 15) zvezdichki = 15;
            if(found>0) {
                textEmail.Text = "***************".Substring(0, zvezdichki) + "@" + data[randomNumber][2].Substring(found + 1);
            }
            else
            {
                textEmail.Text = "";
            }
            if (data[randomNumber][3].Length > 4)
            {
                textPhone.Text = data[randomNumber][3].Substring(0, 3)+"*******";
            }
            else { 
                textPhone.Text = data[randomNumber][3];
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            buttonStart.Enabled = true;
            buttonStop.Enabled = false;

            string[][] dest = new string[data.Length - 1][];
            if (randomNumber > 0)
                Array.Copy(data, 0, dest, 0, randomNumber);

            if (randomNumber < data.Length - 1)
                Array.Copy(data, randomNumber + 1, dest, randomNumber, data.Length - randomNumber - 1);

            data = dest;

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (data.Length == 0) return;
            if (e.KeyCode == Keys.Enter) buttonStart_Click(null, EventArgs.Empty);
            if (e.KeyCode == Keys.Space) buttonStop_Click(null, EventArgs.Empty);
        }

    }
}
