using UnityEngine;
using _Core.Scripts.Units.Player;
using _Core.Scripts.Units.Enemy;

namespace _Core.Scripts.Effects
{
    public class EffectAttack : Effect
    {
        private AttackSettings _attackSettings;

        private void Start()
        {
            _effect = Effects.EffectAttack;
            _effectSign = EffectSign.Negative;
            _attackSettings = _defaultEffectSettings.GetAttackSettings();
            _meshRenderer.material = _attackSettings.GetAttackMaterial();
            _canvasEffectName.SetEffectName(_attackSettings.GetAttackName());
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Player player))
            {
                if (_typeEffect == TypeEffect.Player)
                    return;
                DestroyEffect();
                player.ReduceHealth(_attackDamage);
            }

            if (collision.gameObject.TryGetComponent(out Enemy enemy))
            {
                DestroyEffect();
                enemy.ReduceHealth(_attackDamage);
            }
        }
    }
}