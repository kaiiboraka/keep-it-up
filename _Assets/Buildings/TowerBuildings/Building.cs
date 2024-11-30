using Godot;
using System;

public partial class Building : RigidBody2D
{
	[Export] int healthCurrent = 100;
	[Export] int healthMax = 100;
	int rank = 1;
	Sprite2D sprite_rank1;
	Sprite2D sprite_rank2;
	Sprite2D sprite_rank3;
	Sprite2D sprite_destroyed;
	Sprite2D sprite_construction;

	[Export] private int UnitCost = 50;

	private Spawner rightSpawner;
	private Spawner leftSpawner;
	private Label rightSpawnerLabel;
	private Label leftSpawnerLabel;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sprite_rank1 = GetNode<Sprite2D>("sprite_rank1");
		sprite_rank2 = GetNode<Sprite2D>("sprite_rank2");
		sprite_rank3 = GetNode<Sprite2D>("sprite_rank3");
		sprite_destroyed = GetNode<Sprite2D>("sprite_destroyed");
		sprite_construction = GetNode<Sprite2D>("sprite_construction");
		
		rightSpawner = GetNode<Spawner>("SpawnerRight");
		leftSpawner = GetNode<Spawner>("SpawnerLeft");
		
		rightSpawnerLabel = GetNode<Label>("%SpawnRightLabel");
		leftSpawnerLabel = GetNode<Label>("%SpawnLeftLabel");
		
		rightSpawnerLabel.Text = UnitCost.ToString() + " g";
		leftSpawnerLabel.Text = UnitCost.ToString() + " g";
	}
	
	

	public virtual void OnDeath()
	{
		GD.Print("Building death");
		CollisionLayer = CollisionLayer.WithoutLayer(3);
	}

	public virtual void TakeDamage(int damage)
	{
		healthCurrent -= damage;
		if (healthCurrent <= 0) OnDeath();
	}
}
