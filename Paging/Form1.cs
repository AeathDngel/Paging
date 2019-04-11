using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paging //Zonica Lombard && Estian Yssel
{
    public partial class Form1 : Form
    {
        private int firstclick = 0;
        private int slots;
        private int count;
        private string reference;
        private string[] positions;
        private int counter;
        private int index;
        private int doubles;
        private int i = 0;
        //ESTIAN WAS HERE!

        public Form1()
        {
            InitializeComponent();
            Properties.Settings.Default.Start = "0";
            count = 0;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Set all button (slots) visibility to false
            btn1.Visible = false;
            btn2.Visible = false;
            btn3.Visible = false;
            btn4.Visible = false;
            btn5.Visible = false;
            btn6.Visible = false;
            btn7.Visible = false;
            btn8.Visible = false;
            btn9.Visible = false;
            btn10.Visible = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ELiminate , infront of string (reference list)
            if (textBox1.Text != String.Empty)
            {
                if (firstclick == 0)
                {
                    label2.Text += textBox1.Text;
                    firstclick++;
                }
                else
                {
                    label2.Text += "," + textBox1.Text;
                }
                count++;
                textBox3.AppendText(textBox1.Text + " added to reference list. ");
                textBox3.AppendText(Environment.NewLine);
            }

            reference = label2.Text.Substring(14);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e) //start button
        {
            //After paging starts disable add buttons
            button1.Visible = false;
            button3.Visible = false;

            textBox3.AppendText("Amount of slots available: " + textBox2.Text);
            textBox3.AppendText(Environment.NewLine);
            textBox3.AppendText(" Paging started ...");
            textBox3.AppendText(Environment.NewLine);

            positions = new string[count];
            positions = reference.Split(',');

            counter = 1; // to see that if counter is equal to the amount of slots paging should start
            index = 1;   // btn shown

           doubles = 0; //to check if slot already taken

            //Start paging method
            runpaging();
        }
        public void addPage(int index, int slotpos) {

            switch (index) {
                case 1: btn1.Text = positions[slotpos];
                    break;
                case 2:
                    btn2.Text = positions[slotpos];
                    break;
                case 3:
                    btn3.Text = positions[slotpos];
                    break;
                case 4:
                    btn4.Text = positions[slotpos];
                    break;
                case 5:
                    btn5.Text = positions[slotpos];
                    break;
                case 6:
                    btn6.Text = positions[slotpos];
                    break;
                case 7:
                    btn7.Text = positions[slotpos];
                    break;
                case 8:
                    btn8.Text = positions[slotpos];
                    break;
                case 9:
                    btn9.Text = positions[slotpos];
                    break;
                case 10:
                    btn10.Text = positions[slotpos];
                    break;


            }
        }
        
        public void runpaging() {


            timer1.Start();
            

           }
        
        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            slots = Int32.Parse(textBox2.Text);


            for (int i = 1; i <= slots; i++)
            {
                switch (i) {

                    case 1: btn1.Visible = true;
                        break;
                    case 2:
                        btn2.Visible = true;
                        break;
                    case 3:
                        btn3.Visible = true;
                        break;
                    case 4:
                        btn4.Visible = true;
                        break;
                    case 5:
                        btn5.Visible = true;
                        break;
                    case 6:
                        btn6.Visible = true;
                        break;
                    case 7:
                        btn7.Visible = true;
                        break;
                    case 8:
                        btn8.Visible = true;
                        break;
                    case 9:
                        btn9.Visible = true;
                        break;
                    case 10:
                        btn10.Visible = true;
                        break;

                }
                
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // for (int i = 0; i < count; i++)
            //  {
            if (i < count)
            {
                if (i >= slots)
                {
                    for (int j = i - slots; j < i; j++) //check if page already exists
                    {

                        if (positions[j] == positions[i])
                        {
                            doubles++;
                        }
                    }
                }
                else {
                    for (int j = 0; j < i; j++) //check if page already exists
                    {

                        if (positions[j] == positions[i])
                        {
                            doubles++;
                        }
                    }
                }

                if (index > slots)
                {

                    index = 1;

                }
                if (counter > slots)
                {
                    Console.WriteLine("Page fault occured. Remove slot " + positions[index] + " from memory");

                    textBox3.AppendText("Page fault occured. Remove slot " + positions[index] + " from memory. ");
                    textBox3.AppendText(Environment.NewLine);
                }

                if (doubles == 0)
                {
                    addPage(index, i);
                    index++;
                    Console.Write(positions[i] + " add slot to memory");
                    textBox3.AppendText(positions[i] + " add slot to memory. ");
                    textBox3.AppendText(Environment.NewLine);
                    counter++;
                }
                else
                {
                    Console.Write(positions[i] + " slot already added to memory");
                    textBox3.AppendText(positions[i] + " slot already added to memory. ");
                    textBox3.AppendText(Environment.NewLine);
                }

                doubles = 0;
                i++;
            }
            if (i == positions.Length)
            {
                textBox3.AppendText("...Paging completed");
                textBox3.AppendText(Environment.NewLine);
                timer1.Stop();
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}


//ESTIAN TLB en PAGE FRAMES