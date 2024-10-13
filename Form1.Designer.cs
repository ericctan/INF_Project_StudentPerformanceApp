﻿namespace StudentPerformanceApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            searchButton = new Button();
            searchTextBox = new TextBox();
            searchDataGridView = new DataGridView();
            btnCreateDatabase = new Button();
            btnAddStudent = new Button();
            txtGender = new TextBox();
            txtRace = new TextBox();
            txtParentEdu = new TextBox();
            txtLunch = new TextBox();
            txtTestPrep = new TextBox();
            numMathScore = new NumericUpDown();
            numReadingScore = new NumericUpDown();
            numWritingScore = new NumericUpDown();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            textBox5 = new TextBox();
            textBox6 = new TextBox();
            textBox7 = new TextBox();
            textBox8 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)searchDataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMathScore).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numReadingScore).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numWritingScore).BeginInit();
            SuspendLayout();
            // 
            // searchButton
            // 
            searchButton.Location = new Point(815, 557);
            searchButton.Margin = new Padding(3, 4, 3, 4);
            searchButton.Name = "searchButton";
            searchButton.Size = new Size(86, 31);
            searchButton.TabIndex = 0;
            searchButton.Text = "Search";
            searchButton.UseVisualStyleBackColor = true;
            searchButton.Click += searchButton_Click;
            // 
            // searchTextBox
            // 
            searchTextBox.Location = new Point(559, 519);
            searchTextBox.Margin = new Padding(3, 4, 3, 4);
            searchTextBox.Name = "searchTextBox";
            searchTextBox.Size = new Size(341, 27);
            searchTextBox.TabIndex = 1;
            searchTextBox.TextChanged += searchTextBox_TextChanged;
            // 
            // searchDataGridView
            // 
            searchDataGridView.AllowUserToOrderColumns = true;
            searchDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            searchDataGridView.Location = new Point(15, 13);
            searchDataGridView.Margin = new Padding(3, 4, 3, 4);
            searchDataGridView.Name = "searchDataGridView";
            searchDataGridView.RowHeadersWidth = 51;
            searchDataGridView.Size = new Size(887, 215);
            searchDataGridView.TabIndex = 3;
            // 
            // btnCreateDatabase
            // 
            btnCreateDatabase.Location = new Point(14, 239);
            btnCreateDatabase.Margin = new Padding(3, 4, 3, 4);
            btnCreateDatabase.Name = "btnCreateDatabase";
            btnCreateDatabase.Size = new Size(141, 31);
            btnCreateDatabase.TabIndex = 4;
            btnCreateDatabase.Text = "Initialize Database";
            btnCreateDatabase.UseVisualStyleBackColor = true;
            btnCreateDatabase.Click += btnCreateDatabase_Click;
            // 
            // btnAddStudent
            // 
            btnAddStudent.Location = new Point(806, 239);
            btnAddStudent.Name = "btnAddStudent";
            btnAddStudent.Size = new Size(94, 29);
            btnAddStudent.TabIndex = 5;
            btnAddStudent.Text = "Add";
            btnAddStudent.UseVisualStyleBackColor = true;
            btnAddStudent.Click += btnAddStudent_Click;
            // 
            // txtGender
            // 
            txtGender.Location = new Point(718, 240);
            txtGender.Name = "txtGender";
            txtGender.Size = new Size(82, 27);
            txtGender.TabIndex = 6;
            // 
            // txtRace
            // 
            txtRace.Location = new Point(718, 273);
            txtRace.Name = "txtRace";
            txtRace.Size = new Size(82, 27);
            txtRace.TabIndex = 7;
            // 
            // txtParentEdu
            // 
            txtParentEdu.Location = new Point(718, 306);
            txtParentEdu.Name = "txtParentEdu";
            txtParentEdu.Size = new Size(82, 27);
            txtParentEdu.TabIndex = 8;
            // 
            // txtLunch
            // 
            txtLunch.Location = new Point(718, 339);
            txtLunch.Name = "txtLunch";
            txtLunch.Size = new Size(82, 27);
            txtLunch.TabIndex = 9;
            // 
            // txtTestPrep
            // 
            txtTestPrep.Location = new Point(718, 372);
            txtTestPrep.Name = "txtTestPrep";
            txtTestPrep.Size = new Size(82, 27);
            txtTestPrep.TabIndex = 10;
            // 
            // numMathScore
            // 
            numMathScore.Location = new Point(718, 405);
            numMathScore.Name = "numMathScore";
            numMathScore.Size = new Size(82, 27);
            numMathScore.TabIndex = 11;
            // 
            // numReadingScore
            // 
            numReadingScore.Location = new Point(718, 438);
            numReadingScore.Name = "numReadingScore";
            numReadingScore.Size = new Size(82, 27);
            numReadingScore.TabIndex = 12;
            // 
            // numWritingScore
            // 
            numWritingScore.Location = new Point(718, 471);
            numWritingScore.Name = "numWritingScore";
            numWritingScore.Size = new Size(82, 27);
            numWritingScore.TabIndex = 13;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(638, 405);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(74, 27);
            textBox1.TabIndex = 14;
            textBox1.Text = "Math:";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(638, 437);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(74, 27);
            textBox2.TabIndex = 15;
            textBox2.Text = "Reading:";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(638, 471);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(74, 27);
            textBox3.TabIndex = 16;
            textBox3.Text = "Writing:";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(638, 239);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(74, 27);
            textBox4.TabIndex = 17;
            textBox4.Text = "Gender:";
            // 
            // textBox5
            // 
            textBox5.Location = new Point(638, 272);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(74, 27);
            textBox5.TabIndex = 18;
            textBox5.Text = "Race:";
            // 
            // textBox6
            // 
            textBox6.Location = new Point(638, 306);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(74, 27);
            textBox6.TabIndex = 19;
            textBox6.Text = "Parent Edu:";
            // 
            // textBox7
            // 
            textBox7.Location = new Point(638, 339);
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(74, 27);
            textBox7.TabIndex = 20;
            textBox7.Text = "Lunch:";
            // 
            // textBox8
            // 
            textBox8.Location = new Point(638, 372);
            textBox8.Name = "textBox8";
            textBox8.Size = new Size(74, 27);
            textBox8.TabIndex = 21;
            textBox8.Text = "Test Prep:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(textBox8);
            Controls.Add(textBox7);
            Controls.Add(textBox6);
            Controls.Add(textBox5);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(numWritingScore);
            Controls.Add(numReadingScore);
            Controls.Add(numMathScore);
            Controls.Add(txtTestPrep);
            Controls.Add(txtLunch);
            Controls.Add(txtParentEdu);
            Controls.Add(txtRace);
            Controls.Add(txtGender);
            Controls.Add(btnAddStudent);
            Controls.Add(btnCreateDatabase);
            Controls.Add(searchDataGridView);
            Controls.Add(searchTextBox);
            Controls.Add(searchButton);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)searchDataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMathScore).EndInit();
            ((System.ComponentModel.ISupportInitialize)numReadingScore).EndInit();
            ((System.ComponentModel.ISupportInitialize)numWritingScore).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button searchButton;
        private TextBox searchTextBox;
        private DataGridView searchDataGridView;
        private Button btnCreateDatabase;
        private Button btnAddStudent;
        private TextBox txtGender;
        private TextBox txtRace;
        private TextBox txtParentEdu;
        private TextBox txtLunch;
        private TextBox txtTestPrep;
        private NumericUpDown numMathScore;
        private NumericUpDown numReadingScore;
        private NumericUpDown numWritingScore;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private TextBox textBox5;
        private TextBox textBox6;
        private TextBox textBox7;
        private TextBox textBox8;
    }
}
