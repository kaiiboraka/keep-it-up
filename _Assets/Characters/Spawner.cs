using Godot;
using System;

public partial class Spawner : Node2D
{
    [Export] public PackedScene SceneToSpawn;
    [Export] public int SpawnCount = 2;
    [Export] public float SpawnTime = 5;
    [Export] private float spawnDelay = .33f;

    private Timer spawnTimer;
    private Timer delayTimer;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        spawnTimer = GetNode<Timer>("SpawnTimer");
        delayTimer = GetNode<Timer>("DelayTimer");
        spawnTimer.WaitTime = SpawnTime;
        spawnTimer.Timeout += SpawnScene;
        SpawnScene();
        spawnTimer.Start();
    }

    private void SpawnScene()
    {
        SpawnScene(SceneToSpawn, true);
    }

    public void SpawnScene(PackedScene scene, bool isHostile = true)
    {
        if (scene == null) return;
        if (SpawnCount == 0) return;

        if (SpawnCount == 1)
        {
            InstantiateScene(scene, isHostile);
        }
        else
        {
            InstantiateScenes(scene, isHostile);
        }
    }

    private void InstantiateScene(PackedScene scene, bool isHostile = true)
    {
        Node instance = scene.Instantiate();
        if (instance is Unit unit)
        {
            unit.isHostile = isHostile;
        }

        AddChild(instance);
    }

    private async void InstantiateScenes(PackedScene scene, bool isHostile = true)
    {
        delayTimer.Start();
        for (int i = 0; i < SpawnCount; i++)
        {
            InstantiateScene(scene);
            await ToSignal(delayTimer, "timeout");
        }
    }
}