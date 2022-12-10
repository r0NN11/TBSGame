using System.Collections.Generic;
using Zenject;

namespace _Core.Scripts.Units.Enemy
{
    public class EnemyController
    {
        private GameStateDirector _gameStateDirector;
        private List<Unit> _abstractEnemies;

        [Inject]
        public void Constructor(GameStateDirector gameStateDirector)
        {
            _gameStateDirector = gameStateDirector;
        }

        public EnemyController()
        {
            _abstractEnemies = new List<Unit>();
        }

        public List<Unit> GetEnemiesList() => _abstractEnemies;

        public void AddEnemy(Unit enemy)
        {
            if (!_abstractEnemies.Contains(enemy))
                _abstractEnemies.Add(enemy);
        }

        public void RemoveEnemy(Unit enemy)
        {
            _abstractEnemies.Remove(enemy);
            if (_abstractEnemies.Count == 0)
            {
                _gameStateDirector.SetGameState(GameState.WinGame);
            }
        }
    }
}