using RecipeManagementApp;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            // Implement the logic to display a recipe
            if (recipes.Count > 0)
            {
                int indexVal = GetRecipeIndex("Select a recipe to display:", recipes);
                Recipe.DisplayRecipe(recipes[indexVal].Name, recipes[indexVal]);
            }
            else
            {
                MessageBox.Show("No recipes added yet.");
            }
        }

        private void ChangeScaleBtn_Click(object sender, RoutedEventArgs e)
        {
            // Implement the logic to change the scale of a recipe
            if (recipes.Count > 0)
            {
                int indexVal = GetRecipeIndex("Select a recipe to change scale:", recipes);
                Recipe.ChangeScale(recipes[indexVal]);
            }
            else
            {
                MessageBox.Show("No recipes added yet.");
            }
        }

        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {
            // Implement the logic to reset the recipe to original values
            if (recipes.Count > 0)
            {
                int indexVal = GetRecipeIndex("Select a recipe to reset to original values:", recipes);
                Recipe.ResetToOriginalValues(recipes[indexVal]);
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
