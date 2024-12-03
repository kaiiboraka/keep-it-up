using Godot;
using System;
using System.Collections.Generic;

public partial class Spawner : Node2D
{
    [Export(PropertyHint.Enum)] public UnitType UnitToSpawn;
    [Export] public bool isHostile;
    
     private PackedScene sceneToSpawn;
     private UnitData currentUnitData;
    [Export] public int SpawnCount = 2;
    [Export] public float SpawnTime = 5;
    [Export] private float spawnDelay = .33f;

    private Timer spawnTimer;
    private Timer delayTimer;

    protected Dictionary<UnitType, Tuple<PackedScene, UnitData>> Units = new();
    private bool dictionarySetup = false;
    
    private void SetupDictionary()
    {
        if (dictionarySetup) return;
        Units.Clear();
        Units = new();
        PackedScene UnitScene;
        UnitData UnitData;

        foreach (UnitType type in Enum.GetValues(typeof(UnitType)))
        {
            switch (type)
            {
                case UnitType.Soldier:
                    UnitScene = GD.Load<PackedScene>("res://_Assets/Characters/Melee/Unit.tscn");
                    UnitData = isHostile
                        ? GD.Load<UnitData>("res://_Assets/Characters/Melee/melee_enemy_Data.tres")
                        : GD.Load<UnitData>("res://_Assets/Characters/Melee/melee_ally_Data.tres");
                    break;
                case UnitType.Archer:
                    UnitScene = GD.Load<PackedScene>("res://_Assets/Characters/Archer/Unit_Ranged.tscn");
                    UnitData = isHostile
                        ? GD.Load<UnitData>("res://_Assets/Characters/Archer/archer_enemy_Data.tres")
                        : GD.Load<UnitData>("res://_Assets/Characters/Archer/archer_ally_Data.tres");
                    break;
                case UnitType.Mage:
                    UnitScene = GD.Load<PackedScene>("res://_Assets/Characters/Magic/Unit_Magic.tscn");
                    UnitData = isHostile
                        ? GD.Load<UnitData>("res://_Assets/Characters/Magic/magic_enemy_Data.tres")
                        : GD.Load<UnitData>("res://_Assets/Characters/Magic/magic_ally_Data.tres");
                    ;
                    break;
                case UnitType.Flying:
                    UnitScene = GD.Load<PackedScene>("res://_Assets/Characters/Flying/Unit_Flying.tscn");
                    UnitData = isHostile
                        ? GD.Load<UnitData>("res://_Assets/Characters/Flying/flying_enemy_Data.tres")
                        : GD.Load<UnitData>("res://_Assets/Characters/Flying/flying_ally_Data.tres");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Units.Add(type, new Tuple<PackedScene, UnitData>(UnitScene, UnitData));
        }
        dictionarySetup = true;
    }
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        if (!dictionarySetup) SetupDictionary();
        
        spawnTimer = GetNode<Timer>("SpawnTimer");
        delayTimer = GetNode<Timer>("DelayTimer");
        spawnTimer.WaitTime = SpawnTime;
        spawnTimer.Timeout += SpawnScene;

        sceneToSpawn = Units[UnitToSpawn].Item1;
        currentUnitData = Units[UnitToSpawn].Item2;
        
        SpawnScene();
        spawnTimer.Start();
    }

    private void SpawnScene()
    {
        SpawnScene(UnitToSpawn);
    }

    public void SpawnScene(UnitType unit)
    {
        if (unit == null) return;
        if (SpawnCount == 0) return;

        if (SpawnCount == 1)
        {
            InstantiateScene(Units[unit].Item2);
        }
        else
        {
            InstantiateScenes(Units[unit].Item2);
        }
    }

    private void InstantiateScene(UnitData unitToSpawn)
    {
        Node instance = sceneToSpawn.Instantiate();

        if (instance is Unit unit)
        {
            unit.Data = unitToSpawn;
            // switch (unitToSpawn.UnitType)
            // {
            //     case UnitType.Soldier:
            //         unit.SetScript(GD.Load("res://_Assets/Characters/Unit.cs"));
            //         break;
            //     case UnitType.Archer:
            //         unit.SetScript(GD.Load("res://_Assets/Characters/RangedUnit.cs"));
            //         break;
            //     case UnitType.Mage:
            //         unit.SetScript(GD.Load("res://_Assets/Characters/RangedUnit.cs"));
            //         break;
            //     case UnitType.Flying:
            //         unit.SetScript(GD.Load("res://_Assets/Characters/FlyingUnit.cs"));
            //         break;
            //     default:
            //         throw new ArgumentOutOfRangeException();
            // }
        }

        AddChild(instance);
    }

    private async void InstantiateScenes(UnitData unit)
    {
        delayTimer.Start();
        for (int i = 0; i < SpawnCount; i++)
        {
            InstantiateScene(unit);
            await ToSignal(delayTimer, "timeout");
        }
    }
}