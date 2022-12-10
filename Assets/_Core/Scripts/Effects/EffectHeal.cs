using UnityEngine;
using _Core.Scripts.Units.Player;
using _Core.Scripts.Units.Enemy;

namespace _Core.Scripts.Effects
{
    public class EffectHeal : Effect
    {
        private HealSettings _healSettings;

        private void Start()
        {
            _effect = Effects.EffectHeal;
            _effectSign = EffectSign.Negative;
            _healSettings = _defaultEffectSettings.GetHealSettings();
            _meshRenderer.material = _healSettings.GetHealMaterial();
            _canvasEffectName.SetEffectName(_healSettings.GetHealName());
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Player player))
            {
                if (_selfUnit != player.gameObject)
                    return;
                if (player.IncreaseHealth(_healEffectAmount))
                    DestroyEffect();
            }

            if (collision.gameObject.TryGetComponent(out Enemy enemy))
            {
                if (_typeEffect == TypeEffect.Player)
                    return;
                DestroyEffect();
                enemy.IncreaseHealth(_healEffectAmount);
            }
        }
    }
}