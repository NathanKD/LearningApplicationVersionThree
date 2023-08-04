using SocketIOClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Common;
using System.Runtime.Serialization.Formatters.Binary;

namespace TeacherTool
{
    public partial class TeacherTool : Form
    {
        //User Token which identifys a student
        private string token;
        //Socket Object to connect to server
        private SocketIO socket;
        //Currently Selected Class
        private string classId = string.Empty;
        //List of classes the teacher owns
        private List<string> classIds = new List<string>();
        //This event is called when the user connects to the server

        private void connectionEstablished(object Sender, EventArgs e)
        {
            // get the socket Object
            socket = (SocketIO)Sender;
            Console.WriteLine("Connected");
            //When we connect to the server we give it the classes we think we are apart
            //the server double checks these classes to make sure they are real and asks us to remove some if they no longer exist

            //!!!!!!!!!!!!!!!***************!!!!!!!!!!!!!!! This is not tested please test it sometime
            socket.On("newClassList", res =>
            {
                List<string> classes = new List<string>();
                //Loop through all classes teacher is apart of
                for (int i = 0; i < res.GetValue(0).GetArrayLength(); i++)
                    classes.Add(res.GetValue(0)[i].ToString());

                //Update the classData file
                File.WriteAllLines("./classData", classes.ToArray());
            });
            socket.On("UserDeath", res => {
                File.Delete("./cache");
                Application.Restart();
            });
            //In the event that we've asked the server to create a class the server then needs to return the class ID to us 
            //This is confirmation that the class has been made
            socket.On("returnClass", res =>
            {
                //get the classID
                classId = res.GetValue(0).GetRawText().Replace("\"", "");
                Console.WriteLine("Get New ClassID: " + classId);
                //Delegate the task of Updating the UI to the UI element
                classUIID.Invoke((MethodInvoker)delegate { classUIID.Text = classId; });
                //Make sure classData file exists If not create one
                if (!File.Exists("./classData"))
                    File.Create("./classData").Close();
                //Read the file and update the List within the progam
                classIds = new List<string>(File.ReadAllLines("./classData"));
                //Add the new class to the list
                classIds.Add(classId);
                //Write the new classList to the file 
                File.WriteAllLines("./classData", classIds.ToArray());
            });
            //When we ask the server for a students details the students give them to the server
            //Then the server class this event and gives us the Students Details back
            socket.On("returnStudentDetails", async res =>
            {
                //Create a byte array to store the details that we recive from the server
                List<byte[]> guh = new List<byte[]>();
                //SocektIO has this weird thing where if data is in the format of binary it gets sent after the event is called
                //So we have to setup a task that reads the bytes and when its done when can use all the data
                await Task.Run(() => { guh = res.InComingBytes; });
                //create a BinaryFormatter so we can convert the data
                BinaryFormatter bf = new BinaryFormatter();
                //delegate a task to the form because ill be working alot with UI in this bit and they run on differnt threads
                //So if I dont delegate this task it will throw a thread error
                this.Invoke((MethodInvoker)delegate {
                    //Stream the incoming bytes
                    using (var ms = new MemoryStream(guh[0]))
                    {
                        //Create a new StudentDetails object from the data stream via Deserializeation
                        StudentDetails studentDetails = (StudentDetails)bf.Deserialize(ms);
                        //Set the a screenshot of the students screen to the UI
                        studentWindow.Image = studentDetails.screenshot;
                        //Clear the previous list of errors so we can add some new updated ones
                        errorList.Items.Clear();
                        //Loop through each error in the error list and add it to the UI
                        foreach (string s in studentDetails.errors)
                            errorList.Items.Add(new ListViewItem(s));
                        //Loop through each KeyValuePair in the dict of completed lessons
                        //Write each lesson to the UI so the teacher can see what lesson the students working on
                        //check if the lesson has been completed by the student and set the text to Completed or Incomplete appropriately 
                        foreach (KeyValuePair<string, bool> i in studentDetails.completedLessons)
                            taskList.Items.Add(new ListViewItem(i.Key + " - " + (i.Value == true ? "Completed" : "Incomplete")));

                    }
                });
            });
            //This event is trigger when the server has infomation on a student in the class 
            //Teacher(Reloads StudentList) -> Server(Asks each student for their name and profile pictures) -> Teacher(gets all the info and write it to the UI)
            socket.On("studentInfo", async res => {
                // _name, _pfp, _token (Format of the response)
                //Get the students Token
                string token = res.GetValue(2).GetString();
                //Get the Students Name
                string name = res.GetValue(0).GetString();

                //SocketIO sends binary infomation in later packets rather than with the response
                //Create a byte array to store the profile picture of the student
                List<byte[]> guh = new List<byte[]>();
                //Set the byte[] to the InComingBytes which could take some time to arrive so we wait for the task to finish before we use the data
                await Task.Run(() => { guh = res.InComingBytes; });

                //New Bitmap
                Bitmap bmp;
                //Stream the byte array and write it to the bitmap
                using (var ms = new MemoryStream(guh[0]))
                    bmp = new Bitmap(ms);

                //To avoid double ups of students profile cards on the teachers UI we check if the students already on the UI
                if (studentSelect.Controls.ContainsKey(token))
                {
                    //Student already om the UI so we find the existing UI object
                    StudentUIPanel studentCard = ((StudentUIPanel)studentSelect.Controls.Find(token, true)[0]);
                    //Once again working with the UI in a networking thread rather than the form thread so I gotta delegate the task
                    studentCard.Invoke((MethodInvoker)delegate {
                        //Update the UI of the object with the new name and profile pic
                        studentCard.studentName.Text = name;
                        studentCard.profilePicture.Image = bmp;
                    });
                }
                else
                {
                    //Student does not exist so we make a new card for them
                    StudentUIPanel Student = new StudentUIPanel(name, bmp, token);
                    //Add an eventlistner for when the teacher wants to inspect a student
                    Student.profilePicture.Click += studentClick;
                    //Set the name of the object to the students token so it can be found later and I dont create double ups
                    Student.Name = token;
                    //delegate the task of adding this new student card to the form thread so no cross threading exceptions
                    studentSelect.Invoke((MethodInvoker)delegate { studentSelect.Controls.Add(Student); });
                }
            });
        }
        public void forceRestart() => Application.Restart();
        public TeacherTool()
        {
            //Try and read token if it dont work then we need to Register the teacher
            try { token = File.ReadAllLines("./cache")[0]; }
            catch { 
                this.Enabled = false;
                TeacherRegister t = new TeacherRegister();
                t.Show();
                t.owner = this;
                return;
            }
            //Class data file is used to store all classes the Teacher is the owner of
            //Its important that we check if the file exists before we try to use it or else we throw an error
            if (!File.Exists("./classData"))
                File.Create("./classData").Close();
            //Read the list of classes IDs the teacher owns and store them for later
            classIds = new List<string>(File.ReadAllLines("./classData"));

            //Connect to my Node JS server
            socket = new SocketIO("https://learnappserver20.nathankleine1.repl.co");
            //Create a dict of headers that will be sent with the request to establish connection to the server
            Dictionary<string, string> headers = new Dictionary<string, string>();
            //Send the persistent user token to the server
            headers.Add("token", token);
            //This user is a teacher so we should set this head as true so the server knows
            headers.Add("teacher", true.ToString());
            
            //Format the list of classes the teacher owns into a string seperated by commas so the server can verify that all the users classes exist
            string classes = "";
            foreach (string classId in classIds)
                classes += classId + ",";
            //If we are apart of more than one class remove the trailing comma so not to confuse the server when it parses data
            if (classes.Length > 1)
                classes = classes.Substring(0, classes.Length - 1);
            //Send it with the headers
            headers.Add("classes", classes);

            //Set the headers
            socket.Options.ExtraHeaders = headers;
            //Call connectionEstablished event handler when we connect
            socket.OnConnected += connectionEstablished;
            //Connect to the server asynchronously
            socket.ConnectAsync();
            InitializeComponent();
        }

        private void T_FormClosed(object sender, FormClosedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void createClass(object sender, EventArgs e)
        {
            //Ensure we have a connection 
            if (socket == null)
                return;
            //Ask the teacher to enter the name of the class
            DialogBox d = new DialogBox("Enter Classroom name", (string s) =>{
                //Ensure there is no empty input
                if(s != "")
                    //call the 'createClass' event on the server and pass the teachers token and class name
                    socket.EmitAsync("createClass", new object[] { token, s });
            });
            //Utilize classroom name in a future version
        }
        //Called when the teacher clicked on a students card
        private void studentClick(object sender, EventArgs e)
        {
            //Get the control of the object
            Control s = (Control)sender;
            //Get its paret which stores the students data
            StudentUIPanel selected = (StudentUIPanel)s.Parent;
            //Set the UI element that shows which student is selected to the students name
            studentName.Text = selected.studentName.Text;
            //Promt the server for infomation on the students activities so I can display it on the UI
            socket.EmitAsync("getStudentDetails",new object[]{classId,selected.token,socket.Id});
        }
        private void TeacherPlanner_Load(object sender, EventArgs e)
        {

        }
        //Called when the teachers wants to push a new lesson to the classroom
        private void pushLesson_Click(object sender, EventArgs e)
        {
            //Open a dialog box so the teacher can select the lesson file
            OpenFileDialog lessonSelector = new OpenFileDialog();
           
            lessonSelector.FileOk += (object s, CancelEventArgs okEvent) => {
                //Get the data of the lesson file
                byte[] data = File.ReadAllBytes(lessonSelector.FileName);
                //Call the event submitLessons on the server pass along the selected class ID and the data of the lesson
                socket.EmitAsync("submitLessons", new object[] { classId, data });
            };
            //Display the box
            lessonSelector.ShowDialog();
        }
        //Called when the teacher wants the change classes 
        private void select_class_Click(object sender, EventArgs e)
        {
            //All classes that the teacher is owner of are stored in a file called class data
            //Check that the file exists and make create one if it dont
            if (!File.Exists("./classData"))
                File.Create("./classData").Close();
            //Read all the classIDs from the file 
            classIds = new List<string>(File.ReadAllLines("./classData"));
            //Create a right click context menu(like the ones found on websites and desktpos) to display all the options for the teacher
            ContextMenu c = new ContextMenu();
            //Loop through the classes and add each class to the context menu
            foreach (string s in classIds)
            {
                //Add an event for when each class get selected
                c.MenuItems.Add(s).Click += (object menu, EventArgs even) =>
                {
                    //Update the class ID
                    classId = s;
                    //Update the UI
                    classUIID.Text = s;
                    //maybe get classroom data later 
                };
            }
            //Set the location of the context menu to the top right of the label so it aligns properly and looks nicer
            c.Show((Control)sender, new Point(0, 18));
        }
        //Called when the teacher reloads student profiles 
        //We send a request to the server asking to get all the class profiles for a specified class room and we send our token as proof that we own it 
        private void Profiles_Click(object sender, EventArgs e) => socket.EmitAsync("getClassProfiles", new object[]{ classId, token});
    }
   
}
