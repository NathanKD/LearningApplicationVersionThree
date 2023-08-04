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
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.codeBlock = new System.Windows.Forms.RichTextBox();
            this.compilerBlock = new System.Windows.Forms.RichTextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.infoLessonContainer = new System.Windows.Forms.SplitContainer();
            this.lessonContent = new System.Windows.Forms.Label();
            this.lessonInfoPanel = new System.Windows.Forms.Panel();
            this.hideLessonButton = new System.Windows.Forms.Button();
            this.lessonInfo = new System.Windows.Forms.RichTextBox();
            this.lessonTitle = new System.Windows.Forms.Label();
            this.buttonList = new System.Windows.Forms.ListView();
            this.joinClass = new System.Windows.Forms.Label();
            this.SelectClass = new System.Windows.Forms.Label();
            this.classUIID = new System.Windows.Forms.Label();
            this.selectLesson = new System.Windows.Forms.Label();
            this.Settings = new System.Windows.Forms.Label();
            this.Run = new System.Windows.Forms.Label();
            this.mainMenu = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.infoLessonContainer)).BeginInit();
            this.infoLessonContainer.Panel1.SuspendLayout();
            this.infoLessonContainer.Panel2.SuspendLayout();
            this.infoLessonContainer.SuspendLayout();
            this.lessonInfoPanel.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
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
            this.codeBlock.TextChanged += new System.EventHandler(this.codeBlock_TextChanged);
            this.codeBlock.KeyDown += new System.Windows.Forms.KeyEventHandler(this.codeBlock_KeyDown);
            // 
            // compilerBlock
            // 
            this.compilerBlock.BackColor = System.Drawing.SystemColors.Window;
            this.compilerBlock.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.compilerBlock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.compilerBlock.Location = new System.Drawing.Point(0, 0);
            this.compilerBlock.Name = "compilerBlock";
            this.compilerBlock.ReadOnly = true;
            this.compilerBlock.Size = this.splitContainer2.Panel2.ClientSize;
            this.compilerBlock.TabIndex = 0;
            this.compilerBlock.Text = "";
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
            this.infoLessonContainer.Panel1.BackColor = System.Drawing.SystemColors.Window;
            this.infoLessonContainer.Panel1.Controls.Add(this.lessonContent);
            this.infoLessonContainer.Panel1.Controls.Add(this.lessonInfoPanel);
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
            this.lessonContent.Location = new System.Drawing.Point(-10, 44);
            this.lessonContent.Name = "lessonContent";
            this.lessonContent.Size = new System.Drawing.Size(0, 13);
            this.lessonContent.TabIndex = 2;
            // 
            // lessonInfoPanel
            // 
            this.lessonInfoPanel.Controls.Add(this.hideLessonButton);
            this.lessonInfoPanel.Controls.Add(this.lessonInfo);
            this.lessonInfoPanel.Controls.Add(this.lessonTitle);
            this.lessonInfoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lessonInfoPanel.Location = new System.Drawing.Point(0, 0);
            this.lessonInfoPanel.Name = "lessonInfoPanel";
            this.lessonInfoPanel.Padding = new System.Windows.Forms.Padding(10, 40, 30, 10);
            this.lessonInfoPanel.Size = new System.Drawing.Size(213, 441);
            this.lessonInfoPanel.TabIndex = 4;
            // 
            // hideLessonButton
            // 
            this.hideLessonButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.hideLessonButton.Location = new System.Drawing.Point(187, 152);
            this.hideLessonButton.Name = "hideLessonButton";
            this.hideLessonButton.Size = new System.Drawing.Size(23, 79);
            this.hideLessonButton.TabIndex = 1;
            this.hideLessonButton.Text = ">";
            this.hideLessonButton.UseVisualStyleBackColor = true;
            this.hideLessonButton.Click += new System.EventHandler(this.hideLessonButton_Click);
            // 
            // lessonInfo
            // 
            this.lessonInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lessonInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lessonInfo.Location = new System.Drawing.Point(10, 40);
            this.lessonInfo.Name = "lessonInfo";
            this.lessonInfo.Size = new System.Drawing.Size(173, 391);
            this.lessonInfo.TabIndex = 3;
            this.lessonInfo.Text = "";
            // 
            // lessonTitle
            // 
            this.lessonTitle.AutoSize = true;
            this.lessonTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lessonTitle.Location = new System.Drawing.Point(13, 7);
            this.lessonTitle.Name = "lessonTitle";
            this.lessonTitle.Size = new System.Drawing.Size(0, 21);
            this.lessonTitle.TabIndex = 1;
            this.lessonTitle.UseCompatibleTextRendering = true;
            // 
            // buttonList
            // 
            this.buttonList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.buttonList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonList.HideSelection = false;
            this.buttonList.Location = new System.Drawing.Point(0, 0);
            this.buttonList.Name = "buttonList";
            this.buttonList.Size = new System.Drawing.Size(163, 441);
            this.buttonList.TabIndex = 1;
            this.buttonList.UseCompatibleStateImageBehavior = false;
            // 
            // joinClass
            // 
            this.joinClass.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.joinClass.AutoSize = true;
            this.joinClass.Location = new System.Drawing.Point(0, 0);
            this.joinClass.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.joinClass.Name = "joinClass";
            this.joinClass.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.joinClass.Size = new System.Drawing.Size(66, 17);
            this.joinClass.TabIndex = 2;
            this.joinClass.Text = "Join Class";
            this.joinClass.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.joinClass.UseCompatibleTextRendering = true;
            this.joinClass.Click += new System.EventHandler(this.joinClass_Click);
            // 
            // SelectClass
            // 
            this.SelectClass.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.SelectClass.AutoSize = true;
            this.SelectClass.Location = new System.Drawing.Point(69, 0);
            this.SelectClass.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.SelectClass.Name = "SelectClass";
            this.SelectClass.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.SelectClass.Size = new System.Drawing.Size(77, 17);
            this.SelectClass.TabIndex = 3;
            this.SelectClass.Text = "Select Class";
            this.SelectClass.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.SelectClass.UseCompatibleTextRendering = true;
            this.SelectClass.Click += new System.EventHandler(this.SelectClass_Click);
            // 
            // classUIID
            // 
            this.classUIID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.classUIID.AutoSize = true;
            this.classUIID.Location = new System.Drawing.Point(821, 2);
            this.classUIID.Name = "classUIID";
            this.classUIID.Size = new System.Drawing.Size(97, 17);
            this.classUIID.TabIndex = 4;
            this.classUIID.Text = "No Class Selected";
            this.classUIID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.classUIID.UseCompatibleTextRendering = true;
            // 
            // selectLesson
            // 
            this.selectLesson.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.selectLesson.AutoSize = true;
            this.selectLesson.Location = new System.Drawing.Point(149, 0);
            this.selectLesson.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.selectLesson.Name = "selectLesson";
            this.selectLesson.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.selectLesson.Size = new System.Drawing.Size(85, 17);
            this.selectLesson.TabIndex = 5;
            this.selectLesson.Text = "Select Lesson";
            this.selectLesson.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.selectLesson.UseCompatibleTextRendering = true;
            this.selectLesson.Click += new System.EventHandler(this.selectLesson_Click);
            // 
            // Settings
            // 
            this.Settings.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Settings.AutoSize = true;
            this.Settings.Location = new System.Drawing.Point(237, 0);
            this.Settings.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.Settings.Name = "Settings";
            this.Settings.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.Settings.Size = new System.Drawing.Size(55, 17);
            this.Settings.TabIndex = 6;
            this.Settings.Text = "Settings";
            this.Settings.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Settings.UseCompatibleTextRendering = true;
            this.Settings.Click += new System.EventHandler(this.Settings_Click);
            // 
            // Run
            // 
            this.Run.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Run.AutoSize = true;
            this.Run.Location = new System.Drawing.Point(498, 3);
            this.Run.Name = "Run";
            this.Run.Size = new System.Drawing.Size(25, 17);
            this.Run.TabIndex = 7;
            this.Run.Text = "Run";
            this.Run.UseCompatibleTextRendering = true;
            this.Run.Click += new System.EventHandler(this.Run_Click);
            // 
            // mainMenu
            // 
            this.mainMenu.AutoSize = true;
            this.mainMenu.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mainMenu.Controls.Add(this.joinClass);
            this.mainMenu.Controls.Add(this.SelectClass);
            this.mainMenu.Controls.Add(this.selectLesson);
            this.mainMenu.Controls.Add(this.Settings);
            this.mainMenu.Location = new System.Drawing.Point(3, 2);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(295, 17);
            this.mainMenu.TabIndex = 8;
            // 
            // StudentApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(984, 461);
            this.Controls.Add(this.Run);
            this.Controls.Add(this.classUIID);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.mainMenu);
            this.MinimumSize = new System.Drawing.Size(575, 500);
            this.Name = "StudentApp";
            this.Padding = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.Text = "Student App";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StudentApp_FormClosed);
            this.Load += new System.EventHandler(this.StudentApp_Load);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.infoLessonContainer.Panel1.ResumeLayout(false);
            this.infoLessonContainer.Panel1.PerformLayout();
            this.infoLessonContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.infoLessonContainer)).EndInit();
            this.infoLessonContainer.ResumeLayout(false);
            this.lessonInfoPanel.ResumeLayout(false);
            this.lessonInfoPanel.PerformLayout();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer infoLessonContainer;
        private RichTextBox codeBlock;
        private RichTextBox compilerBlock;
        private Button hideLessonButton;
        private ListView buttonList;
        private Label lessonTitle;
        private Label lessonContent;
        private Label joinClass;
        private Label SelectClass;
        private Label classUIID;
        private Label selectLesson;
        private Label Settings;
        private Label Run;
        private FlowLayoutPanel mainMenu;
        private RichTextBox lessonInfo;
        private Panel lessonInfoPanel;
    }
}

