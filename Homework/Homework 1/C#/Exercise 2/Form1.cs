using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exercise2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            //Reference 1
            var person = new People("Tommaso", "Gastaldi");
            this.richTextBox1.AppendText("Example 1"+ Environment.NewLine);
            this.richTextBox1.AppendText(person.Who() + Environment.NewLine); // outputs "Hello! My name is Tommaso and my surname is Gastaldi"
            var person2 = person;
            person2.Name = "Andrea";
            person2.Surname = "Tripoli";
            this.richTextBox1.AppendText(person2.Who() + Environment.NewLine); // outputs "Hello! My name is Andrea and my surname is Tripoli"
            this.richTextBox1.AppendText(person.Who() + Environment.NewLine + "--------------------------------------------------------------------------------------------"+ Environment.NewLine);  // outputs "Hello! My name is Andrea and my surname is Tripoli"


            //Reference 2
            Student std1 = new Student();
            std1.StudentName = "Tommaso";
            Program.ChangeReferenceType(std1);

            this.richTextBox1.AppendText("Example 2" + Environment.NewLine);
            this.richTextBox1.AppendText(std1.StudentName + Environment.NewLine); // outputs "Andrea"
            this.richTextBox1.AppendText("--------------------------------------------------------------------------------------------" + Environment.NewLine);  // outputs "Hello! My name is Andrea and my surname is Tripoli"




            //Value 1
            this.richTextBox1.AppendText("Example 3" + Environment.NewLine);

            int num1 = 5;
            int num2 = 10;
            this.richTextBox1.AppendText(num1 + " " + num2 + Environment.NewLine);
            this.richTextBox1.AppendText(Square(num1, num2)+ Environment.NewLine);
            this.richTextBox1.AppendText(num1 + " " + num2 + Environment.NewLine);
            this.richTextBox1.AppendText("--------------------------------------------------------------------------------------------" + Environment.NewLine);  // outputs "Hello! My name is Andrea and my surname is Tripoli"



            //Value 2
            this.richTextBox1.AppendText("Example 4" + Environment.NewLine);
            Simple s;
            s.Position = 1;
            s.Exists = false;
            s.LastValue = 5.5;

            this.richTextBox1.AppendText(s.Position + Environment.NewLine);

        }

        class People
        {
            public string Name { get; set; }
            public string Surname { get; set; }

            public People(string name, string surname)
            {
                Name = name;
                Surname = surname;
            }

            public String Who()
            {
                String datas = ($"Hello! My name is {Name} and my surname is {Surname}");
                return datas;
            }

        }



        //Reference 2

        public class Student
        {

            public string StudentName { get; set; }

        }

        public class Program
        {
            public static void ChangeReferenceType(Student std2)
            {
                std2.StudentName = "Andrea";
            }
        }

        //Value 1

        static string Square(int a, int b)

        {

            a = a * a;
            b = b * b;

            String res = a + " " + b; //Just to print on screen and show it

            return res;

        }



        //Value 2
        struct Simple
        {
            public int Position;
            public bool Exists;
            public double LastValue;
        };
    }


}
