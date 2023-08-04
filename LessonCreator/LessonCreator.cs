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
using Common;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;

namespace LessonCreator
{
    public partial class LessonPlanner : Form
    {
        //Currently working with lessons
        private List<Lesson> lessons = new List<Lesson>();
        //the selected lesson
        private LessonButton selected;
        //Path to the file that stores the default code given to a lesson when its created
        //The idea is that this file could be edited for convience and adapted for differnt purposes
        private string defaultCode;
        //Default save path for a lesson
        private string savePath = "./savedLessons/new_unit.les";

        private Theme selectedTheme = new Theme();

        Stack<(string text, int index)> EditHistory = new Stack<(string text, int index)>();
        public LessonPlanner()
        {
            InitializeComponent();
            //make sure that the directory where lessons will be saved exists
            if (!Directory.Exists("./savedLessons"))
                Directory.CreateDirectory("./savedLessons");
            //Ensure that the default code file exists 
            if (File.Exists("./DefaultCode.cs"))
                defaultCode = File.ReadAllText("./DefaultCode.cs");
            codeBlock.SelectionTabs = new int[] { 20, 40, 80, 90, 100, 110, 120 };
        }
        //This event is called when the new Lesson button is clicked
        private void newLesson_Click(object sender, EventArgs e)
        {
            //Create a new lesson object and set some default properties
            Lesson newLesson = new Lesson();
            newLesson.lessonName = "new Lesson";
            newLesson.defaultCode = defaultCode;
            //Add the new lesson to the list of lessons
            lessons.Add(newLesson);
            //Create a new LessonButton for the lesson so the Teacher Can select the unit to work on
            LessonButton newButton = new LessonButton(newLesson);
            //Set the size to the width of the panel its contained in and the height to 40
            newButton.Size = new Size(splitContainer2.Panel2.Width, 40);
            //set the locaiton of the new button to below the last button
            newButton.Location = new Point(0, (lessons.Count-1) * 40);
            //Add the button to the controls of the panel
            lessonButtons.Controls.Add(newButton);

            //set the selected lesson to the new lesson we just made
            selected = newButton;
            //Add an eventlistner to the button for when its clicked
            newButton.Click += selectLessonClick;

            //Give the user the option to delete the button when they right click through a context menu
            ContextMenu c = new ContextMenu();
            c.MenuItems.Add(new MenuItem("Delete", removeLesson));
            newButton.ContextMenu = c;

            //Update the UI with this new lesson 
            lessonContent.Text = newLesson.lessonContent;
            LessonTitle.Text = newLesson.lessonName;
            codeBlock.Text = newLesson.defaultCode;

            //Enable all the UI controls for editing because now a lesson is selected
            setControlsEnabled(true);
            //Save the lesson file
            saveLessonFile(lessons);
        }
        //Called when the user right clicks on a lesson and clicks delete
        //Removes the lesson and its button from the program
        private void removeLesson(object sender, EventArgs e)
        {
            //Get the context menu that called the function
            MenuItem men = (MenuItem)sender;
            //Get the parent of that which is the conext menu
            ContextMenu m = (ContextMenu)men.Parent;
            //Get the lesson button that the menu belongs too
            LessonButton b = (LessonButton)m.SourceControl;
            //Get the Index of the lesson to remove in the lesson list
            int removeIndex = lessons.IndexOf(b.lesson);

            //Remove the lesson from the list of lessons
            lessons.Remove(b.lesson);
            //remove the lesson from the UI controls
            lessonButtons.Controls.Remove(b);
            //Loop through and adjust the position of all the other buttons so there isnt a gap in the UI
            for (int i = removeIndex; i < lessons.Count; i++)
                lessonButtons.Controls[i].Top -= 40;
            //Hide the lesson button and set it to disabled so it essentialy doesnt exist
            b.Visible = false;
            b.Enabled = false;
            //disable editing of the UI controls because now no lesson is selected
            setControlsEnabled(false);
        }
        //called when any lesson is clicked 
        //updates the UI so the teacher can edit the lesson
        private void selectLessonClick(object sender, EventArgs e)
        {
            //Get the lessonButton that was clicked
            selected = (LessonButton)sender;
            //Set the UI elements to the lessons details so it can be edited
            LessonTitle.Text = selected.lesson.lessonName;
            lessonContent.Text = selected.lesson.lessonContent;
            codeBlock.Text = selected.lesson.defaultCode;
            //Allow all UI controls to be edited now that a lesson is selected
            setControlsEnabled(true);
        }
        /// <summary>
        /// disable all UI controls 
        /// </summary>
        /// <param name="enabled">enable or disable</param>
        private void setControlsEnabled(bool enabled)
        {
            lessonContent.Enabled = enabled;
            LessonTitle.Enabled = enabled;
            lessonContent.Enabled = enabled;
            codeBlock.Enabled = enabled;
        }
        //This event is called whenever the user clicks off one of the UI elements after editing it
        //Its meant to create concurrency between the UI and the Lesson object
        private void lessonUpdate(object sender, EventArgs e)
        {
            selected.lesson.lessonName = LessonTitle.Text;
            selected.Text = LessonTitle.Text;
            selected.lesson.lessonContent = lessonContent.Text;
            selected.lesson.defaultCode = codeBlock.Text;
        }
        //Saves a lesson file to the disk
        private void saveLessonFile(List<Lesson> lessons)
        {
            //This is not safe
            //Creates a vulerability might need to look into later
            IFormatter formatter = new BinaryFormatter();
            //Start a file stream 
            Stream fileStream = new FileStream(savePath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
            //Write the lesson to the file
            formatter.Serialize(fileStream, lessons);
            //Close the stream
            fileStream.Close();
        }
        //Called when the user wants to load a lesson to edit
        private void openLesson_Click(object sender, EventArgs e)
        {
            //Open the file Dialog box which is a box that asks the user to select a file
            OpenFileDialog loadLesson = new OpenFileDialog();
            DialogResult res = loadLesson.ShowDialog();
            //Make sure the user clicked ok and actually picked a file and didn't cancel the dialog
            if(res == DialogResult.OK)
            {
                //Get the path to the file
                savePath = loadLesson.FileName;
                //load the file and set its lessons to the current lesson
                lessons = loadLessonFile(savePath);
            }
        }
        /// <summary>
        /// Load a lesson file from the drive
        /// </summary>
        /// <param name="path">path to the lesson file</param>
        /// <returns>List of lessons that comes from the file</returns>
        private List<Lesson> loadLessonFile(string path)
        {
            //get a new BinaryFormatter to interpret the file
            IFormatter formatter = new BinaryFormatter();
            //Stream the file data
            Stream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            //Deserialize the file data and write it to an array 
            List<Lesson> serializedLessons = (List<Lesson>)formatter.Deserialize(fileStream);
            //Close the file stream because I dont need it anymore
            fileStream.Close();
            //Loop through each of the lessons
            for(int lessonOffset = 0; lessonOffset < serializedLessons.Count; lessonOffset++)
            {
                //Get the individual lesson
                Lesson les = serializedLessons[lessonOffset];
                //Create a UI button for the lesson
                LessonButton b = new LessonButton(les);
                //Set its location to the lesson nubmer * 40 so they are spaced properly
                b.Location = new Point(0, lessonOffset * 40);
                //Set the width the the width of the container and height to 40
                b.Size = new Size(splitContainer2.Panel2.Width, 40);
                //All an event for when the lesson is clicked
                b.MouseClick += selectLessonClick;
                //Set the text of the button to the name of the lesson
                b.Text = les.lessonName;

                //Add a right click dialog to the lesson so it can be deleted
                ContextMenu c = new ContextMenu();
                c.MenuItems.Add(new MenuItem("Delete", removeLesson));
                b.ContextMenu = c;

                //Add the new lesson button to the lsit views controls so it can be removed and found later
                lessonButtons.Controls.Add(b);
            }
            // Return the list of lessons
            return serializedLessons;
        }
        //called when the form closes
        //Saves the lessons to keep data safe 
        //In future I should ask the user if they want to save their changes
        private void LessonPlanner_FormClosing(object sender, FormClosingEventArgs e) => saveLessonFile(lessons);
        private void codeBlock_KeyDown(object sender, KeyEventArgs e)
        {

            switch (e.KeyCode)
            {
                case Keys.Z:
                    if (e.Control)
                    {
                        codeBlock.Text = EditHistory.Peek().text;
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
            codeBlock.SelectionColor = Color.Black;
            codeBlock.DeselectAll();

            foreach (KeyValuePair<Regex, Color> x in selectedTheme.syntax)
            {
                //codeBlock.DeselectAll();
                foreach (Match match in x.Key.Matches(codeBlock.Text))
                {
                    codeBlock.Select(match.Index, match.Value.ToString().Length);
                    codeBlock.SelectionColor = x.Value;
                }
            }

            codeBlock.DeselectAll();
            codeBlock.SelectionStart = cursorIndex;
        }
    }
}
