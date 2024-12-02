using Godot;
using System;
using System.Collections.Generic;

public partial class Unit : CharacterBody2D
{
    [Export] protected int healthCurrent = 100;
    [Export] protected int healthMax = 100;
    [Export] protected int speedMax = 10;
    [Export] protected int damage = 10;
    [Export] protected float attackDelay = 2f;

    protected RayCast2D lookahead;
    protected AnimationPlayer animationPlayer;
    protected Area2D attackHitbox;
    protected Timer attackTimer;
    protected int speedCurrent;

    [Export] public bool isHostile = false;

    HashSet<Node> hitTargets = new();
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        attackHitbox = GetNode<Area2D>("Area2D");
        lookahead = GetNode<RayCast2D>("RayCast2D");
        attackTimer = GetNode<Timer>("AttackDelay");
        attackTimer.WaitTime = attackDelay;
        
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
        if (lookahead.IsColliding() && attackTimer.IsStopped())
        { 
            StartAttacking();
        }
        Move(delta);
    }

    public virtual void Move(double delta)
    {
        var velocity = Velocity;
        velocity.X = speedCurrent * GlobalPosition.Sign().X * (isHostile ? -1 : 1);
        if (MotionMode == MotionModeEnum.Grounded)
        {
            velocity.Y += 9.8f;
        }
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
        foreach(var target in attackHitbox.GetOverlappingBodies())
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
