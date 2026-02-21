using System;
using System.Windows;
using System.Windows.Media;
using Animals;
using BirthingRooms;
using BoothItems;
using People;
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

            // Create the Como Zoo.
            this.comoZoo = this.CreateComoZoo();

            this.ConfigureBirthingRoomControls();
        }

        /// <summary>
        /// Creates the Como Zoo.
        /// </summary>
        /// <returns>The Como Zoo.</returns>
        private Zoo CreateComoZoo()
        {
            // Create employees.
            Employee sam = new Employee("Sam", 42);
            Employee flora = new Employee("Flora", 98);

            // Create the zoo.
            Zoo zoo = new Zoo("Como Zoo", 1000, 4, 0.75m, 15.00m, 3.00m, 3640.25m, sam, flora);

            // Set the birthing room temperature to its starting value.
            zoo.BirthingRoomTemperature = 77;

            // Create and add animals.
            Dingo dpierre = new Dingo("Pierre", 3, 25.2);
            zoo.AddAnimal(dpierre);

            Dingo dJackie = new Dingo("Jackie", 4, 35.3);
            zoo.AddAnimal(dJackie);

            Platypus pPatty = new Platypus("Patty", 2, 15.5);
            zoo.AddAnimal(pPatty);

            Hummingbird hHarold = new Hummingbird("Harold", 1, 0.5);
            zoo.AddAnimal(hHarold);

            Chimpanzee cCharlie = new Chimpanzee("Charlie", 5, 90.0);
            zoo.AddAnimal(cCharlie);

            Eagle eEmily = new Eagle("Emily", 3, 12.5);
            zoo.AddAnimal(eEmily);

            Kangaroo kKevin = new Kangaroo("Kevin", 4, 110.0);
            zoo.AddAnimal(kKevin);

            Ostrich oOliver = new Ostrich("Oliver", 2, 250.0);
            zoo.AddAnimal(oOliver);

            Shark sSteve = new Shark("Steve", 6, 500.0);
            zoo.AddAnimal(sSteve);

            Squirrel sSammy = new Squirrel("Sammy", 1, 1.5);
            zoo.AddAnimal(sSammy);

            // Create guests.
            Guest greg = new Guest("Greg", 35, 20.00m, "Brown");
            Guest darla = new Guest("Darla", 7, 25.25m, "Salmon");

            // Sell tickets to the guests.
            Ticket ticket1 = zoo.SellTicket(greg);
            Ticket ticket2 = zoo.SellTicket(darla);

            // Add guests to the zoo.
            zoo.AddGuest(greg, ticket1);
            zoo.AddGuest(darla, ticket2);

            return zoo;
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
        /// Admits a guest to the zoo.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void admitGuestButton_Click(object sender, RoutedEventArgs e)
        {
            // Create a new guest.
            Guest ethel = new Guest("Ethel", 42, 30.00m, "Salmon");

            // Sell a ticket to the guest.
            Ticket ticket = this.comoZoo.SellTicket(ethel);

            // Add the guest to the zoo.
            this.comoZoo.AddGuest(ethel, ticket);
        }

        /// <summary>
        /// Feeds an animal.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void feedAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            // Find Greg.
            Guest greg = this.comoZoo.FindGuest("Greg");

            // Find an ostrich.
            Ostrich ostrich = this.comoZoo.FindAnimal(typeof(Ostrich)) as Ostrich;

            // Have Greg feed the ostrich.
            greg.FeedAnimal(ostrich, this.comoZoo.AnimalSnackMachine);
        }

        /// <summary>
        /// Increases the birthing room temperature by 1 degree.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void increaseTempButton_Click(object sender, RoutedEventArgs e)
        {
            this.comoZoo.BirthingRoomTemperature += 1;
            this.ConfigureBirthingRoomControls();
        }

        /// <summary>
        /// Decreases the birthing room temperature by 1 degree.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void decreaseTempButton_Click(object sender, RoutedEventArgs e)
        {
            this.comoZoo.BirthingRoomTemperature -= 1;
            this.ConfigureBirthingRoomControls();
        }
    }
}
