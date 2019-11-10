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
        public CourseWork cw = new CourseWork();

        public MainWindow()
        {
            InitializeComponent();
            courseWorkTB.IsReadOnly = true;
            courseNameTB.IsReadOnly = true;
            overallGradeTB.IsReadOnly = true;
        }
        private void OpenCW_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // WHY if i put this here the filter doesnt work?
            // DialogResult result = openFileDialog.ShowDialog();

            openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            openFileDialog.Filter = "JSON files (*.json) | *.json";

            // If you have multiple filters is will select the the that is in the given index and the rest will be seen in the dropdown menu
            // Where you will need to manually select them
            openFileDialog.FilterIndex = 1;

            DialogResult result = openFileDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                categoryLV.Items.Clear();
                assignmentLV.Items.Clear();
                submissionLV.Items.Clear();

                courseWorkTB.Text = openFileDialog.FileName;
                cw = (CourseWork)ReadJsonFile(cw);
                courseNameTB.Text = cw.CourseName;
                overallGradeTB.Text = Convert.ToString(cw.CalculateGrade());
                foreach (var i in cw.Categories)
                {
                    categoryLV.Items.Add(i);
                }
                foreach (var i in cw.Assignment)
                {
                    assignmentLV.Items.Add(i);
                }
                foreach (var i in cw.Submission)
                {
                    submissionLV.Items.Add(i);
                }

                targetAsgNameTB.Text = null;
            }

            T ReadJsonFile<T>(T obj)
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
            // System.Diagnostics.Process.Start(Directory.GetCurrentDirectory());
            // courseWorkTB.Text = Directory.GetCurrentDirectory();
        }
        private void FindSubBTN_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(targetAsgNameTB.Text))
            {
                return;
            }
            cw.FindSubmission(targetAsgNameTB.Text);
            assignmentNameTB.Text = cw.FindSubmission(targetAsgNameTB.Text).AssignmentName;
            categoriesNameTB.Text = cw.FindSubmission(targetAsgNameTB.Text).CategoryName;
            gradeTB.Text = cw.FindSubmission(targetAsgNameTB.Text).Grade.ToString();

        }
    }
}
