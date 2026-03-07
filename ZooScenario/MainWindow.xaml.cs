using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

            this.animalTypeComboBox.ItemsSource = Enum.GetValues(typeof(AnimalType));
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
        /// Admits a guest to the zoo by opening a guest window.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void admitGuestButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Accounts.Account account = new Accounts.Account();
                Guest newGuest = new Guest("Guest", 0, Gender.Female, account);

                GuestWindow guestWindow = new GuestWindow(newGuest);
                guestWindow.Owner = this;

                if (guestWindow.ShowDialog() == true)
                {
                    Guest guest = guestWindow.Guest;
                    Ticket ticket = this.comoZoo.SellTicket(guest);
                    this.comoZoo.AddGuest(guest, ticket);
                    this.PopulateGuestListBox();
                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BoothItems.MissingItemException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Adds an animal to the zoo by opening an animal window.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void addAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AnimalType animalType = (AnimalType)this.animalTypeComboBox.SelectedItem;
                Animal animal = AnimalFactory.CreateAnimal(animalType, "Default", 0, 0, Gender.Female);

                AnimalWindow animalWindow = new AnimalWindow(animal);
                animalWindow.Owner = this;

                if (animalWindow.ShowDialog() == true)
                {
                    this.comoZoo.AddAnimal(animal);
                    this.PopulateAnimalListBox();
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Please select an animal type before adding an animal to the zoo.");
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("Please select an animal type before adding an animal to the zoo.");
            }
        }

        /// <summary>
        /// Opens an animal window to edit the double-clicked animal.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void animalListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Animal animal = this.animalListBox.SelectedItem as Animal;

            if (animal != null)
            {
                AnimalWindow animalWindow = new AnimalWindow(animal);
                animalWindow.Owner = this;

                if (animalWindow.ShowDialog() == true)
                {
                    this.PopulateAnimalListBox();
                }
            }
        }

        /// <summary>
        /// Opens a guest window to edit the double-clicked guest.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void guestListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Guest guest = this.guestListBox.SelectedItem as Guest;

            if (guest != null)
            {
                GuestWindow guestWindow = new GuestWindow(guest);
                guestWindow.Owner = this;

                if (guestWindow.ShowDialog() == true)
                {
                    this.PopulateGuestListBox();
                }
            }
        }

        /// <summary>
        /// Shows the cage for the selected animal.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void showCageButton_Click(object sender, RoutedEventArgs e)
        {
            Animal animal = this.animalListBox.SelectedItem as Animal;

            if (animal != null)
            {
                Cage cage = this.comoZoo.FindCage(animal.GetType());

                if (cage != null)
                {
                    CageWindow cageWindow = new CageWindow(cage);
                    cageWindow.Owner = this;
                    cageWindow.Show();
                }
            }
            else
            {
                MessageBox.Show("Please select an animal to show its cage.");
            }
        }

        /// <summary>
        /// Allows the selected guest to adopt the selected animal.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void adoptAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            Guest guest = this.guestListBox.SelectedItem as Guest;
            Animal animal = this.animalListBox.SelectedItem as Animal;

            if (guest != null && animal != null)
            {
                if (guest.AdoptedAnimal == null)
                {
                    guest.AdoptedAnimal = animal;

                    Cage cage = this.comoZoo.FindCage(animal.GetType());
                    if (cage != null)
                    {
                        cage.Add(guest);
                    }

                    this.PopulateGuestListBox();
                }
                else
                {
                    MessageBox.Show(string.Format("{0} has already adopted an animal.", guest.Name));
                }
            }
            else
            {
                MessageBox.Show("You must select both a guest and an animal to adopt.");
            }
        }

        /// <summary>
        /// Removes the adoption of an animal from the selected guest.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void unadoptAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            Guest guest = this.guestListBox.SelectedItem as Guest;

            if (guest != null && guest.AdoptedAnimal != null)
            {
                Cage cage = this.comoZoo.FindCage(guest.AdoptedAnimal.GetType());
                if (cage != null)
                {
                    cage.Remove(guest);
                }

                guest.AdoptedAnimal = null;
                this.PopulateGuestListBox();
            }
            else
            {
                MessageBox.Show("Please select a guest who has adopted an animal.");
            }
        }

        /// <summary>
        /// Removes the selected animal from the zoo.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void removeAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            Animal animal = this.animalListBox.SelectedItem as Animal;

            if (animal != null)
            {
                if (MessageBox.Show(
                    string.Format("Are you sure you want to remove animal: {0}?", animal.Name),
                    "Confirmation",
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    this.comoZoo.RemoveAnimal(animal);
                    this.PopulateAnimalListBox();
                }
            }
            else
            {
                MessageBox.Show("Please select an animal to remove.");
            }
        }

        /// <summary>
        /// Removes the selected guest from the zoo.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void removeGuestButton_Click(object sender, RoutedEventArgs e)
        {
            Guest guest = this.guestListBox.SelectedItem as Guest;

            if (guest != null)
            {
                if (MessageBox.Show(
                    string.Format("Are you sure you want to remove guest: {0}?", guest.Name),
                    "Confirmation",
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    this.comoZoo.RemoveGuest(guest);
                    this.PopulateGuestListBox();
                }
            }
            else
            {
                MessageBox.Show("Please select a guest to remove.");
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
