using System;
using _Core.Scripts.Battle;
using _Core.Scripts.UI;
using DG.Tweening;
using UnityEngine;


namespace _Core.Scripts.Effects
{
    public class Effect : MonoBehaviour
    {
        protected Effects _effect;
        protected TypeEffect _typeEffect;
        protected EffectSign _effectSign;
        protected CanvasEffectName _canvasEffectName;
        protected DefaultEffectSettings _defaultEffectSettings;
        protected MeshRenderer _meshRenderer;
        protected int _attackDamage;
        protected int _healEffectAmount;
        protected int _poisonEffectHpAmount;
        protected int _poisonEffectStepAmount;
        protected int _defenceEffectHpAmount;
        protected int _defenceEffectStepAmount;
        protected GameObject _selfUnit;

        private Vector3 _screenPoint;
        private Vector3 _offset;
        private Vector3 _startPosition;
        private Camera _mainCamera;
        private BattleManager _battleManager;
        public void SetTypeEffect(TypeEffect type) => _typeEffect = type;
        public TypeEffect GetTypeEffect() => _typeEffect;
        public Effects GetEffect() => _effect;
        public void SetSelfUnit(GameObject unit) => _selfUnit = unit;
        public GameObject GetSelfUnit() => _selfUnit;

        protected void DestroyEffect()
        {
            _battleManager.DestroyUnitEffect(this);
        }

        private void Awake()
        {
            _mainCamera = Camera.main;
            _defaultEffectSettings = Resources.Load<DefaultEffectSettings>("DefaultEffectSettings");
            _attackDamage = _defaultEffectSettings.GetAttackDamage();
            _healEffectAmount = _defaultEffectSettings.GetHealEffectAmount();
            _poisonEffectHpAmount = _defaultEffectSettings.GetPoisonEffectHpAmount();
            _poisonEffectStepAmount = _defaultEffectSettings.GetPoisonEffectStepAmount();
            _defenceEffectHpAmount = _defaultEffectSettings.GetDefenceEffectHpAmount();
            _defenceEffectStepAmount = _defaultEffectSettings.GetDefenceEffectStepAmount();
            _battleManager = FindObjectOfType<BattleManager>();
            _meshRenderer = GetComponent<MeshRenderer>();
            if (!GetComponentInChildren<CanvasEffectName>())
                throw new Exception("No effect name script");
            _canvasEffectName = GetComponentInChildren<CanvasEffectName>();
        }

        private void OnMouseDown()
        {
            if (_typeEffect == TypeEffect.Enemy)
                return;
            _screenPoint = _mainCamera.WorldToScreenPoint(transform.position);
            _startPosition = transform.position;
            _offset = _startPosition - _mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, _screenPoint.z));
        }

        private void OnMouseUp()
        {
            if (_typeEffect == TypeEffect.Enemy)
                return;
            transform.DOMove(_startPosition, 0.3f);
        }

        private void OnMouseDrag()
        {
            if (_typeEffect == TypeEffect.Enemy)
                return;
            var curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);
            var curPosition = _mainCamera.ScreenToWorldPoint(curScreenPoint) + _offset;
            transform.position = curPosition;
        }
    }
}