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

namespace WPF_ImgLoad
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            string fileContent;
            string filePath;
            string fileName;
            string dirPath;
            pathTxt.Text = null;

            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png; | All files (*.*) | *.*; ";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                fileName = openFileDialog.SafeFileName;

                filePath = openFileDialog.FileName;

                BitmapImage bitmap = new BitmapImage();

                bitmap.BeginInit();

                bitmap.UriSource = new Uri(filePath);

                bitmap.EndInit();

                pictureBox1.Source = bitmap;

                var fileStream = openFileDialog.OpenFile();

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    fileContent = reader.ReadToEnd();
                }

                dirPath = filePath.Replace(fileName, "");

                pathTxt.Text = dirPath;

                makeList();
            }
        }
        public void makeList()
        {
            string folderName;
            string[] arr = new string[100];

            listView1.Items.Clear();

            folderName = string.Empty;

            if (pathTxt.Text.Length > 0)
            {
                folderName = pathTxt.Text.Substring(0, pathTxt.Text.Length - 1);
            }
            else
            {
                MessageBox.Show("지정된 경로가 없습니다.");
            }

            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(folderName);

            foreach (System.IO.FileInfo file in dir.GetFiles())
            {
                if (file.Name.Contains(".jpg") || file.Name.Contains(".png") || file.Name.Contains(".jfif") || file.Name.Contains(".jpeg")
                    || file.Name.Contains(".jpe"))
                {
                    ListViewClass.GetInstance().Add(new ListViewClass() { name = file.Name, size = file.Length.ToString(), time = file.CreationTime.ToString() });

                }
            }
            listView1.ItemsSource = ListViewClass.GetInstance();
        }

        private void listView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                ListViewClass items = (ListViewClass)listView1.SelectedItems[0];

                Console.WriteLine(items.name);

                string Path = pathTxt.Text + items.name;

                BitmapImage bitmap = new BitmapImage();

                bitmap.BeginInit();

                bitmap.UriSource = new Uri(Path);

                bitmap.EndInit();

                pictureBox1.Source = bitmap;

            }
        }
    }
}
