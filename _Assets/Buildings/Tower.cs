using Godot;
using System;
using System.Diagnostics;

public partial class Tower : Node2D
{
	[Export] public int Gold = 500;
	
	public static Tower Instance; 
	
	[Export] private DebugHUD DebugHUD;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (Instance != null)
		{
			QueueFree();
			return;
		}
		Instance = this;
		
		UpdateHUDValues();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
		UpdateHUDValues();
	}
	
	
	 private void UpdateHUDValues()
    {
        Debug.Assert(DebugHUD != null, "HUD cannot be null - inspector");

        DebugHUD.UpdateProperty("Gold", Gold);


        // DebugHUD.UpdateProperty("~~~~~", "~~~~~~~~~~");
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
