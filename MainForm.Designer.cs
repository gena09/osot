namespace Operation_Structures_of_Texts
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.generateOSTab = new System.Windows.Forms.TabPage();
            this.postMorphology_checkBox = new System.Windows.Forms.CheckBox();
            this.AnalisationButton = new System.Windows.Forms.Button();
            this.semantics_checkBox = new System.Windows.Forms.CheckBox();
            this.syntax_checkBox = new System.Windows.Forms.CheckBox();
            this.Morphology_checkBox = new System.Windows.Forms.CheckBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ColumnInWords = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPartOfSpeeches = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPartOfSpAfter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnOperationAttr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label15 = new System.Windows.Forms.Label();
            this.initialText = new System.Windows.Forms.RichTextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.generateOSTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.generateOSTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1016, 646);
            this.tabControl1.TabIndex = 6;
            // 
            // generateOSTab
            // 
            this.generateOSTab.BackColor = System.Drawing.Color.WhiteSmoke;
            this.generateOSTab.Controls.Add(this.postMorphology_checkBox);
            this.generateOSTab.Controls.Add(this.AnalisationButton);
            this.generateOSTab.Controls.Add(this.semantics_checkBox);
            this.generateOSTab.Controls.Add(this.syntax_checkBox);
            this.generateOSTab.Controls.Add(this.Morphology_checkBox);
            this.generateOSTab.Controls.Add(this.richTextBox2);
            this.generateOSTab.Controls.Add(this.textBox1);
            this.generateOSTab.Controls.Add(this.label1);
            this.generateOSTab.Controls.Add(this.label16);
            this.generateOSTab.Controls.Add(this.textBox15);
            this.generateOSTab.Controls.Add(this.dataGridView1);
            this.generateOSTab.Controls.Add(this.label15);
            this.generateOSTab.Controls.Add(this.initialText);
            this.generateOSTab.Controls.Add(this.button3);
            this.generateOSTab.Location = new System.Drawing.Point(4, 22);
            this.generateOSTab.Name = "generateOSTab";
            this.generateOSTab.Padding = new System.Windows.Forms.Padding(3);
            this.generateOSTab.Size = new System.Drawing.Size(1008, 620);
            this.generateOSTab.TabIndex = 3;
            this.generateOSTab.Text = "Генерация операторных структур";
            this.generateOSTab.UseVisualStyleBackColor = true;
            // 
            // postMorphology_checkBox
            // 
            this.postMorphology_checkBox.AutoSize = true;
            this.postMorphology_checkBox.Checked = true;
            this.postMorphology_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.postMorphology_checkBox.Location = new System.Drawing.Point(725, 38);
            this.postMorphology_checkBox.Name = "postMorphology_checkBox";
            this.postMorphology_checkBox.Size = new System.Drawing.Size(133, 30);
            this.postMorphology_checkBox.TabIndex = 13;
            this.postMorphology_checkBox.Text = "Постсинтаксическая\r\nморфология";
            this.postMorphology_checkBox.UseVisualStyleBackColor = true;
            this.postMorphology_checkBox.CheckStateChanged += new System.EventHandler(this.processors_checkBoxes_CheckStateChanged);
            // 
            // AnalisationButton
            // 
            this.AnalisationButton.Location = new System.Drawing.Point(869, 67);
            this.AnalisationButton.Name = "AnalisationButton";
            this.AnalisationButton.Size = new System.Drawing.Size(102, 24);
            this.AnalisationButton.TabIndex = 12;
            this.AnalisationButton.Text = "Анализировать";
            this.AnalisationButton.UseVisualStyleBackColor = true;
            this.AnalisationButton.Click += new System.EventHandler(this.AnalisationButton_Click);
            // 
            // semantics_checkBox
            // 
            this.semantics_checkBox.AutoSize = true;
            this.semantics_checkBox.Checked = true;
            this.semantics_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.semantics_checkBox.Location = new System.Drawing.Point(725, 97);
            this.semantics_checkBox.Name = "semantics_checkBox";
            this.semantics_checkBox.Size = new System.Drawing.Size(82, 17);
            this.semantics_checkBox.TabIndex = 11;
            this.semantics_checkBox.Text = "Семантика";
            this.semantics_checkBox.UseVisualStyleBackColor = true;
            this.semantics_checkBox.CheckStateChanged += new System.EventHandler(this.processors_checkBoxes_CheckStateChanged);
            // 
            // syntax_checkBox
            // 
            this.syntax_checkBox.AutoSize = true;
            this.syntax_checkBox.Checked = true;
            this.syntax_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.syntax_checkBox.Location = new System.Drawing.Point(725, 74);
            this.syntax_checkBox.Name = "syntax_checkBox";
            this.syntax_checkBox.Size = new System.Drawing.Size(80, 17);
            this.syntax_checkBox.TabIndex = 10;
            this.syntax_checkBox.Text = "Синтаксис";
            this.syntax_checkBox.UseVisualStyleBackColor = true;
            this.syntax_checkBox.CheckStateChanged += new System.EventHandler(this.processors_checkBoxes_CheckStateChanged);
            // 
            // Morphology_checkBox
            // 
            this.Morphology_checkBox.AutoSize = true;
            this.Morphology_checkBox.Checked = true;
            this.Morphology_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Morphology_checkBox.Location = new System.Drawing.Point(725, 21);
            this.Morphology_checkBox.Name = "Morphology_checkBox";
            this.Morphology_checkBox.Size = new System.Drawing.Size(90, 17);
            this.Morphology_checkBox.TabIndex = 9;
            this.Morphology_checkBox.Text = "Морфология";
            this.Morphology_checkBox.UseVisualStyleBackColor = true;
            this.Morphology_checkBox.CheckStateChanged += new System.EventHandler(this.processors_checkBoxes_CheckStateChanged);
            this.Morphology_checkBox.CheckedChanged += new System.EventHandler(this.processors_checkBoxes_CheckStateChanged);
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(550, 274);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(450, 338);
            this.richTextBox2.TabIndex = 8;
            this.richTextBox2.Text = "";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(14, 148);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Длина текста";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(132, 119);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(158, 26);
            this.label16.TabIndex = 5;
            this.label16.Text = "Вероятность воостановления\r\nотказов морфлогии";
            // 
            // textBox15
            // 
            this.textBox15.Location = new System.Drawing.Point(135, 148);
            this.textBox15.MaxLength = 5;
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new System.Drawing.Size(100, 20);
            this.textBox15.TabIndex = 4;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnInWords,
            this.ColumnPartOfSpeeches,
            this.ColumnPartOfSpAfter,
            this.ColumnOperationAttr});
            this.dataGridView1.Location = new System.Drawing.Point(17, 274);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(514, 336);
            this.dataGridView1.TabIndex = 3;
            // 
            // ColumnInWords
            // 
            this.ColumnInWords.HeaderText = "Слова";
            this.ColumnInWords.Name = "ColumnInWords";
            // 
            // ColumnPartOfSpeeches
            // 
            this.ColumnPartOfSpeeches.HeaderText = "Части речи";
            this.ColumnPartOfSpeeches.Name = "ColumnPartOfSpeeches";
            // 
            // ColumnPartOfSpAfter
            // 
            this.ColumnPartOfSpAfter.HeaderText = "Части речи после восстановления";
            this.ColumnPartOfSpAfter.Name = "ColumnPartOfSpAfter";
            // 
            // ColumnOperationAttr
            // 
            this.ColumnOperationAttr.HeaderText = "Атрибут операторной структуры";
            this.ColumnOperationAttr.Name = "ColumnOperationAttr";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 3);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(89, 13);
            this.label15.TabIndex = 2;
            this.label15.Text = "Исходный текст";
            // 
            // initialText
            // 
            this.initialText.Location = new System.Drawing.Point(6, 19);
            this.initialText.Name = "initialText";
            this.initialText.Size = new System.Drawing.Size(660, 81);
            this.initialText.TabIndex = 1;
            this.initialText.Text = "Он злостно нарушает правила.\nМама и папа мыли раму, которая ещё и не открывалась." +
                "\nОн умел хорошо гулять, думать и говорить.\nСмеркалось.\nНарисуй тыкву в верхнем л" +
                "евом углу экрана.";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(869, 38);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(102, 23);
            this.button3.TabIndex = 0;
            this.button3.Text = "Сгенерировать";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1016, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выходToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AcceptButton = this.button3;
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 670);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1024, 704);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Операторные структуры текста";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.generateOSTab.ResumeLayout(false);
            this.generateOSTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.TabPage generateOSTab;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.RichTextBox initialText;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBox15;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.CheckBox semantics_checkBox;
        private System.Windows.Forms.CheckBox syntax_checkBox;
        private System.Windows.Forms.CheckBox Morphology_checkBox;
        private System.Windows.Forms.Button AnalisationButton;
        private System.Windows.Forms.CheckBox postMorphology_checkBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnInWords;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPartOfSpeeches;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPartOfSpAfter;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnOperationAttr;

    }
}

