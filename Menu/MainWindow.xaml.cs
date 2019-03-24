using System;
using System.Collections.Generic;
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
using System.IO;
using Microsoft.Win32;

namespace Menu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string currentfile = "";
        private string initialDir;
        bool opgeslagen = false;

        public MainWindow()
        {
            InitializeComponent();

            MISave.IsEnabled = false;
            MISaveAs.IsEnabled = false;
            MICut.IsEnabled = false;
            MICopy.IsEnabled = false;
            MIPaste.IsEnabled = false;
            
            tab1.Header = "Untitled";
            initialDir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        private void NewMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainWindow secondWindow = new MainWindow();
            secondWindow.Show();
        }

        private void OpenMenuItem_click(object sender, RoutedEventArgs e)
        {
            
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = initialDir;
            if (dialog.ShowDialog() == true)
            {
                currentfile = dialog.FileName;
                using (StreamReader inputstream = File.OpenText(currentfile))
                { 
                    textBox.Text = inputstream.ReadToEnd();
                }

            }
            tab1.Header = splitString(currentfile);
            MISave.IsEnabled = true;
            MISaveAs.IsEnabled = true;
        }

        private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (currentfile == "")
            {
                SaveFileDialog dialog = new SaveFileDialog()
                {
                    InitialDirectory = initialDir,
                    DefaultExt = "txt",
                    Filter = "txt files (*.txt)|.txt| Word document (*.docx)|*.docx",
                    FilterIndex = 1
                };


                try
                {
                    if (dialog.ShowDialog() == true)
                    {
                        currentfile = dialog.FileName;
                    }
                    using (StreamWriter output = File.CreateText(currentfile))
                    {
                        output.Write(textBox.Text);
                    }
                    opgeslagen = true;
                }
                catch (System.ArgumentException ex)
                {

                    currentfile = "";
                }
               
            }

        }

        private void SaveAsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (currentfile == "") ;
            {
                SaveFileDialog dialog = new SaveFileDialog()
                {
                    InitialDirectory = initialDir,
                    DefaultExt = "txt",
                    Filter = "txt files (*.txt)|.txt| Word document (*.docx)|*.docx",
                    FilterIndex = 1
                };


                try
                {
                    if (dialog.ShowDialog() == true)
                    {
                        currentfile = dialog.FileName;
                        using (StreamWriter output = File.CreateText(currentfile))
                        {
                            output.Write(textBox.Text);
                        }
                        opgeslagen = true;
                    }
                    
                }
                catch (System.ArgumentException ex)
                {

                    currentfile = "";
                }

            }


        }

        private void CutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            textBox.Cut();
        }

        private void CopyMenuItem_Click(object sender, RoutedEventArgs e)
        {
            textBox.Copy();
            /*
            if (textBox.SelectedText != "")
            {
                Clipboard.SetDataObject(textBox.SelectedText);
            }
            */
        }

        private void PasteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            textBox.Paste();
          
            /*
            IDataObject iData = Clipboard.GetDataObject();
            if (iData.GetDataPresent(DataFormats.Text))
            {
                textBox.AppendText((string)iData.GetData(DataFormats.Text));
                
            }
            */
        }
                      
        private void NewTabMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //TabItem newTabItem = new TabItem;

            //TabControl.item
        }
        

        private void ExitMenuItem_click(object sender, RoutedEventArgs e)
        {
            if (!opgeslagen)
            {
                MessageBox.Show("Ben je zeker? Je werk is niet opgeslagen.");
            }
            else
            {
                Environment.Exit(0);
            }
        }

        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new AboutWindow();
            newWindow.ShowDialog();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            MISave.IsEnabled = true;
            MISaveAs.IsEnabled = true;
            MIPaste.IsEnabled = true;
            int chars = textBox.Text.Length;
            charCountLbl.Text = $"Chars: {chars}";

            opgeslagen = false;
            
        }

        private void textBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (textBox.SelectionLength > 0)
            {
                MICut.IsEnabled = true;
                MICopy.IsEnabled = true;
            }
            else
            {
                MICut.IsEnabled = false;
                MICopy.IsEnabled = false;
            }
        }

        private string splitString(string path)
        {
            string[] words;
            char[] seperator = {'\\'};
            words = path.Split(seperator);

            return words[words.Length -1];
        }

        /// <summary>
        /// Onderstaande methode werkt niet. Daarom is er voor geopteerd om de menuItems actief te maken vanuit de eventhandler "open"
        /// </summary>
        private void MenuItem_SubmenuClosed(object sender, RoutedEventArgs e)
        {
            MISave.IsEnabled = true;
            MISaveAs.IsEnabled = true;
        }

       
    }
}
