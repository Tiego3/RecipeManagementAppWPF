using RecipeManagementApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RecipeManagementAppWPF
{
    /// <summary>
    /// Interaction logic for AddRecipe.xaml
    /// </summary>
    public partial class AddRecipe : Window, IDataErrorInfo
    {
        public Recipe NewRecipe { get; private set; }
        private List<Ingredients> ingredients = new List<Ingredients>();
        private List<RecipeSteps> steps = new List<RecipeSteps>();

        public AddRecipe()
        {
            InitializeComponent();
            IngredientsDataGrid.ItemsSource = ingredients;
            StepsDataGrid.ItemsSource = steps;
        }

        private void SaveRecipeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(RecipeNameTextBox.Text) && ingredients.All(IngredientIsValid) && steps.All(StepIsValid))
            {
                NewRecipe = new Recipe
                {
                    Name = RecipeNameTextBox.Text,
                    Ingredients = new List<Ingredients>(ingredients),
                    Steps = new List<RecipeSteps>(steps),
                    OriginalQuantities = new List<double>(ingredients.Select(i => i.Quantity)),
                    OriginalCalories = new List<double>(ingredients.Select(i => i.Calories))
                };

                double totalCalories = Recipe.CalculateTotalCalories(NewRecipe.Ingredients);
                if (totalCalories > 300)
                {
                    MessageBox.Show("Warning: Total calories exceed 300 Kcal!");
                }

                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please complete all fields correctly before saving the recipe.");
            }
        }

        // Validation logic for Quantity and Calories
        private bool IngredientIsValid(Ingredients ingredient)
        {
            if (string.IsNullOrWhiteSpace(ingredient.Name) || string.IsNullOrWhiteSpace(ingredient.Unit) || string.IsNullOrWhiteSpace(ingredient.FoodGroup))
                return false;

            if (ingredient.Quantity <= 0)
                return false;

            if (ingredient.Calories < 0) // Allow 0 or positive calories
                return false;

            return true;
        }

        // Validation logic for Step description
        private bool StepIsValid(RecipeSteps step)
        {
            return !string.IsNullOrWhiteSpace(step?.StepsDescription);
        }

        // IDataErrorInfo interface implementation for data grid validation
        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Quantity")
                {
                    foreach (var ingredient in ingredients)
                    {
                        if (!double.TryParse(ingredient.Quantity.ToString(), out double quantity) || quantity <= 0)
                            return "Quantity must be a positive number.";
                    }
                }
                return null;
            }
        }
    }
}
