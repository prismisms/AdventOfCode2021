namespace AdventOfCode2021
{
    public class Day4 : DayBase<string, int>
    {
        private readonly List<string> _calledNumbers;
        private readonly List<BingoBoard> _bingoBoards;

        public Day4(string fileLocation) : base(FileHandler.GetFileContentsAsStrings(fileLocation))
        {
            _calledNumbers = GetAllCalledNumbers();
            _bingoBoards = GetBingoBoards();
        }

        // Product of the unmarked cells in the first winning board and the number that caused that win 
        public override int Part1()
        {
            var (winningNumber, winningBoard) = GetWinner();
            return GetSumOfUnmarkedValues(winningBoard) * winningNumber;
        }

        // Product of the unmarked cells in the last winning board and the number that caused that win 
        public override int Part2()
        {
            var (winningNumber, winningBoard) = GetLastWinner();
            return GetSumOfUnmarkedValues(winningBoard) * winningNumber;
        }

        private static int GetSumOfUnmarkedValues(BingoBoard board)
        {
            return board.Cells
                .FindAll(x => !x.Marked)
                .Select(x => int.Parse(x.Value))
                .Sum();
        }

        private (int, BingoBoard) GetWinner()
        {
            RefreshBingoBoards();

            foreach (var number in _calledNumbers)
            {
                foreach(var board in _bingoBoards)
                {
                    board.MarkCell(number);
                    if (board.HasWin())
                    {
                        return (int.Parse(number), board);
                    }
                }
            }

            throw new Exception();
        }

        private (int, BingoBoard) GetLastWinner()
        {
            RefreshBingoBoards();

            foreach (var number in _calledNumbers)
            {
                foreach (var board in _bingoBoards.Where(board => !board.HasWon))
                {
                    board.MarkCell(number);
                    if (_bingoBoards.Count(x => !x.HasWon) == 1 && board.HasWin())
                    { 
                        return (int.Parse(number), board);
                    }
                }
            }

            throw new Exception();
        }

        private List<string> GetAllCalledNumbers()
        {
            var numbers = FileContents.First();
            return numbers.Split(',').ToList();
        }

        public List<BingoBoard> GetBingoBoards()
        {
            FileContents.RemoveAt(0);

            var bingoBoards = new List<BingoBoard>();

            var bingoBoardNumber = 0;
            var rowNumber = 0;
            FileContents.ForEach(x =>
            {
                if (string.IsNullOrWhiteSpace(x))
                {
                    bingoBoardNumber++;
                    bingoBoards.Add(new BingoBoard
                    {
                        Id = bingoBoardNumber
                    });
                    rowNumber = 0;
                }
                else
                {
                    var bingoBoard = bingoBoards.First(x => x.Id == bingoBoardNumber);
                    var bingoBoardLineContents = x.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
                    for (var i = 0; i < bingoBoardLineContents.Count; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(bingoBoardLineContents[i]))
                        {
                            bingoBoard.Cells.Add(new BingoBoardCell
                            {
                                Row = rowNumber,
                                Column = i,
                                Value = bingoBoardLineContents[i],
                                Marked = false
                            });
                        }
  
                    }
                    rowNumber++;
                }

            });

            return bingoBoards;
        }

        private void RefreshBingoBoards()
        {
            _bingoBoards.ForEach(x =>
            {
                x.HasWon = false;
                x.Cells.ForEach(y => y.Marked = false);
            });
        }
    }
}

public class BingoBoard
{
    public int Id { get; set; }
    public List<BingoBoardCell> Cells { get; set; } = new();
    public bool HasWon { get; set; }
    public bool HasWin()
    {
        for (var i = 0; i < Cells.Max(x => x.Row); i++)
        {
            var row = Cells.Where(x => x.Row == i);
            if (row.All(x => x.Marked))
            {
                HasWon = true;
                return true;
            }
        }

        for (var i = 0; i < Cells.Max(x => x.Column); i++)
        {
            var column = Cells.Where(x => x.Column == i);
            if (column.All(x => x.Marked))
            {
                HasWon = true;
                return true;
            }
        }
        
        return false;
    }

    public void MarkCell(string calledNumber)
    {
        Cells.ForEach(x =>
        {
            if (x.Value == calledNumber)
            {
                x.Marked = true;
            }
        });
    }
}

public class BingoBoardCell
{
    public int Row { get; set; }
    public int Column { get; set; }
    public string Value { get; set; }
    public bool Marked { get; set; }
}
