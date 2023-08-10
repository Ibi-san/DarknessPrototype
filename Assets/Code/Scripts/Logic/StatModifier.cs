using System;

namespace Code.Scripts.Logic
{
    [Serializable]
    public class StatModifier
    {
        public readonly int Value;
        public readonly IModifier Source;

        public StatModifier(int value, IModifier source)
        {
            Value = value;
            Source = source;
        }
    }
}