//******************************************************
// File: MainWindows.xaml.cs
//
// Purpose: Hold all functionality for events triggered on GUI
//
// Written By: Ian Matlak
// Date: 11/11/2019
//
// Compiler: Visual Studio 2019
// 
//******************************************************
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Matlak_Hw4_Csharp_DLL;
using System.Runtime.Serialization.Json;
using System.Windows.Forms;

namespace Matlak_Hw4_Main_CSharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // When a variable is declared as static, then a single copy of the variable is created and shared among all objects at the class level
        // Creates statick CourseWork and Statick OpenFileDialog
        public static CourseWork cw = new CourseWork();
        public static OpenFileDialog openFileDialog = new OpenFileDialog();

        public MainWindow()
        {
            InitializeComponent();

            // Makes text boxes only readable
            courseWorkTB.IsReadOnly = true;
            courseNameTB.IsReadOnly = true;
            overallGradeTB.IsReadOnly = true;
            assignmentNameTB.IsReadOnly = true;
            categoriesNameTB.IsReadOnly = true;
            gradeTB.IsReadOnly = true;
        }
        private void OpenCW_Click(object sender, RoutedEventArgs e)
        {
            // Created a insteance of File Dialog window

            // WHY if i put this here the filter doesnt work?
            // DialogResult result = openFileDialog.ShowDialog();

            // Sets the default path to the dialog window to the current project directory
            openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();

            // Sets the default filter of the dialog windows 
            openFileDialog.Filter = "JSON files (*.json) | *.json";

            // If you have multiple filters is will select the the that is in the given index and the rest will be seen in the dropdown menu
            // Where you will need to manually select them
            openFileDialog.FilterIndex = 1;

            // Creates a dialog result obj based on openFileDialog
            DialogResult result = openFileDialog.ShowDialog();

            // If the dialog box OK is pressed
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                // Clear the List Views
                categoryLV.Items.Clear();
                assignmentLV.Items.Clear();
                submissionLV.Items.Clear();

                // Set text box to the file name selected
                courseWorkTB.Text = openFileDialog.FileName;

                // Deserialize JSON file and save results in cw coursework variable
                cw = (CourseWork)ReadJsonFile(cw);

                // Set text box to the course name from the Json file
                courseNameTB.Text = cw.CourseName;

                // Sets the text box to the grade that is calculated in the Dll file.
                // Then converts it to a String
                overallGradeTB.Text = Convert.ToString(cw.CalculateGrade());

                // Adds iterated through Categories list
                // Adds the datat to the list view
                foreach (var i in cw.Categories)
                {
                    categoryLV.Items.Add(i);
                }

                // Adds iterated through Assignment list
                // Adds the datat to the list view
                foreach (var i in cw.Assignment)
                {
                    assignmentLV.Items.Add(i);
                }

                // Adds iterated through Submission list
                // Adds the datat to the list view
                foreach (var i in cw.Submission)
                {
                    submissionLV.Items.Add(i);
                }
                // Sets FindSubmission textbox's to Null when new file is loaded in
                targetAsgNameTB.Text = null;
                assignmentNameTB.Text = null;
                categoriesNameTB.Text = null;
                gradeTB.Text = null;
            }
            // System.Diagnostics.Process.Start(Directory.GetCurrentDirectory());
            // courseWorkTB.Text = Directory.GetCurrentDirectory();
        }
        private void FindSubBTN_Click(object sender, RoutedEventArgs e)
        {
            // If box is null return and do nothing
            if (String.IsNullOrEmpty(targetAsgNameTB.Text))
            {
                return;
            }
            // If FindSubmission return null then return. 
            // This prevents the code from crashing is a user types in the wrong submission assignment name
            if (cw.FindSubmission(targetAsgNameTB.Text) == null)
            {
                return;
            }
            // If submission found is not null then return it and change the text boxes to that submission data
            cw.FindSubmission(targetAsgNameTB.Text);
            assignmentNameTB.Text = cw.FindSubmission(targetAsgNameTB.Text).AssignmentName;
            categoriesNameTB.Text = cw.FindSubmission(targetAsgNameTB.Text).CategoryName;
            gradeTB.Text = cw.FindSubmission(targetAsgNameTB.Text).Grade.ToString();

        }
        //****************************************************
        // Method: ReadJsonFile 
        //
        // Purpose: Generic function to deserialize json file
        //****************************************************
        public static T ReadJsonFile<T>(T obj)
        {
            // Console.Write("Enter Json File Name: "); 
            string fileName = openFileDialog.FileName;
            FileStream reader = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            DataContractJsonSerializer deser;
            deser = new DataContractJsonSerializer(typeof(T));

            obj = (T)deser.ReadObject(reader);

            reader.Close();
            return obj;
        }
    }
}
