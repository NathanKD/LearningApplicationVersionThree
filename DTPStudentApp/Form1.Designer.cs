using System.Windows.Forms;

namespace DTPStudentApp
{
    partial class StudentApp
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
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.codeBlock = new System.Windows.Forms.RichTextBox();
            this.compilerBlock = new System.Windows.Forms.RichTextBox();
            this.infoLessonContainer = new System.Windows.Forms.SplitContainer();
            this.lessonContent = new System.Windows.Forms.Label();
            this.lessonTitle = new System.Windows.Forms.Label();
            this.hideLessonButton = new System.Windows.Forms.Button();
            this.buttonList = new System.Windows.Forms.ListView();
            this.Run = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.infoLessonContainer)).BeginInit();
            this.infoLessonContainer.Panel1.SuspendLayout();
            this.infoLessonContainer.Panel2.SuspendLayout();
            this.infoLessonContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 20);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.infoLessonContainer);
            this.splitContainer1.Size = new System.Drawing.Size(984, 441);
            this.splitContainer1.SplitterDistance = 600;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.AccessibleName = "CodeContainer";
            this.splitContainer2.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer2.Panel1.Controls.Add(this.codeBlock);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.AccessibleName = "ComplierContainer";
            this.splitContainer2.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer2.Panel2.Controls.Add(this.compilerBlock);
            this.splitContainer2.Panel2MinSize = 50;
            this.splitContainer2.Size = new System.Drawing.Size(600, 441);
            this.splitContainer2.SplitterDistance = 231;
            this.splitContainer2.TabIndex = 0;
            // 
            // codeBlock
            // 
            this.codeBlock.AcceptsTab = true;
            this.codeBlock.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.codeBlock.DetectUrls = false;
            this.codeBlock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeBlock.Location = new System.Drawing.Point(0, 0);
            this.codeBlock.Name = "codeBlock";
            this.codeBlock.Size = this.splitContainer2.Panel1.ClientSize;
            this.codeBlock.TabIndex = 0;
            this.codeBlock.Text = "";
            // 
            // compilerBlock
            // 
            this.compilerBlock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.compilerBlock.Location = new System.Drawing.Point(0, 0);
            this.compilerBlock.Name = "compilerBlock";
            this.compilerBlock.ReadOnly = true;
            this.compilerBlock.Size = this.splitContainer2.Panel2.ClientSize;
            this.compilerBlock.TabIndex = 0;
            this.compilerBlock.Text = "";
            // 
            // infoLessonContainer
            // 
            this.infoLessonContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoLessonContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.infoLessonContainer.IsSplitterFixed = true;
            this.infoLessonContainer.Location = new System.Drawing.Point(0, 0);
            this.infoLessonContainer.Name = "infoLessonContainer";
            // 
            // infoLessonContainer.Panel1
            // 
            this.infoLessonContainer.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.infoLessonContainer.Panel1.Controls.Add(this.lessonContent);
            this.infoLessonContainer.Panel1.Controls.Add(this.lessonTitle);
            this.infoLessonContainer.Panel1.Controls.Add(this.hideLessonButton);
            // 
            // infoLessonContainer.Panel2
            // 
            this.infoLessonContainer.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.infoLessonContainer.Panel2.Controls.Add(this.buttonList);
            this.infoLessonContainer.Panel2MinSize = 100;
            this.infoLessonContainer.Size = new System.Drawing.Size(380, 441);
            this.infoLessonContainer.SplitterDistance = 213;
            this.infoLessonContainer.TabIndex = 0;
            // 
            // lessonContent
            // 
            this.lessonContent.AutoSize = true;
            this.lessonContent.Location = new System.Drawing.Point(15, 64);
            this.lessonContent.Name = "lessonContent";
            this.lessonContent.Size = new System.Drawing.Size(0, 13);
            this.lessonContent.TabIndex = 2;
            // 
            // lessonTitle
            // 
            this.lessonTitle.AutoSize = true;
            this.lessonTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lessonTitle.Location = new System.Drawing.Point(12, 13);
            this.lessonTitle.Name = "lessonTitle";
            this.lessonTitle.Size = new System.Drawing.Size(0, 33);
            this.lessonTitle.TabIndex = 1;
            // 
            // hideLessonButton
            // 
            this.hideLessonButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.hideLessonButton.Location = new System.Drawing.Point(188, 175);
            this.hideLessonButton.Name = "hideLessonButton";
            this.hideLessonButton.Size = new System.Drawing.Size(23, 79);
            this.hideLessonButton.TabIndex = 1;
            this.hideLessonButton.Text = ">";
            this.hideLessonButton.UseVisualStyleBackColor = true;
            this.hideLessonButton.Click += new System.EventHandler(this.hideLessonButton_Click);
            // 
            // buttonList
            // 
            this.buttonList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonList.HideSelection = false;
            this.buttonList.Location = new System.Drawing.Point(0, 0);
            this.buttonList.Name = "buttonList";
            this.buttonList.Size = new System.Drawing.Size(163, 441);
            this.buttonList.TabIndex = 1;
            this.buttonList.UseCompatibleStateImageBehavior = false;
            // 
            // Run
            // 
            this.Run.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Run.Location = new System.Drawing.Point(500, 0);
            this.Run.Name = "Run";
            this.Run.Size = new System.Drawing.Size(50, 20);
            this.Run.TabIndex = 1;
            this.Run.Text = "Run";
            this.Run.UseVisualStyleBackColor = true;
            this.Run.Click += new System.EventHandler(this.Run_Click);
            // 
            // StudentApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(984, 461);
            this.Controls.Add(this.Run);
            this.Controls.Add(this.splitContainer1);
            this.Name = "StudentApp";
            this.Padding = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.Text = "Student App";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.infoLessonContainer.Panel1.ResumeLayout(false);
            this.infoLessonContainer.Panel1.PerformLayout();
            this.infoLessonContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.infoLessonContainer)).EndInit();
            this.infoLessonContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer infoLessonContainer;
        private RichTextBox codeBlock;
        private RichTextBox compilerBlock;
        private Button hideLessonButton;
        private ListView buttonList;
        private Button Run;
        private Label lessonTitle;
        private Label lessonContent;
    }
}

