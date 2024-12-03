using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;

// [Tool]
public partial class Unit : CharacterBody2D
{
    [Export] public UnitData Data;
    
    protected int healthCurrent = 100;
    protected int healthMax = 100;
    protected int speedMax = 10;
    protected int damage = 10;
    protected float attackDelay = 1f; // 1 for melee, 2 for ranged

    protected AnimatedSprite2D characterSprite;
    protected AnimationPlayer animationPlayer;
    protected string animationLibrary;
    protected Area2D attackArea;
    protected Area2D detectionArea;
    protected Timer attackTimer;
    protected int speedCurrent;

    public bool isHostile = false;

    HashSet<Node> hitTargets = new();
    // Called when the node enters the scene tree for the first time.
    
    // [ExportToolButton("Ready")]
    // public Callable ReadyButton => Callable.From(_Ready);
    //
    public override void _Ready()
    {
        Debug.Assert(Data != null, "UnitData cannot be null");
        
        characterSprite = GetNode<AnimatedSprite2D>("CharacterSprite");
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        attackArea = GetNode<Area2D>("AttackArea");
        detectionArea = GetNode<Area2D>("DetectionArea");
        attackTimer = GetNode<Timer>("AttackDelay");

        Name = Data.UnitName;
        isHostile = Data.isHostile;
        healthMax = Data.HealthMax;
        speedMax = Data.SpeedMax;
        damage = Data.Damage;
        attackDelay = Data.AttackDelay;
        attackTimer.WaitTime = attackDelay;
        
        characterSprite.SpriteFrames = Data.CharacterSprites;
        characterSprite.Frame = 0;
        characterSprite.SpeedScale = 1.0f;
        
        ((CollisionShape2D)attackArea.GetChild(0)).Shape = Data.AttackHitbox;
        ((CollisionShape2D)attackArea.GetChild(0)).Position += Data.AttackHitboxOffset;
        
        ((CollisionShape2D)detectionArea.GetChild(0)).Shape = Data.DetectionRadius;
        ((CollisionShape2D)detectionArea.GetChild(0)).Position = Data.DetectionRadiusOffset;
        
        CollisionLayer = Data.CharacterCollision.Layer;
        CollisionMask = Data.CharacterCollision.Mask;
        
        attackArea.CollisionLayer = Data.AttackCollision.Layer;
        attackArea.CollisionMask = Data.AttackCollision.Mask;
        detectionArea.CollisionLayer = Data.AttackCollision.Layer;
        detectionArea.CollisionMask = Data.AttackCollision.Mask;

        MotionMode = Data.MotionMode;
        
        healthCurrent = healthMax;
        speedCurrent = speedMax;

        // Scale = GetGlobalPosition().X > 0 ? Scale.FaceRight() : Scale.FaceLeft();
        var boolMatrix = (GlobalPosition.X < 0).Int() + isHostile.Int();
        if (boolMatrix.IsEven())
        {
            this.FaceLeft();
        }
        else
        {
            this.FaceRight();
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if (detectionArea.HasOverlappingBodies() && attackTimer.IsStopped())
        { 
            StartAttacking();
        }
        Move(delta);
    }

    public virtual void Move(double delta)
    {
        var velocity = Velocity;
        velocity.X = speedCurrent * GlobalPosition.Sign().X * (isHostile ? -1 : 1);
        velocity.Y = MotionMode switch
        {
            MotionModeEnum.Grounded => velocity.Y + 9.8f,
            MotionModeEnum.Floating => (float)Mathf.Sin(GlobalPosition.X * Math.PI / 180 * 20) * 50,
            _ => velocity.Y
        };
        if (IsOnFloor())
        {
            velocity.Y = 0;
        }
        
        SetVelocity(velocity);
        MoveAndSlide();
        // OnDamage(1);
    }

    public virtual void StartAttacking()
    {
        animationPlayer.Play("attack");
        speedCurrent = 0;
        hitTargets = new();
    }
    
    public virtual void Attack()
    {
        foreach(var target in attackArea.GetOverlappingBodies())
        {
            if (hitTargets.Contains(target)) continue;
            
            if (target.HasMethod("TakeDamage"))
            {
                target.Call("TakeDamage", damage);
                hitTargets.Add(target);
                continue;
            }

            if (target is Building building)
            {
                building.TakeDamage(damage);
                hitTargets.Add(target);
                continue;
            }

            if (target is Unit { isHostile: false } unit)
            {
                unit.TakeDamage(damage);
                hitTargets.Add(target);
                continue;
            }
        }
    }

    public virtual void OnDeath()
    {
        QueueFree();
    }

    public virtual void TakeDamage(int damage)
    {
        healthCurrent -= damage;
        if (healthCurrent <= 0) OnDeath();
    }

    public void ResetSpeed()
    {
        animationPlayer.Play("move");
        speedCurrent = speedMax;
        attackTimer.Start();
    }

}
