using DTPStudentApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LessonCreator
{
    public partial class LessonPlanner : Form
    {
        private List<Lesson> lessons = new List<Lesson>();
        private LessonButton selected;
        private string defaultCode;
        private string savePath = "./savedLessons/new_unit.les";
        public LessonPlanner()
        {
            InitializeComponent();
            if (!Directory.Exists("./savedLessons"))
                Directory.CreateDirectory("./savedLessons");
            if (File.Exists("./DefaultCode.cs"))
                defaultCode = File.ReadAllText("./DefaultCode.cs");
        }

        private void newLesson_Click(object sender, EventArgs e)
        {
            //Lesson Stuff
            Lesson newLesson = new Lesson();
            newLesson.lessonName = "new Lesson";
            newLesson.defaultCode = defaultCode;
            lessons.Add(newLesson);

            //Button Stuff
            LessonButton newButton = new LessonButton(newLesson);
            newButton.Size = new Size(splitContainer2.Panel2.Width, 40);
            newButton.Location = new Point(0, (lessons.Count-1) * 40);
            lessonButtons.Controls.Add(newButton);

            selected = newButton;
            newButton.Click += selectLessonClick;

            //RightClick Dialong Stuff
            ContextMenu c = new ContextMenu();
            c.MenuItems.Add(new MenuItem("Delete", removeLesson));
            newButton.ContextMenu = c;

            //UI stuff
            lessonContent.Text = newLesson.lessonContent;
            LessonTitle.Text = newLesson.lessonName;
            codeBlock.Text = newLesson.defaultCode;

            setControlsEnabled(true);
            saveLessonFile(lessons);
        }
        private void removeLesson(object sender, EventArgs e)
        {
            MenuItem men = (MenuItem)sender;
            ContextMenu m = (ContextMenu)men.Parent;
            LessonButton b = (LessonButton)m.SourceControl;
            lessons.Remove(b.lesson);
            b.Visible = false;
            b.Enabled = false;
            setControlsEnabled(false);
        }
        private void selectLessonClick(object sender, EventArgs e)
        {
            selected = (LessonButton)sender;
            LessonTitle.Text = selected.lesson.lessonName;
            lessonContent.Text = selected.lesson.lessonContent;
            codeBlock.Text = selected.lesson.defaultCode;
            setControlsEnabled(true);


        }
        private void setControlsEnabled(bool enabled)
        {
            lessonContent.Enabled = enabled;
            LessonTitle.Enabled = enabled;
            lessonContent.Enabled = enabled;
            codeBlock.Enabled = enabled;
        }
        private void lessonUpdate(object sender, EventArgs e)
        {
            selected.lesson.lessonName = LessonTitle.Text;
            selected.lesson.lessonContent = lessonContent.Text;
            selected.lesson.defaultCode = codeBlock.Text;
        }

        private void LessonPlanner_Load(object sender, EventArgs e)
        {

        }
        private void saveLessonFile(List<Lesson> lessons)
        {
            //This is not safe
            IFormatter formatter = new BinaryFormatter();
            Stream fileStream = new FileStream(savePath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
            formatter.Serialize(fileStream, lessons);
            fileStream.Close();
        }

        private void openLesson_Click(object sender, EventArgs e)
        {
            OpenFileDialog loadLesson = new OpenFileDialog();
            DialogResult res = loadLesson.ShowDialog();
            if(res == DialogResult.OK)
            {
                savePath = loadLesson.FileName;
                Console.WriteLine(savePath);
                lessons = loadLessonFile(savePath);
            }
        }
        private List<Lesson> loadLessonFile(string path)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            List<Lesson> serializedLessons = (List<Lesson>)formatter.Deserialize(fileStream);
            fileStream.Close();
            for(int lessonOffset = 0; lessonOffset < serializedLessons.Count; lessonOffset++)
            {
                Lesson les = serializedLessons[lessonOffset];
                LessonButton b = new LessonButton(les);

                b.Location = new Point(0, lessonOffset * 40);
                b.Size = new Size(splitContainer2.Panel2.Width, 40);
                b.MouseClick += selectLessonClick;
                b.Text = les.lessonName;

                //RightClick Dialong Stuff
                ContextMenu c = new ContextMenu();
                c.MenuItems.Add(new MenuItem("Delete", removeLesson));
                b.ContextMenu = c;

                lessonButtons.Controls.Add(b);
            }
            return serializedLessons;
        }

        private void LessonPlanner_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveLessonFile(lessons);
        }
    }
}
