using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeacherTool
{
    public partial class TokenTest : Form
    {
        public Form owner;
        public TokenTest()
        {
            InitializeComponent();
        }

        private async void Submit_Click(object sender, EventArgs e)
        {
            
            HttpClient client = new HttpClient();
            var data = new Dictionary<string, string>() {{"token",token.Text}};
            var content = new FormUrlEncodedContent(data);
            var response = await client.PostAsync("https://learnappserver20.nathankleine1.repl.co/submitToken", content);
            int postStatus = (int)response.StatusCode;

            if(postStatus == 200)
            {
                File.WriteAllText("./cache", token.Text);
                owner.Enabled = true;
                (owner as TeacherTool).forceRestart();
                this.Close();
            }
            else
            {
                info.Text = "Sorry But the token you submitted does not match";
                return;
            }
        }
    }
}
