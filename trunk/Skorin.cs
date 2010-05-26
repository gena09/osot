using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Operation_Structures_of_Texts
{
    public partial class Skorin : Form
    {
        public Skorin(string[] text)
        {
            InitializeComponent();
            textBox1.Text = "";
            for (int i = 0; i < text.Length; i++)
            {
                textBox1.Text += text[i];
                textBox1.Text += "\r\n";
            }
        }
    }
}
