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
			var targets = detectionArea.GetOverlappingBodies();
			foreach (var target in targets)
			{
				AimAtTarget(target);
			}
			StartAttacking();
		}
	}

	public void AimAtTarget(Node2D body)
	{
		// StartAttacking();
		var distanceToBody = body.GlobalPosition.DistanceTo(GlobalPosition);
		if (distanceToBody < distanceToTarget)
		{
			currentTarget = body;
		}
	}

	private void FireArrow()
	{
		var aimDirection = currentTarget.GlobalPosition - GlobalPosition;

		var h = currentTarget.GlobalPosition[0];
		var k = currentTarget.GlobalPosition[1];
		var x = GlobalPosition.X;
		var y = GlobalPosition.Y;
		var a = (y - k) / Mathf.Pow((x - h), 2);
		var s = 2 * a * (x - h);
		var b = -s * x + y;
		var e = s * h + b;
		
		Vector2 direction = GlobalPosition.DirectionTo(new Vector2(h, e));
		Node newArrow = arrow.Instantiate();
		AddChild(newArrow);
		if (newArrow is Arrow arrowInstance)
		{
			arrowInstance.Velocity = direction * e;
			arrowInstance.damage = this.damage;
		}
	}
}
