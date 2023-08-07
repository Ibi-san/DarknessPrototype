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
            _unitHealth.OnHealthChanged += HealthBasedStatsChange;
        }

        private void InsanityBasedStatsChange(float insanityValue)
        {
            if (insanityValue > 50 && !_unitAttack.Modifiers.Any(modifier => modifier.Source is PlayerInsanity))
            {
                var damageModifier = new StatModifier(1, source: PlayerInsanity);
                _unitAttack.AddModifier(damageModifier);
            }
            else if (insanityValue <= 50 && _unitAttack.Modifiers.Any(modifier => modifier.Source is PlayerInsanity))
            {
                _unitAttack.RemoveModifier(PlayerInsanity);
            }
        }

#if UNITY_EDITOR //Damage modificator test based on Player health
        private void HealthBasedStatsChange(int healthValue)
        {
            if (healthValue <= 10 && !_unitAttack.Modifiers.Any(modifier => modifier.Source is UnitHealth))
            {
                var damageModifier = new StatModifier(1, source: PlayerHealth);
                _unitAttack.AddModifier(damageModifier);
            }
            else if (healthValue > 10 && _unitAttack.Modifiers.Any(modifier => modifier.Source is UnitHealth))
            {
                _unitAttack.RemoveModifier(PlayerHealth);
            }
        }
#endif
        

        private void OnDisable()
        {
            _playerInsanity.OnInsanityChanged -= InsanityBasedStatsChange;
            _unitHealth.OnHealthChanged -= HealthBasedStatsChange;
        }
    }
}