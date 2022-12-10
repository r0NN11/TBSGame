using TMPro;
using UnityEngine;

namespace _Core.Scripts.UI
{
    public class CanvasEffectName : CanvasAbstract
    {
        [SerializeField] private TextMeshProUGUI _effectName;
        public void SetEffectName(string effectName) => _effectName.text = effectName;

        protected override void ChangeGameState(GameState gameState)
        {
        }
    }
}