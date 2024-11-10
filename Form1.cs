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

        // MongoDB Connection
        private void ConnectToMongoDB()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            _database = client.GetDatabase("SP_DB");
            _studentCollection = _database.GetCollection<BsonDocument>("StudentPerformance");
            _extraCollection = _database.GetCollection<BsonDocument>("StudentExtra");
        }

        // Create Database and Load CSV Data
        private async void btnCreateDatabase_Click(object sender, EventArgs e)
        {
            await _database.DropCollectionAsync("StudentPerformance");
            await _database.DropCollectionAsync("StudentExtra");

            _studentCollection = _database.GetCollection<BsonDocument>("StudentPerformance");
            _extraCollection = _database.GetCollection<BsonDocument>("StudentExtra");

            LoadStudentPerformanceData("C:/StudentsPerformance.csv");
            LoadStudentExtraData("C:/student-por.csv");

            MessageBox.Show("Database and collections created successfully.");
        }

        private void LoadStudentPerformanceData(string filePath)
        {
            var csvLines = System.IO.File.ReadAllLines(filePath);
            for (int i = 1; i < csvLines.Length; i++)
            {
                var values = csvLines[i].Split(',');
                var document = new BsonDocument
                {
                    { "gender", values[0] },
                    { "race_ethnicity", values[1] },
                    { "parental_education", values[2] },
                    { "lunch", values[3] },
                    { "test_preparation", values[4] },
                    { "math_score", int.Parse(values[5]) },
                    { "reading_score", int.Parse(values[6]) },
                    { "writing_score", int.Parse(values[7]) }
                };
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
                    { "school", values[0] },
                    { "sex", values[1] },
                    { "age", int.Parse(values[2]) },
                    { "address", values[3] },
                    { "famsize", values[4] },
                    { "Pstatus", values[5] },
                    { "Medu", int.Parse(values[6]) },
                    { "Fedu", int.Parse(values[7]) },
                    { "Mjob", values[8] },
                    { "Fjob", values[9] },
                    { "reason", values[10] },
                    { "guardian", values[11] },
                    { "traveltime", int.Parse(values[12]) },
                    { "studytime", int.Parse(values[13]) },
                    { "failures", int.Parse(values[14]) },
                    { "schoolsup", values[15] },
                    { "famsup", values[16] },
                    { "paid", values[17] },
                    { "activities", values[18] },
                    { "nursery", values[19] },
                    { "higher", values[20] },
                    { "internet", values[21] },
                    { "romantic", values[22] },
                    { "famrel", int.Parse(values[23]) },
                    { "freetime", int.Parse(values[24]) },
                    { "goout", int.Parse(values[25]) },
                    { "Dalc", int.Parse(values[26]) },
                    { "Walc", int.Parse(values[27]) },
                    { "health", int.Parse(values[28]) },
                    { "absences", int.Parse(values[29]) },
                    { "G1", int.Parse(values[30]) },
                    { "G2", int.Parse(values[31]) },
                    { "G3", int.Parse(values[32]) }
                };
                _extraCollection.InsertOne(document);
            }
        }

        // Search Student Function
        private async void searchButton_Click(object sender, EventArgs e)
        {
            string searchQuery = searchTextBox.Text.Trim();
            if (string.IsNullOrEmpty(searchQuery))
            {
                MessageBox.Show("Please enter a student's ID or name.");
                return;
            }

            var filter = Builders<BsonDocument>.Filter.Eq("student_id", int.Parse(searchQuery));
            var results = await _studentCollection.Find(filter).ToListAsync();

            searchDataGridView.DataSource = ConvertToDataTable(results);
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(searchTextBox.Text))
            {
                Console.WriteLine($"Text changed: {searchTextBox.Text}");
            }
        }

        private void btnClearResults_Click(object sender, EventArgs e)
        {
            searchDataGridView.DataSource = null; // clr DataGridView
            searchTextBox.Clear(); // clr the search text box
            MessageBox.Show("Search results cleared.");
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
            var newStudent = new BsonDocument
            {
                { "gender", txtGender.Text },
                { "race_ethnicity", txtRace.Text },
                { "parental_education", txtParentEdu.Text },
                { "lunch", txtLunch.Text },
                { "test_preparation", txtTestPrep.Text },
                { "math_score", (int)numMathScore.Value },
                { "reading_score", (int)numReadingScore.Value },
                { "writing_score", (int)numWritingScore.Value }
            };

            await _studentCollection.InsertOneAsync(newStudent);
            MessageBox.Show("Student added successfully.");
        }
    

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            var studentId = int.Parse(searchTextBox.Text);
            var filter = Builders<BsonDocument>.Filter.Eq("student_id", studentId);

            var update = Builders<BsonDocument>.Update
                .Set("gender", txtGender.Text)
                .Set("race_ethnicity", txtRace.Text)
                .Set("parental_education", txtParentEdu.Text)
                .Set("lunch", txtLunch.Text)
                .Set("test_preparation", txtTestPrep.Text)
                .Set("math_score", (int)numMathScore.Value)
                .Set("reading_score", (int)numReadingScore.Value)
                .Set("writing_score", (int)numWritingScore.Value);

            var result = await _studentCollection.UpdateOneAsync(filter, update);

            MessageBox.Show(result.ModifiedCount > 0 ? "Student updated successfully." : "No student found.");
        }

        private async void btnDeleteStudent_Click(object sender, EventArgs e)
        {
            var studentId = int.Parse(searchTextBox.Text);
            var filter = Builders<BsonDocument>.Filter.Eq("student_id", studentId);

            var result = await _studentCollection.DeleteOneAsync(filter);

            MessageBox.Show(result.DeletedCount > 0 ? "Student deleted successfully." : "No student found.");
        }

        // Advanced Pattern Search Example
        private async void btnPatternSearch_Click(object sender, EventArgs e)
        {
            var filter = Builders<BsonDocument>.Filter.Lt("student_average", 40);
            var results = await _studentCollection.Find(filter).ToListAsync();

            searchDataGridView.DataSource = ConvertToDataTable(results);
        }
    }
}
