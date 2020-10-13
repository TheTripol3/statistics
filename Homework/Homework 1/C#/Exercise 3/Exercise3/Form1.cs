using System.Windows.Forms;

namespace Exercise3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            //Insert effects to the cursor when moving files around the panel
            e.Effect = DragDropEffects.All;

        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            //Stores the data extracted from the drag and drop operation in an array of strings. 
            //The boolean variable is used when extrapolating data to not convert it to the specific default format
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            //For each files, we extrapolate the data and store it in the file variable and then 
            //set it as text in a label
            foreach (string file in files)
                this.label4.Text = file;
        }

 
    }
}
