using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml.Linq;

namespace StudentPerformanceApp
{
    public partial class Form1 : Form
    {
        private string connectionString = "server=localhost;database=SP_DB;user=root;password=password;";

        public Form1()
        {
            InitializeComponent();

        }

        //purely to make it easier, load csv from C:/
        private void btnCreateDatabase_Click(object sender, EventArgs e) //this button creates/initializes the database if it doesnt already exist, we using darrence format
        {
            string createTableQuery = @"
            DROP TABLE IF EXISTS StudentPerformance;
            DROP TABLE IF EXISTS StudentExtra;

            CREATE TABLE IF NOT EXISTS StudentPerformance (
                student_id INT PRIMARY KEY AUTO_INCREMENT,
                gender VARCHAR(10),
                race_ethnicity VARCHAR(10),
                parental_education VARCHAR(100),
                lunch VARCHAR(15),
                test_preparation VARCHAR(10),
                math_score INT,
                reading_score INT,
                writing_score INT
            );
        
            CREATE TABLE IF NOT EXISTS StudentExtra (
                student_id INT PRIMARY KEY AUTO_INCREMENT,
                school VARCHAR(10),
                sex CHAR(1),
                age INT,
                address VARCHAR(5),
                famsize VARCHAR(10),
                Pstatus CHAR(1),
                Medu INT,  -- Mother's education
                Fedu INT,  -- Father's education
                Mjob CHAR(10),   -- mother job
                Fjob CHAR(10),  -- father job
                reason CHAR(10),
                guardian CHAR(10),
                traveltime INT,
                studytime INT,
                failures INT,
                schoolsup CHAR(3),
                famsup CHAR(3),
                paid CHAR(3),
                activities CHAR(3),
                nursery CHAR(3),
                higher CHAR(3),
                internet CHAR(3),
                romantic CHAR(3),
                famrel INT,
                freetime INT,
                goout INT,
                Dalc INT,
                Walc INT,
                health INT,
                absences INT,
                G1 INT,
                G2 INT,
                G3 INT
            );

            LOAD DATA INFILE 'C:/StudentsPerformance.csv'
            INTO TABLE StudentPerformance
            FIELDS TERMINATED BY ',' 
            ENCLOSED BY '""' 
            LINES TERMINATED BY '\n'
            IGNORE 1 LINES
            (gender, race_ethnicity, parental_education, lunch, test_preparation, math_score, reading_score, writing_score);
            
            LOAD DATA INFILE 'C:/student-por.csv'
            INTO TABLE StudentExtra
            FIELDS TERMINATED BY ',' 
            ENCLOSED BY '""' 
            LINES TERMINATED BY '\n'
            IGNORE 1 LINES
            (school, sex, age, address, famsize, Pstatus, Medu, Fedu, Mjob, Fjob, reason, guardian, traveltime, studytime, failures, schoolsup, famsup, paid, activities, nursery, higher, internet, romantic, famrel, freetime, goout, Dalc, Walc, health, absences, G1, G2, G3);
        
            ";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(createTableQuery, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Database and tables created successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void searchButton_Click(object sender, EventArgs e)
        {
            string searchQuery = searchTextBox.Text;  // Get the text entered in the search box

            if (string.IsNullOrEmpty(searchQuery))
            {
                MessageBox.Show("Please enter one or more student IDs.");
                return;
            }

            // Call the search function and pass the search query
            DataTable searchResults = SearchStudents(searchQuery);

            // Display the results in the DataGridView (or ListBox)
            searchDataGridView.DataSource = searchResults;  // DataGridView 
        }
        private DataTable accumulatedResults = new DataTable();
        private DataTable SearchStudents(string searchQuery)
        {
            DataTable dt = new DataTable();

            try
            {
                // Connect to the MariaDB database
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Split the search query by commas to allow multiple student IDs
                    string[] studentIds = searchQuery.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    // Create a SQL query using IN clause for multiple student IDs
                    string query = "SELECT * FROM StudentPerformance WHERE student_id IN (" + string.Join(",", studentIds.Select(id => "@studentId" + id.Trim())) + ")";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Add parameters for each student ID
                        foreach (string id in studentIds)
                        {
                            cmd.Parameters.AddWithValue("@studentId" + id.Trim(), int.Parse(id.Trim()));
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
        private void btnClearResults_Click(object sender, EventArgs e)
        {
            accumulatedResults.Clear();  // Clear the accumulated results
            searchDataGridView.DataSource = null;  // Clear the DataGridView
            MessageBox.Show("Search results cleared.");
        }


        private void btnDeleteStudent_Click(object sender, EventArgs e)
        {
            // Check if the search box is empty or if it's not a valid number
            if (string.IsNullOrEmpty(searchTextBox.Text) || !int.TryParse(searchTextBox.Text, out int studentId))
            {
                // Prompt the user to enter the student ID
                string input = Microsoft.VisualBasic.Interaction.InputBox("Enter the Student ID to delete:", "Delete Student", "");

                // If the user canceled or entered an invalid value, return
                if (string.IsNullOrEmpty(input) || !int.TryParse(input, out studentId))
                {
                    MessageBox.Show("You must enter a valid student ID.", "Error");
                    return;
                }
            }

            string deleteQuery = "DELETE FROM StudentPerformance WHERE student_id = @student_id"; // delete based on id

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(deleteQuery, conn);
                    cmd.Parameters.AddWithValue("@student_id", studentId);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Student deleted successfully.");
                    }
                    else
                    {
                        MessageBox.Show("No student found with that ID.", "Error");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            // for search box
        }


        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            // Collect inputs from UI fields
            string gender = txtGender.Text.Trim();
            string race = txtRace.Text.Trim();
            string parentEdu = txtParentEdu.Text.Trim();
            string lunch = txtLunch.Text.Trim();
            string testPrep = txtTestPrep.Text.Trim();
            int mathScore = (int)numMathScore.Value;
            int readingScore = (int)numReadingScore.Value;
            int writingScore = (int)numWritingScore.Value;

            string insertQuery = "INSERT INTO StudentPerformance (gender,race_ethnicity,parental_education,lunch,test_preparation,math_score,reading_score,writing_score)" +
                  " VALUES (@gender,@race,@parentEdu,@lunch,@testPrep,@mathScore,@readingScore,@writingScore);";

            // Validate inputs
            if (string.IsNullOrEmpty(gender) || string.IsNullOrEmpty(race) ||
                string.IsNullOrEmpty(parentEdu) || string.IsNullOrEmpty(lunch) ||
                string.IsNullOrEmpty(testPrep))
            {
                MessageBox.Show("All fields must be filled.");
                return;
            }

            if (mathScore < 0 || mathScore > 100 ||
                readingScore < 0 || readingScore > 100 ||
                writingScore < 0 || writingScore > 100)
            {
                MessageBox.Show("Scores must be between 0 and 100.");
                return;
            }

            // Call the procedure to add the student
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(insertQuery, conn);

                    // Add parameters for the stored procedure
                    cmd.Parameters.AddWithValue("@gender", gender);
                    cmd.Parameters.AddWithValue("@race", race);
                    cmd.Parameters.AddWithValue("@parentEdu", parentEdu);
                    cmd.Parameters.AddWithValue("@lunch", lunch);
                    cmd.Parameters.AddWithValue("@testPrep", testPrep);
                    cmd.Parameters.AddWithValue("@mathScore", mathScore);
                    cmd.Parameters.AddWithValue("@readingScore", readingScore);
                    cmd.Parameters.AddWithValue("@writingScore", writingScore);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student added successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            // Collect inputs from UI fields
            int studentId = int.Parse(searchTextBox.Text);
            string gender = txtGender.Text.Trim();
            string race = txtRace.Text.Trim();
            string parentEdu = txtParentEdu.Text.Trim();
            string lunch = txtLunch.Text.Trim();
            string testPrep = txtTestPrep.Text.Trim();
            int mathScore = (int)numMathScore.Value;
            int readingScore = (int)numReadingScore.Value;
            int writingScore = (int)numWritingScore.Value;

            string updateQuery = "UPDATE StudentPerformance SET gender=@gender, race_ethnicity=@race, parental_education=@parentEdu, " +
                "lunch=@lunch, test_preparation=@testPrep, math_score=@mathScore, reading_score=@readingScore, writing_score=@writingScore " +
                " WHERE student_id=@student_id";

            // Validate inputs
            if (string.IsNullOrEmpty(gender) || string.IsNullOrEmpty(race) ||
                string.IsNullOrEmpty(parentEdu) || string.IsNullOrEmpty(lunch) ||
                string.IsNullOrEmpty(testPrep))
            {
                MessageBox.Show("All fields must be filled.");
                return;
            }

            if (mathScore < 0 || mathScore > 100 ||
                readingScore < 0 || readingScore > 100 ||
                writingScore < 0 || writingScore > 100)
            {
                MessageBox.Show("Scores must be between 0 and 100.");
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(updateQuery, conn);

                    // Add parameters for the stored procedure
                    cmd.Parameters.AddWithValue("@gender", gender);
                    cmd.Parameters.AddWithValue("@race", race);
                    cmd.Parameters.AddWithValue("@parentEdu", parentEdu);
                    cmd.Parameters.AddWithValue("@lunch", lunch);
                    cmd.Parameters.AddWithValue("@testPrep", testPrep);
                    cmd.Parameters.AddWithValue("@mathScore", mathScore);
                    cmd.Parameters.AddWithValue("@readingScore", readingScore);
                    cmd.Parameters.AddWithValue("@writingScore", writingScore);
                    cmd.Parameters.AddWithValue("@student_id", studentId); // Ensure student_id is added

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student updated successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }
    }

}
