using UnityEngine;

namespace _Core.Scripts
{
    [CreateAssetMenu(fileName = "SettingsSpawnCount")]
    public class SettingsSpawnCount : ScriptableObject
    {
        [SerializeField] private int _spawnCount = 4;
        [SerializeField] private int _health = 30;
        public int GetSpawnCount() => _spawnCount;
        public int GetHP() => _health;
    }
}