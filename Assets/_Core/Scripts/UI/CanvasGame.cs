using UnityEngine;

namespace _Core.Scripts.UI
{
    public class CanvasGame : CanvasAbstract
    {
        public void EndStep()
        {
            _gameStateDirector.SetGameState(GameState.EnemyStep);
        }

        protected override void ChangeGameState(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.StartGame:
                    SetCanvasState(true);
                    break;
                case GameState.PlayerStep:
                    SetCanvasState(true);
                    break;
                case GameState.EnemyStep:
                    SetCanvasState(false);
                    break;
                case GameState.LossGame:
                    SetCanvasState(false);
                    break;
                case GameState.WinGame:
                    SetCanvasState(false);
                    break;
            }
        }
    }
}