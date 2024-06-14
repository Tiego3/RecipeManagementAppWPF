using RecipeManagementApp;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RecipeManagementAppWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Recipe> recipes = new List<Recipe>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddRecipeBtn_Click(object sender, RoutedEventArgs e)
        {
            var addRecipeWindow = new AddRecipe();
            if (addRecipeWindow.ShowDialog() == true)
            {
                var newRecipe = addRecipeWindow.NewRecipe;
                if (newRecipe != null)
                {
                    recipes.Add(newRecipe);
                    MessageBox.Show("Recipe added successfully!");
                }
            }
        }

        private void DisplayRecipeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (recipes.Count > 0)
            {
                int indexVal = GetRecipeIndex("Select a recipe to display:", recipes);
                if (indexVal >= 0)
                {
                    var displayRecipeWindow = new DisplayRecipe(recipes[indexVal]);
                    displayRecipeWindow.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("No recipes added yet.");
            }
        }

        private void ChangeScaleBtn_Click(object sender, RoutedEventArgs e)
        {
            if (recipes.Count > 0)
            {
                int indexVal = GetRecipeIndex("Select a recipe to change scale:", recipes);
                if (indexVal >= 0)
                {
                    var selectedRecipe = recipes[indexVal];
                    var changeScaleWindow = new ChangeScale(selectedRecipe);
                    bool? dialogResult = changeScaleWindow.ShowDialog();

                    if (dialogResult == true)
                    {
                        // Optionally update UI or display a message after scale change
                    }
                }
            }
            else
            {
                MessageBox.Show("No recipes added yet.");
            }
        }

        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {
            if (recipes.Count > 0)
            {
                int indexVal = GetRecipeIndex("Select a recipe to reset to original values:", recipes);
                if (indexVal >= 0)
                {
                    var selectedRecipe = recipes[indexVal];
                    var resetRecipeWindow = new ResetRecipe(selectedRecipe);
                    bool? dialogResult = resetRecipeWindow.ShowDialog();

                    if (dialogResult == true)
                    {
                        // Optionally update UI or display a message after reset
                    }
                }
            }
            else
            {
                MessageBox.Show("No recipes added yet.");
            }
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private int GetRecipeIndex(string prompt, List<Recipe> recipes)
        {
            // Show a simple dialog to select a recipe
            var sortedRecipes = recipes.OrderBy(r => r.Name).ToList();
            string message = prompt + "\n";
            for (int i = 0; i < sortedRecipes.Count; i++)
            {
                message += $"{i + 1}. {sortedRecipes[i].Name}\n";
            }

            string input = Microsoft.VisualBasic.Interaction.InputBox(message, "Select Recipe", "1");

            if (int.TryParse(input, out int recipeIndex) && recipeIndex >= 1 && recipeIndex <= sortedRecipes.Count)
            {
                return recipeIndex - 1;
            }

            MessageBox.Show("Invalid input. Please enter a valid recipe number.");
            return -1;
        }
    }
}
