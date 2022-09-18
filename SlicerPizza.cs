namespace GoogleHashCode19
{
    public class SlicerPizza
    {

        public struct Limits
        {
            public int minIngredients;
            public int maxPortions;
        }

        public Limits SlicerLimits;


        public SlicerPizza(Limits limits)
        {
            SlicerLimits = limits;
        }

        
        public Pizza Slicer(Pizza pizzaToSlicer, Slice currentSlice, Slice.Types sliceType, int sliceFrom, int portionFrom)
        {

            // Comprueba el final de las rebanadas
            if (sliceFrom == (pizzaToSlicer.Rows - 1) )
            {
                return pizzaToSlicer;
            }


            // Prepara la rebanada
            // Si no es nula tenemos rebanada sin cortar.
            Slice slice = null;
            if (currentSlice != null)
            {
                slice = currentSlice;

                slice.ColumnFrom = portionFrom;
            }
            else
            {
                slice = new Slice(sliceType);

                slice.RowFrom = sliceFrom;
                slice.ColumnFrom = portionFrom > 0 ?  portionFrom + 1 : portionFrom;
            }


            // Comprueba el final de las porciones y aumenta rebanada
            if (portionFrom == pizzaToSlicer.Columns)
            {
                portionFrom = 0;
                sliceFrom++;

                // Guarda índice
                slice.RowFrom = sliceFrom;
                slice.RowTo = sliceFrom;
            }


           

            // Calcula el final de la rebanada en base al total de porciones
            if ((portionFrom + SlicerLimits.maxPortions) > pizzaToSlicer.Columns)
            {
                slice.ColumnTo = pizzaToSlicer.Columns;
            }
            else
            {
                slice.ColumnTo =( portionFrom + SlicerLimits.maxPortions);
            }

          slice.ColumnTo--;

            // Analiza el tramo máximo permitido de rebanada
            for (int portion = portionFrom; portion < slice.ColumnTo; portion++)
            {
                slice = AnalyzePizzaPortion(pizzaToSlicer, slice, portion);
            }

            // Comprueba si la rebanada cumple los requisitos
            if (IsValidSlice(slice))
            {
                // Guarda la rebanada / índice
                slice.RowTo = sliceFrom;

                sliceFrom = slice.RowTo;
                portionFrom = slice.ColumnTo;

                pizzaToSlicer.AddSlice(slice);

                slice = null;
            }
            else
            {
                // Vacia las porciones de la rebanada
                slice.SliceIngredients.MushroomsTotal = 0;
                slice.SliceIngredients.TomatosTotal = 0;
                slice.PortionsTotal = 0;

                // Analiza siguiente porción de pizza
                portionFrom++;
                slice.ColumnFrom++;
                slice.ColumnTo++;
            }

            return Slicer(pizzaToSlicer, slice, sliceType, sliceFrom, portionFrom);
        }


        private Slice AnalyzePizzaPortion(Pizza pizza, Slice slice, int portion)
        {
            // Cuenta ingredientes de la rebanada
            char pizzaPortion = pizza.PizzaMap[slice.RowFrom, portion];

            if (pizzaPortion == (char)Pizza.Ingredients.TOMATOS)
            {
                slice.SliceIngredients.TomatosTotal++;
            }
            else
            {
                slice.SliceIngredients.MushroomsTotal++;
            }

            // Cuenta porciones de la rebanada
            slice.PortionsTotal++;

            return slice;
        }


        /// <summary>
        /// Comprueba si no sobrepasa el máximo de porciones
        /// y si tiene el número de ingredientes necesarios.
        /// </summary>
        /// <param name="pizzaSlice">Rebanada de pizza</param>
        /// <returns>Retorna true si la rebanada es válida</returns>
        private bool IsValidSlice(Slice pizzaSlice)
        {
            return ((pizzaSlice.PortionsTotal <= SlicerLimits.maxPortions) &&
                  (pizzaSlice.SliceIngredients.TomatosTotal >= SlicerLimits.minIngredients &&
                  pizzaSlice.SliceIngredients.MushroomsTotal >= SlicerLimits.minIngredients));           
        }


        public Pizza CalculateScore(Pizza pizza)
        {
            int score = 0;

            foreach (Slice slice in pizza.HorizontalSlices)
            {
                score += slice.PortionsTotal;
            }
            pizza.HorizontalScore = score;

            score = 0;

            foreach (Slice slice in pizza.VerticalSlices)
            {
                score += slice.PortionsTotal;
            }
            pizza.VerticalScore = score;

            return pizza;
        }

    }
}
