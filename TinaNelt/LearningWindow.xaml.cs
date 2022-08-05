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
using System.Windows.Shapes;

namespace TinaNelt
{
    /// <summary>
    /// Interaction logic for LearningWindow.xaml
    /// </summary>
    public partial class LearningWindow : Window
    {
        public LearningWindow(SortedDictionary<int, BitmapImage> images, Divisions division)
        {
            InitializeComponent();          
            foreach(var image in images)
            {
                Border br = new Border();
                br.BorderBrush = Brushes.Gray;
                br.BorderThickness = new Thickness(2);
                br.Margin = new Thickness(20,10,20,0);
                Image tempImage = new Image();                
                tempImage.Source = image.Value;
                br.Child = tempImage;
                MainStackPanel.Children.Add(br);
            }
        }
    }
}
