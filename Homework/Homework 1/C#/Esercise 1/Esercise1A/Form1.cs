using System;
using System.Drawing;
using System.Windows.Forms;

namespace Esercise1A
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }


        //Click on the button to insert the text
        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            //Initialize a variable of type String
            String text = "Hi Tommaso Gastaldi";

            //Set the text of the richtextbox element with the contents of the variable text
            this.richTextBox1.Text = text;
        }

        //Click on the button to clean the text
        private void button2_MouseClick(object sender, MouseEventArgs e)
        {
            //The element is completely cleaned and is not set with " " (null)
            this.richTextBox1.Clear();
        }


        //Mouse access in the richtextbox element
        private void richTextBox1_MouseEnter(object sender, EventArgs e)
        {
            //Changing the background color from default to orange
            this.richTextBox1.BackColor = Color.Orange;

            //Changing the text color from default to white
            this.richTextBox1.ForeColor = Color.White;
        }

        //Mouse leave from the richtextbox element
        private void richTextBox1_MouseLeave(object sender, EventArgs e)
        {
            //Changing the background color from orange to default
            this.richTextBox1.BackColor = default(Color);

            //Changing the text color from white to default
            this.richTextBox1.ForeColor = default(Color);
        }
    }
}
