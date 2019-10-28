using Microsoft.Win32;
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

namespace Matlak_Hw4_Main_CSharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog= new OpenFileDialog();

            openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            openFileDialog.Filter = "JSON files (*.json) | *.json";
            // If you have multiple filters is will select the the that is in the given index and the rest will be seen in the dropdown menu
            // Where you will need to manually select them
            openFileDialog.FilterIndex = 1;

            // What does this do?
            //openFileDialog1.RestoreDirectory = true;
            if(openFileDialog.ShowDialog() == true)
            {
                courseWorkTB.Text = openFileDialog.FileName;
            }
            //System.Diagnostics.Process.Start(Directory.GetCurrentDirectory());
            //courseWorkTB.Text = Directory.GetCurrentDirectory();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
