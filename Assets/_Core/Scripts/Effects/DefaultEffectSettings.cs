using System;
using UnityEngine;

namespace _Core.Scripts.Effects
{
    public enum Effects
    {
        EffectAttack,
        EffectHeal,
        EffectPoison,
        EffectDefence,
    }

    public enum TypeEffect
    {
        Player,
        Enemy,
    }

    public enum EffectSign
    {
        Positive,
        Negative,
    }

    [CreateAssetMenu(fileName = "DefaultEffectSettings")]
    public class DefaultEffectSettings : ScriptableObject
    {
        [SerializeField] private AttackSettings _attackSettings;
        [SerializeField] private HealSettings _healSettings;
        [SerializeField] private PoisonSettings _poisonSettings;
        [SerializeField] private DefenceSettings _defenceSettings;
        [SerializeField] private int _attackDamage = 3;
        [SerializeField] private int _healEffectAmount = 1;
        [SerializeField] private int _poisonEffectHpAmount = 1;
        [SerializeField] private int _poisonEffectStepAmount = 1;
        [SerializeField] private int _defenceEffectHpAmount = 5;
        [SerializeField] private int _defenceEffectStepAmount = 3;

        public AttackSettings GetAttackSettings() => _attackSettings;
        public HealSettings GetHealSettings() => _healSettings;
        public PoisonSettings GetPoisonSettings() => _poisonSettings;
        public DefenceSettings GetDefenceSettings() => _defenceSettings;
        public int GetAttackDamage() => _attackDamage;
        public int GetHealEffectAmount() => _healEffectAmount;
        public int GetPoisonEffectHpAmount() => _poisonEffectHpAmount;
        public int GetPoisonEffectStepAmount() => _poisonEffectStepAmount;
        public int GetDefenceEffectHpAmount() => _defenceEffectHpAmount;
        public int GetDefenceEffectStepAmount() => _defenceEffectStepAmount;
    }

    [Serializable]
    public struct AttackSettings
    {
        [SerializeField] private Effect _attackPrefab;
        [SerializeField] private Material _attack;
        [SerializeField] private String _attackName;
        public Effect GetAttackPrefab() => _attackPrefab;
        public Material GetAttackMaterial() => _attack;
        public string GetAttackName() => _attackName;
    }

    [Serializable]
    public struct HealSettings
    {
        [SerializeField] private Effect _healPrefab;
        [SerializeField] private Material _heal;
        [SerializeField] private String _healName;
        public Effect GetHealPrefab() => _healPrefab;
        public Material GetHealMaterial() => _heal;
        public string GetHealName() => _healName;
    }

    [Serializable]
    public struct PoisonSettings
    {
        [SerializeField] private Effect _poisonPrefab;
        [SerializeField] private Material _poison;
        [SerializeField] private String _poisonName;
        public Effect GetPoisonPrefab() => _poisonPrefab;
        public Material GetPoisonMaterial() => _poison;
        public string GetPoisonName() => _poisonName;
    }

    [Serializable]
    public struct DefenceSettings
    {
        [SerializeField] private Effect _defencePrefab;
        [SerializeField] private Material _defence;
        [SerializeField] private String _defenceName;
        public Effect GetDefencePrefab() => _defencePrefab;
        public Material GetDefenceMaterial() => _defence;
        public string GetDefenceName() => _defenceName;
    }
}