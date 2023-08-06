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
    }
}