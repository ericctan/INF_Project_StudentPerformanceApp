using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

//IF YOU SEE THIS MESSAGE, YOU'VE MIGRATED TO NOSQL BRANCH
//CERTAIN FUNCTIONS NEED TO BE FIXED, CURRENT VERSION NOT FUNCTIONING
//rn loading csv into db works, but the prog cant read

namespace StudentPerformanceApp
{
    public partial class Form1 : Form
    {
        private IMongoDatabase _database;
        private IMongoCollection<BsonDocument> _studentCollection;
        private IMongoCollection<BsonDocument> _extraCollection;

        public Form1()
        {
            InitializeComponent();
            ConnectToMongoDB();
        }

        // db connect
        private void ConnectToMongoDB()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            _database = client.GetDatabase("SP_DB");
            _studentCollection = _database.GetCollection<BsonDocument>("StudentPerformance");
            _extraCollection = _database.GetCollection<BsonDocument>("StudentExtra");
        }
        
        //need to explicitly turn into index, may want to add more indexes depending on our use case
        private void CreateIndexes()
        {
            var studentIndexKeys = Builders<BsonDocument>.IndexKeys.Ascending("student_id");
            _studentCollection.Indexes.CreateOne(new CreateIndexModel<BsonDocument>(studentIndexKeys));

            var extraIndexKeys = Builders<BsonDocument>.IndexKeys.Ascending("student_id");
            _extraCollection.Indexes.CreateOne(new CreateIndexModel<BsonDocument>(extraIndexKeys));

            MessageBox.Show("Student ID indexes created successfully.");
        }

        // load db
        private async void btnCreateDatabase_Click(object sender, EventArgs e)
        {
            await _database.DropCollectionAsync("StudentPerformance");
            await _database.DropCollectionAsync("StudentExtra");

            _studentCollection = _database.GetCollection<BsonDocument>("StudentPerformance");
            _extraCollection = _database.GetCollection<BsonDocument>("StudentExtra");

            LoadStudentPerformanceData("C:/StudentsPerformance.csv");
            LoadStudentExtraData("C:/student-por.csv");
            CreateIndexes(); //index student_id here 

            MessageBox.Show("Database and collections created successfully.");
        }

        private void LoadStudentPerformanceData(string filePath)
        {
            var csvLines = System.IO.File.ReadAllLines(filePath);
            for (int i = 1; i < csvLines.Length; i++) //also skips header
            {
                var values = csvLines[i].Split(',');
                var document = new BsonDocument
                {
                    { "student_id", values[0] },
                    { "gender", values[1] },
                    { "race_ethnicity", values[2] },
                    { "parental_education", values[3] },
                    { "lunch", values[4] },
                    { "test_preparation", values[5] },
                    { "math_score", int.Parse(values[6]) },
                    { "reading_score", int.Parse(values[7]) },
                    { "writing_score", int.Parse(values[8]) },
                    { "student_average", (int.Parse(values[6]) + int.Parse(values[7]) + int.Parse(values[8])) / 3 }

                };
                //for indexing, need fix
                //await _studentCollection.Indexes.CreateOneAsync(new CreateIndexModel<BsonDocument>(Builders<BsonDocument>.IndexKeys.Ascending("math_score")));

                _studentCollection.InsertOne(document);
            }
        }

        private void LoadStudentExtraData(string filePath)
        {
            var csvLines = System.IO.File.ReadAllLines(filePath);
            for (int i = 1; i < csvLines.Length; i++)
            {
                var values = csvLines[i].Split(',');
                var document = new BsonDocument
                {
                    { "student_id", values[0] }, //saving this as a student id value for now
                    { "school", values[1] },
                    { "sex", values[2] },
                    { "age", int.Parse(values[3]) },
                    { "address", values[4] },
                    { "famsize", values[5] },
                    { "Pstatus", values[6] },
                    { "Medu", int.Parse(values[7]) },
                    { "Fedu", int.Parse(values[8]) },
                    { "Mjob", values[9] },
                    { "Fjob", values[10] },
                    { "reason", values[11] },
                    { "guardian", values[12] },
                    { "traveltime", int.Parse(values[13]) },
                    { "studytime", int.Parse(values[14]) },
                    { "failures", int.Parse(values[15]) },
                    { "schoolsup", values[16] },
                    { "famsup", values[17] },
                    { "paid", values[18] },
                    { "activities", values[19] },
                    { "nursery", values[20] },
                    { "higher", values[21] },
                    { "internet", values[22] },
                    { "romantic", values[23] },
                    { "famrel", int.Parse(values[24]) },
                    { "freetime", int.Parse(values[25]) },
                    { "goout", int.Parse(values[26]) },
                    { "Dalc", int.Parse(values[27]) },
                    { "Walc", int.Parse(values[28]) },
                    { "health", int.Parse(values[29]) },
                    { "absences", int.Parse(values[30]) },
                    { "G1", int.Parse(values[31]) },
                    { "G2", int.Parse(values[32]) },
                    { "G3", int.Parse(values[33]) },
                    { "parent_average", (int.Parse(values[7]) + int.Parse(values[8])) / 2 } //temp
            };
                _extraCollection.InsertOne(document);
            }
        }

        // search
        private async void searchButton_Click(object sender, EventArgs e)
        {
            string searchQuery = searchTextBox.Text.Trim();
            if (string.IsNullOrEmpty(searchQuery))
            {
                MessageBox.Show("Please enter a student's ID or name.");
                return;
            }

            var filter = Builders<BsonDocument>.Filter.Eq("student_id", searchQuery); //may need input validation for large int, also need make sure check int or string
            var results = await _studentCollection.Find(filter).ToListAsync();

            searchDataGridView.DataSource = ConvertToDataTable(results);

            if (results.Any())
            {
                searchDataGridView.DataSource = ConvertToDataTable(results);
            }
            else
            {
                MessageBox.Show("No matching records found.");
            }
        }


        //dont remove for now
        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(searchTextBox.Text))
            {
                Console.WriteLine($"Text changed: {searchTextBox.Text}");
            }
        }

        private DataTable ConvertToDataTable(List<BsonDocument> documents)
        {
            var dt = new DataTable();
            foreach (var doc in documents)
            {
                foreach (var element in doc.Elements)
                {
                    if (!dt.Columns.Contains(element.Name))
                        dt.Columns.Add(element.Name);
                }
                var row = dt.NewRow();
                foreach (var element in doc.Elements)
                {
                    row[element.Name] = element.Value;
                }
                dt.Rows.Add(row);
            }
            return dt;
        }

        private async void btnAddStudent_Click(object sender, EventArgs e)
        {
            string studentId = searchTextBox.Text.Trim();

            if (string.IsNullOrEmpty(studentId))
            {
                MessageBox.Show("Please enter a valid student ID.");
                return;
            }

            // Check for duplicate student_id
            var filter = Builders<BsonDocument>.Filter.Eq("student_id", studentId);
            var existingStudent = await _studentCollection.Find(filter).FirstOrDefaultAsync();

            if (existingStudent != null) // If a student with the same ID already exists
            {
                MessageBox.Show($"A student with ID {studentId} already exists. Please use a different ID.");
                return;
            }

            // Create the new student document
            var newStudent = new BsonDocument
            {
                { "student_id", studentId },  // Ensure this ID is unique
                { "gender", txtGender.Text.Trim() },
                { "race_ethnicity", txtRace.Text.Trim() },
                { "parental_education", txtParentEdu.Text.Trim() },
                { "lunch", txtLunch.Text.Trim() },
                { "test_preparation", txtTestPrep.Text.Trim() },
                { "math_score", (int)numMathScore.Value },
                { "reading_score", (int)numReadingScore.Value },
                { "writing_score", (int)numWritingScore.Value },
                { "student_average", ((int)numMathScore.Value + (int)numReadingScore.Value + (int)numWritingScore.Value) / 3 }
            };

            // Attempt to insert the new student into the database
            try
            {
                await _studentCollection.InsertOneAsync(newStudent);

                MessageBox.Show($"Student with ID {studentId} added successfully."); // Debug message

                // Verification: Retrieve and display the newly added student
                var results = await _studentCollection.Find(filter).ToListAsync();

                if (results.Any())
                {
                    searchDataGridView.DataSource = ConvertToDataTable(results);
                }
                else
                {
                    MessageBox.Show("Student was added but not found during verification.");
                }
            }
            catch (Exception ex) // Catch any errors during insertion
            {
                MessageBox.Show($"Error adding student: {ex.Message}");
            }
        }



        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            // Validate the student ID input
            if (string.IsNullOrWhiteSpace(searchTextBox.Text))
            {
                MessageBox.Show("Please enter a valid student ID.");
                return;
            }

            string studentId = searchTextBox.Text.Trim(); // Get the ID as a string

            // Create a filter for the specified student ID
            var filter = Builders<BsonDocument>.Filter.Eq("student_id", studentId);

            try
            {
                // Fetch the existing record
                var existingStudent = await _studentCollection.Find(filter).FirstOrDefaultAsync();

                if (existingStudent == null)
                {
                    MessageBox.Show("No student found with the specified ID.");
                    return;
                }

                // Check if the current input matches the existing record
                bool isIdentical =
                    existingStudent["gender"].AsString == txtGender.Text.Trim() &&
                    existingStudent["race_ethnicity"].AsString == txtRace.Text.Trim() &&
                    existingStudent["parental_education"].AsString == txtParentEdu.Text.Trim() &&
                    existingStudent["lunch"].AsString == txtLunch.Text.Trim() &&
                    existingStudent["test_preparation"].AsString == txtTestPrep.Text.Trim() &&
                    existingStudent["math_score"].AsInt32 == (int)numMathScore.Value &&
                    existingStudent["reading_score"].AsInt32 == (int)numReadingScore.Value &&
                    existingStudent["writing_score"].AsInt32 == (int)numWritingScore.Value;

                if (isIdentical)
                {
                    MessageBox.Show("Identical record. Please make changes to the fields before updating.");
                    return;
                }

                // Create the update definition
                var update = Builders<BsonDocument>.Update
                    .Set("gender", txtGender.Text.Trim())
                    .Set("race_ethnicity", txtRace.Text.Trim())
                    .Set("parental_education", txtParentEdu.Text.Trim())
                    .Set("lunch", txtLunch.Text.Trim())
                    .Set("test_preparation", txtTestPrep.Text.Trim())
                    .Set("math_score", (int)numMathScore.Value)
                    .Set("reading_score", (int)numReadingScore.Value)
                    .Set("writing_score", (int)numWritingScore.Value)
                    .Set("student_average", ((int)numMathScore.Value + (int)numReadingScore.Value + (int)numWritingScore.Value) / 3);

                // Execute the update
                var result = await _studentCollection.UpdateOneAsync(filter, update);

                if (result.ModifiedCount > 0)
                {
                    MessageBox.Show("Student updated successfully.");

                    // Fetch and display the updated record
                    var updatedRecord = await _studentCollection.Find(filter).ToListAsync();
                    searchDataGridView.DataSource = ConvertToDataTable(updatedRecord);
                }
                else
                {
                    MessageBox.Show("No changes were made to the record.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating student: {ex.Message}");
            }
        }



        private async void btnDeleteStudent_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchTextBox.Text)) // Validate input
            {
                MessageBox.Show("Please enter a valid student ID.");
                return;
            }

            string studentId = searchTextBox.Text.Trim(); // Get student ID

            // Create a filter for the student ID
            var filter = Builders<BsonDocument>.Filter.Eq("student_id", studentId);

            try
            {
                // Attempt to delete the student
                var result = await _studentCollection.DeleteOneAsync(filter);

                if (result.DeletedCount > 0)
                {
                    MessageBox.Show("Student deleted successfully.");

                    // Refresh the DataGridView by showing all remaining students
                    var allStudents = await _studentCollection.Find(new BsonDocument()).ToListAsync();
                    searchDataGridView.DataSource = ConvertToDataTable(allStudents);
                }
                else
                {
                    MessageBox.Show("No student found with the specified ID.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting student: {ex.Message}");
            }
        }


        //moved clear down here
        private void btnClearResults_Click(object sender, EventArgs e)
        {
            searchDataGridView.DataSource = null; // clr DataGridView
            searchTextBox.Clear(); // clr the search text box
            MessageBox.Show("Search results cleared.");
        }

        // pattern search find under threshold
        private async void btnPatternSearch_Click(object sender, EventArgs e)
        {
            int threshold;
            if (!int.TryParse(searchTextBox.Text, out threshold))
            {
                MessageBox.Show("Please enter a valid threshold in the field above.");
                return;
            }

            var filter = Builders<BsonDocument>.Filter.Lt("student_average", threshold);
            var results = await _studentCollection.Find(filter).ToListAsync();

            if (results.Count == 0)
            {
                MessageBox.Show("No students found matching the pattern/threshold.");
            }
            else
            {
                searchDataGridView.DataSource = ConvertToDataTable(results);
            }
        }

        // advanced join, unused atm
        private async void btnAdvancedJoin_Click(object sender, EventArgs e)
        {
            var aggregate = await _studentCollection.Aggregate()
                .Lookup("StudentExtra", "student_id", "student_id", "extraDetails")
                .ToListAsync();

            searchDataGridView.DataSource = ConvertToDataTable(aggregate);
        }
    }
}
