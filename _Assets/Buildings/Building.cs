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
	protected AnimatedSprite2D sprite;
	protected TextureProgressBar healthBar;
	protected Button upgradeButton;
	protected Label upgradeCostLabel;
	protected AnimatedSprite2D upgradeButtonSprite;
	protected Timer constructionTimer;
	[Export] protected DebugHUD DebugHUD;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sprite = GetNode<AnimatedSprite2D>("BuildingSprite");
		healthBar = GetNode<TextureProgressBar>("%HealthBar");
		upgradeButton = GetNode<Button>("BuildingUI/UpgradeButton");
		upgradeCostLabel = GetNode<Label>("%UpgradeCostLabel");
		upgradeButtonSprite = GetNode<AnimatedSprite2D>("%UpgradeButtonSprite");
		constructionTimer = GetNode<Timer>("ConstructionTimer");
		constructionTimer.WaitTime = baseConstructionTime;
		constructionTimer.OneShot = true;
		
		healthBar.MaxValue = healthMax;
		healthBar.Value = healthCurrent;
		
		UpdateUpgradeCost();
	}


	public override void _Process(double delta)
	{
		healthBar.Value = healthCurrent;
		UpdateHUDValues();
	}

	public virtual void OnDeath()
	{
		GD.Print("Building death");
		CollisionLayer = CollisionLayer.WithoutLayer(3);
		QueueFree();
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
		
		constructionTimer.Timeout += ChangeTier;
		constructionTimer.Start();
		sprite.Play("Construction");
	}

	public void ChangeTier()
	{
		var nextRank = rankCurrent + 1;
		var animationToPlay = nextRank switch
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
		rankCurrent++;
		UpdateUpgradeCost();
		
		animationToPlay = nextRank switch
		{
			0 or 1 or 2 or 3 => rankMax + "Rank_" + nextRank,
			(4 or 5) when rankMax == 5 => rankMax + "Rank_" + nextRank,
			_ => rankMax + "Rank_" + 0,
		};
		upgradeButtonSprite.Play(animationToPlay);
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
