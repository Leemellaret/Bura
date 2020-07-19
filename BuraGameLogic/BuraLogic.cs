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
    public class BuraLogic
    {
        [DataMember]
        private List<Player> players;
        [DataMember]
        public Scores Scores { get; }
        [DataMember]
        private Deck deck;
        [DataMember]
        public int Dealer { get; private set; }
        [DataMember]
        public NowMove NowMove { get; private set; }
        [DataMember]
        public CombinationsChecker CombinationsChecker { get; private set; }

        public BuraLogic(TeamsSettings teamsSettings, CombinationsChecker combinationsChecker)
        {
            if (!teamsSettings.IsRecruitmentDone())
                throw new ArgumentException("Набор игроков ещё не завершён", "teamsSettings");

            players = new List<Player>((int)teamsSettings.RequiredNumberOfPlayers);
            foreach (var p in teamsSettings.RecruitedPlayers)
            {
                players.Add(new Player(p.Item1, p.Item2));
            }

            Scores = new Scores(teamsSettings.NumberOfTeams());

            deck = new Deck();

            Dealer = -1;//Т.к. в StartNewGame() при первой игре Dealer должен иметь значение 0

            CombinationsChecker = combinationsChecker;

            StartNewGame();

        }

        private void StartNewGame()
        {
            SetNextPlayerAsDealer();
            DealStartingCardsToPlayers();
            //NowMove = new NowMove(players[(Dealer+1)%players.Count], TypeOfMove.

        }
        private void DealStartingCardsToPlayers()
        {
            for (int i = 0; i < 4 * NumberOfPlayers; i++)
            {
                players[(Dealer + 1 + i) % NumberOfPlayers].AddCard(deck.TakeCard());
            }
        }
        private void SetNowMoveAtStartGame()
        {

        }
        private void SetNextPlayerAsDealer()
        {
            Dealer++;
            if (Dealer >= NumberOfPlayers)
                Dealer = 0;
        }
        private void UpdateNowMove()
        {
            
        }


        public int NumberOfPlayers { get => players.Count; }

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
                throw new ArgumentException("Когда в игре двое или четверо игроков, то можно выбирать только первые две команды", "team");

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
