using System.Collections.Generic;

namespace GoogleHashCode19
{
    public class Pizza
    {

        public char[,] PizzaMap;
  
        public int Rows, Columns;

        public List<Slice> HorizontalSlices, VerticalSlices;

        public int HorizontalScore, VerticalScore;

        public enum Ingredients
        {
            TOMATOS = 'T',
            MUSHROOMS = 'M'
        }

        public Pizza(int rows, int columns)
        {
            PizzaMap = new char[rows, columns];

            Rows = rows;
            Columns = columns;

            HorizontalSlices = new List<Slice>();
            VerticalSlices = new List<Slice>();
        }

        public void AddSlice(Slice slice)
        {
            if (slice.SliceType == Slice.Types.SLICE_HORIZONTAL)
            {
                HorizontalSlices.Add(slice);
            }
            else
            {
                VerticalSlices.Add(slice);
            }   
        }

    }
}
