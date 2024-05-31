using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DataBaseProject
{
    public partial class LoginForm : Form
    {
        private string connectionString = "Server=localhost;Uid=root;Pwd=12345678;Database=GameSuggestor;";

        public LoginForm()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox3.Text;

            string role = ValidateUser(username, password);
            if (!string.IsNullOrEmpty(role))
            {
                // Login successful
                MessageBox.Show("Login successful.", "Success");

                // Create and show the GameSuggestor form with the role as a parameter
                using (GameSuggestor form1 = new GameSuggestor(role))
                {
                    form1.ShowDialog();
                    // Close the login form
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.", "Login Failed");
            }
        }

        private string ValidateUser(string username, string password)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT Role FROM Users WHERE Username = @Username AND Password = @Password";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);

                        object result = command.ExecuteScalar();

                        connection.Close();

                        if (result != null)
                        {
                            return result.ToString();
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error");
                return null;
            }
        }
    }
}
