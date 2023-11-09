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

namespace Password_Generator
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int passwordLength = 2;

        public static List<int> allLetters = new List<int>();
        public static string passwordLengthInput;

        private string currentPassword;

        public MainWindow()
        {
            InitializeLetters();
            InitializeComponent();
        }

        private static void InitializeLetters()
        {
            // small letters from a - z
            for (int i = 65; i <= 90; i++)
            {
                allLetters.Add(i);
            }

            // big letters from A - Z
            for (int i = 97; i <= 122; i++)
            {
                allLetters.Add(i);
            }

            // numbers from 0 - 9
            for (int i = 48; i <= 57; i++)
            {
                allLetters.Add(i);
            }
        }

        public static string GeneratePassword()
        {
            Random random = new Random();

            // empty string
            string password = "";

            for (int i = 0; i < passwordLength; i++)
            {
                password += (char)allLetters[random.Next(0, allLetters.Count)];
            }

            Console.WriteLine(password);

            return password;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            currentPassword = GeneratePassword();
            PasswordText.Text = currentPassword;
        }

        private void Copy_Button_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(currentPassword);
        }

        private void Length_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            passwordLength = (int)Length_Slider.Value;

            if (Length_Number != null)
            {
                Length_Number.Text = Length_Slider.Value.ToString();
            }

            currentPassword = GeneratePassword();
            PasswordText.Text = currentPassword;
        }
    }
}
