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
    /// Interaction logic for ChangeScale.xaml
    /// </summary>
    public partial class ChangeScale : Window
    {

        private Recipe recipe;

        public ChangeScale(Recipe recipe)
        {
            InitializeComponent();
            this.recipe = recipe;
        }

        private void ChangeScaleBtn_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ScaleFactorTextBox.Text, out double scaleFactor) && scaleFactor > 0)
            {
                // Apply scale factor to ingredients
                foreach (var ingredient in recipe.Ingredients)
                {
                    ingredient.Quantity *= scaleFactor;
                    ingredient.Calories *= scaleFactor; // Adjust calories accordingly if needed
                }

                MessageBox.Show("Scale changed successfully!");
                this.DialogResult = true; // Set DialogResult to true to indicate success
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid scale factor. Please enter a valid positive number.");
            }
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false; // Set DialogResult to false to indicate cancellation
            this.Close();
        }
    }
}
