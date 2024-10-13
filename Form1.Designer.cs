namespace StudentPerformanceApp
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
            btnDeleteStudent = new Button();
            btnClearResults = new Button();
            ((System.ComponentModel.ISupportInitialize)searchDataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMathScore).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numReadingScore).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numWritingScore).BeginInit();
            SuspendLayout();
            // 
            // searchButton
            // 
            searchButton.Location = new Point(635, 303);
            searchButton.Margin = new Padding(4, 5, 4, 5);
            searchButton.Name = "searchButton";
            searchButton.Size = new Size(108, 31);
            searchButton.TabIndex = 0;
            searchButton.Text = "Search";
            searchButton.UseVisualStyleBackColor = true;
            searchButton.Click += searchButton_Click;
            // 
            // searchTextBox
            // 
            searchTextBox.Location = new Point(202, 303);
            searchTextBox.Margin = new Padding(4, 5, 4, 5);
            searchTextBox.Name = "searchTextBox";
            searchTextBox.Size = new Size(425, 31);
            searchTextBox.TabIndex = 1;
            searchTextBox.TextChanged += searchTextBox_TextChanged;
            // 
            // searchDataGridView
            // 
            searchDataGridView.AllowUserToOrderColumns = true;
            searchDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            searchDataGridView.Location = new Point(19, 16);
            searchDataGridView.Margin = new Padding(4, 5, 4, 5);
            searchDataGridView.Name = "searchDataGridView";
            searchDataGridView.RowHeadersWidth = 51;
            searchDataGridView.Size = new Size(1109, 269);
            searchDataGridView.TabIndex = 3;
            // 
            // btnCreateDatabase
            // 
            btnCreateDatabase.Location = new Point(18, 299);
            btnCreateDatabase.Margin = new Padding(4, 5, 4, 5);
            btnCreateDatabase.Name = "btnCreateDatabase";
            btnCreateDatabase.Size = new Size(176, 39);
            btnCreateDatabase.TabIndex = 4;
            btnCreateDatabase.Text = "Initialize Database";
            btnCreateDatabase.UseVisualStyleBackColor = true;
            btnCreateDatabase.Click += btnCreateDatabase_Click;
            // 
            // btnAddStudent
            // 
            btnAddStudent.Location = new Point(1008, 299);
            btnAddStudent.Margin = new Padding(4);
            btnAddStudent.Name = "btnAddStudent";
            btnAddStudent.Size = new Size(118, 36);
            btnAddStudent.TabIndex = 5;
            btnAddStudent.Text = "Add";
            btnAddStudent.UseVisualStyleBackColor = true;
            btnAddStudent.Click += btnAddStudent_Click;
            // 
            // txtGender
            // 
            txtGender.Location = new Point(898, 300);
            txtGender.Margin = new Padding(4);
            txtGender.Name = "txtGender";
            txtGender.Size = new Size(102, 31);
            txtGender.TabIndex = 6;
            // 
            // txtRace
            // 
            txtRace.Location = new Point(898, 341);
            txtRace.Margin = new Padding(4);
            txtRace.Name = "txtRace";
            txtRace.Size = new Size(102, 31);
            txtRace.TabIndex = 7;
            // 
            // txtParentEdu
            // 
            txtParentEdu.Location = new Point(898, 382);
            txtParentEdu.Margin = new Padding(4);
            txtParentEdu.Name = "txtParentEdu";
            txtParentEdu.Size = new Size(102, 31);
            txtParentEdu.TabIndex = 8;
            // 
            // txtLunch
            // 
            txtLunch.Location = new Point(898, 424);
            txtLunch.Margin = new Padding(4);
            txtLunch.Name = "txtLunch";
            txtLunch.Size = new Size(102, 31);
            txtLunch.TabIndex = 9;
            // 
            // txtTestPrep
            // 
            txtTestPrep.Location = new Point(898, 465);
            txtTestPrep.Margin = new Padding(4);
            txtTestPrep.Name = "txtTestPrep";
            txtTestPrep.Size = new Size(102, 31);
            txtTestPrep.TabIndex = 10;
            // 
            // numMathScore
            // 
            numMathScore.Location = new Point(898, 506);
            numMathScore.Margin = new Padding(4);
            numMathScore.Name = "numMathScore";
            numMathScore.Size = new Size(102, 31);
            numMathScore.TabIndex = 11;
            // 
            // numReadingScore
            // 
            numReadingScore.Location = new Point(898, 548);
            numReadingScore.Margin = new Padding(4);
            numReadingScore.Name = "numReadingScore";
            numReadingScore.Size = new Size(102, 31);
            numReadingScore.TabIndex = 12;
            // 
            // numWritingScore
            // 
            numWritingScore.Location = new Point(898, 589);
            numWritingScore.Margin = new Padding(4);
            numWritingScore.Name = "numWritingScore";
            numWritingScore.Size = new Size(102, 31);
            numWritingScore.TabIndex = 13;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(798, 506);
            textBox1.Margin = new Padding(4);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(92, 31);
            textBox1.TabIndex = 14;
            textBox1.Text = "Math:";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(798, 546);
            textBox2.Margin = new Padding(4);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(92, 31);
            textBox2.TabIndex = 15;
            textBox2.Text = "Reading:";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(798, 589);
            textBox3.Margin = new Padding(4);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(92, 31);
            textBox3.TabIndex = 16;
            textBox3.Text = "Writing:";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(798, 299);
            textBox4.Margin = new Padding(4);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(92, 31);
            textBox4.TabIndex = 17;
            textBox4.Text = "Gender:";
            // 
            // textBox5
            // 
            textBox5.Location = new Point(798, 340);
            textBox5.Margin = new Padding(4);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(92, 31);
            textBox5.TabIndex = 18;
            textBox5.Text = "Race:";
            // 
            // textBox6
            // 
            textBox6.Location = new Point(798, 382);
            textBox6.Margin = new Padding(4);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(92, 31);
            textBox6.TabIndex = 19;
            textBox6.Text = "Parent Edu:";
            // 
            // textBox7
            // 
            textBox7.Location = new Point(798, 424);
            textBox7.Margin = new Padding(4);
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(92, 31);
            textBox7.TabIndex = 20;
            textBox7.Text = "Lunch:";
            // 
            // textBox8
            // 
            textBox8.Location = new Point(798, 465);
            textBox8.Margin = new Padding(4);
            textBox8.Name = "textBox8";
            textBox8.Size = new Size(92, 31);
            textBox8.TabIndex = 21;
            textBox8.Text = "Test Prep:";
            // 
            // btnDeleteStudent
            // 
            btnDeleteStudent.Location = new Point(1008, 343);
            btnDeleteStudent.Margin = new Padding(4);
            btnDeleteStudent.Name = "btnDeleteStudent";
            btnDeleteStudent.Size = new Size(118, 35);
            btnDeleteStudent.TabIndex = 5;
            btnDeleteStudent.Text = "Delete Student";
            btnDeleteStudent.UseVisualStyleBackColor = false;
            btnDeleteStudent.Click += btnDeleteStudent_Click;
            // 
            // btnClearResults
            // 
            btnClearResults.Location = new Point(635, 340);
            btnClearResults.Name = "btnClearResults";
            btnClearResults.Size = new Size(108, 34);
            btnClearResults.TabIndex = 22;
            btnClearResults.Text = "Clear Results";
            btnClearResults.UseVisualStyleBackColor = true;
            btnClearResults.Click += btnClearResults_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1245, 706);
            Controls.Add(btnClearResults);
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
            Controls.Add(btnDeleteStudent);
            Controls.Add(btnCreateDatabase);
            Controls.Add(searchDataGridView);
            Controls.Add(searchTextBox);
            Controls.Add(searchButton);
            Margin = new Padding(4, 5, 4, 5);
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
        private Button btnDeleteStudent;
        private Button btnClearResults;
    }
}
