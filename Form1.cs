using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Diagnostics;
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

        //purely to make it easier, load csv from C:/
        private void btnCreateDatabase_Click(object sender, EventArgs e) //this button creates/initializes the database if it doesnt already exist, we using darrence format
        {
            string createTableQuery = @"
            DROP TABLE StudentPerformance;
            DROP TABLE StudentExtra;

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
                    MySqlCommand cmd = new MySqlCommand("add_student", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters for the stored procedure
                    cmd.Parameters.AddWithValue("@student_gender", gender);
                    cmd.Parameters.AddWithValue("@student_race", race);
                    cmd.Parameters.AddWithValue("@student_parentlevel", parentEdu);
                    cmd.Parameters.AddWithValue("@student_lunch", lunch);
                    cmd.Parameters.AddWithValue("@student_testprep", testPrep);
                    cmd.Parameters.AddWithValue("@student_math", mathScore);
                    cmd.Parameters.AddWithValue("@student_reading", readingScore);
                    cmd.Parameters.AddWithValue("@student_writing", writingScore);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student added successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
//end of code