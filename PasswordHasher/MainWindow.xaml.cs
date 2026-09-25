using System.Security.Cryptography;
using System.Windows;
using System.Windows.Input;

namespace PasswordEncrypter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) // exit
        {
            Environment.Exit(1);
        }

        private string CustomEncrypt()
        {
            string plainPassword = PasswordBox.Text;
            string encryptedPassword = "";

            Random random = new Random();

            int keyNumber = random.Next(1, 9);

            for (int i = 0; i < plainPassword.Length + 1; i++)
            {
                if (i == 0)
                {
                    encryptedPassword += keyNumber;
                    continue;
                }

                int currentI = plainPassword[i - 1];
                char nextLetter = (char)(currentI + keyNumber);
                encryptedPassword += nextLetter;
            }

            return encryptedPassword;
        }

        private string CustomDecrypt()
        {
            string encryptedPassword = DecryptField.Text;
            string decryptedPassword = "";

            if (encryptedPassword.Length == 0)
                return "Invalid password";

            if (!int.TryParse(encryptedPassword[0].ToString(), out int keyNumber))
                return "No key number found";

            for (int i = 1; i < encryptedPassword.Length; i++)
            {
                int currentInt = encryptedPassword[i];
                char dehashedChar = (char)(currentInt - keyNumber);
                decryptedPassword += dehashedChar;
            }

            return decryptedPassword;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // Encrypt
        {
            if (AlgorithmCombo.Text == "Custom")
            {
                EncryptedResult.Text = CustomEncrypt();
            }
        }

        private void VerifyButton_Click(object sender, RoutedEventArgs e) // Decrypt
        {
            if (AlgorithmCombo.Text == "Custom")
            {
                DecryptField.Text = CustomDecrypt();
            }
        }
    }
}