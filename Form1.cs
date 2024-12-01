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

            // error catch for inserting student record
            try
            {
                //insert
                await _studentCollection.InsertOneAsync(newStudent);

                MessageBox.Show($"Student with ID {studentId} added successfully."); //debug msg

                // verification
                var filter = Builders<BsonDocument>.Filter.Eq("student_id", studentId);
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
            catch (Exception ex) //error catch
            {
                MessageBox.Show($"Error adding student: {ex.Message}");
            }
        }


        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(searchTextBox.Text, out int studentId)) //copium input validation
            {
                MessageBox.Show("Please enter a valid student ID.");
                return; 
            }

            var filter = Builders<BsonDocument>.Filter.Eq("student_id", searchTextBox.Text);

            var update = Builders<BsonDocument>.Update
                .Set("gender", txtGender.Text)
                .Set("race_ethnicity", txtRace.Text)
                .Set("parental_education", txtParentEdu.Text)
                .Set("lunch", txtLunch.Text)
                .Set("test_preparation", txtTestPrep.Text)
                .Set("math_score", (int)numMathScore.Value)
                .Set("reading_score", (int)numReadingScore.Value)
                .Set("writing_score", (int)numWritingScore.Value)
                .Set("student_average", ((int)numMathScore.Value + (int)numReadingScore.Value + (int)numWritingScore.Value) / 3);

            var result = await _studentCollection.UpdateOneAsync(filter, update);

            MessageBox.Show(result.ModifiedCount > 0 ? "Student updated successfully." : "No student found.");
        }

        private async void btnDeleteStudent_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(searchTextBox.Text, out int studentId)) //more cope
            {
                MessageBox.Show("Please enter a valid student ID.");
                return;
            }

            var filter = Builders<BsonDocument>.Filter.Eq("student_id", searchTextBox.Text);
            var result = await _studentCollection.DeleteOneAsync(filter);

            MessageBox.Show(result.DeletedCount > 0 ? "Student deleted successfully." : "No student found.");
        }

        //moved clear down here
        private void btnClearResults_Click(object sender, EventArgs e)
        {
            searchDataGridView.DataSource = null; // clr DataGridView
            searchTextBox.Clear(); // clr the search text box
            MessageBox.Show("Search results cleared.");
        }

        // pattern search find under 50 avg
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
