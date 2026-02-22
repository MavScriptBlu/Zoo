using System;
using System.Windows;
using System.Windows.Media;
using Animals;
using BirthingRooms;
using BoothItems;
using People;
using Reproducers;
using Zoos;

namespace ZooScenario
{
    /// <summary>
    /// Contains interaction logic for MainWindow.xaml.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Event handlers may begin with lower-case letters.")]
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Minnesota's Como Zoo.
        /// </summary>
        private Zoo comoZoo;

        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
#if DEBUG
            this.Title += " [DEBUG]";
#endif
        }

        /// <summary>
        /// Updates the temperature border height, label, and color to reflect the current birthing room temperature.
        /// </summary>
        private void ConfigureBirthingRoomControls()
        {
            this.temperatureBorder.Height = this.comoZoo.BirthingRoomTemperature * 2;
            this.temperatureLabel.Content = string.Format("{0:0.0} °F", this.comoZoo.BirthingRoomTemperature);

            double colorLevel = ((this.comoZoo.BirthingRoomTemperature - BirthingRoom.MinTemperature) * 255) / (BirthingRoom.MaxTemperature - BirthingRoom.MinTemperature);
            this.temperatureBorder.Background = new SolidColorBrush(Color.FromRgb(Convert.ToByte(colorLevel), Convert.ToByte(255 - colorLevel), Convert.ToByte(255 - colorLevel)));
        }

        /// <summary>
        /// Creates the zoo when the window loads.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            this.comoZoo = Zoo.NewZoo();
            this.ConfigureBirthingRoomControls();
            this.PopulateAnimalListBox();
            this.PopulateGuestListBox();
        }

        /// <summary>
        /// Populates the animal list box with the zoo's animals.
        /// </summary>
        private void PopulateAnimalListBox()
        {
            this.animalListBox.ItemsSource = null;
            this.animalListBox.ItemsSource = this.comoZoo.Animals;
        }

        /// <summary>
        /// Populates the guest list box with the zoo's guests.
        /// </summary>
        private void PopulateGuestListBox()
        {
            this.guestListBox.ItemsSource = null;
            this.guestListBox.ItemsSource = this.comoZoo.Guests;
        }

        /// <summary>
        /// Admits a guest to the zoo.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void admitGuestButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create a new guest.
                Guest ethel = new Guest("Ethel", 42, 30.00m, WalletColor.Salmon, Gender.Female);

                // Sell a ticket to the guest.
                Ticket ticket = this.comoZoo.SellTicket(ethel);

                // Add the guest to the zoo.
                this.comoZoo.AddGuest(ethel, ticket);

                // Refresh the guest list.
                this.PopulateGuestListBox();
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Feeds an animal.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void feedAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected guest and animal from the list boxes.
            Guest guest = this.guestListBox.SelectedItem as Guest;
            Animal animal = this.animalListBox.SelectedItem as Animal;

            if (guest != null && animal != null)
            {
                // Have the guest feed the animal.
                guest.FeedAnimal(animal, this.comoZoo.AnimalSnackMachine);

                // Refresh the list boxes.
                this.PopulateAnimalListBox();
                this.PopulateGuestListBox();
            }
            else
            {
                MessageBox.Show("You must choose both a guest and an animal.");
            }

            // Keep both items selected after refresh.
            this.guestListBox.SelectedItem = guest;
            this.animalListBox.SelectedItem = animal;
        }

        /// <summary>
        /// Increases the birthing room temperature by 1 degree.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void increaseTempButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.comoZoo.BirthingRoomTemperature += 1;
                this.ConfigureBirthingRoomControls();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (FormatException)
            {
                MessageBox.Show("A number must be entered as a parameter.");
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("A parameter must be entered for the temperature command.");
            }
        }

        /// <summary>
        /// Decreases the birthing room temperature by 1 degree.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void decreaseTempButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.comoZoo.BirthingRoomTemperature -= 1;
                this.ConfigureBirthingRoomControls();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (FormatException)
            {
                MessageBox.Show("A number must be entered as a parameter.");
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("A parameter must be entered for the temperature command.");
            }
        }
    }
}
