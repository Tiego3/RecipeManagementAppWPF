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
    /// Interaction logic for AddSteps.xaml
    /// </summary>
    public partial class AddSteps : Window
    {
        public RecipeSteps Step { get; private set; }
        public AddSteps()
        {
            InitializeComponent();
        }

        private void AddStepBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(StepDescriptionTextBox.Text))
            {
                Step = new RecipeSteps
                {
                    StepsDescription = StepDescriptionTextBox.Text
                };
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter a step description.");
            }
        }
    }
}
