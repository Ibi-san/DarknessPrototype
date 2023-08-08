using System;

namespace Code.Scripts.Logic
{
    [Serializable]
    public class StatModifier
    {
        public readonly int Value;
        public readonly IStat Source;

        public StatModifier(int value, IStat source)
        {
            Value = value;
            Source = source;
        }
    }
}