namespace LessonCreator
{
    partial class LessonPlanner
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.codeBlock = new System.Windows.Forms.RichTextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.lessonContent = new System.Windows.Forms.RichTextBox();
            this.LessonTitle = new System.Windows.Forms.TextBox();
            this.openLesson = new System.Windows.Forms.Button();
            this.lessonButtons = new System.Windows.Forms.ListView();
            this.newLesson = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.codeBlock);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(908, 512);
            this.splitContainer1.SplitterDistance = 302;
            this.splitContainer1.TabIndex = 0;
            // 
            // codeBlock
            // 
            this.codeBlock.AcceptsTab = true;
            this.codeBlock.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.codeBlock.DetectUrls = false;
            this.codeBlock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeBlock.Enabled = false;
            this.codeBlock.Location = new System.Drawing.Point(0, 0);
            this.codeBlock.Name = "codeBlock";
            this.codeBlock.Size = new System.Drawing.Size(302, 512);
            this.codeBlock.TabIndex = 1;
            this.codeBlock.Text = "";
            this.codeBlock.Leave += new System.EventHandler(this.lessonUpdate);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.lessonContent);
            this.splitContainer2.Panel1.Controls.Add(this.LessonTitle);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.openLesson);
            this.splitContainer2.Panel2.Controls.Add(this.lessonButtons);
            this.splitContainer2.Panel2.Controls.Add(this.newLesson);
            this.splitContainer2.Size = new System.Drawing.Size(602, 512);
            this.splitContainer2.SplitterDistance = 450;
            this.splitContainer2.TabIndex = 0;
            // 
            // lessonContent
            // 
            this.lessonContent.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lessonContent.Enabled = false;
            this.lessonContent.Location = new System.Drawing.Point(0, 47);
            this.lessonContent.Name = "lessonContent";
            this.lessonContent.Size = new System.Drawing.Size(450, 465);
            this.lessonContent.TabIndex = 1;
            this.lessonContent.Text = "";
            this.lessonContent.Leave += new System.EventHandler(this.lessonUpdate);
            // 
            // LessonTitle
            // 
            this.LessonTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.LessonTitle.Enabled = false;
            this.LessonTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LessonTitle.Location = new System.Drawing.Point(0, 0);
            this.LessonTitle.Name = "LessonTitle";
            this.LessonTitle.Size = new System.Drawing.Size(450, 38);
            this.LessonTitle.TabIndex = 0;
            this.LessonTitle.Text = "New Lesson";
            this.LessonTitle.Leave += new System.EventHandler(this.lessonUpdate);
            // 
            // openLesson
            // 
            this.openLesson.Location = new System.Drawing.Point(91, 474);
            this.openLesson.Name = "openLesson";
            this.openLesson.Size = new System.Drawing.Size(57, 38);
            this.openLesson.TabIndex = 3;
            this.openLesson.Text = "Load Lesson";
            this.openLesson.UseVisualStyleBackColor = true;
            this.openLesson.Click += new System.EventHandler(this.openLesson_Click);
            // 
            // lessonButtons
            // 
            this.lessonButtons.HideSelection = false;
            this.lessonButtons.Location = new System.Drawing.Point(0, 0);
            this.lessonButtons.Name = "lessonButtons";
            this.lessonButtons.Size = new System.Drawing.Size(148, 475);
            this.lessonButtons.TabIndex = 2;
            this.lessonButtons.UseCompatibleStateImageBehavior = false;
            // 
            // newLesson
            // 
            this.newLesson.Location = new System.Drawing.Point(0, 474);
            this.newLesson.Name = "newLesson";
            this.newLesson.Size = new System.Drawing.Size(94, 38);
            this.newLesson.TabIndex = 1;
            this.newLesson.Text = "New Lesson";
            this.newLesson.UseVisualStyleBackColor = true;
            this.newLesson.Click += new System.EventHandler(this.newLesson_Click);
            // 
            // LessonPlanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 512);
            this.Controls.Add(this.splitContainer1);
            this.Name = "LessonPlanner";
            this.Text = "Lesson Planner";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LessonPlanner_FormClosing);
            this.Load += new System.EventHandler(this.LessonPlanner_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox codeBlock;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox LessonTitle;
        private System.Windows.Forms.RichTextBox lessonContent;
        private System.Windows.Forms.Button newLesson;
        private System.Windows.Forms.ListView lessonButtons;
        private System.Windows.Forms.Button openLesson;
    }
}

