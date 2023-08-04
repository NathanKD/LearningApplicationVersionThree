namespace TeacherTool
{
    partial class TeacherRegister
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Welcome = new System.Windows.Forms.Label();
            this.Submit = new System.Windows.Forms.Button();
            this.info = new System.Windows.Forms.Label();
            this.email_text = new System.Windows.Forms.Label();
            this.email = new System.Windows.Forms.TextBox();
            this.Recover = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Welcome
            // 
            this.Welcome.AutoSize = true;
            this.Welcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Welcome.Location = new System.Drawing.Point(12, 9);
            this.Welcome.Name = "Welcome";
            this.Welcome.Size = new System.Drawing.Size(191, 20);
            this.Welcome.TabIndex = 0;
            this.Welcome.Text = "Welcome New Teacher";
            // 
            // Submit
            // 
            this.Submit.Location = new System.Drawing.Point(12, 116);
            this.Submit.Name = "Submit";
            this.Submit.Size = new System.Drawing.Size(260, 20);
            this.Submit.TabIndex = 1;
            this.Submit.Text = "Submit";
            this.Submit.UseVisualStyleBackColor = true;
            this.Submit.Click += new System.EventHandler(this.Submit_Click);
            // 
            // info
            // 
            this.info.AutoSize = true;
            this.info.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.info.Location = new System.Drawing.Point(24, 29);
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(157, 15);
            this.info.TabIndex = 2;
            this.info.Text = "Create a New Account Here";
            // 
            // email_text
            // 
            this.email_text.AutoSize = true;
            this.email_text.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.email_text.Location = new System.Drawing.Point(13, 57);
            this.email_text.Name = "email_text";
            this.email_text.Size = new System.Drawing.Size(42, 15);
            this.email_text.TabIndex = 3;
            this.email_text.Text = "Email:";
            // 
            // email
            // 
            this.email.Location = new System.Drawing.Point(12, 75);
            this.email.Name = "email";
            this.email.Size = new System.Drawing.Size(260, 20);
            this.email.TabIndex = 4;
            // 
            // Recover
            // 
            this.Recover.AutoSize = true;
            this.Recover.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Recover.ForeColor = System.Drawing.SystemColors.Highlight;
            this.Recover.Location = new System.Drawing.Point(78, 139);
            this.Recover.Name = "Recover";
            this.Recover.Size = new System.Drawing.Size(134, 13);
            this.Recover.TabIndex = 5;
            this.Recover.Text = "Already Have an account?";
            this.Recover.Click += new System.EventHandler(this.Recover_Click);
            // 
            // TeacherRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 157);
            this.Controls.Add(this.Recover);
            this.Controls.Add(this.email);
            this.Controls.Add(this.email_text);
            this.Controls.Add(this.info);
            this.Controls.Add(this.Submit);
            this.Controls.Add(this.Welcome);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximumSize = new System.Drawing.Size(300, 200);
            this.MinimumSize = new System.Drawing.Size(300, 200);
            this.Name = "TeacherRegister";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Register";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Welcome;
        private System.Windows.Forms.Button Submit;
        private System.Windows.Forms.Label info;
        private System.Windows.Forms.Label email_text;
        private System.Windows.Forms.TextBox email;
        private System.Windows.Forms.Label Recover;
    }
}