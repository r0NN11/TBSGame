using _Core.Scripts.Units.Enemy;
using _Core.Scripts.Units.Player;
using UnityEngine;

namespace _Core.Scripts.Effects
{
    public class EffectPoison : Effect
    {
        private PoisonSettings _poisonSettings;

        private void Start()
        {
            _effect = Effects.EffectPoison;
            _effectSign = EffectSign.Negative;
            _poisonSettings = _defaultEffectSettings.GetPoisonSettings();
            _meshRenderer.material = _poisonSettings.GetPoisonMaterial();
            _canvasEffectName.SetEffectName(_poisonSettings.GetPoisonName());
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Player player))
            {
                if (_typeEffect == TypeEffect.Player)
                    return;
                player.TakePoison(_poisonEffectHpAmount, _poisonEffectStepAmount);
                DestroyEffect();
            }

            if (collision.gameObject.TryGetComponent(out Enemy enemy))
            {
                if (enemy.TakePoison(_poisonEffectHpAmount, _poisonEffectStepAmount))
                    DestroyEffect();
            }
        }
    }
}