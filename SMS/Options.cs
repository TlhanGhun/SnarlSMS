using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SMS;

namespace SMS
{
    public partial class Options : Form
    {

         public Options()
        {
            InitializeComponent();

            this.textBox_password.Text = Properties.Settings.Default.password;
            this.textBox_username.Text = Properties.Settings.Default.username;
            this.textBox_phonenumber.Text = Properties.Settings.Default.phonenumber;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void textBox_username_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.username = this.textBox_username.Text;
        }

        private void textBox_password_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.password = this.textBox_password.Text;
        }

        private void textBox_phonenumber_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.phonenumber = this.textBox_phonenumber.Text;

        }




    }
}
