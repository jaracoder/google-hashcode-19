
using System;
using System.Linq;

namespace GoogleHashCode19
{
    public class RunSlicerPizza
    {
        const string IN_FILE = "in_file.in";
        const string OUT_FILE = "out_file.in";

        static string[] lines;


        static Pizza pizza;
        static Pizza slicedPizza;


        static SlicerPizza slicerPizza;
        static SlicerPizza.Limits slicerPizzaLimits;
        

        static void Main()
        {
            ReadFilePizza();

            SetPizza();
            SlicerPizza();
            

            WriterSlicedPizza(slicedPizza);
        }

      
        static void SetPizza()
        {
            var paramss = lines[0].Split(' ').Select(int.Parse).ToList();


            // Crea mapa de la pizza

            pizza = new Pizza(paramss[0], paramss[1]);

            for (int i = 1; i <= paramss[0]; i++)
            {

                string ingredients = lines[i];
                for (int c = 0; c < ingredients.Length; c++)
                {
                    pizza.PizzaMap[(i-1), c] = ingredients[c];
                }

            }


            // Pone los límites de corte

            slicerPizzaLimits = new SlicerPizza.Limits()
            {
                minIngredients = paramss[2],
                maxPortions = paramss[3]
            };
        }


        static void SlicerPizza()
        {
            slicerPizza = new SlicerPizza(slicerPizzaLimits);

            //slicedPizza = slicerPizza.Slicer(pizza);

            slicedPizza = slicerPizza.Slicer(pizza, null, Slice.Types.SLICE_HORIZONTAL, 0, 0);
            slicedPizza = slicerPizza.CalculateScore(slicedPizza);
        }


        static void ReadFilePizza()
        {
            lines = Utils.ReadFromFile(IN_FILE);
        }


        static void WriterSlicedPizza(Pizza slicedPizza)
        {
            var totalPortionsPizza = slicedPizza.Rows * slicedPizza.Columns;

            var totalHorizontalSlices = slicedPizza.HorizontalSlices.Count;
            var totalVerticalSlices = slicedPizza.VerticalSlices.Count;

            var totalHorizontalPortionsSliced = 0;
            var totalVerticalPortionsSliced = 0;

            var remainderHorizontalPortions = 0;
            var remainderVerticalPortions = 0;


            Console.WriteLine("Horizontal slices pizza");
            Console.WriteLine(totalHorizontalSlices);

            foreach (Slice slice in slicedPizza.HorizontalSlices)
            {
                totalHorizontalPortionsSliced += slice.PortionsTotal;

                Console.WriteLine(slice.ToString());
            }


            Console.WriteLine();
            

            Console.WriteLine("Vertical slices pizza");
            Console.WriteLine(totalVerticalSlices);

            foreach (Slice slice in slicedPizza.VerticalSlices)
            {
                totalVerticalPortionsSliced += slice.PortionsTotal;

                Console.WriteLine(slice.ToString());
            }


            Console.WriteLine();


            Console.WriteLine("Amount of portions pizza: " + totalPortionsPizza);
            Console.WriteLine("Total of portions horizontal sliced: " + totalHorizontalPortionsSliced);
            Console.WriteLine("Total of portions vertical sliced: " + totalVerticalPortionsSliced);

            Console.WriteLine();


            remainderHorizontalPortions = totalPortionsPizza - totalHorizontalPortionsSliced;
            if (remainderHorizontalPortions == 0)
            {
                Console.WriteLine("Perfect sliced in horizontal. Good job!");
            }

            remainderVerticalPortions = totalPortionsPizza - totalVerticalPortionsSliced;
            if (remainderVerticalPortions == 0)
            {
                Console.WriteLine("Perfect sliced in vertical. Good job!");
            }

            Console.WriteLine();
            Console.WriteLine("Enter a key to continue...");
            Console.WriteLine();

            Console.ReadKey();

            Utils.WriteToFile(OUT_FILE, "");
        }

    }
}
