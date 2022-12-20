using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
using Path = System.IO.Path;

namespace MarathonSkills
{
    /// <summary>
    /// Логика взаимодействия для WindowCharityInfo.xaml
    /// </summary>
    public partial class WindowCharityInfo : Window
    {
        public WindowCharityInfo(Charity charity)
        {
            InitializeComponent();

            label_CharityName.Content = charity.CharityName;
            textBlock.Text = charity.CharityDescription;
             
            image.Source = new BitmapImage(new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "charities", charity.CharityLogo)));
        }
    }
}
