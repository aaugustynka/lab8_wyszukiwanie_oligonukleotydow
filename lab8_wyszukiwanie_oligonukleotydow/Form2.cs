using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper;

namespace lab8_wyszukiwanie_oligonukleotydow
{/*
    public partial class Form2 : Form
    {
        Form1 form;
        public Form2(Form1 form)
        {
            InitializeComponent();
            this.form = form;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGrid_Save();
        }
        public void dataGrid_Save()
        {
            label1.Text = "";
            int l = 0; bool empty = true;
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "CSV|*.csv";
                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    StreamWriter sw = new StreamWriter(sfd.FileName, true, Encoding.ASCII);
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        l = 0;
                        empty = true;
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            if (cell.Value != null)
                                empty = false;
                        }
                        if (empty == false)
                        {
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                if (l == 0)
                                    sw.Write(cell.Value);
                                else
                                    sw.Write("," + cell.Value);
                                l++;
                            }
                            sw.Write("\n");
                        }
                    }
                    sw.Close();
                }
            }
            catch (Exception e)
            {
                label1.Text = e.Message;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    
    public partial class Form2 : Form
    {
        private string sequence;

        public string Sequence
        {
            get { return sequence; }
            set { sequence = value; }
        }

        public Form2(string sequence)
        {
            InitializeComponent();
            Sequence = sequence;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Oligonukleotyd");

            // Dodaj sekwencję do DataTable
            dt.Rows.Add(Sequence);

            // Ustaw DataTable jako źródło danych dla DataGridView
            dataGridView1.DataSource = dt;
        }
    }
}
    */
    public partial class Form2 : Form
    {

        public Form2(int count, List<string> distinctOligos)
        {
            InitializeComponent();
            DataTable dt = new DataTable();
            dt.Columns.Add("Oligonukleotyd");
            dt.Columns.Add("Liczba wystąpień");

            foreach (var oligo in distinctOligos)
            {
                int occurrences = 1;
                if (oligo.Equals(distinctOligos))
                { 
                    occurrences++;
                }
                dt.Rows.Add(oligo, occurrences);
            }
            
            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Pliki CSV (*.csv)|*.csv";
            saveFileDialog.Title = "Zapisz wyniki do pliku CSV";
            saveFileDialog.FileName = "wyniki.csv";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                using (var writer = new StreamWriter(filePath))
                using (var csv = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
                {
                    DataTable dt = (DataTable)dataGridView1.DataSource;
                    foreach (DataRow row in dt.Rows)
                    {
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            csv.WriteField(row[i]);
                        }
                        csv.NextRecord();
                    }
                }

                MessageBox.Show("Wyniki zostały zapisane do pliku CSV.");
            }
        }

    }
}
