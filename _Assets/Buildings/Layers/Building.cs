using Godot;
using System;

public partial class Building : RigidBody2D
{
	int healthCurrent = 100;
	int healthMax = 100;
	int rank = 1;
	Sprite2D sprite_rank1;
	Sprite2D sprite_rank2;
	Sprite2D sprite_rank3;
	Sprite2D sprite_destroyed;
	Sprite2D sprite_construction;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sprite_rank1 = GetNode<Sprite2D>("sprite_rank1");
		sprite_rank2 = GetNode<Sprite2D>("sprite_rank2");
		sprite_rank3 = GetNode<Sprite2D>("sprite_rank3");
		sprite_destroyed = GetNode<Sprite2D>("sprite_destroyed");
		sprite_construction = GetNode<Sprite2D>("sprite_construction");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void ReceiveDamage(Rid rid, Node body, int bodyShapeIndex, int localShapeIndex)
	{
		GD.Print("Tower received damage");
		GD.Print("localShapeIndex: " + localShapeIndex);
	}
}
