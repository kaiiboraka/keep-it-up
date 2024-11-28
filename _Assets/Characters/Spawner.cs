using Godot;
using System;

public partial class Spawner : Node2D
{
	[Export] public PackedScene SceneToSpawn;
	[Export] public int SpawnTime = 5;
	[Export] public int SpawnCount = 2;
	
	private Timer spawnTimer;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		spawnTimer = GetNode<Timer>("SpawnTimer");
		spawnTimer.WaitTime = SpawnTime;
		spawnTimer.Timeout += SpawnScene;
		SpawnScene();
		spawnTimer.Start();
	}

	private void SpawnScene()
	{
		if (SceneToSpawn == null) return;
		Node instance = SceneToSpawn.Instantiate();
		AddChild(instance);
	}
}
