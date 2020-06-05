using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace BuraGameLogic
{
    public class Scores
    {
        [DataMember]
        private int[] gameScore;
        [DataMember]
        private int[] globalScore;
        [DataMember]
        public int NumberOfTeams;
        [DataMember]
        public int Factor { get; private set; }

        internal Scores(int numberOfTeams)
        {
            if (numberOfTeams != 2 && numberOfTeams != 3)
                throw new ArgumentException($"Количество команд не может быть равным {numberOfTeams}. Допустимые значения 2 и 3");
            NumberOfTeams = numberOfTeams;
            gameScore = new int[numberOfTeams];
            globalScore = new int[numberOfTeams];
            Factor = 1;
        }
        internal void UpdateScore(int?[] points)
        {
            if (IsGameEnded())
                throw new InvalidOperationException("Эта партия уже закончилась");

            if (points == null)
                throw new ArgumentNullException("points");

            if (points.Length != NumberOfTeams)
                throw new InvalidOperationException($"Неверное количество команд - {points.Length}.");

            var numberOfCrashedTeams = points.Count(x => !x.HasValue);
            if ((NumberOfTeams == 2 && numberOfCrashedTeams > 0) || (NumberOfTeams == 3 && numberOfCrashedTeams > 1))
                throw new InvalidOperationException($"Недопустимое количество вылетевших команд: {numberOfCrashedTeams}");

            for (int i = 0; i < NumberOfTeams; i++)
            {
                if (gameScore[i] >= 12 && points[i].HasValue)
                    throw new InvalidOperationException($"{(Team)(i + 1)} команда вылетела и не могла участвовать в игре");
            }

            if (NumberOfTeams - numberOfCrashedTeams == 2 && points.All(x => !x.HasValue || x == 60))
            {
                Factor *= 2;
                return;
            }

            for (int i = 0; i < NumberOfTeams; i++)
            {
                if (!points[i].HasValue)
                    continue;

                if (points[i] < 0)
                {
                    gameScore[i] += Factor * 6;
                }
                else if (points[i] < 31)
                {
                    gameScore[i] += Factor * 4;
                }
                else if (points[i] < 60)
                {
                    gameScore[i] += Factor * 2;
                }
            }
            Factor = 1;
        }
        internal void StartNewGame()
        {
            if (!IsGameEnded())
                throw new InvalidOperationException("Эта партия ещё не закончилась.");

            for (int i = 0; i < NumberOfTeams; i++)
            {
                if (gameScore[i] < 12)
                {
                    globalScore[i]++;
                    break;
                }
                gameScore[i] = 0;
            }
            Factor = 1;
        }

        public bool IsGameEnded()
        {
            return gameScore.Count(x => x < 12) <= 1;
        }
        public Team Winner()
        {
            if (!IsGameEnded())
                throw new InvalidOperationException("Эта партия ещё не закончилась.");
            if (gameScore.Count(x => x < 12) == 0)
                throw new InvalidOperationException("Нет победителей в этой партии");

            int winningTeamIndex = 0;
            for (; gameScore[winningTeamIndex] >= 12; winningTeamIndex++) ;
            return (Team)(winningTeamIndex + 1);
        }
        public int GameScoreOf(Team team)
        {
            if (NumberOfTeams != 3 && team == Team.Third)
                throw new InvalidOperationException("В игре всего 2 команды");

            return gameScore[(int)team - 1];
        }
        public int GlobalScoreOf(Team team)
        {
            if (NumberOfTeams != 3 && team == Team.Third)
                throw new InvalidOperationException("В игре всего 2 команды");

            return globalScore[(int)team - 1];
        }
        public override string ToString()
        {
            string res = "";
            for (int i = 0; i < NumberOfTeams; i++)
            {
                res += $"{(Team)(i + 1)} team(GlobalScore={globalScore[i]} GameScore={gameScore[i]}). ";
            }

            return res;
        }
    }
}
