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
            btnDeleteStudent = new Button();
            ((System.ComponentModel.ISupportInitialize)searchDataGridView).BeginInit();
            SuspendLayout();
            // 
            // searchButton
            // 
            searchButton.Location = new Point(713, 418);
            searchButton.Name = "searchButton";
            searchButton.Size = new Size(75, 23);
            searchButton.TabIndex = 0;
            searchButton.Text = "Search";
            searchButton.UseVisualStyleBackColor = true;
            searchButton.Click += searchButton_Click;
            // 
            // searchTextBox
            // 
            searchTextBox.Location = new Point(489, 389);
            searchTextBox.Name = "searchTextBox";
            searchTextBox.Size = new Size(299, 23);
            searchTextBox.TabIndex = 1;
            searchTextBox.TextChanged += searchTextBox_TextChanged;
            // 
            // searchDataGridView
            // 
            searchDataGridView.AllowUserToOrderColumns = true;
            searchDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            searchDataGridView.Location = new Point(12, 12);
            searchDataGridView.Name = "searchDataGridView";
            searchDataGridView.Size = new Size(776, 161);
            searchDataGridView.TabIndex = 3;
            // 
            // btnCreateDatabase
            // 
            btnCreateDatabase.Location = new Point(12, 179);
            btnCreateDatabase.Name = "btnCreateDatabase";
            btnCreateDatabase.Size = new Size(123, 23);
            btnCreateDatabase.TabIndex = 4;
            btnCreateDatabase.Text = "Initialize Database";
            btnCreateDatabase.UseVisualStyleBackColor = true;
            btnCreateDatabase.Click += btnCreateDatabase_Click;
            // 
            // btnDeleteStudent
            // 
            btnDeleteStudent.Location = new Point(632, 418);
            btnDeleteStudent.Name = "btnDeleteStudent";
            btnDeleteStudent.Size = new Size(75, 23);
            btnDeleteStudent.TabIndex = 5;
            btnDeleteStudent.Text = "Delete Student";
            btnDeleteStudent.UseVisualStyleBackColor = false;
            btnDeleteStudent.Click += btnDeleteStudent_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnDeleteStudent);
            Controls.Add(btnCreateDatabase);
            Controls.Add(searchDataGridView);
            Controls.Add(searchTextBox);
            Controls.Add(searchButton);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)searchDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button searchButton;
        private TextBox searchTextBox;
        private DataGridView searchDataGridView;
        private Button btnCreateDatabase;
        private Button btnDeleteStudent;
    }
}
