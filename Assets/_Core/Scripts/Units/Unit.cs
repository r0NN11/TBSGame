using UnityEngine;
using Zenject;

namespace _Core.Scripts.Units
{
    public abstract class Unit : MonoBehaviour
    {
        protected SettingsSpawnCount _settingsSpawnCount;
        protected GameStateDirector _gameStateDirector;
        protected int _health;

        [Inject]
        public void Constructor(SettingsSpawnCount settingsSpawnCount, GameStateDirector gameStateDirector)
        {
            _settingsSpawnCount = settingsSpawnCount;
            _gameStateDirector = gameStateDirector;
            _gameStateDirector.OnChangeGameState += ChangeGameState;
        }

        public int GetHealth() => _health;

        private void Awake()
        {
            _health = _settingsSpawnCount.GetHP();
        }

        protected abstract void ChangeGameState(GameState gameState);
        protected abstract void OnEnable();

        protected virtual void DisableUnit()
        {
            Destroy(gameObject);
            _gameStateDirector.OnChangeGameState -= ChangeGameState;
        }
    }
}