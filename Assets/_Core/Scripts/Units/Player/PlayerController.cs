using System.Collections.Generic;
using Zenject;

namespace _Core.Scripts.Units.Player
{
    public class PlayerController
    {
        private GameStateDirector _gameStateDirector;
        private List<Unit> _players;

        [Inject]
        public void Constructor(GameStateDirector gameStateDirector)
        {
            _gameStateDirector = gameStateDirector;
        }

        public PlayerController()
        {
            _players = new List<Unit>();
        }

        public List<Unit> GetPlayersList() => _players;

        public void AddPlayer(Unit player)
        {
            if (!_players.Contains(player))
                _players.Add(player);
        }

        public void RemovePlayer(Unit player)
        {
            _players.Remove(player);
            if (_players.Count == 0)
            {
                _gameStateDirector.SetGameState(GameState.LossGame);
            }
        }
    }
}