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

            this.genderComboBox.ItemsSource = Enum.GetValues(typeof(Gender));
            this.genderComboBox.SelectedItem = this.guest.Gender;

            this.walletColorComboBox.ItemsSource = Enum.GetValues(typeof(WalletColor));
            this.walletColorComboBox.SelectedIndex = 0;

            this.UpdateMoneyLabels();
        }

        /// <summary>
        /// Updates the displayed wallet and account balance labels.
        /// </summary>
        private void UpdateMoneyLabels()
        {
            this.walletBalanceLabel.Content = this.guest.Wallet.MoneyBalance.ToString("C");
            this.accountBalanceLabel.Content = this.guest.CheckingAccount.MoneyBalance.ToString("C");
        }

        /// <summary>
        /// Updates the guest's name when the name text box loses focus.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void nameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                this.guest.Name = this.nameTextBox.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Updates the guest's age when the age text box loses focus.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void ageTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                this.guest.Age = int.Parse(this.ageTextBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Updates the guest's gender when the gender combo box selection changes.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void genderComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (this.genderComboBox.SelectedItem != null)
            {
                this.guest.Gender = (Gender)this.genderComboBox.SelectedItem;
            }
        }

        /// <summary>
        /// Adds money to the guest's wallet when the add money button is clicked.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void addMoneyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                decimal amount = decimal.Parse(this.walletAmountTextBox.Text);
                this.guest.Wallet.AddMoney(amount);
                this.UpdateMoneyLabels();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Subtracts money from the guest's wallet when the subtract money button is clicked.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void subtractMoneyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                decimal amount = decimal.Parse(this.walletAmountTextBox.Text);
                this.guest.Wallet.RemoveMoney(amount);
                this.UpdateMoneyLabels();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Adds money to the guest's checking account when the add account money button is clicked.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void addAccountMoneyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                decimal amount = decimal.Parse(this.accountAmountTextBox.Text);
                this.guest.CheckingAccount.AddMoney(amount);
                this.UpdateMoneyLabels();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Subtracts money from the guest's checking account when the subtract account money button is clicked.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void subtractAccountMoneyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                decimal amount = decimal.Parse(this.accountAmountTextBox.Text);
                this.guest.CheckingAccount.RemoveMoney(amount);
                this.UpdateMoneyLabels();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                this.guest.Name = this.nameTextBox.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            try
            {
                this.guest.Age = int.Parse(this.ageTextBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            this.DialogResult = true;
        }
    }
}
