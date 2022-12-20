using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MarathonSkills
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
        }

        ~MainWindow()
        {
            Util.db.Database.Connection.Close();
        }

        public string Time
        {
            get
            {
                DateTime dt1 = DateTime.Now;
                DateTime dt2 = DateTime.Parse("2022-12-23 9:00");

                TimeSpan ts = dt2 - dt1;
                return string.Format("{0} дн. {1} ч. {2} мин. {3} сек. до старта марафона!", ts.Days, ts.Hours, ts.Minutes, ts.Seconds);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Timer tmr = new Timer();

            tmr.Interval = 1000;
            tmr.Elapsed += Tmr_Elapsed;

            tmr.Start();
        }

        private void Tmr_Elapsed(object sender, ElapsedEventArgs e)
        {
            PropertyChange("Time");
        }

        private void PropertyChange(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
