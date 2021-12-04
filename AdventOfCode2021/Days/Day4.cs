namespace AdventOfCode2021
{
    public class Day4
    {
        private readonly List<string> _fileContents;
        private readonly List<string> _calledNumbers;
        private readonly List<BingoBoard> _bingoBoards;

        public Day4(string fileLocation)
        {
            _fileContents = FileHandler.GetFileContentsAsStrings(fileLocation);
            _calledNumbers = GetAllCalledNumbers();
            _bingoBoards = GetBingoBoards();
        }

        public (int, int) GetDay4Result() 
        {
            return (GetWinningBoardSum(), GetLastWinningBoardSum());
        }

        public int GetWinningBoardSum()
        {
            var (winningNumber, winningBoard) = GetWinner();

            var boardSum = 0;
            winningBoard.Cells.Where(x => !x.Marked).ToList().ForEach(x => boardSum += int.Parse(x.Value));

            return boardSum * winningNumber;
        }

        public int GetLastWinningBoardSum()
        {
            var (winningNumber, winningBoard) = GetLastWinner();

            var boardSum = 0;
            winningBoard.Cells.Where(x => !x.Marked).ToList().ForEach(x => boardSum += int.Parse(x.Value));

            return boardSum * winningNumber;
        }

        public (int, BingoBoard) GetWinner()
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

        public (int, BingoBoard) GetLastWinner()
        {
            RefreshBingoBoards();

            foreach (var number in _calledNumbers)
            {
                foreach (var board in _bingoBoards.Where(board => !board.HasWon))
                {
                    board.MarkCell(number);
                    if (board.HasWin() & _bingoBoards.Count(x => !x.HasWon) == 1)
                    { 
                        return (int.Parse(number), board);
                    }
                }
            }

            throw new Exception();
        }

        public List<string> GetAllCalledNumbers()
        {
            var numbers = _fileContents.First();
            return numbers.Split(',').ToList();
        }

        public List<BingoBoard> GetBingoBoards()
        {
            _fileContents.RemoveAt(0);

            var bingoBoards = new List<BingoBoard>();

            var bingoBoardNumber = 0;
            var rowNumber = 0;
            _fileContents.ForEach(x =>
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
                    for (var i = 0; i < bingoBoardLineContents.Count(); i++)
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
