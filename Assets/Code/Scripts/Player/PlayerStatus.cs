using System;
using System.Linq;
using Code.Scripts.Logic;
using UnityEngine;

namespace Code.Scripts.Player
{
    public class PlayerStatus : Unit
    {
        [Header("Set In Inspector")]
        [SerializeField] private UnitHealth _unitHealth;
        [SerializeField] private UnitAttack _unitAttack;
        [SerializeField] private PlayerInsanity _playerInsanity;

        public UnitHealth PlayerHealth => _unitHealth;
        public UnitAttack PlayerAttack => _unitAttack;
        public PlayerInsanity PlayerInsanity => _playerInsanity;

        private void OnEnable()
        {
            _playerInsanity.OnInsanityChanged += InsanityBasedStatsChange;
        }

        private void InsanityBasedStatsChange(float insanityValue)
        {
            if (insanityValue > 50 && !_unitAttack.Modifiers.Any(x => Equals(x.Source, PlayerInsanity)))
            {
                var insanityStatModifier = new StatModifier(1, PlayerInsanity);
                _unitAttack.AddModifier(insanityStatModifier);
            }
            else if (insanityValue < 50 && _unitAttack.Modifiers.Any(x => Equals(x.Source, PlayerInsanity)))
            {
                _unitAttack.RemoveModifier(typeof(PlayerInsanity));
            }
        }

        private void OnDisable()
        {
            _playerInsanity.OnInsanityChanged -= InsanityBasedStatsChange;
        }
    }
}