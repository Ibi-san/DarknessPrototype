using System;

namespace Code.Scripts.Logic
{
    [Serializable]
    public class StatModifier
    {
        public readonly int Value;
        public readonly object Source;

        public StatModifier(int value, object source)
        {
            Value = value;
            Source = source;
        }
    }
}