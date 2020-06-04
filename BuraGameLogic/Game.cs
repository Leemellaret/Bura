using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace BuraGameLogic
{
    [DataContract]
    public class Game
    {
        [DataMember]
        private List<Player> players;

        public Game(TeamsSettings teamsSettings)
        {
            if (!teamsSettings.IsRecruitmentDone())
                throw new ArgumentException("Набор игроков ещё не завершён", "teamsSettings");

            players = new List<Player>((int)teamsSettings.RequiredNumberOfPlayers);
            foreach (var p in teamsSettings.RecruitedPlayers)
            {
                players.Add(new Player(p.Item1, p.Item2));
            }
        }

    }

    public class TeamsSettings
    {
        private List<(string, Team)> recruitedPlayers;
        public NumberOfPlayers RequiredNumberOfPlayers { get; }
        private int[] countOfPlayersInTeams;

        public TeamsSettings(NumberOfPlayers numberOfPlayers)
        {
            recruitedPlayers = new List<(string, Team)>((int)numberOfPlayers);
            RequiredNumberOfPlayers = numberOfPlayers;
            countOfPlayersInTeams = new int[3];
        }

        public IEnumerable<(string, Team)> RecruitedPlayers { get => recruitedPlayers; }

        public void RecruitPlayer(string name, Team team)
        {
            if (IsRecruitmentDone())
                throw new InvalidOperationException("Набор игроков уже завершён");

            if ((RequiredNumberOfPlayers == NumberOfPlayers.Two || RequiredNumberOfPlayers == NumberOfPlayers.Four) && team == Team.Third)
                throw new ArgumentException("Когда в игре двое или четверо игроков, то можно выбирать только первые две команду", "team");

            if (((RequiredNumberOfPlayers == NumberOfPlayers.Two || RequiredNumberOfPlayers == NumberOfPlayers.Three) && countOfPlayersInTeams[(int)team - 1] == 1) ||
                (RequiredNumberOfPlayers == NumberOfPlayers.Four && countOfPlayersInTeams[(int)team - 1] == 2))
                throw new InvalidOperationException($"В команде {team} уже максимальное количество игроков");

            if (name == "")
                throw new ArgumentException("Пустое имя недопустимо", "name");

            if (recruitedPlayers.Contains((name, team)))
                throw new InvalidOperationException($"Игрок с именем {name} и желающий вступить в {team} команду уже существует");

            recruitedPlayers.Add((name, team));
            countOfPlayersInTeams[(int)team - 1]++;
        }

        public int NumberOfTeams()
        {
            return RequiredNumberOfPlayers == NumberOfPlayers.Three ? 3 : 2;
        }

        public void RemovePlayer(string name)
        {
            var removingPlayer = recruitedPlayers.Find(x => x.Item1 == name);
            if (removingPlayer == ("", 0))
                throw new InvalidOperationException($"Нет игрока с именем {name}");

            recruitedPlayers.Remove(removingPlayer);
            countOfPlayersInTeams[(int)removingPlayer.Item2 - 1]--;

        }
        public bool IsRecruitmentDone()
        {
            return recruitedPlayers.Count == (int)RequiredNumberOfPlayers;
        }
    }
    public enum NumberOfPlayers
    {
        Two = 2,
        Three = 3,
        Four = 4
    }
    public enum Team
    {
        First = 1,
        Second = 2,
        Third = 3
    }
}
