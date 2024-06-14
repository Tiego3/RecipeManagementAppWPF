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
    /// Interaction logic for ResetRecipe.xaml
    /// </summary>
    public partial class ResetRecipe : Window
    {
        private Recipe recipe;

        public ResetRecipe(Recipe recipe)
        {
            InitializeComponent();
            this.recipe = recipe;
        }

        private void ResetRecipeBtn_Click(object sender, RoutedEventArgs e)
        {
            Recipe.ResetToOriginalValues(recipe);
            MessageBox.Show("Recipe reset to original values successfully!");
            this.DialogResult = true; // Set DialogResult to true to indicate success
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false; // Set DialogResult to false to indicate cancellation
            this.Close();
        }
    }
}
