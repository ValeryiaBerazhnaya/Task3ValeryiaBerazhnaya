namespace Task3
{
    public class Dice
    {
        public int[] Sides { get; private set; }

        public Dice(int[] sides)
        {
            Sides = sides;
        }

        public int Roll(int index)
        {
            return Sides[index];
        }
    }
}