using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exercise_1
{
    public partial class Form1 : Form
    {
        
        public Random R = new Random();
        public Double CurrentAvarage = 0;
        public int countNumber = 0;

        public SortedDictionary<int, frequencies> FrequencyDistribution = new SortedDictionary<int, frequencies>();

        public Form1()
        {
            InitializeComponent();

            this.richTextBox1.AppendText("Exam".PadRight(10) + "Grade".PadRight(8) + "Current Mean" + Environment.NewLine);

            this.chart1.ChartAreas["ChartArea1"].AxisX.MinorTickMark.Enabled = true;
            this.chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            this.chart1.ChartAreas["ChartArea1"].AxisX.IsLabelAutoFit = true;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            //Online arithmetical mean
            //----------------------------------------------------

            //Increment the counter value
            countNumber += 1;

            //The Knuth formula
            int grade = R.Next(18, 31);
            CurrentAvarage = CurrentAvarage + (grade - CurrentAvarage) / countNumber;

            string nameOfTheExam = "Exam " + countNumber;

            this.richTextBox1.AppendText(nameOfTheExam.PadRight(10) +
                                       grade.ToString().PadRight(8) + CurrentAvarage +
                                       Environment.NewLine);




            //---------------------------------------------------
            //Distribution

            if (FrequencyDistribution.ContainsKey(grade))
            {
                FrequencyDistribution[grade].countFrequencies += 1;
            }
            else
            {
                FrequencyDistribution.Add(grade, new frequencies());
            }

            //Clear every time
            this.richTextBox2.Clear();
            this.chart1.Series["Relative Frequency"].Points.Clear();
            this.richTextBox2.AppendText("Grade".PadRight(7) + "Count".PadRight(7) + "Freq".PadRight(7) + "Perc".PadRight(7) + Environment.NewLine);

            //Calculate the relative frequencies and percentage frequencies. Then draw the graphic
            foreach (KeyValuePair<int, frequencies> freq in FrequencyDistribution)
            {
                FrequencyDistribution[freq.Key].RelativeFrequencies = Convert.ToDouble(FrequencyDistribution[freq.Key].countFrequencies) / Convert.ToDouble(countNumber);
                FrequencyDistribution[freq.Key].PercentageFrequencies = Convert.ToDouble(FrequencyDistribution[freq.Key].RelativeFrequencies) * 100;

                string fr = "0." + freq.Value.RelativeFrequencies.ToString("0.##");
                this.chart1.Series["Relative Frequency"].Points.AddXY(freq.Key.ToString(), fr);

                this.richTextBox2.AppendText(freq.Key.ToString().PadRight(7) +
                                           freq.Value.countFrequencies.ToString().PadRight(7) +
                                           freq.Value.RelativeFrequencies.ToString("0.##").PadRight(7) +
                                           freq.Value.PercentageFrequencies.ToString("0.##").PadRight(7) + " % " + Environment.NewLine);



            }

            this.richTextBox2.AppendText(Environment.NewLine + Environment.NewLine +
                                       "Total Count: " + countNumber + Environment.NewLine);



        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            this.timer1.Start();
        }

        private void button2_MouseClick(object sender, MouseEventArgs e)
        {
            this.timer1.Stop();
        }
    }

    // Class Frequencies
    public class frequencies
    {

        public int countFrequencies = 1;
        public Double RelativeFrequencies;
        public Double PercentageFrequencies;

    }
}
