using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.IO;


namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        public string user = "";
        public string email = "";
        public string password = "";
        public bool didLogin = false;
        public bool showLogin = true;

        public Form1()
        {
            InitializeComponent();
            textBox2.PasswordChar = 'x';
            textBox1.MaxLength = 14;
            textBox2.MaxLength = 30;
            button3.Visible = false;
        }

        private List<customer> Users()
        {
            string fileName = @"../../users.json";
            if (File.Exists(fileName))
            {
                var DBusers = JsonConvert.DeserializeObject<List<customer>>
                    (File.ReadAllText(fileName));

                return DBusers;
            }

            return null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // MessageBox.Show("Settings not implemented yet");
            try
            {
                using (Process myProcess = new Process())
                {
                    var p = new Process();
                    p.StartInfo.FileName = textBox3.Text;  // just for example, you can use yours.
                    p.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            var users = Users();
            if (users != null)
            {
                for (int i = 0; i < users.Count; i++)
                {
                    if (users[i].name == username && users[i].password == password)
                    {
                        var mail = users[i].email;
                        userLogin(ref username, ref mail, ref password);
                    }
                }
                if (user == "")
                {
                    MessageBox.Show("Incorrect details");

                }
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }

        private void userLogin(ref string name, ref string mail, ref string pwd)
        {
            user = name;
            email = mail;
            password = pwd;
            didLogin = true;
            loggedIn(ref didLogin);
        }
        private void loginVisible(ref bool show)
        {
            textBox1.Visible = show;
            textBox2.Visible = show;
            button2.Visible = show;
            label2.Visible = show;
            label3.Visible = show;
            button3.Visible = !show;
        }

        private void loggedIn(ref bool isLoggedIn)
        {
            if (isLoggedIn == true)
            {
                showLogin = false;
                loginVisible(ref showLogin);
                label1.Text = "Logged in as " + user + "\n" + email;
            }
            else
            {
                user = "";
                password = "";
                label1.Text = "Not logged in";
                didLogin = false;
                if (didLogin == false)
                {
                    showLogin = true;
                }
                loginVisible(ref showLogin);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool didLogin = false;
            loggedIn(ref didLogin);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var users = Users();
            if (users != null)
            {
                for (int i = 0; i < users.Count; i++)
                {
                    MessageBox.Show(users[i].name + "\n" + users[i].email + "\n" + users[i].password);
                }
            }
        }
    }
}
