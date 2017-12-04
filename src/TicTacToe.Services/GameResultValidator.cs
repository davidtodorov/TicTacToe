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
            var selectedPosition = row + col;
            char playerChar = ' '; 
            GameResult gameResult;

            // Invalid Position
            if (row > 2 || row < 0)
            {
                throw new NotImplementedException();
            }

            // Invalid Position
            if (col > 2 || col < 0)
            {
                throw new NotImplementedException();
            }

            // Invalid Postion
            if (board[selectedPosition] == 'X' || board[selectedPosition] == 'O')
            {
                throw new NotImplementedException();
            }

            //Check if it's not finished
            if (board.Contains("-"))
                {
                    gameResult = GameResult.NotFinished;
                }

            //Check for player X
            if (playerChar == 'X')
            {
                gameResult = GameResult.WonByX;
            }

            //Check for player O
            else if (playerChar == 'O')
                {
                    gameResult = GameResult.WonByO;

                }

            //Check for invalid player char
            else
                {
                    throw new NotImplementedException();
                }

            //Won, diagonal
            if (board[0] == playerChar && board[4] == playerChar && board[8] == playerChar)
                {
                    return gameResult;
                }

            //Won, diagonal
            if (board[2] == playerChar && board[4] == playerChar && board[6] == playerChar)
                {
                    return gameResult;
                }

            //Won, row 1
            if (board[0] == playerChar && board[1] == playerChar && board[2] == playerChar)
                {
                    return gameResult;
                }

            //Won, row 2
            if (board[3] == playerChar && board[4] == playerChar && board[5] == playerChar)
                {
                    return gameResult;
                }

            //Won, row 3
            if (board[6] == playerChar && board[7] == playerChar && board[8] == playerChar)
                {
                    return gameResult;
                }

            //Won, column 1
            if (board[0] == playerChar && board[3] == playerChar && board[6] == playerChar)
                {
                    return gameResult;
                }

            //Won, column 2
            if (board[1] == playerChar && board[4] == playerChar && board[7] == playerChar)
                {
                    return gameResult;
                }

            //Won, column 3
            if (board[0] == playerChar && board[3] == playerChar && board[6] == playerChar)
                {
                    return gameResult;
                }

            gameResult = GameResult.Draw;

            return gameResult;
        }
    }
}