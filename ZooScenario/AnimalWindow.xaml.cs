using System;
using System.Windows;
using System.Windows.Controls;
using Animals;
using Reproducers;

namespace ZooScenario
{
    /// <summary>
    /// Contains interaction logic for AnimalWindow.xaml.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Event handlers may begin with lower-case letters.")]
    public partial class AnimalWindow : Window
    {
        /// <summary>
        /// The animal being created or edited.
        /// </summary>
        private Animal animal;

        /// <summary>
        /// Initializes a new instance of the AnimalWindow class.
        /// </summary>
        /// <param name="animal">The animal to create or edit.</param>
        public AnimalWindow(Animal animal)
        {
            this.animal = animal;
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes window controls with the animal's current values.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            this.nameTextBox.Text = this.animal.Name;
            this.ageTextBox.Text = this.animal.Age.ToString();
            this.weightTextBox.Text = this.animal.Weight.ToString();

            this.genderComboBox.ItemsSource = Enum.GetValues(typeof(Gender));
            this.genderComboBox.SelectedItem = this.animal.Gender;

            this.pregnancyStatusLabel.Content = this.animal.IsPregnant ? "Yes" : "No";
            this.makePregnantButton.IsEnabled = this.animal.Gender == Gender.Female && !this.animal.IsPregnant;
        }

        /// <summary>
        /// Updates the animal's name when the name text box loses focus.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void nameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.animal.Name = this.nameTextBox.Text;
        }

        /// <summary>
        /// Updates the animal's age when the age text box loses focus.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void ageTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.animal.Age = int.Parse(this.ageTextBox.Text);
        }

        /// <summary>
        /// Updates the animal's weight when the weight text box loses focus.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void weightTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.animal.Weight = double.Parse(this.weightTextBox.Text);
        }

        /// <summary>
        /// Updates the animal's gender when the gender combo box selection changes.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void genderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.animal.Gender = (Gender)this.genderComboBox.SelectedItem;
            this.makePregnantButton.IsEnabled = this.animal.Gender == Gender.Female;
        }

        /// <summary>
        /// Makes the animal pregnant and updates the pregnancy status label.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void makePregnantButton_Click(object sender, RoutedEventArgs e)
        {
            this.animal.MakePregnant();
            this.pregnancyStatusLabel.Content = "Yes";
            this.makePregnantButton.IsEnabled = false;
        }

        /// <summary>
        /// Confirms the animal creation/edit and closes the window.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
