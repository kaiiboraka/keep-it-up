using Godot;
using System;
using System.Diagnostics;
using Godot.Collections;

public partial class Building : RigidBody2D
{
	[Export] protected string Name = "TowerBuilding";
	[ExportCategory("Stats")]
	[Export] protected int healthMax = 100;
	protected int healthCurrent = 100;
	[Export] protected int rankMax = 3;
	protected int rankCurrent = 1;
	[Export] private int baseConstructionTime = 5;
	protected int constructionTime = 5;
	[Export] private int baseUpgradeCost = 100;
	protected int upgradeCost = 100;
	
	[ExportCategory("Components")]
	[Export] protected Array<Texture2D> upgradeIconSprites;
	protected AnimatedSprite2D sprite;
	protected Button upgradeButton;
	protected Label upgradeCostLabel;
	protected Timer constructionTimer;
	[Export] protected DebugHUD DebugHUD;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sprite = GetNode<AnimatedSprite2D>("BuildingSprite");
		upgradeCostLabel = GetNode<Label>("%UpgradeButtonLabel");
		constructionTimer = GetNode<Timer>("ConstructionTimer");
		constructionTimer.WaitTime = baseConstructionTime;
		constructionTimer.OneShot = true;
		
		UpdateUpgradeCost();
	}


	public override void _Process(double delta)
	{
		UpdateHUDValues();
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

	protected virtual void UpdateUpgradeCost()
	{
		upgradeCost = (1+rankCurrent) * baseUpgradeCost; 
		upgradeCostLabel.Text = upgradeCost.ToString() + " g";
	}
	
	public void UpgradeTier()
	{
		if (Tower.Instance.Gold < upgradeCost)
		{
			GD.Print("Not enough gold: UPGRADE");
			return;
		}
		if (rankCurrent >= rankMax)
		{
			GD.Print("Building is MAX rank");
			return;
		}
		
		Tower.Instance.Gold -= upgradeCost;
		rankCurrent++;
		constructionTimer.Timeout += ChangeTier;
		constructionTimer.Start();
		sprite.Play("Construction");
	}

	public void ChangeTier()
	{
		var animationToPlay = rankCurrent switch
		{
			0 => "Destroyed",
			1 => "Rank1",
			2 => "Rank2",
			3 => "Rank3",
			4 when rankMax == 5 => "Rank4",
			5 when rankMax == 5 => "Rank5",
			_ => "Construction",
		};
		sprite.Play(animationToPlay);
		upgradeButton.Icon = upgradeIconSprites[rankCurrent];
		UpdateUpgradeCost();
	}
	
	protected virtual void UpdateHUDValues()
	{
		Debug.Assert(DebugHUD != null, "HUD cannot be null - inspector");
		DebugHUD.UpdateProperty("~~~~~", "~~~~~~~~~~");

		DebugHUD.UpdateProperty(Name+" Current Rank", rankCurrent.ToString());
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
