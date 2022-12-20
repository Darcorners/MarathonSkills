using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace MarathonSkills.Pages
{
    /// <summary>
    /// Логика взаимодействия для SponsorPage.xaml
    /// </summary>
    public partial class SponsorPage : Page, INotifyPropertyChanged
    {
        public int donate = 50;
        public int donation
        {
            get
            {
                return donate;
            } 
            set
            {
                int temp = 0;

                int.TryParse(value.ToString(), out temp);

                if (temp >= 10)
                {
                    donate = value;
                    PropertyChange("donation");
                }
            }
        }

        public SponsorPage()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void PropertyChange (string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/MainPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<RunnerInfo> runnerInfo = new List<RunnerInfo>();
            Util.db.Runner.ToList().ForEach(r => runnerInfo.Add(new RunnerInfo() { runner = r , reg = r }));
            comboBox_Runner.ItemsSource = runnerInfo;
            comboBox_Runner.DisplayMemberPath = "info";

            this.DataContext = this;

            comboBox_Runner.SelectedIndex = 0;
        }

        private void Up_Click(object sender, RoutedEventArgs e)
        {
            if (donation >= 20 || donation == 10)
            {
                donation += 10;
            }
        }

        private void Down_Click(object sender, RoutedEventArgs e)
        {
            if (donation >= 20)
            {
                donation -= 10;
            }
        }

        private void comboBox_Runner_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RunnerInfo runner = comboBox_Runner.SelectedItem as RunnerInfo;

            Registration reg = runner.runner.Registration.SingleOrDefault();

            if(reg != null)
            {
                Charity charity = reg.Charity;

                Label_charity.Content = charity.CharityName;  
            }
        }

        private void Image_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            RunnerInfo runner = comboBox_Runner.SelectedItem as RunnerInfo;

            Registration reg = runner.runner.Registration.SingleOrDefault();

            if (reg != null)
            {
                new WindowCharityInfo(reg.Charity).ShowDialog();
            }
        }

        private void Button_pay_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Name.Text) || Name.Text.Length <= 0)
            {
                MessageBox.Show("Введите ваше имя");
                return;
            }

            if (string.IsNullOrWhiteSpace(Card.Text) || Card.Text.Length <= 0)
            {
                MessageBox.Show("Введите владельца карты");
                return;
            }

            if (string.IsNullOrWhiteSpace(Card_Number.Text) || Card_Number.Text.Length <= 0)
            {
                MessageBox.Show("Введите номер карты");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox_cardMon.Text) || textBox_cardMon.Text.Length <= 0)
            {
                MessageBox.Show("Введите месяц");
                return;
            }

            if (int.Parse(textBox_cardMon.Text) < 1 || int.Parse(textBox_cardMon.Text) > 12)
            {
                MessageBox.Show("Введите корректный месяц");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox_cardYear.Text) || textBox_cardYear.Text.Length <= 0)
            {
                MessageBox.Show("Введите год");
                return;
            }

            if (string.IsNullOrWhiteSpace(CVC.Text) || CVC.Text.Length <= 0)
            {
                MessageBox.Show("Введите cvc");
                return;
            }

            Sponsorship sponsor = new Sponsorship();

            sponsor.Amount = donation;
            sponsor.Registration = (comboBox_Runner.SelectedItem as RunnerInfo).reg;
            sponsor.SponsorName = Name.Text;

            try
            {
                Util.db.Sponsorship.Add(sponsor);
                Util.db.SaveChanges();
            }
            catch
            {
                MessageBox.Show("Произошла ошибка при добавлении");
            }
            finally
            {
                NavigationService.Navigate(new SponsorshipSuccessPage(comboBox_Runner.SelectedItem as RunnerInfo, sponsor.Registration.Charity, donation));
            }
        }

        private void textbox_cardnum_KeyDown(object sender, KeyEventArgs e)
        {
            if (!new Regex("[0-9]").IsMatch(e.Key.ToString()))
                e.Handled = true;
        }
    }
}