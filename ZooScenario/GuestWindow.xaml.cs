using System;
using System.Windows;
using System.Windows.Controls;
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
        /// The guest being created or edited.
        /// </summary>
        private Guest guest;

        /// <summary>
        /// Initializes a new instance of the GuestWindow class.
        /// </summary>
        /// <param name="guest">The guest to create or edit.</param>
        public GuestWindow(Guest guest)
        {
            this.guest = guest;
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes window controls with the guest's current values.
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
            this.walletColorComboBox.SelectedItem = this.guest.Wallet.Color;

            this.moneyBalanceLabel.Content = this.guest.Wallet.MoneyBalance.ToString("C");
            this.moneyAmountComboBox.Items.Add(1);
            this.moneyAmountComboBox.Items.Add(5);
            this.moneyAmountComboBox.Items.Add(10);
            this.moneyAmountComboBox.Items.Add(20);

            this.accountBalanceLabel.Content = this.guest.CheckingAccount.MoneyBalance.ToString("C");
            this.accountComboBox.Items.Add(1);
            this.accountComboBox.Items.Add(5);
            this.accountComboBox.Items.Add(10);
            this.accountComboBox.Items.Add(20);
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
        private void genderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.genderComboBox.SelectedItem == null)
            {
                return;
            }

            try
            {
                this.guest.Gender = (Gender)this.genderComboBox.SelectedItem;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Updates the guest's wallet color when the wallet color combo box selection changes.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void walletColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.walletColorComboBox.SelectedItem == null)
            {
                return;
            }

            try
            {
                this.guest.Wallet.Color = (WalletColor)this.walletColorComboBox.SelectedItem;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Adds the selected amount to the guest's wallet.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void addMoneyButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.moneyAmountComboBox.SelectedItem == null)
            {
                return;
            }

            decimal amount = Convert.ToDecimal(this.moneyAmountComboBox.SelectedItem);
            this.guest.Wallet.AddMoney(amount);
            this.moneyBalanceLabel.Content = this.guest.Wallet.MoneyBalance.ToString("C");
        }

        /// <summary>
        /// Removes the selected amount from the guest's wallet.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void subtractMoneyButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.moneyAmountComboBox.SelectedItem == null)
            {
                return;
            }

            decimal amount = Convert.ToDecimal(this.moneyAmountComboBox.SelectedItem);
            this.guest.Wallet.RemoveMoney(amount);
            this.moneyBalanceLabel.Content = this.guest.Wallet.MoneyBalance.ToString("C");
        }

        /// <summary>
        /// Adds the selected amount to the guest's checking account.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void addAccountButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.accountComboBox.SelectedItem == null)
            {
                return;
            }

            decimal amount = Convert.ToDecimal(this.accountComboBox.SelectedItem);
            this.guest.CheckingAccount.AddMoney(amount);
            this.accountBalanceLabel.Content = this.guest.CheckingAccount.MoneyBalance.ToString("C");
        }

        /// <summary>
        /// Removes the selected amount from the guest's checking account.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void subtractAccountButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.accountComboBox.SelectedItem == null)
            {
                return;
            }

            decimal amount = Convert.ToDecimal(this.accountComboBox.SelectedItem);
            this.guest.CheckingAccount.RemoveMoney(amount);
            this.accountBalanceLabel.Content = this.guest.CheckingAccount.MoneyBalance.ToString("C");
        }

        /// <summary>
        /// Confirms the guest creation/edit and closes the window.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
