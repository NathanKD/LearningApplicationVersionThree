﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeacherTool
{
    public partial class TeacherRecovery : Form
    {
        public Form owner;
        public TeacherRecovery()
        {
            InitializeComponent();
        }

        private void Welcome_Click(object sender, EventArgs e)
        {

        }

        private void Recover_Click(object sender, EventArgs e)
        {
            TokenTest t = new TokenTest();
            t.owner = owner;
            t.Show();
            this.Close();
        }

        private async void Submit_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            var data = new Dictionary<string, string>() { { "email", email.Text } };
            var content = new FormUrlEncodedContent(data);
            var response = await client.PostAsync("http://158.140.244.74:3000/recoverToken", content);
            int postStatus = (int)response.StatusCode;
            Debug.Write(response.StatusCode);
            if (postStatus != 200)
            {
                info.Text = "Email Not Found";
                return;
            }
            TokenTest t = new TokenTest();
            t.owner = owner;
            t.Show();
            this.Close();
        }

        private void TeacherRecovery_Load(object sender, EventArgs e)
        {

        }

        private void TeacherRecovery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Submit_Click(sender, new EventArgs());
        }
    }
}
