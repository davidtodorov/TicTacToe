using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicTacToe.Services.Interfaces;
using TicTacToe.Services.Interfaces.Models;

namespace TicTacToe.Services.UnitTests
{
    [TestClass]
    public class GameResultValidatorTests
    {
        private readonly IGameResultValidator gameResultValidator = new GameResultValidator();

        /// <summary>
        /// X X X
        /// - O -
        /// O - O
        /// </summary>
        [TestMethod]
        public void GetGameResult_WhenPlayerXWinsOnFirstRow_ReturnsWinByX()
        {
            // Arrange
            var board = "XXX-O-O-O";

            // Act
            var gameResult = this.gameResultValidator.GetGameResult(board);

            // Assert
            Assert.AreEqual(GameResult.WonByX, gameResult);
        }

        /// <summary>
        /// O - -
        /// X X X
        /// O - O
        /// </summary>
        [TestMethod]
        public void GetGameResult_WhenPlayerXWinsOnSecondRow_ReturnsWinByX()
        {
            // Arrange
            var board = "O--XXXO-O";

            // Act
            var gameResult = this.gameResultValidator.GetGameResult(board);

            // Assert
            Assert.AreEqual(GameResult.WonByX, gameResult);
        }
    }
}