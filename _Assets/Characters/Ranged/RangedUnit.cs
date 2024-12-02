using Godot;
using System;

public partial class RangedUnit : Unit
{
	Area2D detectionArea;
	[Export] bool isAttacking = false;
	float distanceToTarget = Single.PositiveInfinity;
	Node2D currentTarget;

	[Export] private PackedScene arrow;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		detectionArea = GetNode<Area2D>("DetectionArea");
	}


	public override void _PhysicsProcess(double delta)
	{
		if (!isAttacking) Move(delta);
		if (detectionArea.HasOverlappingBodies() && !isAttacking && attackTimer.IsStopped())
		{
			StartAttacking();
		}
	}

	public void AimAtTarget(Node2D body)
	{
		// StartAttacking();
		var bodyPosition = body.Position;
		var distanceToBody = bodyPosition.DistanceTo(GlobalPosition);
		if (distanceToBody < distanceToTarget)
		{
			currentTarget = body;
		}
		
		var aimDirection = bodyPosition - GlobalPosition;

		var h = aimDirection[0];
		var k = aimDirection[1];
		var x = GlobalPosition.X;
		var y = GlobalPosition.Y;
		var a = (y - k) / Mathf.Pow((x - h), 2);
		var s = 2 * a * (x - h);
		var b = -s * x + y;
		var e = s * h + b;
		
		Vector2 direction = new Vector2((float)h, e) - GlobalPosition;
		Node newArrow = arrow.Instantiate();
		AddSibling(newArrow);
		if (newArrow is Arrow arrowInstance)
		{
			arrowInstance.Velocity = direction;
		}
	}
	
}
