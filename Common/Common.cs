using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace Common
{
    /// <summary>
    /// Stores lesson data. Created by teachers and viewed by students
    /// </summary>
    [Serializable]
    public class Lesson
    {
        public string lessonContent;
        public string lessonName;
        public string lessonDescription;
        public string defaultCode;
        private string expectedOutput;
        private string functionOutputName;
        public bool completed = false;
        public bool hideMainFunction = false;
        public Lesson(string lessonName = "new Lesson", string lessonDescription = "new Lesson", string lessonContent = "new Lesson", string defaultCode = "")
        {
            this.lessonDescription = lessonDescription;
            this.lessonName = lessonName;
            this.lessonContent = lessonContent;
            this.defaultCode = defaultCode;

        }
        public void setChallangeExpectedOutput(string exptectedOutput, string functionOutName, string functionType)
        {
            if (functionOutName == "Main")
                throw new Exception("Cannot Use Main Function to create challange");
            //if (!defaultCode.Contains(functionOutName))
            //    throw new Exception("Code does not contain that function");
            this.expectedOutput = exptectedOutput;
            this.functionOutputName = functionOutName;
            if (!File.Exists("./ChallangeFramework.cs"))
                throw new Exception("ChallangeFramework.cs could not be found");
            hideMainFunction = true;
            this.defaultCode = File.ReadAllText("./ChallangeFramework.cs");
            this.defaultCode = defaultCode.Replace("FunctionName", functionOutName);
            this.defaultCode = defaultCode.Replace("ExpectedOutput", exptectedOutput);
            this.defaultCode = defaultCode.Replace("Function", $"{functionType} {functionOutName}()\n        {{\n\n\n        }}");
        }
    }
    //Extend the Default C# button class to include a refernce to a lesson
    public class LessonButton : Button
    {
        public Lesson lesson;
        public LessonButton(Lesson lesson)
        {
            this.lesson = lesson;
            this.Text = lesson.lessonName;
        }
        public void completed()
        {
            this.BackColor = Color.FromArgb(200, 232, 208);
        }
    }
    /// <summary>
    /// A Dialog box that prompts the user for an input
    /// </summary>
    public class DialogBox : Form
    {
        public bool exited = false;
        private TextBox input;
        private Action<string> callBack;
        public DialogBox(string question, Action<string> callBack)
        {
            this.Size = new Size(300, 150);
            Label questionLabel = new Label();
            questionLabel.Text = question;
            questionLabel.Location = new Point(150, 30);

            input = new TextBox();
            input.Location = new Point(150, 60);

            input.KeyDown += keyPress;
            this.callBack = callBack;
            this.Controls.Add(questionLabel);
            this.Controls.Add(input);
            this.Show();
        }
        private void keyPress(object sender, KeyEventArgs e)
        {
            exited = e.KeyCode == Keys.Enter;
            if (exited)
            {
                callBack.Invoke(input.Text);
                this.Close();
            }
        }

    }
    /// <summary>
    /// Student UI card that the teacher can select
    /// </summary>
    public class StudentUIPanel : Panel
    {
        public PictureBox profilePicture;
        public Label studentName;
        public string token;
        public StudentUIPanel(string username, Bitmap profileBitmap, string token)
        {
            this.token = token;

            studentName = new Label();
            studentName.Text = username;
            studentName.Parent = this;
            studentName.Anchor = AnchorStyles.Bottom;
            studentName.Location = new Point(50, 85);
            studentName.TextAlign = ContentAlignment.TopCenter;

            profilePicture = new PictureBox();
            profilePicture.Image = profileBitmap;
            profilePicture.Size = new Size(70, 70);
            profilePicture.Parent = this;
            profilePicture.Dock = DockStyle.Fill;

            this.Size = new Size(70, 70);
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(studentName);
            this.Controls.Add(profilePicture);
        }
        public void setLocation(Point newLocation) => this.Location = newLocation;

    }
    /// <summary>
    /// Used to store and send student details to the server 
    /// </summary>
    [Serializable]
    public class StudentDetails
    {
        public Bitmap screenshot;
        public Dictionary<string, bool> completedLessons;
        public List<string> errors;
        public StudentDetails(Bitmap screenshot, Dictionary<string, bool> completedLessons, List<string> compilerBlock)
        {
            this.screenshot = screenshot;
            this.completedLessons = completedLessons;
            this.errors = compilerBlock.GetRange(0, Math.Min(10, compilerBlock.Count));
        }
    }
    /// <summary>
    /// Token generator class to create new user tokens
    /// TODO: Make more secure
    /// </summary>
    public static class genToken
    {
        public static byte[] genNewToken()
        {
            byte[] rnd = new byte[12];
            Random r = new Random();
            r.NextBytes(rnd);
            return rnd;
            
        }
    }
    public class Theme
    {
        public string name = "Default";
        public Color TextColor;
        public Color backgorund;
        public Color buttonOutline;
        //public Font TextFont = SystemFonts.DefaultFont;
        public Dictionary<string, Color> controlColorSettings = new Dictionary<string, Color>() {
            { "codeBlock", Color.White},
            { "compilerBlock", Color.White},
            { "buttonList", Color.White},
            { "lessonInfo", Color.White},
            { "lessonInfoPanel", Color.White},
            { "splitContainer1", Color.FromArgb(242,242,242)},
        };
        public Dictionary<Regex, Color> syntax = new Dictionary<Regex, Color>() {
            { new Regex("using"), Color.Blue },
            { new Regex("private"), Color.Blue },
            { new Regex("object"), Color.Blue },
            { new Regex("string"), Color.Blue },
            { new Regex("int"), Color.Blue },
            { new Regex("new"), Color.Blue },
            { new Regex("static"), Color.Blue },
            { new Regex("void"), Color.Blue},
            { new Regex("try"), Color.FromArgb(204, 0, 153)},
            { new Regex("catch"), Color.FromArgb(204, 0, 153)},
            { new Regex("for"), Color.FromArgb(204, 0, 153)},
            { new Regex("while"), Color.FromArgb(204, 0, 153)},
            { new Regex("foreach"), Color.FromArgb(204, 0, 153)},
            { new Regex("return"), Color.FromArgb(204, 0, 153)},
            { new Regex("else"), Color.FromArgb(204, 0, 153)},
            { new Regex("if"), Color.FromArgb(204, 0, 153)},
            { new Regex("true"), Color.Blue},
            { new Regex("false"), Color.Blue},
            { new Regex("public"), Color.Blue},
            { new Regex("partial"), Color.Blue},
            { new Regex("class"), Color.Blue},
            { new Regex("namespace"), Color.Blue},
            { new Regex("Console"), Color.Green},

            { new Regex("\"(.*)\""), Color.FromArgb(214,157,133)},

            //{ new Regex(@"\.(.*)[\s]"), Color.FromArgb(220,72,0)}
            //This one has to be done last
            { new Regex(@"(//)(.*)\n"), Color.Green},
        };
        public Theme(
            string name = "Default",
            Color? textColor = null,
            Color? backColor = null,
            Color? buttonOutline = null,
            Dictionary<string, Color> controlColors = null,
            Dictionary<Regex, Color> syntax = null)
        {
            
            this.name = name;
            this.TextColor = textColor ?? Color.Black;
            this.buttonOutline = buttonOutline ?? Color.White;
            this.backgorund = backColor ?? Color.FromArgb(227, 227, 227);
            this.controlColorSettings = controlColors ?? this.controlColorSettings;
            this.syntax = syntax ?? this.syntax;
        }
    }
    public static class DefaultThemes
    {
        public static Theme[] defaultThemes = new Theme[] {
            new Theme(),
            new Theme("Dark Theme",Color.White, Color.FromArgb(40,40,40),Color.FromArgb(40,40,40), new Dictionary<string, Color>(){
                { "codeBlock", Color.FromArgb(30,30,30)},
                { "compilerBlock", Color.FromArgb(30,30,30)},
                { "buttonList", Color.FromArgb(30,30,30)},
                { "lessonInfo", Color.FromArgb(30,30,30)},
                //{ "lessonButton", Color.FromArgb(40,40,40)},
                { "lessonInfoPanel", Color.FromArgb(30,30,30)},
                { "splitContainer1", Color.FromArgb(40,40,40)},
            })
        };
    }
}
