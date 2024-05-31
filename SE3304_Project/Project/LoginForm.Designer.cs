namespace DataBaseProject
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button Login;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.Login = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(100, 50);
            this.textBox1.Size = new System.Drawing.Size(150, 20);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(100, 80);
            this.textBox3.Size = new System.Drawing.Size(150, 20);
            this.textBox3.PasswordChar = '*';
            // 
            // Login
            // 
            this.Login.Location = new System.Drawing.Point(100, 110);
            this.Login.Size = new System.Drawing.Size(75, 23);
            this.Login.Text = "Login";
            this.Login.Click += new System.EventHandler(this.Login_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(20, 50);
            this.label1.Text = "Username:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(20, 80);
            this.label2.Text = "Password:";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(20, 10);
            this.label4.Text = "Login Form";
            // 
            // LoginForm
            // 
            this.ClientSize = new System.Drawing.Size(293, 185);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.Login);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Name = "LoginForm";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
