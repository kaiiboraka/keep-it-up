namespace Elythia
{
    public enum StatType
    {
        Life,
        LifePerSecond,
        Mana,
        ManaPerSecond,
        Armor,
        MoveSpeed,
        PickupRadius,
        CritChance,
        ProcChance,
        ManaCost,
        Duration,
        FireResist,
        IronResist,
        EarthResist,
        WoodResist,
        WaterResist,
        IceResist,
        WindResist,
        LightningResist,
        SunResist,
        MoonResist,
        LightResist,
        ShadowResist,
        LifeResist,
        DeathResist,
        ArcaneResist,
        MinDamage,
        MaxDamage,
        MaxLife,
        MaxMana,
        SpellSize,
        InertResist,
        LifePercentRegen,
        ManaPercentRegen,
        JumpHeight,
        JumpDistance,
        JumpMidairCount,
        JackpotBonus,
        ItemDropBonus,
        SuperEnergy,
        SuperPerSecond,
        Money,
        ChargeAttackTime,
        Stagger,
        MaxSuperEnergy,
        SuperPercentRegen
    }

    public enum BubbleType
    {
        Life = StatType.Life,
        Mana = StatType.Mana,
        Super = StatType.SuperEnergy,
        Money = StatType.Money
    }

    public enum ParameterType
    {
        Life = StatType.Life,
        Mana = StatType.Mana,
        Super = StatType.SuperEnergy,
    }
}