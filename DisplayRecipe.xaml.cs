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
    /// Interaction logic for DisplayRecipe.xaml
    /// </summary>
    public partial class DisplayRecipe : Window
    {
        public DisplayRecipe(Recipe recipe)
        {
            InitializeComponent();            
            DisplayRecipeDetails(recipe);
        }
        private void DisplayRecipeDetails(Recipe recipe)
        {
            RecipeNameTextBlock.Text = recipe.Name;

            foreach (var ingredient in recipe.Ingredients)
            {
                IngredientsListBox.Items.Add($"{ingredient.Quantity} {ingredient.Unit} of {ingredient.Name} - {ingredient.Calories} Kcal ({ingredient.FoodGroup})");
            }

            foreach (var step in recipe.Steps)
            {
                StepsListBox.Items.Add(step.StepsDescription);
            }

            // Check if calories exceed threshold and display warning
            double totalCalories = Recipe.CalculateTotalCalories(recipe.Ingredients);
            if (totalCalories > 300)
            {
                CaloriesWarningTextBlock.Text = "Warning: Total calories exceed 300 Kcal!";
                CaloriesWarningTextBlock.Foreground = Brushes.Red; // Optionally change color for emphasis
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
