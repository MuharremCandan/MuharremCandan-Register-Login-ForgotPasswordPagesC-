using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginScreen
{
    public partial class PasswordPage : Form
    {
        public PasswordPage()
        {
            InitializeComponent();
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            SmtpClient smtp = new SmtpClient(); 
            smtp.Credentials = new System.Net.NetworkCredential("3muharremcandan@gmail.com", "ozan122001");
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            MailMessage ePosta = new MailMessage();

            DB db = new DB();
            string query = $"select password from users where user_mail='{txtMail.Text}'";
            NpgsqlDataReader reader = db.Reader(query);
            while (reader.Read())
            {
                ePosta.Body = $"Sifre : {reader[0].ToString()}";
               
            }



           
            ePosta.To.Add($"{txtMail.Text}");
            ePosta.Subject = "Konu";
           
            ePosta.From = new MailAddress("3muharremcandan@gmail.com");

            // smtp.SendAsync(ePosta, (object)ePosta);
            smtp.Send(ePosta);



            LoginPage logPAge = new LoginPage();
            logPAge.Show();
            this.Hide();
        }
    }
}
