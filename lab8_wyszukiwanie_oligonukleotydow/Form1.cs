using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab8_wyszukiwanie_oligonukleotydow
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.TextChanged += textBox1_TextChanged; 

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string userInput = textBox1.Text;
            string allowedCharacters = "CGTA";

            
            bool isValid = userInput.All(c => allowedCharacters.Contains(c));

            if (!isValid)
            {
                MessageBox.Show("Wprowadź tylko litery C, G, T lub A.");
                textBox1.Clear();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string userInput = textBox1.Text;
            List<string> oligoResults = SearchOligos(userInput);
            Form2 form2 = new Form2(oligoResults.Count, oligoResults.Distinct().ToList());
            form2.ShowDialog();
        }

        private List<string> SearchOligos(string sequence)
        {
            List<string> oligos = new List<string>();

            int oligoLength = 4;
            int maxLength = sequence.Length - oligoLength;

            for (int i = 0; i <= maxLength; i++)
            {
                string oligo = sequence.Substring(i, oligoLength);
                oligos.Add(oligo);
            }
            
            return oligos;

        }
    }
}
