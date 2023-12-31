﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Diagnostics;

namespace TeacherTool
{
    public partial class TeacherRegister : Form
    {
        public Form owner;
        public TeacherRegister()
        {
            InitializeComponent();
        }

        private async void Submit_Click(object sender, EventArgs e)
        {
            if (!email.Text.Contains("@") || email.Text.Length < 5)
                return;
            HttpClient client = new HttpClient();
            var values = new Dictionary<string, string>()
            {
                { "email", email.Text}
            };
            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("http://158.140.244.74:3000/registerTeacher", content);
            Debug.Write(response.StatusCode);
            if ((int)response.StatusCode != 200)
            {
                info.Text = "Registration Failed";
                return;
            }
            TokenTest checkToken = new TokenTest();
            checkToken.owner = owner;
            checkToken.Show();
            
            //
            this.Close();
        }

        private void Recover_Click(object sender, EventArgs e)
        {
            TeacherRecovery t = new TeacherRecovery();
            t.owner = owner;
            t.Show();
            this.Close();
        }

        private void TeacherRegister_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Submit_Click(sender, new EventArgs());
        }
    }
}
