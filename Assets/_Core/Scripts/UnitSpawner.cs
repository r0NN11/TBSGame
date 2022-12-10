using System;
using System.Collections.Generic;
using _Core.Scripts.Units.Enemy.EnemyInject;
using _Core.Scripts.Units.Player.PlayerInject;
using UnityEngine;
using Zenject;

namespace _Core.Scripts
{
    public class UnitSpawner : MonoBehaviour
    {
        [SerializeField] private Vector3 _centerPoint = new Vector3(0, 0, 1);
        private List<PlayerInject> _playerInject;
        private List<EnemyInject> _enemyInject;
        private SettingsSpawnCount _settingsSpawnCount;
        [Inject] private PlayerInject.FactoryEnemyInject _factoryPlayerInject;
        [Inject] private EnemyInject.FactoryEnemyInject _factoryEnemyInject;

        [Inject]
        public void Constructor(SettingsSpawnCount settingsSpawnCount)
        {
            _settingsSpawnCount = settingsSpawnCount;
        }

        public UnitSpawner()
        {
            _playerInject = new List<PlayerInject>();
            _enemyInject = new List<EnemyInject>();
        }

        private void Awake()
        {
            CreateUnits();
        }

        private void CreateUnits()
        {
            _playerInject.Clear();
            _enemyInject.Clear();
            for (var i = 0; i < _settingsSpawnCount.GetSpawnCount(); i++)
            {
                _playerInject.Add(_factoryPlayerInject.Create());
                _enemyInject.Add(_factoryEnemyInject.Create());
                var tempEnemyInject = _enemyInject[i].transform;
                tempEnemyInject.position = SetSpawnPosition(i, _centerPoint);
                var tempPlayerInject = _playerInject[i].transform;
                tempPlayerInject.position = SetSpawnPosition(i, -_centerPoint);
            }
        }

        private Vector3 SetSpawnPosition(int i, Vector3 centerPoint)
        {
            float x, y, z;
            float spacing = 3f;
            int columns = 1;
            int row = i / columns;
            int column = i % columns;
            x = -_centerPoint.x + row * spacing;
            y = centerPoint.y;
            z = centerPoint.z + column * spacing;
            return new Vector3(x, y, z);
        }
    }
}