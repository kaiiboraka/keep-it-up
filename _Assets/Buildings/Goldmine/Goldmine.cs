using Godot;
using System;
using System.Diagnostics;

public partial class Goldmine : Building
{
	[Export] private float incomeDelay = 3f;
	[Export] private int goldPerInterval = 10;
	Timer incomeTimer;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		
		sprite.FlipH = GetGlobalPosition().X < 0;
		incomeTimer = GetNode<Timer>("IncomeTimer");
		incomeTimer.WaitTime = incomeDelay;
		incomeTimer.Timeout += AddIncome;
	}
	
	public override void _Process(double delta)
	{
		UpdateHUDValues();
	}

	public void AddIncome()
	{
		Tower.Instance.Gold += goldPerInterval;
	}

	protected override void UpdateHUDValues()
	{
		Debug.Assert(DebugHUD != null, "HUD cannot be null - inspector");

		DebugHUD.UpdateProperty("Name", Name);
		DebugHUD.UpdateProperty("Current Rank", rankCurrent.ToString());
		DebugHUD.UpdateProperty("~~~~~", "~~~~~~~~~~");
		//
		//
		//
		// DebugHUD.UpdateProperty("CrouchToggled", CrouchToggled);
		//
		// DebugHUD.UpdateProperty("~~~~~~~", "~~~~~~~~~~");
		//
		// DebugHUD.UpdateProperty("Animation", spriteAnimator.animator.CurrentAnimation);
	}
}
