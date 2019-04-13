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
    public partial class Paging : Form
    {
        private int firstclick = 0;
        private int slots;
        private int count;
        private string reference;
        private string[] virtualPositions;
        private string[] positions;
        private int counter;
        private int index;
        private int doubles;
        private int i = 0;
        private int tlbCount = 0;
        private int tlbMax = 5;

        public Paging()
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
            //Eliminate , infront of string (reference list)
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

            //Gets string from label, after "Page Sequence: ".
            reference = label2.Text.Substring(15);
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
            textBox3.AppendText("Paging started ...");
            textBox3.AppendText(Environment.NewLine);

            //Get page sequence from reference string
            positions = new string[count];
            positions = reference.Split(',');

            //Initialise virtual pages for later use
            virtualPositions = new string[count];

            /*virtualPositions = new string[count];
            virtualPositions = reference.Split(',');

            positions = getPhysicalPages(virtualPositions);*/



            counter = 1; // to see that if counter is equal to the amount of slots paging should start
            index = 1;   // btn shown

            doubles = 0; //to check if slot already taken

            //Start paging method
            runpaging();
        }

        /*public string[] getPhysicalPages(string[] virtualPages)
        {
            int c = 0;
            string[] physicalPages = new string[count];
            foreach(string item in virtualPages)
            {
                string tempString = checkTLB(item);
                if (tempString != null)
                {
                    physicalPages[c] = tempString; 
                }
                else
                {
                    tempString = checkPageTable(item);
                }

                physicalPages[c] = tempString;

                c++;
            }

            return virtualPages;
        }*/

        //Method which returns the physical page from the virtual page number given.
        public string getPhysicalPage(string virtualPage)
        {
            //First checks if the page is loaded into the TLB
            string tempString = checkTLB(virtualPage);
            
            if (tempString == null) //If the page is not loaded into TLB
            {
                //Check page table for the physical page
                tempString = checkPageTable(virtualPage);
            }

            //Return the physical page
            return tempString;
        }

        //Method searches TLB for page frame
        public string checkTLB(string param)
        {
            //Loop through the items within the TLB list box
            for(int itemIndex = 0; itemIndex < lbxTLB.Items.Count; itemIndex++)
            {
                try
                {
                    //Check if the item is not a header
                    if (!lbxTLB.Items[itemIndex].ToString().Contains("Page") || !lbxTLB.Items[itemIndex].ToString().Contains("="))
                    {
                        //Store possible match in candidate
                        string candidate = lbxTLB.Items[itemIndex].ToString();
                        //Get Page Number
                        string candidatePageNum = candidate.Remove(candidate.IndexOf(' '));
                        //Get Page address
                        string candidateAddress = candidate.Substring(candidate.LastIndexOf(' '));
                        
                        try
                        {
                            //If the page number is a match
                            if (candidatePageNum == param)
                            {
                                //Page loaded from TLB
                                textBox3.AppendText("Page " + param + " found in TLB");
                                textBox3.AppendText(Environment.NewLine);
                                return candidateAddress;
                            }
                        }
                        catch (System.ArgumentOutOfRangeException e)
                        {
                            Console.WriteLine("Item is header...");

                        }

                    }
                }
                catch (System.ArgumentOutOfRangeException e)
                {
                    Console.WriteLine("Item is header...");

                }
                //MessageBox.Show(item.ToString());


            }
            return null;

        }

        public string checkPageTable(string param)
        {
            
            string physicalPage = lbxPageTable.Items[Convert.ToInt32(param) + 2].ToString();

            if (lbxTLB.Items.Count <= 5)
            {
                textBox3.AppendText("Page " + param + " NOT found in TLB, adding to TLB");
                textBox3.AppendText(Environment.NewLine);
                lbxTLB.Items.Add(param + "                                   " + physicalPage);
                tlbCount++;
                return physicalPage;
            }
            else
            {
                textBox3.AppendText("TLB Full, using page table");
                textBox3.AppendText(Environment.NewLine);
                //lbxTLB.Items.Add(param + "                                   " + physicalPage);
                return physicalPage;
            }
            
            
        }

        public void addPage(int index, int slotpos)
        {

            switch (index)
            {
                case 1: btn1.Text = "Frame: " + positions[slotpos] + ", Page: " +virtualPositions[slotpos]; // positions[slotpos];
                    break;
                case 2:
                    btn2.Text = "Frame: " + positions[slotpos] + ", Page: " + virtualPositions[slotpos]; // positions[slotpos];
                    break;
                case 3:
                    btn3.Text = "Frame: " + positions[slotpos] + ", Page: " + virtualPositions[slotpos]; // positions[slotpos];
                    break;
                case 4:
                    btn4.Text = "Frame: " + positions[slotpos] + ", Page: " + virtualPositions[slotpos]; // positions[slotpos];
                    break;
                case 5:
                    btn5.Text = "Frame: " + positions[slotpos] + ", Page: " + virtualPositions[slotpos]; // positions[slotpos];
                    break;
                case 6:
                    btn6.Text = "Frame: " + positions[slotpos] + ", Page: " + virtualPositions[slotpos]; // positions[slotpos];
                    break;
                case 7:
                    btn7.Text = "Frame: " + positions[slotpos] + ", Page: " + virtualPositions[slotpos]; // positions[slotpos];
                    break;
                case 8:
                    btn8.Text = "Frame: " + positions[slotpos] + ", Page: " + virtualPositions[slotpos]; // positions[slotpos];
                    break;
                case 9:
                    btn9.Text = "Frame: " + positions[slotpos] + ", Page: " + virtualPositions[slotpos]; // positions[slotpos];
                    break;
                case 10:
                    btn10.Text = "Frame: " + positions[slotpos] + ", Page: " + virtualPositions[slotpos]; // positions[slotpos];
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
                    virtualPositions[i] = getPhysicalPage(positions[i]);
                    addPage(index, i);
                    
                    //Console.Write(virtualPositions[i] + " add slot to memory");
                    textBox3.AppendText(virtualPositions[i] + " add slot to memory. ");
                    textBox3.AppendText(Environment.NewLine);
                    index++;
                    counter++;
                }
                else
                {
                    virtualPositions[i] = getPhysicalPage(positions[i]);
                    Console.Write(virtualPositions[i] + " slot already added to memory");
                    textBox3.AppendText(virtualPositions[i] + " slot already added to memory. ");
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

        private void lbxPageTable_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}


//ESTIAN TLB en PAGE FRAMES