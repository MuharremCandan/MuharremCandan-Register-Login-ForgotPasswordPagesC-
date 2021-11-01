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
using System.Text.RegularExpressions;

namespace LoginScreen
{
    public partial class RegisterPage : Form
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public bool IsHasWiteSpace(string text)
        {
            bool den = false;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i].Equals(' '))
                {
                    den = true;
                }
            }
            return den;

        }

        public  bool IsValidEmailAddress(string emailaddress)
        {

            
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(emailaddress);
            if (match.Success)
                return false;
            else
                return true;
            
        }


        private void button2_Click(object sender, EventArgs e)
        {

            string name = txtName.Text;
            string mail = txtMail.Text;
            string password = txtPassword.Text;
            DB db = new DB();

            string isAlreadyHave = $"select count(user_mail) as control From users where user_mail = '{txtMail.Text}'";

            NpgsqlDataReader dr = db.Reader(isAlreadyHave);

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(mail) || string.IsNullOrWhiteSpace(password))
            {

                MessageBox.Show("Boşluk olamaz !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if (IsHasWiteSpace(name) || IsHasWiteSpace(mail) || IsHasWiteSpace(password))
            {
                MessageBox.Show("Boşluk olamaz !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (IsValidEmailAddress(mail))
            {
                MessageBox.Show("Geçerli bir mail adresi giriniz !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                while (dr.Read())
                {
                    if (dr[0].ToString() == "1")
                    {
                        MessageBox.Show("User already registered!");
                    }
                    else
                    {
                        db.ExecuteQuery($"insert into users (user_mail,user_name,password) values('{txtMail.Text}','{txtName.Text}','{txtPassword.Text}')");

                        MessageBox.Show("Hoşgeldiniz :)","İnformation." , MessageBoxButtons.OK,MessageBoxIcon.Information);
                        LoginPage logPage = new LoginPage();
                        logPage.btnReg.Visible = false;
                        logPage.Show();
                        
                        this.Hide();
                    }
                }

            }



        }
    }
}
