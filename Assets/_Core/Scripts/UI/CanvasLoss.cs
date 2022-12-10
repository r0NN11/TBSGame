using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Core.Scripts.UI
{
    public class CanvasLoss : CanvasAbstract
    {
        public void RestartGame()
        {
            SetCanvasState(false);
            SceneManager.LoadScene(0);
        }

        protected override void ChangeGameState(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.LossGame:
                    SetCanvasState(true);
                    break;
            }
        }
    }
}