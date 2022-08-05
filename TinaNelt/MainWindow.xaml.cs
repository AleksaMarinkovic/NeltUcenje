using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TinaNelt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SortedDictionary<int,BitmapImage>[] images = new SortedDictionary<int, BitmapImage>[10];
        public List<QuestionAndAnswers>[] questions = new List<QuestionAndAnswers>[10];
        public List<string> names = new List<string>();
        public ObservableCollection<Keyword> Keywords { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Keywords = new ObservableCollection<Keyword>();
            CenterWindowOnScreen();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region Ucitavanje slika
            ResourceSet rs = Properties.Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            IDictionaryEnumerator id = rs.GetEnumerator();

            for (int i = 0; i < 10; i++)
            {
                images[i] = new SortedDictionary<int, BitmapImage>();
            }

            while (id.MoveNext())
            {
                if (!id.Key.ToString().Contains("LOQuestions") && !id.Key.ToString().Contains("Keywords") && !id.Key.ToString().Contains("icon"))
                {
                    string[] name = id.Key.ToString().Split('_');
                    string division = name[1];
                    string imageNumber = name[2];


                    object image = Properties.Resources.ResourceManager.GetObject(id.Key.ToString());
                    switch (division)
                    {
                        case "0":
                            images[0].Add(Convert.ToInt32(imageNumber), BitmapToBitmapImageConverter.ToBitmapImage((Bitmap)image));
                            break;
                        case "1":
                            images[1].Add(Convert.ToInt32(imageNumber), BitmapToBitmapImageConverter.ToBitmapImage((Bitmap)image));
                            break;
                        case "2":
                            images[2].Add(Convert.ToInt32(imageNumber), BitmapToBitmapImageConverter.ToBitmapImage((Bitmap)image));
                            break;
                        case "3":
                            images[3].Add(Convert.ToInt32(imageNumber), BitmapToBitmapImageConverter.ToBitmapImage((Bitmap)image));
                            break;
                        case "4":
                            images[4].Add(Convert.ToInt32(imageNumber), BitmapToBitmapImageConverter.ToBitmapImage((Bitmap)image));
                            break;
                        case "5":
                            images[5].Add(Convert.ToInt32(imageNumber), BitmapToBitmapImageConverter.ToBitmapImage((Bitmap)image));
                            break;
                        case "6":
                            images[6].Add(Convert.ToInt32(imageNumber), BitmapToBitmapImageConverter.ToBitmapImage((Bitmap)image));
                            break;
                        case "7":
                            images[7].Add(Convert.ToInt32(imageNumber), BitmapToBitmapImageConverter.ToBitmapImage((Bitmap)image));
                            break;
                        case "8":
                            images[8].Add(Convert.ToInt32(imageNumber), BitmapToBitmapImageConverter.ToBitmapImage((Bitmap)image));
                            break;
                        case "9":
                            images[9].Add(Convert.ToInt32(imageNumber), BitmapToBitmapImageConverter.ToBitmapImage((Bitmap)image));
                            break;
                    }

                }
            }
            #endregion


            #region Ucitavanje pitanja

            for (int i = 0; i < 10; i++)
            {
                questions[i] = new List<QuestionAndAnswers>();
            }

            string resource_data = TinaNelt.Properties.Resources.LOQuestions;
            List<string> allQuestions = new List<string>();
            allQuestions = resource_data.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (var entry in allQuestions)
            {
                string[] question = entry.Split('/');
                int division = Convert.ToInt32(question[0]);

                string actualQuestion = question[1];
                string answer1 = question[2];
                string answer2 = question[3];
                string answer3 = question[4];

                string rightAnswer = "";
                string[] wrongAnswers = new string[2];
                if (answer1.Contains('!'))
                {
                    rightAnswer = answer1;
                    wrongAnswers[0] = answer2;
                    wrongAnswers[1] = answer3;
                }
                else if (answer2.Contains('!'))
                {
                    rightAnswer = answer2;
                    wrongAnswers[0] = answer1;
                    wrongAnswers[1] = answer3;
                }
                else if (answer3.Contains('!'))
                {
                    rightAnswer = answer3;
                    wrongAnswers[0] = answer2;
                    wrongAnswers[1] = answer1;
                }

                QuestionAndAnswers QnA = new QuestionAndAnswers(actualQuestion, wrongAnswers, rightAnswer);


                switch (division)
                {
                    case 0:
                        questions[0].Add(QnA);
                        break;
                    case 1:
                        questions[1].Add(QnA);
                        break;
                    case 2:
                        questions[2].Add(QnA);
                        break;
                    case 3:
                        questions[3].Add(QnA);
                        break;
                    case 4:
                        questions[4].Add(QnA);
                        break;
                    case 5:
                        questions[5].Add(QnA);
                        break;
                    case 6:
                        questions[6].Add(QnA);
                        break;
                    case 7:
                        questions[7].Add(QnA);
                        break;
                    case 8:
                        questions[8].Add(QnA);
                        break;
                    case 9:
                        questions[9].Add(QnA);
                        break;

                }
            }


            #endregion

            #region Ucitavanje kljucnih_pitanja

            string resource_data1 = TinaNelt.Properties.Resources.Keywords;
            List<string> everyLine = resource_data1.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (var entry in everyLine)
            {
                string[] parts = entry.Split('/');
                Keyword keyword = new Keyword((Divisions)Convert.ToInt32(parts[0]), parts[1]);
                Keywords.Add(keyword);
            }
            Combobox.SelectedIndex = 0;

            #endregion

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string senderTag = (e.OriginalSource as FrameworkElement).Tag.ToString();
            switch (senderTag)
            {
                case "0":
                    TestWindow tw0 = new TestWindow(questions[0], 0);
                    tw0.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    tw0.Show();
                    break;
                case "0u":
                    LearningWindow lw1 = new LearningWindow(images[0], (Divisions)0);
                    lw1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    lw1.Show();
                    break;
                case "1":
                    TestWindow tw1 = new TestWindow(questions[1], 1);
                    tw1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    tw1.Show();
                    break;
                case "1u":
                    LearningWindow lw2 = new LearningWindow(images[1], (Divisions)1);
                    lw2.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    lw2.Show();
                    break;
                case "2":
                    TestWindow tw2 = new TestWindow(questions[2], 2);
                    tw2.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    tw2.Show();
                    break;
                case "2u":
                    LearningWindow lw3 = new LearningWindow(images[2], (Divisions)2);
                    lw3.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    lw3.Show();
                    break;
                case "3":
                    TestWindow tw3 = new TestWindow(questions[3], 3);
                    tw3.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    tw3.Show();
                    break;
                case "3u":
                    LearningWindow lw4 = new LearningWindow(images[3], (Divisions)3);
                    lw4.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    lw4.Show();
                    break;
                case "4":
                    TestWindow tw4 = new TestWindow(questions[4], 4);
                    tw4.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    tw4.Show();
                    break;
                case "4u":
                    LearningWindow lw5 = new LearningWindow(images[4], (Divisions)4);
                    lw5.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    lw5.Show();
                    break;
                case "5":
                    TestWindow tw5 = new TestWindow(questions[5], 5);
                    tw5.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    tw5.Show();
                    break;
                case "5u":
                    LearningWindow lw6 = new LearningWindow(images[5], (Divisions)5);
                    lw6.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    lw6.Show();
                    break;
                case "6":
                    TestWindow tw6 = new TestWindow(questions[6], 6);
                    tw6.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    tw6.Show();
                    break;
                case "6u":
                    LearningWindow lw7 = new LearningWindow(images[6], (Divisions)6);
                    lw7.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    lw7.Show();
                    break;
                case "7":
                    TestWindow tw7 = new TestWindow(questions[7], 7);
                    tw7.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    tw7.Show();
                    break;
                case "7u":
                    LearningWindow lw8= new LearningWindow(images[7], (Divisions)7);
                    lw8.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    lw8.Show();
                    break;
                case "8":
                    TestWindow tw8 = new TestWindow(questions[8], 8);
                    tw8.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    tw8.Show();
                    break;
                case "8u":
                    LearningWindow lw9 = new LearningWindow(images[8], (Divisions)8);
                    lw9.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    lw9.Show();
                    break;
                case "9":
                    TestWindow tw9 = new TestWindow(questions[9], 9);
                    tw9.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    tw9.Show();
                    break;
                case "9u":
                    LearningWindow lw10 = new LearningWindow(images[9], (Divisions)9);
                    lw10.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    lw10.Show();
                    break;
            }
            
        }

        private void CenterWindowOnScreen()
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var cbi = Combobox.SelectedValue;
            switch (Convert.ToInt32(cbi))
            {
                case 0:
                    LearningWindow lw1 = new LearningWindow(images[0], (Divisions)0);
                    lw1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    lw1.Show();
                    break;
                case 1:
                    LearningWindow lw2 = new LearningWindow(images[1], (Divisions)1);
                    lw2.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    lw2.Show();
                    break;
                case 2:
                    LearningWindow lw3 = new LearningWindow(images[2], (Divisions)2);
                    lw3.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    lw3.Show();
                    break;
                case 3:
                    LearningWindow lw4 = new LearningWindow(images[3], (Divisions)3);
                    lw4.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    lw4.Show();
                    break;
                case 4:
                    LearningWindow lw5 = new LearningWindow(images[4], (Divisions)4);
                    lw5.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    lw5.Show();
                    break;
                case 5:
                    LearningWindow lw6 = new LearningWindow(images[5], (Divisions)5);
                    lw6.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    lw6.Show();
                    break;
                case 6:
                    LearningWindow lw7 = new LearningWindow(images[6], (Divisions)6);
                    lw7.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    lw7.Show();
                    break;
                case 7:
                    LearningWindow lw8 = new LearningWindow(images[7], (Divisions)7);
                    lw8.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    lw8.Show();
                    break;
                case 8:
                    LearningWindow lw9 = new LearningWindow(images[8], (Divisions)8);
                    lw9.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    lw9.Show();
                    break;
                case 9:
                    LearningWindow lw10 = new LearningWindow(images[9], (Divisions)9);
                    lw10.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    lw10.Show();
                    break;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
