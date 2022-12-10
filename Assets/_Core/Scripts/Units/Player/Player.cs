using System;
using Zenject;

namespace _Core.Scripts.Units.Player
{
    public class Player : Unit
    {
        public event Action<int, int> OnHealthChange;
        private int _tempHealth;
        private int _tempHealthStepCount;
        private int _poisonEffectHpAmount;
        private int _poisonEffectStepAmount;
        private PlayerController _playerController;

        [Inject]
        public void Constructor(PlayerController playerController)
        {
            _playerController = playerController;
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

        public bool IncreaseHealth(int increaseAmount)
        {
            if (_health == _settingsSpawnCount.GetHP() & _poisonEffectStepAmount == 0)
                return false;
            if (_poisonEffectStepAmount > 0)
                _poisonEffectStepAmount = 0;
            _health += increaseAmount;
            OnHealthChange?.Invoke(_health, _tempHealth);
            return true;
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

        public bool TakeDefence(int defenceAmount, int defenceStepAmount)
        {
            if (_tempHealthStepCount == defenceStepAmount)
                return false;
            _tempHealth = defenceAmount;
            _tempHealthStepCount = defenceStepAmount;
            OnHealthChange?.Invoke(_health, _tempHealth);
            return true;
        }

        protected override void OnEnable()
        {
            _playerController.AddPlayer(this);
        }

        protected override void DisableUnit()
        {
            base.DisableUnit();
            _playerController.RemovePlayer(this);
        }

        protected override void ChangeGameState(GameState gameState)
        {
            switch (gameState)
            {
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

        public void ReduceTempHealthStepCount()
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