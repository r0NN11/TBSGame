using System.Collections.Generic;
using _Core.Scripts.Effects;
using _Core.Scripts.Units;
using _Core.Scripts.Units.Player;
using DG.Tweening;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Core.Scripts.Battle
{
    public class AIBattleLogic : MonoBehaviour
    {
        private GameStateDirector _gameStateDirector;
        private BattleManager _battleManager;
        private PlayerController _playerController;
        private List<Effect> _enemyEffect;
        private List<Unit> _listPlayer;
        private float _stepDelay = 1f;

        [Inject]
        public void Constructor(GameStateDirector gameStateDirector, BattleManager battleManager,
            PlayerController playerController)
        {
            _battleManager = battleManager;
            _playerController = playerController;
            _gameStateDirector = gameStateDirector;
            _gameStateDirector.OnChangeGameState += ChangeGameState;
        }

        private void EnemyStep()
        {
            DOTween.Sequence()
                .AppendInterval(_stepDelay)
                .AppendCallback(() =>
                {
                    foreach (var effect in _enemyEffect)
                    {
                        ApplyEffect(effect);
                    }
                });
        }

        private void ApplyEffect(Effect effect)
        {
            switch (effect.GetEffect())
            {
                case Effects.Effects.EffectAttack:
                    MoveAttackPoisonEffect(effect);
                    break;
                case Effects.Effects.EffectHeal:
                    MoveHealDefenseEffect(effect);
                    break;
                case Effects.Effects.EffectPoison:
                    MoveAttackPoisonEffect(effect);
                    break;
                case Effects.Effects.EffectDefence:
                    MoveHealDefenseEffect(effect);
                    break;
            }
        }

        private void MoveAttackPoisonEffect(Effect effect)
        {
            var randomPlayerUnit = _listPlayer[Random.Range(0, _listPlayer.Count)].gameObject;
            effect.gameObject.transform.DOMove(randomPlayerUnit.transform.position, 1f);
        }

        private void MoveHealDefenseEffect(Effect effect)
        {
            effect.gameObject.transform.DOMove(effect.GetSelfUnit().transform.position, 1f);
        }

        private void ChangeGameState(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.StartGame:
                    _enemyEffect = _battleManager.GetListEnemyEffects();
                    _listPlayer = _playerController.GetPlayersList();
                    break;
                case GameState.EnemyStep:
                    EnemyStep();
                    break;
            }
        }
    }
}