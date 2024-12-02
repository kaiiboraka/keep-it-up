using Godot;
using System;

public partial class Arrow : CharacterBody2D
{
	AnimatedSprite2D sprite;
	[Export] float moveSpeed = 0.2f;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		MotionMode = MotionModeEnum.Floating;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		DecideFrame();
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		
		Move(delta);
	}
	
	public virtual void Move(double delta)
	{
		var velocity = Velocity;
		velocity.X = moveSpeed * -GlobalPosition.Sign().X;
		velocity.Y += 9.8f;
		if (IsOnFloor())
		{
			velocity.Y = 0;
		}
        
		SetVelocity(velocity);
		MoveAndSlide();
		// OnDamage(1);
	}

	private void DecideFrame()
	{
		var degrees = Mathf.RadToDeg(Velocity.Angle());
		sprite.Frame = (degrees / 180 / 25).FloorToInt();
	}
}
