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
    public partial class MainForm : Form
    {
        public Projection proj;

        public MainForm()
        {
            InitializeComponent();
        }        

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Controller controller = Controller.Instance;
            controller.generateAllFromOneProcessor(initialText.Text, this, Morphology_checkBox.Checked, postMorphology_checkBox.Checked, syntax_checkBox.Checked, semantics_checkBox.Checked);
        }
        

        internal void setDataFromProcessors(string[] words, string[] partOfspeechs, List<string> pos, double p, string syntaxText, string opStructs)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Rows.Add(words.Length);
            int posCorrection = 0;
            for (int i = 0; i < words.Length; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = words[i];
                dataGridView1.Rows[i].Cells[1].Value = partOfspeechs[i];
                if ( !((words[i] == "\n") || (words[i] == "\r")) )
                {
                    if (i != 0)
                        dataGridView1.Rows[i].Cells[2].Value = pos[i - 1 - posCorrection];
                }
                else posCorrection++;
            }            
            textBox15.Text = Convert.ToString(p);
            textBox1.Text = Convert.ToString(pos.Count);

            richTextBox2.Text = syntaxText;
            richTextBox2.Text += "\n" + opStructs;
        }

        private void AnalisationButton_Click(object sender, EventArgs e)
        {
            proj.work();
        }

        private void processors_checkBoxes_CheckStateChanged(object sender, EventArgs e)
        {
            if (semantics_checkBox.Checked == true)
            {

                Morphology_checkBox.Checked = true;
                Morphology_checkBox.Enabled = false;

                postMorphology_checkBox.Checked = true;
                postMorphology_checkBox.Enabled = false;

                syntax_checkBox.Checked = true;
                syntax_checkBox.Enabled = false;
            }
            else 
            {
                Morphology_checkBox.Enabled = true;
                postMorphology_checkBox.Enabled = true;
                syntax_checkBox.Enabled = true;

                if (syntax_checkBox.Checked == true)
                {
                    Morphology_checkBox.Checked = true;
                    Morphology_checkBox.Enabled = false;

                    postMorphology_checkBox.Checked = true;
                    postMorphology_checkBox.Enabled = false;
                }
                else
                {
                    Morphology_checkBox.Enabled = true;
                    postMorphology_checkBox.Enabled = true;

                    if (postMorphology_checkBox.Checked == true)
                    {
                        Morphology_checkBox.Checked = true;
                        Morphology_checkBox.Enabled = false;                        
                    }
                    else
                    {
                        Morphology_checkBox.Enabled = true;                        
                    }
                }
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            processors_checkBoxes_CheckStateChanged(null, null);
        }
               
    }
}
