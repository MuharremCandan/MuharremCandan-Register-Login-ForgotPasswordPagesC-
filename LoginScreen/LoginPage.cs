using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginScreen
{
    public partial class LoginPage : Form
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private RegisterPage regPage = new RegisterPage();

        public void button2_Click(object sender, EventArgs e)
        {
            RegisterPage regPage = new RegisterPage();
            regPage.Show();
            this.Hide();
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            string query = $"SELECT count(user_mail) as control FROM users WHERE user_mail = '{txtMail.Text}' AND password = '{txtPassword.Text}'";

            DB db = new DB();

            try
            {
                if (string.IsNullOrWhiteSpace(txtMail.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
                {

                    MessageBox.Show("Boşluk olamaz !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else if (regPage.IsHasWiteSpace(txtMail.Text) || regPage.IsHasWiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Boşluk olamaz !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (regPage.IsValidEmailAddress(txtMail.Text))
                {
                    MessageBox.Show("Geçerli bir mail adresi giriniz !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    NpgsqlDataReader reader = db.Reader(query);
                    while (reader.Read())
                    {
                        if (reader[0].ToString() == "1")
                        {
                            MessageBox.Show("Succesfully login, Welcome !","Information !", MessageBoxButtons.OK,MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("User not found");
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("err" + MessageBox.Show(err.Message) + MessageBoxButtons.OK + MessageBoxIcon.Error);
            }
            finally
            {
                db.Disconnect();
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PasswordPage pPage = new PasswordPage();
            pPage.Show();
            this.Hide();
        }
    }
}
