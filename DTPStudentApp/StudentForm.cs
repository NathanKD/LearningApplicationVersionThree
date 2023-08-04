using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using SocketIOClient;
using Woof.SystemEx;
using static System.Resources.ResXFileRef;
using Common;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.Resources;
using System.Net.Http;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Threading.Tasks;

namespace DTPStudentApp
{
    public partial class StudentApp : Form
    {
        //Variable to store the code that get displayed for the user to run
        private string defaultCode = "";
        //Stores the selected lesson button variable
        private LessonButton selected = null;
        //Stores a list of lessons from a loaded file
        private List<Lesson> loadedLessonFile;
        //Stores the path of the selected lessonFile
        private string loadedLessonFileName;
        //Stores the students persistent token
        private string token;
        //SocketIO object
        private SocketIO socket;
        //Stores a list of classes that the student is apart of
        private List<string> classIds = new List<string>();
        //Stores the currently selected classID
        private string classId = "";
        //Stores a list of lessonNames that the student can selected
        private List<string> lessonNames = new List<string>();
        //Stores a list of all the users erros to send to the teacher on request
        private List<string> errors = new List<string>();
        //Store a collection of fonts including dyslexia font
        private PrivateFontCollection pfc = new PrivateFontCollection();
        private async Task RegisterStudent() {
            HttpClient client = new HttpClient();
            var data = new Dictionary<string, string>() { { "name", Environment.UserName } };
            var content = new FormUrlEncodedContent(data);
            var response = await client.PostAsync("https://learnappserver20.nathankleine1.repl.co/registerStudent", content);
            var ReadString = await response.Content.ReadAsStringAsync();
            token = ReadString.Split('\"')[3];
            File.WriteAllText("./cache", token);
        }
        private async void safeConnect()
        {
            try { token = File.ReadAllLines("./cache")[0]; }
            catch { await RegisterStudent(); }

            socket = new SocketIO("https://learnappserver20.nathankleine1.repl.co");
            //Create a dict to pass headers to server
            Dictionary<string, string> headers = new Dictionary<string, string>();
            //Give the persistent token to the server
            headers.Add("token", token);
            //Format classes student is apart of for server into a single string
            string classes = "";
            foreach (string classId in classIds)
                classes += classId + ",";
            //remove the trailing comma if there are more than one class in the list
            if (classes.Length > 1)
                classes = classes.Substring(0, classes.Length - 1);
            //add the classes header
            headers.Add("classes", classes);
            socket.Options.ExtraHeaders = headers;

            //add event listner called connectionEstablished for when we connect to server
            socket.OnConnected += connectionEstablished;
            //Connect to the server asynchronously
            await socket.ConnectAsync();
        }
        public StudentApp()
        {
            
            //Read the file of called cache that stores the persistent user token
            //check if a class data file exists if it doenst create one
            if (!File.Exists("./classData"))
                File.Create("./classData").Close();
            //read all ids from the file that the student belongs to
            classIds = new List<string>(File.ReadAllLines("./classData"));
            //
            InitializeComponent();
            safeConnect();
            //check if the defaultCode.cs file exists 
            if (File.Exists("./DefaultCode.cs"))
                defaultCode = File.ReadAllText("./DefaultCode.cs");
            //Set it to the codeBlock UI elements text so the user can edit it 
            codeBlock.SelectionTabs = new int[] { 20,40,80,90,100,110,120 };
            codeBlock.Text = defaultCode;
            codeBlock_TextChanged(codeBlock, new EventArgs());

            //https://stackoverflow.com/questions/1297264/using-custom-fonts-on-a-label-on-winforms
            int fontSize = Properties.Resources.OpenDyslexicMono_Regular.Length;
            byte[] font = Properties.Resources.OpenDyslexicMono_Regular;
            IntPtr data = Marshal.AllocCoTaskMem(fontSize);
            Marshal.Copy(font, 0, data, fontSize);
            pfc.AddMemoryFont(data, fontSize);
            //End of custom font
            LoadTheme(new Theme());

        }
        //Called when we conenct to the NODE JS server
        private void connectionEstablished(object Sender, EventArgs e)
        {
            //Get the socket element
            SocketIO socket = (SocketIO)Sender;
            //Called by the server when a teacher wants the users profile
            socket.On("getUserProfile", res =>
            {
                //https://stackoverflow.com/questions/5570113/c-sharp-how-to-get-current-user-picture
                //Get the users profile picture
                Bitmap rawPfp = new Bitmap(SysInfo.GetUserPicturePath());
                //Resize it to a smaller size that fits on the teachers screen
                Bitmap userPfp = new Bitmap(rawPfp, new Size(70,70));
                //use JPG in later version for compression reasons 
                //Save the teachers token so we can tell the server who to give this profile too 
                string teacherToken = res.GetValue(0).GetString();
                //https://stackoverflow.com/questions/7350679/convert-a-bitmap-into-a-byte-array
                ImageConverter converter = new ImageConverter();    
                //Emit an event to the server wit hthe user data and teachers token so the server can forward it to the teacher who requested it
                socket.EmitAsync("returnUserProfile", new object[]{ Environment.UserName, (byte[])converter.ConvertTo(userPfp, typeof(byte[])), token, teacherToken });
            });
            //Called by the server on connect in response to the users list of classIDS that they claim to be apart of
            socket.On("deleteClasses", res =>
            {
                //Loop through each of the classes in the data and remove them from the class list
                //These classes dont exist anymore and must be removed from the users classData File
                for(int i = 0; i < res.GetValue(0).GetArrayLength(); i++)
                    classIds.Remove(res.GetValue(0)[i].ToString());
                //Update the file
                File.WriteAllLines("./classData", classIds.ToArray());
            });
            //Teacher(getStudentDetails) -> Server(returnStudentDetails) -> Student
            //This is used as part of a chain to get the students details for the teacher to use when monitoring a classroom
            socket.On("returnStudentDetails", res =>
            {
                //this return socket variable is the socketID of the teacher who asked for the student Details
                //This socketID get passed on to the server so the server knows who to send it too
                string returnSocket = res.GetValue(0).GetString();
                //create a bitmap the size of the form to store a screenshot
                Bitmap screenShot = new Bitmap(Width, Height);
                //ImageConverter Object allows me to play with types when sending it to the server
                ImageConverter converter = new ImageConverter();

                //delegate a job to the form thread. I need to delegate this job because socketIO works on a differnt thread to the form
                //so we need to ask the form to run this code when it has time which is instantly 99% of the time
                //Ask to to create a bitmap of the form and put it on the screenshot bitmap
                this.Invoke((MethodInvoker) delegate{ DrawToBitmap(screenShot, new Rectangle(0, 0, Width, Height)); });
                
                //Create a Dict of all the lessons the student has loaded and a bool to for if its marked as completed
                Dictionary<string, bool> completedLessons = new Dictionary<string, bool>();
                //Make sure we have a lesson loaded before we do anything
                if(loadedLessonFile != null)
                    //Loop through each of the lessons and add their data to my dict
                    for (int i = 0; i < loadedLessonFile.Count; i++)
                        completedLessons.Add(loadedLessonFile[i].lessonName, loadedLessonFile[i].completed);
                //Once again I need to use the Form thread when I get the compiler data due to threading issues
                compilerBlock.Invoke((MethodInvoker) delegate {
                    BinaryFormatter bf = new BinaryFormatter();
                    //Create a new object to make sending data easier and more readable. Both the teacher and Student share the same definition for StudentDetails
                    StudentDetails student = new StudentDetails(screenShot, completedLessons, errors);
                    //Use the BinaryFormatter from earlier and conver the student variable into a stream so it can be sent to the server
                    using (var ms = new MemoryStream())
                    {
                        //Serialize student data
                        bf.Serialize(ms, student);
                        //Send it to the server
                        socket.EmitAsync("returnStudentDetails", new object[] {returnSocket,ms.ToArray()});
                    }
                });
            });
            //This event happens when the server sends the lesson back to the student after they have asked for it
            //Student(getLesson) -> Server(returnLesson) -> Student
            socket.On("returnLesson", (res) => {
                //I need to store all my lessons in a folder specific to each class
                //Check if it exists and create one if it dont for this class
                if (!Directory.Exists("./" + classId))
                    Directory.CreateDirectory("./" + classId);
                //Create a name for this new lesson
                string newLessonName = lessonNames.Count + ".les";
                //Add it to my list of lesson names
                lessonNames.Add(newLessonName);

                //Convert the lesson file to an array of bytes for deserlizeation
                byte[] data = Encoding.ASCII.GetBytes(res.GetValue(0).GetString());
                //Wrtie these bytes to a file because where gonna have to save it anyway
                File.WriteAllBytes("./"+classId+"/"+ newLessonName, data);
                //Load the lesson we just saved to ensure that it saved properly
                loadedLessonFile = loadLessonFile("./" + classId + "/" + newLessonName);
            });
            socket.On("UserDeath", (res) => {
                socket.DisconnectAsync();
                File.Delete("./cache");File.Delete("./classData"); 
                this.Invoke((MethodInvoker) delegate { Application.Restart(); }); 
            });
            Console.WriteLine("Connected");
        }
        /// <summary>
        /// save a lesson to the disk for later use
        /// </summary>
        /// <param name="path">Path to the save file</param>
        /// <param name="lessons">List of lessons to save</param>
        private void saveLessonFile(string path, List<Lesson> lessons)
        {
            //Binary Formatter to conver the lesson to a binary that can be saved
            IFormatter formatter = new BinaryFormatter();
            //Convert the lesson to a stream for serializeation
            Stream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            //Make sure lesson is real
            if(lessons != null)
                formatter.Serialize(fileStream, lessons);
            //close the stream so I dont throw an error when I try to read later
            fileStream.Close();
        }
        /// <summary>
        /// Given a path to a file load a list of lessons from the disk
        /// </summary>
        /// <param name="path">Path to the lesson</param>
        /// <returns>List of lessons</returns>
        private List<Lesson> loadLessonFile(string path)
        {
            //Binary Formatter to conver the binary to a lesson that can be loaded
            IFormatter formatter = new BinaryFormatter();
            //Stream the data from the file
            Stream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            //Deserialize the list of lessons
            List<Lesson> serializedLessons = (List<Lesson>)formatter.Deserialize(fileStream);
            //Close the file so we dont throw an exception if I try to open later
            fileStream.Close();
            //Another threading issue so I delegate the task to the buttonList to 
            //This just creates buttons in the UI for each lesson so the user can swap between them
            buttonList.Invoke((MethodInvoker)delegate{
                //Loop through all the lessons in my new lesson list
                for (int i = 0; i < serializedLessons.Count; i++)
                {
                    //Get the lesson
                    Lesson les = serializedLessons[i];
                    //Create a new LessonButton
                    LessonButton b = new LessonButton(les);
                    b.Name = "lessonButton";
                    //Set its size to the width of its container and height to 40
                    b.Size = new Size(infoLessonContainer.Panel2.Width, 40);
                    //Set the location to a multiple of 40 depending on how many come before it
                    b.Location = new Point(0,40*i);
                    //Add an event listner for when its clicked
                    b.MouseClick += lessonSelect;
                    b.UseCompatibleTextRendering = true;
                    //Set the text of the button to the name of the lesson
                    b.Text = les.lessonName;
                    b.Font = selectedFont;
                    //Check if the student has completed this lesson before if so make it as completed on the button
                    if (les.completed)
                        b.completed();
                    //Add the button to the list of controls
                    buttonList.Controls.Add(b);
                }
            });
            return serializedLessons;
        }
        //This event listner is given to LessonButtons when they are created 
        private void lessonSelect(object sender, EventArgs e)
        {
            //Get the button who called it
            selected = (LessonButton)sender;
            //Set the UI Text Title to the selected lesson
            lessonTitle.Text = selected.lesson.lessonName;
            //Set the lessons contents to the UI
            lessonInfo.Text = selected.lesson.lessonContent;

            //ToDo write a function that deletes the main function if the lesson requires it

            //Set the code block that the student writes code into to the code provided by the lesson
            codeBlock.Text = selected.lesson.defaultCode;
        }
        //This Event listner provides functionailty for the popout menu on the far right of the UI
        private void hideLessonButton_Click(object sender, EventArgs e)
        {
            //Get the button who called the function
            Button self = (Button)sender;
            //Collapse the UI panel to hide the list of lessons from the student
            //use the bitwise operation NOT to invert the state of the UI panel
            infoLessonContainer.Panel2Collapsed = !infoLessonContainer.Panel2Collapsed;
            //Set the location of the button to the edge of the Collapsed or expanded Panel
            self.Location = new Point(infoLessonContainer.Panel1.Width - self.Width, self.Location.Y);
            //Use a ternary statement to alternate the text of the button between both inequality symbols to show the direciton of the collapse
            //if the text of the button == '<' make it '>' otherwise set it to '<'
            self.Text = self.Text == "<" ? ">" : "<";

        }
        //This is an eventlistner thats called when the run button is called on the UI
        private void Run_Click(object sender, EventArgs e)
        {
            //Delete the previous compiled exe file so we can make a new one
            File.Delete("code.exe");
            //run the C# compiler and write its output to the compiler block to show any errors in compilation to the student
            compilerBlock.Text = compileCode();
            //add a new line character for visual purposes
            compilerBlock.Text += "\n";
            //Check that code.exe exists if it doesnt then compilation failed due to a students error
            if (File.Exists("code.exe"))
            {
                //Run the program the student made 
                //tuple stores the output of the running of the program and the exit code of the program
                (string stdOut, int exitCode) processOutput = runProcess("code.exe", "");
                //Set the compilerBlock UI to the output of the students program
                compilerBlock.Text += processOutput.stdOut;
                Console.WriteLine(processOutput.stdOut.Contains("\n"));
                //check that we have a lesson selected if we dont we done with this function
                if (selected == null)
                    return;
                //check the exit code of the program if its one then the student has completed the challange from the lesson
                if (processOutput.exitCode == 1)
                {
                    //make the lesson as complete 
                    selected.lesson.completed = true;
                    //call the complete function might add animation later thats why its a function
                    selected.completed();
                }

            }
        }
        /// <summary>
        /// compile the code found in the compiler block 
        /// </summary>
        /// <returns>the standard output of the process</returns>
        private string compileCode()
        {
            //Delete the code.cs file which is used to store the students code so it can be passed to the compiler
            File.Delete("code.cs");
            //Write the new code to the code.cs file
            File.WriteAllText("./code.cs", codeBlock.Text);
            //Call the runProcess function to run the C# compiler and pass code.cs as an argument
            (string stdOut, int exitCode) processOutput = runProcess("csc.exe", "code.cs");
            //the stdOut of this compiler process can show the students errors
            //This info could be interesting to a teacher as it can show what the student needs to work on
            //so I split each line up in the output 
            foreach(string s in processOutput.stdOut.Split('\n'))
                //check if its an error 
                if (processOutput.stdOut.Contains("error"))
                    //add it to a list so it can be sent to the teacher on request
                    errors.Add(processOutput.stdOut.Substring(processOutput.stdOut.IndexOf("error")));
            //return the output of compilation 
            return processOutput.stdOut;
        }
        /// <summary>
        /// a generic function to run an external exe in the background
        /// i.e. the students program and the C# compiler
        /// </summary>
        /// <param name="path">Path to the process</param>
        /// <param name="args">Arguments to pass to the process</param>
        /// <returns>A tuple with the output of the process and the exit code of the process</returns>
        private (string stdOut, int exitCode) runProcess(string path, string args)
        {
            //Start a new process with the path and arguments
            Process Complier = Process.Start(path, args);
            //This block of code trys to hide any kind of UI or console window that comes from the process
            //Keeps it hidden in the background
            Complier.StartInfo.UseShellExecute = false;
            Complier.StartInfo.RedirectStandardOutput = true;
            Complier.StartInfo.CreateNoWindow = true;
            Complier.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            Complier.EnableRaisingEvents = true;

            //new string to store any data that comes from the output
            string recived = "";
            //Called when data is recived from the process
            Complier.OutputDataReceived += new DataReceivedEventHandler((s, ev) =>
            {
                //if the standard output gives data that is valid then add it the recived string
                if (!String.IsNullOrEmpty(ev.Data))
                    recived += ev.Data;
            });
            //Start the process
            Complier.Start();
            //Start reading lines
            Complier.BeginOutputReadLine();
            //Wait for the process to exit
            Complier.WaitForExit();
            //return the data we got and the exit code of the process in the form of a tuple
            return (recived, Complier.ExitCode);
        }
        //Save the lesson when the form gets closed as a new student copy so not to interfere with the original 
        private void StudentApp_FormClosed(object sender, FormClosedEventArgs e) => saveLessonFile($"./{loadedLessonFileName}_StudentCpy.les", loadedLessonFile);
        //An eventlistner for when the student clicks on the UI button to join a new classroom
        private void joinClass_Click(object sender, EventArgs e)
        {
            //make sure we are connected to the server
            if (!socket.Connected)
                return;
            //Open a new Dialog Box and promt the student for a code
            DialogBox d = new DialogBox("Enter Class Code", (string s) => {
                //make sure we dont get an empty input
                if (s == String.Empty)
                    return;
                //Call an event on the server to join a new class and wait for an acknowledgement back 
                socket.EmitAsync("joinNewClass", (ack) => {
                    //Check that the server has said its ok to join
                    if (ack.GetValue(0).GetString() != "ok")
                        return;
                    //Append the new classroom to the file that stores all the classes the student is apart of
                    using (StreamWriter sw = File.AppendText("./classData")) sw.WriteLine(s);
                    //Set the class ID to the class student just joined
                    classId = s;
                    //More threading problems so I delegate the task to the classUIID element 
                    //Set the text of the UI class element to the new classroom we just joined
                    classUIID.Invoke((MethodInvoker)delegate { classUIID.Text = classId; });
                    //add it to the list of classes we apart of 
                    classIds.Add(s);
                }, new object[] { Environment.UserName, token, s});
            });
        }
        //Called by the UI when the top menu button 'select class' is clicked
        //Allows the student to swap between classes they are apart of 
        private void SelectClass_Click(object sender, EventArgs e)
        {
            //make sure the file that stores all the classes exists if not make one
            if (!File.Exists("./classData"))
                File.Create("./classData").Close();
            //read the file and get a list of classes the student claims to be apart of
            classIds = new List<string>(File.ReadAllLines("./classData"));
            //create a new context menu to display all the classes
            ContextMenu c = new ContextMenu();
            //add each class to the context menu
            foreach (string s in classIds)
            {
                //add listner for if a classroom is clicked 
                //and add the class to the context menu
                c.MenuItems.Add(s).Click += (object menu, EventArgs even) =>
                {
                    //if the class is selected then set the classID to the selected one
                    classId = s;
                    //Update the UI
                    classUIID.Text = s;
                };
            }
            //align the context menu with the UI so its position is not differnt each time its clicked
            //Set it just below the button
            c.Show((Control)sender, new Point(0, 18));
        }
        //Name of this eventlistner is deciving and its called when the user pulls a lesson
        private void selectLesson_Click(object sender, EventArgs e)
        {
            //Ask the server for the first lesson of the classroom 
            //Add funcitonailty for multiple lessons later
            socket.EmitAsync("getLesson",new object[] { classId,1});
        }
        Stack<(string text,int index)> EditHistory = new Stack<(string text,int index)>();
        private void codeBlock_KeyDown(object sender, KeyEventArgs e)
        {
            //Switch between keys that have been pressed down
            switch (e.KeyCode)
            {
                //If the Z key is pressed
                case Keys.Z:
                    //And The Control Key is Down
                    if (e.Control)
                    {
                        //Get the text off the top of the history stack and set it to the code on the UI
                        codeBlock.Text = EditHistory.Peek().text;
                        //Get the cursor position from the top of the history stack and adjust the cursor position
                        //This doesnt work rly well for some reason 
                        codeBlock.SelectionStart = EditHistory.Pop().index;
                    }
                    break;
            }
        }
        private void codeBlock_TextChanged(object sender, EventArgs e)
        {
            if (!ModifierKeys.HasFlag(Keys.Control)) 
                EditHistory.Push((codeBlock.Text, codeBlock.SelectionStart));

            int cursorIndex = codeBlock.SelectionStart;
            
            codeBlock.SelectAll();
            codeBlock.SelectionColor = selectedTheme.TextColor;
            codeBlock.DeselectAll();
            foreach (KeyValuePair<Regex, Color> x in selectedTheme.syntax)
            {
                //codeBlock.DeselectAll();
                foreach(Match match in x.Key.Matches(codeBlock.Text))
                {
                    codeBlock.Select(match.Index, match.Value.ToString().Length);
                    codeBlock.SelectionColor = x.Value;
                }
            }

            codeBlock.DeselectAll();
            codeBlock.SelectionStart = cursorIndex;
        }
        private Theme selectedTheme = new Theme();
        private void LoadTheme(Theme load)
        {
            this.BackColor = load.backgorund;
            List<Type> textComponents = new List<Type> { typeof(Label), typeof(TextBox), typeof(RichTextBox) };

            foreach (KeyValuePair<string, Color> i in load.controlColorSettings)
                Controls.Find(i.Key, true)[0].BackColor = i.Value;

            selectedTheme = load;
            codeBlock.ForeColor = load.TextColor;
            compilerBlock.ForeColor = load.TextColor;
            lessonInfo.ForeColor = load.TextColor;
            lessonTitle.ForeColor = load.TextColor;

            foreach (Control c in this.Controls.Find("lessonButton", true))
                (c as Button).ForeColor = load.TextColor;
            foreach (Control c in this.Controls)
            {
                if (textComponents.Contains(c.GetType()))
                    c.ForeColor = load.TextColor;
                foreach (Control x in c.Controls)
                    if (textComponents.Contains(x.GetType()))
                        x.ForeColor = load.TextColor;
            }
        }
        
        private void Settings_Click(object sender, EventArgs e)
        {
            ContextMenu c = new ContextMenu();
            c.MenuItems.Add(new MenuItem("Theme"));
            c.MenuItems.Add(new MenuItem("Dyslexia Font", fontClick));
            c.MenuItems.Add(new MenuItem("Change Language"));
            c.MenuItems[2].MenuItems.Add("English", (object s, EventArgs args) => { 
                changeLang(Properties.en_local.ResourceManager);
            });
            c.MenuItems[2].MenuItems.Add("Te Reo", (object s, EventArgs args) => {
                changeLang(Properties.mi_local.ResourceManager);
            });
            foreach (Theme t in DefaultThemes.defaultThemes)
                c.MenuItems[0].MenuItems.Add(t.name, (object s, EventArgs args) => LoadTheme(t));
            c.Show((Control)sender,new Point(0,10));
        }
        private void changeLang(ResourceManager rm)
        {
            foreach (Control c in this.Controls)
                c.Text = rm.GetString(c.Name);
            foreach(Control c in mainMenu.Controls)
                c.Text = rm.GetString(c.Name);
        }
        private bool DYSLEXIA = false;
        private Font selectedFont = new Font("Microsoft Sans Serif", 8.25f);
        private void fontClick(object sender, EventArgs e)
        {
            DYSLEXIA = !DYSLEXIA;
            selectedFont = DYSLEXIA ? new Font(pfc.Families[0], 8.25f) : new Font("Microsoft Sans Serif", 8.25f);
            adjustMenus(DYSLEXIA? -10 : 10);

            foreach (Control control in this.Controls)
                control.Font = selectedFont;
            lessonInfo.Font = selectedFont;
            lessonTitle.Font = selectedFont;
            
            codeBlock.Font = selectedFont;
            codeBlock.SelectionFont = selectedFont;
            codeBlock_TextChanged((object)codeBlock,new EventArgs());
        }
        private void adjustMenus(int offset)
        {
            Padding = new Padding(0, Padding.Top-offset, 0, 0);
            mainMenu.Location = new Point(mainMenu.Location.X, mainMenu.Location.Y+offset);
            Run.Location = new Point(Run.Location.X, Run.Location.Y + offset);
            classUIID.Location = new Point(classUIID.Location.X, classUIID.Location.Y + offset);
            //return true;
        }

        private void StudentApp_Load(object sender, EventArgs e)
        {
            if (File.Exists("./builtIn.les"))
                loadLessonFile("./builtIn.les");
            
        }
    }
}