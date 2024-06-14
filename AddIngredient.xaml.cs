using RecipeManagementApp;
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
using System.Windows.Shapes;

namespace RecipeManagementAppWPF
{
    /// <summary>
    /// Interaction logic for AddIngredient.xaml
    /// </summary>
    public partial class AddIngredient : Window
    {
        public Ingredients Ingredient { get; private set; }
        public AddIngredient()
        {
            InitializeComponent();
        }
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            double quantity, calories;
            if (!string.IsNullOrWhiteSpace(NameTextBox.Text) &&
                double.TryParse(QuantityTextBox.Text, out quantity) && quantity > 0 &&
                !string.IsNullOrWhiteSpace(UnitTextBox.Text) &&
                double.TryParse(CaloriesTextBox.Text, out calories) && calories > 0 &&
                !string.IsNullOrWhiteSpace(FoodGroupTextBox.Text))
            {
                Ingredient = new Ingredients
                {
                    Name = NameTextBox.Text,
                    Quantity = quantity,
                    Unit = UnitTextBox.Text,
                    Calories = calories,
                    FoodGroup = FoodGroupTextBox.Text
                };
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please fill out all fields with valid values.");
            }
        }
    }
}
