using System.Windows;
using System.Windows.Controls;

namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        private char[,] board;
        private char currentPlayer;
        private bool gameOver;

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            board = new char[3, 3];
            currentPlayer = 'X';
            gameOver = false;

            // Initialize the buttons and board
            foreach (Button button in grid.Children)
            {
                button.Content = "";
                button.IsEnabled = true;
            }

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    board[row, col] = ' ';
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string[] tagParts = button.Tag.ToString().Split(',');

            if (tagParts.Length == 2 && int.TryParse(tagParts[0], out int row) && int.TryParse(tagParts[1], out int col))
            {
                if (!gameOver && IsValidMove(row, col))
                {
                    board[row, col] = currentPlayer;
                    button.Content = currentPlayer.ToString();

                    if (IsWinningMove(currentPlayer))
                    {
                        MessageBox.Show($"Player {currentPlayer} wins!");
                        gameOver = true;
                    }
                    else if (IsBoardFull())
                    {
                        MessageBox.Show("It's a tie!");
                        gameOver = true;
                    }
                    else
                    {
                        currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
                    }

                    button.IsEnabled = false;
                }
            }
        }


        private bool IsValidMove(int row, int col)
        {
            if (board[row, col] == ' ')
            {
                return true;
            }
            return false;
        }

        private bool IsWinningMove(char player)
        {
            // Check rows
            for (int row = 0; row < 3; row++)
            {
                if (board[row, 0] == player && board[row, 1] == player && board[row, 2] == player)
                {
                    return true;
                }
            }

            // Check columns
            for (int col = 0; col < 3; col++)
            {
                if (board[0, col] == player && board[1, col] == player && board[2, col] == player)
                {
                    return true;
                }
            }

            // Check diagonals
            if ((board[0, 0] == player && board[1, 1] == player && board[2, 2] == player) ||
                (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player))
            {
                return true;
            }

            return false;
        }

        private bool IsBoardFull()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (board[row, col] == ' ')
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
