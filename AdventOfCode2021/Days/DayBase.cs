namespace AdventOfCode2021
{
    public abstract class DayBase<T>
    {
        protected readonly List<T> FileContents;

        protected DayBase(List<T> fileContents)
        {
            FileContents = fileContents;
        }

        public abstract int Part1();
        public abstract int Part2();

        public (int, int) GetResult()
        {
            return (Part1(), Part2());
        }
    }
}
