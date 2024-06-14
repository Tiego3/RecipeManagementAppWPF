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
    /// Interaction logic for AddRecipe.xaml
    /// </summary>
    public partial class AddRecipe : Window
    {
        public Recipe NewRecipe { get; private set; }
        private List<Ingredients> ingredients = new List<Ingredients>();
        private List<RecipeSteps> steps = new List<RecipeSteps>();
        public AddRecipe()
        {
            InitializeComponent();
        }
        private void AddIngredientsBtn_Click(object sender, RoutedEventArgs e)
        {
            int numOfIngredients;
            if (int.TryParse(NumIngredientsTextBox.Text, out numOfIngredients) && numOfIngredients > 0)
            {
                ingredients.Clear();
                for (int i = 0; i < numOfIngredients; i++)
                {
                    var addIngredientWindow = new AddIngredient();
                    if (addIngredientWindow.ShowDialog() == true)
                    {
                        ingredients.Add(addIngredientWindow.Ingredient);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid number of ingredients.");
            }
        }

        private void AddStepsBtn_Click(object sender, RoutedEventArgs e)
        {
            int numOfSteps;
            if (int.TryParse(NumStepsTextBox.Text, out numOfSteps) && numOfSteps > 0)
            {
                steps.Clear();
                for (int i = 0; i < numOfSteps; i++)
                {
                    var addStepWindow = new AddSteps();
                    if (addStepWindow.ShowDialog() == true)
                    {
                        steps.Add(addStepWindow.Step);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid number of steps.");
            }
        }

        private void SaveRecipeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(RecipeNameTextBox.Text) && ingredients.Count > 0 && steps.Count > 0)
            {
                NewRecipe = new Recipe
                {
                    Name = RecipeNameTextBox.Text,
                    Ingredients = ingredients,
                    Steps = steps,
                    OriginalQuantities = new List<double>(ingredients.Select(i => i.Quantity)),
                    OriginalCalories = new List<double>(ingredients.Select(i => i.Calories))
                };
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please complete all fields before saving the recipe.");
            }
        }
    }
}
