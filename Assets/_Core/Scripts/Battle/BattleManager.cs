using System;
using System.Collections.Generic;
using _Core.Scripts.Effects;
using _Core.Scripts.Units;
using _Core.Scripts.Units.Enemy;
using _Core.Scripts.Units.Player;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Core.Scripts.Battle
{
    public class BattleManager : MonoBehaviour
    {
        private DefaultEffectSettings _defaultEffectSettings;
        private PlayerController _playerController;
        private EnemyController _enemyController;
        private GameStateDirector _gameStateDirector;

        private List<Effect> _playerEffect;
        private List<Effect> _enemyEffect;
        private List<Unit> _listPlayer;
        private List<Unit> _listEnemy;

        private Effect _attackPrefab;
        private Effect _healPrefab;
        private Effect _poisonPrefab;
        private Effect _defencePrefab;

        private float _zPos = 1;

        [Inject]
        public void Constructor(DefaultEffectSettings defaultEffectSettings, GameStateDirector gameStateDirector,
            PlayerController playerController, EnemyController enemyController)
        {
            _defaultEffectSettings = defaultEffectSettings;
            _playerController = playerController;
            _enemyController = enemyController;
            _gameStateDirector = gameStateDirector;
            _gameStateDirector.OnChangeGameState += ChangeGameState;
        }

        public List<Effect> GetListEnemyEffects() => _enemyEffect;

        public void DestroyUnitEffect(Effect effect)
        {
            if (effect.GetTypeEffect() == TypeEffect.Player)
                _playerEffect.Remove(effect);
            else
                _enemyEffect.Remove(effect);
            Destroy(effect.gameObject);
            if (_enemyEffect.Count == 0 & _gameStateDirector.GetCurrentGameState() == GameState.EnemyStep)
                _gameStateDirector.SetGameState(GameState.PlayerStep);
        }

        private void Start()
        {
            _gameStateDirector.SetGameState(GameState.StartGame);
        }

        private void CreateUnitEffect(List<Unit> unitList, TypeEffect typeEffect,
            List<Effect> unitEffect, float zPos)
        {
            for (var i = 0; i < unitList.Count; i++)
            {
                var randomEffect = (Effects.Effects) Random.Range(0, Enum.GetValues(typeof(Effects.Effects)).Length);
                switch (randomEffect)
                {
                    case Effects.Effects.EffectAttack:
                        unitEffect.Add(Instantiate(_attackPrefab, transform.position, transform.rotation));
                        break;
                    case Effects.Effects.EffectHeal:
                        unitEffect.Add(Instantiate(_healPrefab, transform.position, transform.rotation));
                        break;
                    case Effects.Effects.EffectPoison:
                        unitEffect.Add(Instantiate(_poisonPrefab, transform.position, transform.rotation));
                        break;
                    case Effects.Effects.EffectDefence:
                        unitEffect.Add(Instantiate(_defencePrefab, transform.position, transform.rotation));
                        break;
                }

                SetUpEffectPosition(unitEffect[i], unitList[i].transform, zPos);
                unitEffect[i].SetSelfUnit(unitList[i].gameObject);
                unitEffect[i].SetTypeEffect(typeEffect);
            }
        }

        private void SetUpEffectPosition(Effect effectObj, Transform unitTransform, float zPos)
        {
            effectObj.transform.position = unitTransform.position;
            var tempPos = effectObj.transform.position;
            tempPos.z += zPos;
            tempPos.y += _zPos;
            effectObj.transform.position = tempPos;
        }

        private void DestroyEffects(List<Effect> unitEffect)
        {
            foreach (var effect in unitEffect)
            {
                Destroy(effect.gameObject);
            }

            unitEffect.Clear();
        }

        private void ChangeGameState(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.StartGame:
                    _listPlayer = _playerController.GetPlayersList();
                    _listEnemy = _enemyController.GetEnemiesList();
                    _playerEffect = new List<Effect>();
                    _enemyEffect = new List<Effect>();
                    _attackPrefab = _defaultEffectSettings.GetAttackSettings().GetAttackPrefab();
                    _healPrefab = _defaultEffectSettings.GetPoisonSettings().GetPoisonPrefab();
                    _poisonPrefab = _defaultEffectSettings.GetPoisonSettings().GetPoisonPrefab();
                    _defencePrefab = _defaultEffectSettings.GetDefenceSettings().GetDefencePrefab();
                    CreateUnitEffect(_listPlayer, TypeEffect.Player, _playerEffect, _zPos);
                    break;
                case GameState.PlayerStep:
                    CreateUnitEffect(_listPlayer, TypeEffect.Player, _playerEffect, _zPos);
                    break;
                case GameState.EnemyStep:
                    DestroyEffects(_playerEffect);
                    CreateUnitEffect(_listEnemy, TypeEffect.Enemy, _enemyEffect, -_zPos);
                    break;
                case GameState.LossGame:
                    DestroyEffects(_playerEffect);
                    break;
                case GameState.WinGame:
                    DestroyEffects(_playerEffect);
                    break;
            }
        }
    }
}