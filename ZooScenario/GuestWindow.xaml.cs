using System;
using System.Windows;
using Accounts;
using MoneyCollectors;
using People;
using Reproducers;

namespace ZooScenario
{
    /// <summary>
    /// Contains interaction logic for GuestWindow.xaml.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Event handlers may begin with lower-case letters.")]
    public partial class GuestWindow : Window
    {
        /// <summary>
        /// The guest being created in this window.
        /// </summary>
        private Guest guest;

        /// <summary>
        /// Initializes a new instance of the GuestWindow class.
        /// </summary>
        /// <param name="guest">The guest to edit.</param>
        public GuestWindow(Guest guest)
        {
            this.guest = guest;
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets the guest created in this window.
        /// </summary>
        public Guest Guest
        {
            get
            {
                return this.guest;
            }
        }

        /// <summary>
        /// Initializes the window controls when the window loads.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            this.nameTextBox.Text = this.guest.Name;
            this.ageTextBox.Text = this.guest.Age.ToString();
            this.walletBalanceTextBox.Text = this.guest.Wallet.MoneyBalance.ToString("0.00");
            this.checkingBalanceTextBox.Text = "0.00";

            this.genderComboBox.ItemsSource = Enum.GetValues(typeof(Gender));
            this.genderComboBox.SelectedIndex = 0;

            this.walletColorComboBox.ItemsSource = Enum.GetValues(typeof(WalletColor));
            this.walletColorComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Confirms the dialog when the OK button is clicked.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = this.nameTextBox.Text.Trim();
                if (string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Name must have a value.");
                    return;
                }

                int age;
                if (!int.TryParse(this.ageTextBox.Text, out age))
                {
                    MessageBox.Show("Age must be a whole number.");
                    return;
                }

                Gender gender = (Gender)this.genderComboBox.SelectedItem;
                WalletColor walletColor = (WalletColor)this.walletColorComboBox.SelectedItem;

                decimal walletBalance;
                if (!decimal.TryParse(this.walletBalanceTextBox.Text, out walletBalance))
                {
                    MessageBox.Show("Wallet balance must be a number.");
                    return;
                }

                decimal checkingBalance;
                if (!decimal.TryParse(this.checkingBalanceTextBox.Text, out checkingBalance))
                {
                    MessageBox.Show("Checking account balance must be a number.");
                    return;
                }

                IMoneyCollector account = new Account();
                account.AddMoney(checkingBalance);

                this.guest = new Guest(name, age, gender, account);
                this.guest.Wallet.AddMoney(walletBalance);

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
