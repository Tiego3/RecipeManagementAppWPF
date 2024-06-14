using RecipeManagementApp;
using System;
using static System.Formats.Asn1.AsnWriter;

namespace RecipeManagementApp
{
    
    public class Recipe
    {

        public string Name { get; set; }
        public List<Ingredients> Ingredients { get; set; }
        public List<RecipeSteps> Steps { get; set; }
        public List<double> OriginalQuantities { get; set; }
        public List<double> OriginalCalories { get; set; }
        //Defining Delegate
        public delegate void RecipeExceedsCaloriesHandler(Recipe recipe);

        //Method to add full recipe
        public static Recipe AddNewRecipe()
        {
            Console.Write("Enter the name of the recipe: ");
            string recipeName = Console.ReadLine();

            int numOfIngredients = GetIntValue("Enter the Number of Ingredients: ");
            List<Ingredients> ingredients = GetIngredients(numOfIngredients);


            //Calculate total calories and check if it exceeds the threshold
            CheckIfKcalExceeds(ingredients, 300, NotifyUserExceedsCalories);

            int numSteps = GetIntValue("Enter the Number of Steps: ");
            List<RecipeSteps> steps = GetSteps(numSteps);

            var recipe = new Recipe
            {
                Name = recipeName,
                Ingredients = ingredients,
                Steps = steps
            };

            // Store original quantities & calories
            recipe.OriginalQuantities = new List<double>();
            recipe.OriginalCalories = new List<double>();

            foreach (var ingredient in ingredients)
            {
                recipe.OriginalQuantities.Add(ingredient.Quantity);
                recipe.OriginalCalories.Add(ingredient.Calories);
            }
            DisplayRecipe(recipeName,recipe);
            return recipe;
        }

        //This method gets the value(whole numbers) of items (ingredients or steps or calories) from the user
        //and checks if the value is valid (value is not text and not a negetive value)        
        public static int GetIntValue(string itemType)
        {
            int numItems;
            do
            {
                Console.Write($"\n{itemType}");
                if (!int.TryParse(Console.ReadLine(), out numItems) || numItems <= 0)
                {
                    Console.WriteLine("Invalid input. Please enter a valid positive number.");
                }
            } while (numItems <= 0);
            return numItems;
        }

        //This method gets the details of each the ingredient from user (Name of ingredient, quantity and the unit)
        //Prompting user to enter details for each ingredient,
        //iterating based on the value entered by user
        public static List<Ingredients> GetIngredients(int numIngredients)
        {
            List<Ingredients> ingredients = new List<Ingredients>();

            for (int i = 0; i < numIngredients; i++)
            {
                Console.WriteLine($"\nEnter details  (Name, Quantity, Unit) for Ingredient #{i + 1}:");
                Console.Write("Name: ");
                string name = Console.ReadLine();

                double quantity;
                quantity = GetValueDouble("Quantity: ");

                Console.Write("Unit: ");
                string unit = Console.ReadLine();

                double calories;
                calories = GetValueDouble("Calories: ");

                Console.Write("Enter the Food Group: ");
                string foodGroup = Console.ReadLine();

                ingredients.Add(new Ingredients { Name = name, Quantity = quantity, Unit = unit, Calories = calories, FoodGroup = foodGroup });

            }
            return ingredients;
        }

        //This method gets the details of the steps(description) for the Recipe
        //Prompting user to enter the steps(description),
        //iterating based on the value entered by user
        public static List<RecipeSteps> GetSteps(int numSteps)
        {
            List<RecipeSteps> steps = new List<RecipeSteps>();
            for (int i = 0; i < numSteps; i++)
            {
                Console.WriteLine($"\nStep #{i + 1}:");
                string description = Console.ReadLine();
                steps.Add(new RecipeSteps { StepsDescription = description });
            }
            return steps;
        }

        //This method gets the value(with decimal) (quantity or scale) from the user
        //and checks if the value is valid (value is not text and not a negetive value)
        public static double GetValueDouble(string itemType)
        {
            double value;
            do
            {
                Console.Write(itemType);
                if (!double.TryParse(Console.ReadLine(), out value) || value <= 0)
                {
                    Console.WriteLine("Invalid input. Please enter a valid positive number.");
                }
            } while (value <= 0);
            return value;
        }

        //This method changes the Scale Factor for the quantity & calories of ingredients
        public static void ChangeScale(Recipe recipe)
        {
            double scaleFactor;
            scaleFactor = GetValueDouble("\nScale: ");
            for (int i = 0; i < recipe.Ingredients.Count; i++)
            {

                recipe.Ingredients[i].Quantity *= scaleFactor;
                recipe.Ingredients[i].Calories *= scaleFactor;

            }
            Console.WriteLine("\nQuantities adjusted successfully.");
            DisplayRecipe(recipe.Name, recipe);
        }

        //This method Reset the quantity to the original value
        public static void ResetToOriginalValues(Recipe recipe)
        {
            for (int i = 0; i < recipe.Ingredients.Count; i++)
            {
                recipe.Ingredients[i].Quantity = recipe.OriginalQuantities[i];
                recipe.Ingredients[i].Calories = recipe.OriginalCalories[i];
            }
            Console.WriteLine("\nQuantities reset to original values successfully.");
           DisplayRecipe(recipe.Name, recipe);
        }

        //This method displays the full Recipe(Ingredients & Steps) to the User
        // Method to display the full Recipe (Ingredients & Steps) to the User
        public static void DisplayRecipe(string recipeName, Recipe recipe)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\n------------------------------\n" +
                $"{recipeName}\n------------------------------");

            // Display Ingredients
            Console.WriteLine("Ingredients:");
            for (int i = 0; i < recipe.Ingredients.Count; i++)
            {
                var ingredient = recipe.Ingredients[i];
                Console.WriteLine($"- {ingredient.Quantity} {ingredient.Unit} of {ingredient.Name} - {ingredient.Calories} Kcal & of {ingredient.FoodGroup} food group");
            }

            CheckIfKcalExceeds(recipe.Ingredients, 300, NotifyUserExceedsCalories);

            // Display steps
            Console.WriteLine("\nSteps:");
            for (int i = 0; i < recipe.Steps.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {recipe.Steps[i].StepsDescription}");
            }

        }

        // Method to calculate the total calories of all ingredients
        public static double CalculateTotalCalories(List<Ingredients> ingredients)
        {
            double totalCalories = 0;
            foreach (var ingredient in ingredients)
            {
                if (ingredient.Calories > 0)
                    totalCalories += ingredient.Calories;
                else
                    throw new Exception();
               
            }
            Console.WriteLine($"\nTotal calories for this recipe is a {totalCalories} Kcal");
            return totalCalories;
        }

        // Method to check if the total calories exceed
        public static void CheckIfKcalExceeds(List<Ingredients> ingredients, double threshold, RecipeExceedsCaloriesHandler handler)
        {
            double totalCalories = CalculateTotalCalories(ingredients);
            if (totalCalories > threshold)
            {
                // Console.WriteLine($"\nTotal calories for this recipe is ({totalCalories})");
                // Invoking delegate
                handler?.Invoke(null); 
            }

        }

        public static void NotifyUserExceedsCalories(Recipe recipe)
        {
            Console.WriteLine("!!!This recipe exceeds 300 Kcal!");
        }
    }
}
