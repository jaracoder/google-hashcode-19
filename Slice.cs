namespace GoogleHashCode19
{
    public class Slice
    {
        public int RowFrom, ColumnFrom;
        public int RowTo, ColumnTo;

        public Types SliceType;

        public enum Types
        {
            SLICE_HORIZONTAL = 0,
            SLICE_VERTICAL = 1
        }

        public struct Ingredients
        {
            public int TomatosTotal;

            public int MushroomsTotal;
        }

        public Ingredients SliceIngredients;

        public int PortionsTotal;

        public Slice(Types SliceType)
        {
            this.SliceType = SliceType;

            SliceIngredients = new Ingredients();
        }

        public void AddIngredients(Ingredients Ingredients)
        {
            SliceIngredients = Ingredients;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}", RowFrom, ColumnFrom, RowTo, ColumnTo);
        }
    }
}
