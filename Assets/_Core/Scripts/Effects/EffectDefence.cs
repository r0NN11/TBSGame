using _Core.Scripts.Units.Enemy;
using _Core.Scripts.Units.Player;
using UnityEngine;

namespace _Core.Scripts.Effects
{
    public class EffectDefence : Effect
    {
        private DefenceSettings _defenceSettings;

        private void Start()
        {
            _effect = Effects.EffectDefence;
            _effectSign = EffectSign.Negative;
            _defenceSettings = _defaultEffectSettings.GetDefenceSettings();
            _meshRenderer.material = _defenceSettings.GetDefenceMaterial();
            _canvasEffectName.SetEffectName(_defenceSettings.GetDefenceName());
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Player player))
            {
                if (_selfUnit != player.gameObject)
                    return;
                if (player.TakeDefence(_defenceEffectHpAmount, _defenceEffectStepAmount))
                    DestroyEffect();
            }

            if (collision.gameObject.TryGetComponent(out Enemy enemy))
            {
                if (_typeEffect == TypeEffect.Player)
                    return;
                DestroyEffect();
                enemy.TakeDefence(_defenceEffectHpAmount, _defenceEffectStepAmount);
            }
        }
    }
}