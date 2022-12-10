using System;
using _Core.Scripts.Units.Player;
using TMPro;
using UnityEngine;

namespace _Core.Scripts.UI
{
    public class CanvasPlayerHealth : CanvasAbstract
    {
        [SerializeField] private TextMeshProUGUI _health;
        [SerializeField] private GameObject _image;
        private Player _player;

        private void Start()
        {
            if (!GetComponentInParent<Player>())
                throw new Exception("No unit script");
            _player = GetComponentInParent<Player>();
            _player.OnHealthChange += UpdateHpText;
            UpdateHpText(_player.GetHealth(), _player.GetTempHealth());
        }

        private void UpdateHpText(int health, int tempHealth)
        {
            _health.text = tempHealth == 0 ? $"HP {health}" : $"HP {health} + {tempHealth}";
            if (_player.GetPoisonEffectStepAmount() > 0)
                _image.SetActive(true);
            if (_player.GetPoisonEffectStepAmount() == 0)
                _image.SetActive(false);
        }

        protected override void ChangeGameState(GameState gameState)
        {
        }
    }
}