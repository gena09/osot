using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Operation_Structures_of_Texts.Classes.Sci_fi.Processors;
using Operation_Structures_of_Texts.Classes;
using Operation_Structures_of_Texts.Classes.Analysis;

namespace Operation_Structures_of_Texts
{
    public partial class Form1 : Form
    {
        public Projection proj;

        public Form1()
        {
            InitializeComponent();
            controller = new Controller(null);
        }

        Controller controller;        

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }        

        private void button3_Click(object sender, EventArgs e)
        {
            //richTextBox2.Text += this.Size.Height.ToString() + " " + this.Size.Width.ToString();
            //controller.genAllFromDifferentProcessors(richTextBox1.Text, this, checkBox1.Checked, checkBox2.Checked, checkBox3.Checked);
            controller.genAllFromOneProcessor(richTextBox1.Text, this, checkBox1.Checked, checkBox2.Checked, checkBox3.Checked);

        }
        

        internal void setDataFromProcessors(string[] words, string[] partOfspeechs,List<string> pos, double p, string sintaxText, string opStructs)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Rows.Add(words.Length);
            for (int i = 0; i < words.Length; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = words[i];            
                dataGridView1.Rows[i].Cells[1].Value = partOfspeechs[i];
            }
            for (int i = 0; i < pos.Count; i++)
            {
                dataGridView1.Rows[i + 1].Cells[2].Value = pos[i];
            }
            textBox15.Text = Convert.ToString(p);
            textBox1.Text = Convert.ToString(pos.Count);

            richTextBox2.Text = sintaxText;
            richTextBox2.Text += "\n" + opStructs;
        }

        private void AnalisationButton_Click(object sender, EventArgs e)
        {
            proj.work();
        }       
               
    }
}
