using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace StudentPerformanceApp
{
    public partial class Form1 : Form
    {
        private string connectionString = "server=localhost;database=SP_DB;user=root;password=password;";

        public Form1()
        {
            InitializeComponent();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string searchQuery = searchTextBox.Text;  // Get the text entered in the search box

            if (string.IsNullOrEmpty(searchQuery))
            {
                MessageBox.Show("Please enter a student's name or ID.");
                return;
            }

            // Call the search function
            DataTable searchResults = SearchStudents(searchQuery);

            // Display the results in the DataGridView (or ListBox)
            searchDataGridView.DataSource = searchResults;  // DataGridView 
        }

        // Function to search students by name or ID
        private DataTable SearchStudents(string searchQuery)
        {
            DataTable dt = new DataTable();

            try
            {
                // Connect to the MariaDB database
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Check if the search query is a numeric value (student ID)
                    bool isNumeric = int.TryParse(searchQuery, out int studentId);
                    string query;

                    if (isNumeric)
                    {
                        // Search by student ID
                        query = "SELECT * FROM StudentPerformance WHERE student_id = @studentId";
                    }
                    else
                    {
                        // Search by name using LIKE
                        query = "SELECT * FROM StudentPerformance WHERE name LIKE @searchQuery";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (isNumeric)
                        {
                            // Add student_id parameter if searching by ID
                            cmd.Parameters.AddWithValue("@studentId", studentId);
                        }
                        else
                        {
                            // Add name parameter with wildcards for LIKE search
                            cmd.Parameters.AddWithValue("@searchQuery", "%" + searchQuery + "%");
                        }

                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            return dt;
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
