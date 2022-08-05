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
using System.Windows.Shapes;

namespace TinaNelt
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {

        private bool[] correctAnswers = new bool[10];
        bool ended = false;
        bool polozio = false;
        private int Division;

        public TestWindow(List<QuestionAndAnswers> allQuestions, int division)
        {
            InitializeComponent();
            Division = division;
            for (int i = 0; i < 10; i++)
            {
                correctAnswers[i] = false;
            }
            Random rnd = new Random();

            List<int> randomIdxs = new List<int>();
            int newIdx;
            for (int i = 0; i < 10; i++)
            {
                do
                {
                    newIdx = rnd.Next(allQuestions.Count);
                } while (randomIdxs.Contains(newIdx));
                randomIdxs.Add(newIdx);
            }

            List<QuestionAndAnswers> newQuestions = new List<QuestionAndAnswers>();

            foreach (var idx in randomIdxs)
            {
                newQuestions.Add(allQuestions[idx]);
            }

            int groupId = 0;
            foreach (var question in newQuestions)
            {
                string correctedRightAnswer = question.RightAnswer.Replace("!", string.Empty);

                Border br = new Border();
                br.BorderBrush = Brushes.Gray;
                br.BorderThickness = new Thickness(1);
                br.Margin = new Thickness(5);

                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Vertical;


                TextBlock tb = new TextBlock();
                tb.Text = question.Question;
                tb.Margin = new Thickness(10, 5, 5, 5);

                int rightRadioButton = rnd.Next(0, 3);

                RadioButton rb1 = new RadioButton();
                rb1.GroupName = groupId.ToString();
                rb1.Margin = new Thickness(15, 1, 1, 1);

                RadioButton rb2 = new RadioButton();
                rb2.GroupName = groupId.ToString();
                rb2.Margin = new Thickness(15, 1, 1, 1);

                RadioButton rb3 = new RadioButton();
                rb3.GroupName = groupId.ToString();
                rb3.Margin = new Thickness(15, 1, 1, 10);

                if (rightRadioButton == 0)
                {
                    rb1.Content = correctedRightAnswer;
                    rb1.Tag = "TRUE";
                    rb2.Content = question.WrongAnswers[0];
                    rb2.Tag = "FALSE";
                    rb3.Content = question.WrongAnswers[1];
                    rb3.Tag = "FALSE";
                }
                else if (rightRadioButton == 1)
                {
                    rb2.Content = correctedRightAnswer;
                    rb2.Tag = "TRUE";
                    rb1.Content = question.WrongAnswers[0];
                    rb1.Tag = "FALSE";
                    rb3.Content = question.WrongAnswers[1];
                    rb3.Tag = "FALSE";
                }
                else if (rightRadioButton == 2)
                {
                    rb3.Content = correctedRightAnswer;
                    rb3.Tag = "TRUE";
                    rb1.Content = question.WrongAnswers[0];
                    rb1.Tag = "FALSE";
                    rb2.Content = question.WrongAnswers[1];
                    rb2.Tag = "FALSE";
                }




                sp.Children.Add(tb);
                sp.Children.Add(rb1);
                sp.Children.Add(rb2);
                sp.Children.Add(rb3);

                br.Child = sp;

                MainStackPanel.Children.Add(br);

                groupId++;
            }
            Button btn = new Button();
            btn.Click += btn1_Click;
            btn.Content = "PROVERI";
            btn.Margin = new Thickness(10);
            btn.Name = "ZavrsiTest";
            MainStackPanel.Children.Add(btn);
        }


        private void btn1_Click(object sender, RoutedEventArgs e)
        {

            if (ended)
            {   
                Window rootWindow = Application.Current.MainWindow as MainWindow;
                foreach(Button btn in FindVisualChildren<Button>(rootWindow))
                {
                    if (btn.Tag.ToString() == Division.ToString())
                    {
                        if (polozio)
                        {
                            btn.Background = Brushes.LawnGreen;
                        }
                        else
                        {
                            btn.Background = Brushes.DarkRed;
                        }
                    }
                }
                this.Close();
            }
            else
            {
                foreach (RadioButton rb in FindVisualChildren<RadioButton>(this))
                {
                    int group = Convert.ToInt32(rb.GroupName);

                    switch (group)
                    {
                        case 0:
                            if (rb.Tag.ToString() == "TRUE" && rb.IsChecked == true)
                            {
                                correctAnswers[0] = true;
                            }
                            break;
                        case 1:
                            if (rb.Tag.ToString() == "TRUE" && rb.IsChecked == true)
                            {
                                correctAnswers[1] = true;
                            }
                            break;
                        case 2:
                            if (rb.Tag.ToString() == "TRUE" && rb.IsChecked == true)
                            {
                                correctAnswers[2] = true;
                            }
                            break;
                        case 3:
                            if (rb.Tag.ToString() == "TRUE" && rb.IsChecked == true)
                            {
                                correctAnswers[3] = true;
                            }
                            break;
                        case 4:
                            if (rb.Tag.ToString() == "TRUE" && rb.IsChecked == true)
                            {
                                correctAnswers[4] = true;
                            }
                            break;
                        case 5:
                            if (rb.Tag.ToString() == "TRUE" && rb.IsChecked == true)
                            {
                                correctAnswers[5] = true;
                            }
                            break;
                        case 6:
                            if (rb.Tag.ToString() == "TRUE" && rb.IsChecked == true)
                            {
                                correctAnswers[6] = true;
                            }
                            break;
                        case 7:
                            if (rb.Tag.ToString() == "TRUE" && rb.IsChecked == true)
                            {
                                correctAnswers[7] = true;
                            }
                            break;
                        case 8:
                            if (rb.Tag.ToString() == "TRUE" && rb.IsChecked == true)
                            {
                                correctAnswers[8] = true;
                            }
                            break;
                        case 9:
                            if (rb.Tag.ToString() == "TRUE" && rb.IsChecked == true)
                            {
                                correctAnswers[9] = true;
                            }
                            break;
                    }


                    if (rb.Tag.ToString() == "TRUE")
                    {
                        rb.Background = Brushes.Green;
                        rb.Opacity = 0.5;
                    }
                    if (rb.Tag.ToString() == "FALSE" && rb.IsChecked == true)
                    {
                        rb.Background = Brushes.Red;
                        rb.Opacity = 0.5;
                    }

                }

                int numberOfCorrectAnswers = correctAnswers.Count(c => c);
                int numberOfWrongAnsers = 10 - numberOfCorrectAnswers;

                List<int> indices = new List<int>();
                for (int i = 0; i < correctAnswers.Length; ++i)
                {
                    if (!correctAnswers[i])
                    {
                        indices.Add(i);
                    }
                }

                string wrongAwnsers = "";
                if (indices.Count == 0)
                {
                    wrongAwnsers = " Nema netačnih pitanja";
                }
                else
                {
                    wrongAwnsers += "| ";
                    foreach (var idx in indices)
                    {
                        wrongAwnsers += (idx + 1) + ". | ";
                    }
                }


                if (numberOfCorrectAnswers > 8)
                {
                    MessageBox.Show("Broj grešaka: " + numberOfWrongAnsers + " . (Netačna pitanja: " + wrongAwnsers + ")", "Uspešno položen test!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    polozio = true;

                }
                else
                {
                    MessageBox.Show("Broj grešaka: " + numberOfWrongAnsers + " . (Netačna pitanja: " + wrongAwnsers + ")", "Pali ste test!", MessageBoxButton.OK, MessageBoxImage.Error);
                    polozio = false;

                }
                foreach (Button btn in FindVisualChildren<Button>(this))
                {
                    btn.Content = "ZAVRŠI TEST";
                }
                ended = true;
            }
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
}
