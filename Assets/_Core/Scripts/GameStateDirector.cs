using System;
using UnityEngine;

namespace _Core.Scripts
{
    public enum GameState
    {
        StartGame,
        PlayerStep,
        EnemyStep,
        LossGame,
        WinGame,
    }

    public class GameStateDirector
    {
        public event Action<GameState> OnChangeGameState;

        private GameState _gameState;

        public GameStateDirector()
        {
            OnChangeGameState = null;
        }

        public void SetGameState(GameState gameState)
        {
#if UNITY_EDITOR
            Debug.Log($"gameState: {gameState}");
#endif

            OnChangeGameState?.Invoke(gameState);
            _gameState = gameState;
        }

        public GameState GetCurrentGameState() => _gameState;
    }
}