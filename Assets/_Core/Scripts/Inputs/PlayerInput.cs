using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace _Core.Scripts.Inputs
{
    public class PlayerInput : MonoBehaviour
    {
        private GameStateDirector _gameStateDirector;

        [Inject]
        public void Construct(GameStateDirector gameStateDirector)
        {
            _gameStateDirector = gameStateDirector;
        }

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.R))
                SceneManager.LoadScene(0);
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
                _gameStateDirector.SetGameState(GameState.EnemyStep);
        }
    }
}