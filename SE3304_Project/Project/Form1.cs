﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DataBaseProject
{
    public partial class GameSuggestor : Form
    {
        private string connectionString = "Server=localhost;Uid=root;Pwd=12345678;Database=GameSuggestor;";
        private string userRole;

        public GameSuggestor(string role)
        {
            InitializeComponent();
            userRole = role;
            LoadGenresIntoComboBox();
            InitializeRoleBasedControls();
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            comboBox3.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
        }

        private void LoadGenresIntoComboBox()
        {
            List<string> genres = GetGenresFromDatabase();
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox1.Items.AddRange(genres.ToArray());
            comboBox2.Items.AddRange(genres.ToArray());
            comboBox3.Items.AddRange(genres.ToArray());
        }

        private List<string> GetGenresFromDatabase()
        {
            List<string> genres = new List<string>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT GenreName FROM Genres";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            genres.Add(reader["GenreName"].ToString());
                        }
                    }
                }
                connection.Close();
            }
            return genres;
        }

        private void InitializeRoleBasedControls()
        {
            // Debugging output
            MessageBox.Show($"User Role: {userRole}");

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedGenre = comboBox1.SelectedItem as string;
            if (!string.IsNullOrEmpty(selectedGenre))
            {
                List<string> suggestions = GetGameSuggestions(selectedGenre);
                int userId = GetUserId();
                LogUserActivity(userId, $"User selected genre: {selectedGenre}");
                MessageBox.Show($"Game Suggestions for {selectedGenre}:\n{string.Join("\n", suggestions)}", "Suggestions");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedGenre = comboBox2.SelectedItem as string;
            if (!string.IsNullOrEmpty(selectedGenre))
            {
                List<string> suggestions = GetGameSuggestions(selectedGenre);
                int userId = GetUserId();
                LogUserActivity(userId, $"User selected genre: {selectedGenre}");
                MessageBox.Show($"Game Suggestions for {selectedGenre}:\n{string.Join("\n", suggestions)}", "Suggestions");
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedGenre = comboBox3.SelectedItem as string;
            if (!string.IsNullOrEmpty(selectedGenre))
            {
                List<string> suggestions = GetGameSuggestions(selectedGenre);
                int userId = GetUserId();
                LogUserActivity(userId, $"User selected genre: {selectedGenre}");
                MessageBox.Show($"Game Suggestions for {selectedGenre}:\n{string.Join("\n", suggestions)}", "Suggestions");
            }
        }

        private List<string> GetGameSuggestions(string genre)
        {
            List<string> suggestions = new List<string>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT g.Title FROM Games g " +
                               "INNER JOIN GameGenres gg ON g.GameID = gg.GameID " +
                               "INNER JOIN Genres ge ON ge.GenreID = gg.GenreID " +
                               "WHERE ge.GenreName = @Genre";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Genre", genre);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            suggestions.Add(reader["Title"].ToString());
                        }
                    }
                }
                connection.Close();
            }
            return suggestions;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string genre1 = comboBox1.SelectedItem as string;
            string genre2 = comboBox2.SelectedItem as string;
            string genre3 = comboBox3.SelectedItem as string;

            List<string> suggestions = GetGameSuggestionsForAllGenres(genre1, genre2, genre3);
            int userId = GetUserId();
            LogUserActivity(userId, "User clicked 'Get Suggestions' button");
            string message = $"Game Suggestions for all selected genres:\n{string.Join("\n", suggestions)}";
            MessageBox.Show(message, "Suggestions");
        }

        private List<string> GetGameSuggestionsForAllGenres(string genre1, string genre2, string genre3)
        {
            List<string> suggestions = new List<string>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT g.Title FROM Games g " +
                               "WHERE EXISTS (SELECT 1 FROM GameGenres gg INNER JOIN Genres ge ON gg.GenreID = ge.GenreID WHERE ge.GenreName = @Genre1 AND gg.GameID = g.GameID) " +
                               "AND EXISTS (SELECT 1 FROM GameGenres gg INNER JOIN Genres ge ON gg.GenreID = ge.GenreID WHERE ge.GenreName = @Genre2 AND gg.GameID = g.GameID) " +
                               "AND EXISTS (SELECT 1 FROM GameGenres gg INNER JOIN Genres ge ON gg.GenreID = ge.GenreID WHERE ge.GenreName = @Genre3 AND gg.GameID = g.GameID)";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Genre1", genre1);
                    command.Parameters.AddWithValue("@Genre2", genre2);
                    command.Parameters.AddWithValue("@Genre3", genre3);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            suggestions.Add(reader["Title"].ToString());
                        }
                    }
                }
                connection.Close();
            }
            if (suggestions.Count == 0)
            {
                MessageBox.Show("No game found in database.");
            }
            return suggestions;
        }

        private int GetUserId()
        {
            return 1; // Sample user ID for testing
        }

        private void LogUserActivity(int userId, string activityDescription)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO UserLogs (UserID, ActivityDescription) VALUES (@UserID, @ActivityDescription)";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userId);
                    command.Parameters.AddWithValue("@ActivityDescription", activityDescription);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int gameID = GetSelectedGameID();
            int userID = GetLoggedInUserID();
            UserRate userRateForm = new UserRate(gameID, userID);
            userRateForm.ShowDialog();
        }

        private int GetSelectedGameID()
        {
            return 1; // Sample game ID for testing
        }

        private int GetLoggedInUserID()
        {
            return 1; // Sample logged in user ID for testing
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (userRole == "Admin")
            {
                AddGame addGameForm = new AddGame(this);
                addGameForm.Show();
            }
            else
            {
                MessageBox.Show("You do not have permission to add a game.", "Access Denied");
            }
        }

        public void RefreshGameData()
        {
            LoadGenresIntoComboBox();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Aggregate aggregateForm = new Aggregate();
            aggregateForm.Show();
        }
    }
}
