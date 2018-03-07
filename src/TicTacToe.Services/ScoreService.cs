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

        private readonly ICacheService cacheService;

        public ScoreService(TicTacToeDbContext context, ICacheService cacheService)
        {
            this.context = context;
            this.cacheService = cacheService;
        }

        public IList<GameScoresInfoOutput> GetScores()
        {
            var scores = this.context.Users.AsNoTracking()
                .Select(GameMappings.ToGameScoresOutput)
                .OrderByDescending(s => s.Wins)
                .Take(10)
                .ToList();

            return scores;
        }
    }
}
