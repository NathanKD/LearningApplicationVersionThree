using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;

namespace DTPStudentApp
{
    public partial class StudentApp : Form
    {
        private string defaultCode = "";
        public StudentApp()
        {
            InitializeComponent();
            //Lesson testLesson = new Lesson("lessonName", "Test Lesson", "This is a test lesson", defaultCode);
            //List<Lesson> lessons = new List<Lesson>();
            //lessons.Add(testLesson);
            //saveLessonFile("testLes.les",lessons);

            if (File.Exists("./DefaultCode.cs"))
                defaultCode = File.ReadAllText("./DefaultCode.cs");
            
            List <Lesson> testLessons = loadLessonFile("./testLes.les");
            codeBlock.Text = testLessons[0].defaultCode;
        }
        private void saveLessonFile(string path, List<Lesson> lessons)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(fileStream, lessons);
            fileStream.Close();
        }
        private List<Lesson> loadLessonFile(string path, bool initButtons = true)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            List<Lesson> serializedLessons = (List<Lesson>)formatter.Deserialize(fileStream);
            fileStream.Close();
            if(!initButtons)
                return serializedLessons;
            foreach(Lesson les in serializedLessons)
            {
                LessonButton b = new LessonButton(les);
                b.Text = les.lessonName;
                b.Size = new Size(infoLessonContainer.Panel2.Width, 40);
                b.MouseClick += lessonSelect;
                buttonList.Controls.Add(b);
            }
            return serializedLessons;
        }
        private void lessonSelect(object sender, EventArgs e)
        {
            LessonButton self = (LessonButton)sender;
            lessonTitle.Text = self.lesson.lessonName;
            lessonContent.Text = self.lesson.lessonContent;
            
        }
        private void hideLessonButton_Click(object sender, EventArgs e)
        {
            Button self = (Button)sender;
            infoLessonContainer.Panel2Collapsed = !infoLessonContainer.Panel2Collapsed;
            self.Location = new Point(infoLessonContainer.Panel1.Width-self.Width,self.Location.Y);
            self.Text = self.Text == "<" ? ">" : "<";
            
        }

        private void Run_Click(object sender, EventArgs e)
        {
            File.Delete("code.exe");
            compilerBlock.Text = compileCode();
            compilerBlock.Text += "\n";
            if(File.Exists("code.exe"))
                compilerBlock.Text += runProcess("code.exe","");
        }
        private string compileCode()
        {
            File.Delete("code.cs");
            File.WriteAllText("./code.cs", codeBlock.Text);

            return runProcess("csc.exe","code.cs");
        }
        private string runProcess(string path, string args)
        {
            Process Complier = Process.Start(path, args);
            Complier.StartInfo.UseShellExecute = false;
            Complier.StartInfo.RedirectStandardOutput = true;
            Complier.StartInfo.CreateNoWindow = true;
            Complier.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            Complier.EnableRaisingEvents = true;

            string recived = "";
            Complier.OutputDataReceived += new DataReceivedEventHandler((s, ev) => {
                if (!String.IsNullOrEmpty(ev.Data))
                    recived += ev.Data;
            });
            Complier.Start();
            Complier.BeginOutputReadLine();
            Complier.WaitForExit();
            return recived;
        }
    }
    [Serializable]
    class Lesson
    {
        public string lessonContent;
        public string lessonName;
        public string lessonDescription;
        public string defaultCode;
        public Lesson(string lessonName,string lessonDescription,string lessonContent, string defaultCode)
        {
            this.lessonDescription = lessonDescription;
            this.lessonName = lessonName;
            this.lessonContent = lessonContent;
            this.defaultCode = defaultCode;

        }
    }
    //Extend the Default C# button class to include a refernce to a lesson
    class LessonButton : Button
    {
        public Lesson lesson;
        public LessonButton(Lesson lesson)
        {
            this.lesson = lesson;
        }
    }
}
