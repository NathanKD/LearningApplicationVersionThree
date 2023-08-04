namespace TeacherTool
{
    partial class TeacherTool
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
            this.Classroom = new System.Windows.Forms.SplitContainer();
            this.studentSelect = new System.Windows.Forms.FlowLayoutPanel();
            this.errorList = new System.Windows.Forms.ListView();
            this.StudentErrors = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.taskList = new System.Windows.Forms.ListView();
            this.studentName = new System.Windows.Forms.Label();
            this.studentWindow = new System.Windows.Forms.PictureBox();
            this.create_class = new System.Windows.Forms.Label();
            this.select_class = new System.Windows.Forms.Label();
            this.classUIID = new System.Windows.Forms.Label();
            this.Profiles = new System.Windows.Forms.Label();
            this.pushLesson = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Classroom)).BeginInit();
            this.Classroom.Panel1.SuspendLayout();
            this.Classroom.Panel2.SuspendLayout();
            this.Classroom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.studentWindow)).BeginInit();
            this.SuspendLayout();
            // 
            // Classroom
            // 
            this.Classroom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Classroom.Location = new System.Drawing.Point(0, 20);
            this.Classroom.Name = "Classroom";
            // 
            // Classroom.Panel1
            // 
            this.Classroom.Panel1.Controls.Add(this.studentSelect);
            // 
            // Classroom.Panel2
            // 
            this.Classroom.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.Classroom.Panel2.Controls.Add(this.errorList);
            this.Classroom.Panel2.Controls.Add(this.taskList);
            this.Classroom.Panel2.Controls.Add(this.studentName);
            this.Classroom.Panel2.Controls.Add(this.studentWindow);
            this.Classroom.Size = new System.Drawing.Size(1261, 639);
            this.Classroom.SplitterDistance = 200;
            this.Classroom.TabIndex = 0;
            // 
            // studentSelect
            // 
            this.studentSelect.AutoScroll = true;
            this.studentSelect.BackColor = System.Drawing.SystemColors.Control;
            this.studentSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.studentSelect.Location = new System.Drawing.Point(0, 0);
            this.studentSelect.Name = "studentSelect";
            this.studentSelect.Size = new System.Drawing.Size(200, 639);
            this.studentSelect.TabIndex = 0;
            // 
            // errorList
            // 
            this.errorList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.StudentErrors});
            this.errorList.HideSelection = false;
            this.errorList.Location = new System.Drawing.Point(13, 426);
            this.errorList.Margin = new System.Windows.Forms.Padding(50);
            this.errorList.Name = "errorList";
            this.errorList.Size = new System.Drawing.Size(669, 210);
            this.errorList.TabIndex = 2;
            this.errorList.UseCompatibleStateImageBehavior = false;
            this.errorList.View = System.Windows.Forms.View.Details;
            // 
            // StudentErrors
            // 
            this.StudentErrors.Text = "StudentErrors";
            this.StudentErrors.Width = 500;
            // 
            // taskList
            // 
            this.taskList.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.taskList.HideSelection = false;
            this.taskList.Location = new System.Drawing.Point(692, 426);
            this.taskList.Name = "taskList";
            this.taskList.Size = new System.Drawing.Size(351, 210);
            this.taskList.TabIndex = 0;
            this.taskList.UseCompatibleStateImageBehavior = false;
            this.taskList.View = System.Windows.Forms.View.List;
            // 
            // studentName
            // 
            this.studentName.AutoSize = true;
            this.studentName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.studentName.Location = new System.Drawing.Point(10, 9);
            this.studentName.Name = "studentName";
            this.studentName.Size = new System.Drawing.Size(209, 25);
            this.studentName.TabIndex = 1;
            this.studentName.Text = "No Student Selected";
            // 
            // studentWindow
            // 
            this.studentWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.studentWindow.Location = new System.Drawing.Point(13, 37);
            this.studentWindow.Name = "studentWindow";
            this.studentWindow.Size = new System.Drawing.Size(1030, 383);
            this.studentWindow.TabIndex = 0;
            this.studentWindow.TabStop = false;
            // 
            // create_class
            // 
            this.create_class.AutoSize = true;
            this.create_class.Location = new System.Drawing.Point(3, 4);
            this.create_class.Name = "create_class";
            this.create_class.Size = new System.Drawing.Size(66, 13);
            this.create_class.TabIndex = 1;
            this.create_class.Text = "Create Class";
            this.create_class.Click += new System.EventHandler(this.createClass);
            // 
            // select_class
            // 
            this.select_class.AutoSize = true;
            this.select_class.Location = new System.Drawing.Point(75, 4);
            this.select_class.Name = "select_class";
            this.select_class.Size = new System.Drawing.Size(65, 13);
            this.select_class.TabIndex = 2;
            this.select_class.Text = "Select Class";
            this.select_class.Click += new System.EventHandler(this.select_class_Click);
            // 
            // classUIID
            // 
            this.classUIID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.classUIID.AutoSize = true;
            this.classUIID.Location = new System.Drawing.Point(1164, 4);
            this.classUIID.Name = "classUIID";
            this.classUIID.Size = new System.Drawing.Size(94, 13);
            this.classUIID.TabIndex = 3;
            this.classUIID.Text = "No Class Selected";
            // 
            // Profiles
            // 
            this.Profiles.AutoSize = true;
            this.Profiles.Location = new System.Drawing.Point(146, 4);
            this.Profiles.Name = "Profiles";
            this.Profiles.Size = new System.Drawing.Size(118, 13);
            this.Profiles.TabIndex = 4;
            this.Profiles.Text = "Reload Student Profiles";
            this.Profiles.Click += new System.EventHandler(this.Profiles_Click);
            // 
            // pushLesson
            // 
            this.pushLesson.AutoSize = true;
            this.pushLesson.Location = new System.Drawing.Point(270, 4);
            this.pushLesson.Name = "pushLesson";
            this.pushLesson.Size = new System.Drawing.Size(68, 13);
            this.pushLesson.TabIndex = 6;
            this.pushLesson.Text = "Push Lesson";
            this.pushLesson.Click += new System.EventHandler(this.pushLesson_Click);
            // 
            // TeacherTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(1261, 659);
            this.Controls.Add(this.pushLesson);
            this.Controls.Add(this.Profiles);
            this.Controls.Add(this.classUIID);
            this.Controls.Add(this.select_class);
            this.Controls.Add(this.create_class);
            this.Controls.Add(this.Classroom);
            this.MaximumSize = new System.Drawing.Size(1277, 698);
            this.MinimumSize = new System.Drawing.Size(1277, 698);
            this.Name = "TeacherTool";
            this.Padding = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.Load += new System.EventHandler(this.TeacherPlanner_Load);
            this.Classroom.Panel1.ResumeLayout(false);
            this.Classroom.Panel2.ResumeLayout(false);
            this.Classroom.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Classroom)).EndInit();
            this.Classroom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.studentWindow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer Classroom;
        private System.Windows.Forms.ListView taskList;
        private System.Windows.Forms.Label studentName;
        private System.Windows.Forms.PictureBox studentWindow;
        private System.Windows.Forms.FlowLayoutPanel studentSelect;
        private System.Windows.Forms.Label create_class;
        private System.Windows.Forms.Label select_class;
        private System.Windows.Forms.Label classUIID;
        private System.Windows.Forms.Label Profiles;
        private System.Windows.Forms.Label pushLesson;
        private System.Windows.Forms.ListView errorList;
        private System.Windows.Forms.ColumnHeader StudentErrors;
    }
}

