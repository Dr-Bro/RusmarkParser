using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using System.Xml;
using System.Xml.Linq;

namespace WpfApplication2
{
    delegate void UpdateProgressBarDelegate(DependencyProperty dp, object value);
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Кнопка нажата");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            //OpenFileDialog openFileDialog = new OpenFileDialog();

            //openFileDialog.Filter = "Текстик (*.xml)|*.xml";

            //if (openFileDialog.ShowDialog() == true)
            //{
            //    FileInfo fileInfo = new FileInfo(openFileDialog.FileName);

            //    StreamReader reader = new StreamReader(fileInfo.Open(FileMode.Open, FileAccess.Read), Encoding.GetEncoding(1251));

 
             
                var doc = XDocument.Load("setting.xml");
                   
                    var result = doc.Descendants("field").Select(x => new
                    {
                        subfield = x.Element("subfield").Value,
                        translate = x.Element("translate").Value,
                        number = x.Element("number").Value
                    });

                    grid1.ItemsSource = result; // DataGrid
                //reader.Close();
                //return;
            }

        private async void ParserRun(object sender, RoutedEventArgs e)
        {
            parser_check.IsChecked = false;
            progressBar.IsIndeterminate = true;
            await DoWorkAsync();
            progressBar.IsIndeterminate = false;
            MessageBox.Show("Операция выполнена");
            parser_check.IsChecked = true;
        }


        private async Task DoWorkAsync()
        {
            await Task.Run(() =>
            {
                Parser();
            });
        }

        private void Parser()
        {
            RusmarkParser rusmark = new RusmarkParser();
            rusmark.run("sample.xml","setting.xml");
            }
        }
    }
