using RecipeManagementApp;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RecipeManagementAppWPF
{
    public partial class MenuSelection : Window
    {
        private List<Recipe> allRecipes;

        public List<Recipe> SelectedRecipes { get; private set; }

        public MenuSelection(List<Recipe> recipes)
        {
            InitializeComponent();
            allRecipes = recipes;
            RecipesListBox.ItemsSource = recipes.Select(r => r.Name).ToList();
        }

        private void AddToMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedNames = RecipesListBox.SelectedItems.Cast<string>().ToList();
            if (selectedNames.Count > 0)
            {
                SelectedRecipes = selectedNames.Select(name => allRecipes.First(r => r.Name == name)).ToList();
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please select at least one recipe.");
            }
        }
    }
}
