public class GameState
{
    public event Action? OnChange;

    public int[,] Board { get; set; } = new int[6, 7];
    public int CurrentPlayer { get; set; } = 1;
    public string WinningMessage { get; set; } = "";
    public List<string> MoveHistory { get; set; } = new();

    public void ResetGame()
    {
        Board = new int[6, 7];
        CurrentPlayer = 1;
        WinningMessage = "";
        MoveHistory.Clear();
        NotifyStateChanged();
    }

    public void PlayPiece(int col)
    {
        if (!string.IsNullOrEmpty(WinningMessage)) return;

        for (int row = 5; row >= 0; row--)
        {
            if (Board[row, col] == 0)
            {
                Board[row, col] = CurrentPlayer;
                MoveHistory.Add($"Player {CurrentPlayer} placed in Column {col + 1}");
                if (CheckWin(row, col))
                {
                    WinningMessage = $"Player {CurrentPlayer} Wins!";
                }
                else
                {
                    CurrentPlayer = CurrentPlayer == 1 ? 2 : 1;
                }
                NotifyStateChanged();
                break;
            }
        }
    }

    private void NotifyStateChanged() => OnChange?.Invoke();

    private bool CheckWin(int row, int col)
{
    int player = Board[row, col];

    
    int count = 0;
    for (int c = 0; c < 7; c++)
    {
        count = (Board[row, c] == player) ? count + 1 : 0;
        if (count >= 4) return true;
    }

    
    count = 0;
    for (int r = 0; r < 6; r++)
    {
        count = (Board[r, col] == player) ? count + 1 : 0;
        if (count >= 4) return true;
    }

    
    for (int r = 0; r < 3; r++)
    {
        for (int c = 0; c < 4; c++)
        {
            if (Board[r, c] == player && Board[r+1, c+1] == player && 
                Board[r+2, c+2] == player && Board[r+3, c+3] == player) return true;
        }
    }

    
    for (int r = 3; r < 6; r++)
    {
        for (int c = 0; c < 4; c++)
        {
            if (Board[r, c] == player && Board[r-1, c+1] == player && 
                Board[r-2, c+2] == player && Board[r-3, c+3] == player) return true;
        }
    }

    return false;
    }
}