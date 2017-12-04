using System;
using TicTacToe.Services.Interfaces;
using TicTacToe.Services.Interfaces.Models;

namespace TicTacToe.Services
{
    public class GameResultValidator : IGameResultValidator
    {
        /// <inheritdoc />
        public GameResult GetGameResult(string board)
        {
            GameResult gameResult;

            //Won, diagonal (0,4,8)
            if (board[0] == board[4] && board[4] == board[8])
            {
                GetWinner(board[0]);
            }

            //Won, diagonal (2,4,6)
            else if (board[2] == board[4] && board[4] == board[6])
            {
                GetWinner(board[2]);
            }

            //Won, row 1 (0,1,2)
            else if (board[0] == board[1] && board[1] == board[2])
            {
                GetWinner(board[0]);
            }

            //Won, row 2 (3,4,5)
            else if (board[3] == board[4] && board[4] == board[5])
            {
                GetWinner(board[3]);
            }

            //Won, row 3 (6,7,8)
            else if (board[6] == board[7] && board[7] == board[8])
            {
                GetWinner(board[6]);
            }

            //Won, column 1 (0,3,6)
            else if (board[0] == board[3] && board[3] == board[6])
            {
                GetWinner(board[0]);
            }

            //Won, column 2 (1,4,7)
            else if (board[1] == board[4] && board[4] == board[7])
            {
                GetWinner(board[1]);
            }

            //Won, column 3 (2, 5, 8)
            else if (board[3] == board[5] && board[5] == board[8])
            {
                GetWinner(board[3]);
            }

            //Check if it's not finished
            
            else if (board.Contains("-"))
            {
                gameResult = GameResult.NotFinished;
                return gameResult;
            }

            else
            {
                gameResult = GameResult.Draw;
                return gameResult;
            }

            throw new Exception();
        }
        

        private GameResult GetWinner(char playerChar)
        {
            if (playerChar == 'X')
            {
                return GameResult.WonByX;
            }
            else if (playerChar == 'O')
            {
                return GameResult.WonByO;
            }
            else
            {
                throw new NotImplementedException();
            }

            
        }
    }
}