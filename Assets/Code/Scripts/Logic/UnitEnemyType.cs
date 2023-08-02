using System;

public enum UnitEnemyType
{
    Small = UnitEnemyTypeFlags.Small,
    Medium = UnitEnemyTypeFlags.Medium,
    Big = UnitEnemyTypeFlags.Big,
    Boss = UnitEnemyTypeFlags.Boss
}

[Flags]
public enum UnitEnemyTypeFlags
{
    None = 0,
    Small = 1,
    Medium = 2,
    Big = 4,
    Boss = 8
}
