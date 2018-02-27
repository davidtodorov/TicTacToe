using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Data;
using TicTacToe.Services.Interfaces;
using TicTacToe.Services.Interfaces.Models;
using TicTacToe.Services.Mappings;

namespace TicTacToe.Services
{
    public class ScoreService : IScoreService
    {
        private readonly TicTacToeDbContext context;

        public ScoreService(TicTacToeDbContext context)
        {
            this.context = context;
        }

        public IList<GameScoresInfoOutput> GetScores()
        {
            this.context.ChangeTracker.AutoDetectChangesEnabled = false;

            var users = this.context.Users.AsNoTracking()
                .Select(GameMappings.ToGameScoresOutput)
                .OrderByDescending(s => s.Wins)
                .Take(10)
                .ToList();

            return users;
        }
    }
}
