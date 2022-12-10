using System;
using Zenject;

namespace _Core.Scripts.Units.Enemy
{
    public class Enemy : Unit
    {
        public event Action<int, int> OnHealthChange;
        private int _tempHealth;
        private int _tempHealthStepCount;
        private int _poisonEffectHpAmount;
        private int _poisonEffectStepAmount;
        private EnemyController _enemyController;

        [Inject]
        public void Constructor(EnemyController enemyController)
        {
            _enemyController = enemyController;
        }

        public int GetTempHealth() => _tempHealth;
        public int GetPoisonEffectStepAmount() => _poisonEffectStepAmount;

        public void ReduceHealth(int reduceAmount)
        {
            if (_tempHealth == 0)
            {
                _health -= reduceAmount;
            }
            else
            {
                if (_tempHealth > reduceAmount)
                    _tempHealth -= reduceAmount;
                else
                {
                    var diff = reduceAmount - _tempHealth;
                    _health -= diff;
                    _tempHealth = 0;
                }
            }

            if (_health <= 0)
                DisableUnit();

            OnHealthChange?.Invoke(_health, _tempHealth);
        }

        public void IncreaseHealth(int increaseAmount)
        {
            if (_health == _settingsSpawnCount.GetHP() & _poisonEffectStepAmount == 0)
                return;
            if (_poisonEffectStepAmount > 0)
                _poisonEffectStepAmount = 0;
            _health += increaseAmount;
            OnHealthChange?.Invoke(_health, _tempHealth);
        }

        public bool TakePoison(int poisonAmount, int poisonStepAmount)
        {
            if (_poisonEffectStepAmount > 0)
                return false;
            _poisonEffectHpAmount = poisonAmount;
            _poisonEffectStepAmount = poisonStepAmount;
            ReduceHealth(_poisonEffectHpAmount);
            return true;
        }

        public void TakeDefence(int defenceAmount, int defenceStepAmount)
        {
            if (_tempHealthStepCount == defenceStepAmount)
                return;
            _tempHealth = defenceAmount;
            _tempHealthStepCount = defenceStepAmount;
            OnHealthChange?.Invoke(_health, _tempHealth);
        }

        protected override void OnEnable()
        {
            _enemyController.AddEnemy(this);
        }

        protected override void DisableUnit()
        {
            base.DisableUnit();
            _enemyController.RemoveEnemy(this);
        }

        protected override void ChangeGameState(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.StartGame:
                    break;
                case GameState.PlayerStep:
                    CheckPoisonAndReduceHP();
                    ReduceTempHealthStepCount();
                    break;
                case GameState.EnemyStep:
                    CheckPoisonAndReduceHP();
                    ReduceTempHealthStepCount();
                    break;
                case GameState.LossGame:
                    break;
                case GameState.WinGame:
                    break;
            }
        }

        private void CheckPoisonAndReduceHP()
        {
            if (_poisonEffectStepAmount > 0)
            {
                ReduceHealth(_poisonEffectHpAmount);
                if (_health <= 0)
                    DisableUnit();
                _poisonEffectStepAmount -= 1;
            }

            OnHealthChange?.Invoke(_health, _tempHealth);
        }

        private void ReduceTempHealthStepCount()
        {
            if (_tempHealth == 0)
                return;
            if (_tempHealthStepCount == 0)
            {
                _tempHealth = 0;
                return;
            }

            _tempHealthStepCount -= 1;
            OnHealthChange?.Invoke(_health, _tempHealth);
        }
    }
}