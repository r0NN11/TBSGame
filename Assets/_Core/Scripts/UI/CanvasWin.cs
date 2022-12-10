using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Core.Scripts.UI
{
    public class CanvasWin : CanvasAbstract
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
                case GameState.WinGame:
                    SetCanvasState(true);
                    break;
            }
        }
    }
}