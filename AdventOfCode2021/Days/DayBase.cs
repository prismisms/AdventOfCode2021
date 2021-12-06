namespace AdventOfCode2021
{
    public abstract class DayBase<TFileType,TOutputType>
    {
        protected readonly List<TFileType> FileContents;

        protected DayBase(List<TFileType> fileContents)
        {
            FileContents = fileContents;
        }

        public abstract TOutputType Part1();
        public abstract TOutputType Part2();

        public (TOutputType, TOutputType) GetResult()
        {
            return (Part1(), Part2());
        }
    }
}
