using System;
using _Core.Scripts.Units.Enemy;
using TMPro;
using UnityEngine;

namespace _Core.Scripts.UI
{
    public class CanvasEnemyHealth : CanvasAbstract
    {
        [SerializeField] private TextMeshProUGUI _health;
        [SerializeField] private GameObject _image;
        private Enemy _enemy;

        private void Start()
        {
            if (!GetComponentInParent<Enemy>())
                throw new Exception("No enemy script");
            _enemy = GetComponentInParent<Enemy>();
            _enemy.OnHealthChange += UpdateHpText;
            UpdateHpText(_enemy.GetHealth(), _enemy.GetTempHealth());
        }

        private void UpdateHpText(int health, int tempHealth)
        {
            _health.text = tempHealth == 0 ? $"HP {health}" : $"HP {health} + {tempHealth}";
            if (_enemy.GetPoisonEffectStepAmount() > 0)
                _image.SetActive(true);
            if (_enemy.GetPoisonEffectStepAmount() == 0)
                _image.SetActive(false);
        }

        protected override void ChangeGameState(GameState gameState)
        {
        }
    }
}