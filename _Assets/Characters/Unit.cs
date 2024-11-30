using Godot;
using System;

public partial class Unit : CharacterBody2D
{
    [Export] int healthCurrent = 100;
    [Export] int healthMax = 100;
    [Export] int speedMax = 10;
    [Export] int damage = 10;
    [Export] float attackDelay = 2f;

    RayCast2D lookahead;
    AnimationPlayer animationPlayer;
    Area2D attackHitbox;
    Timer attackTimer;
    int speedCurrent;

    [Export] public bool isHostile = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        attackHitbox = GetNode<Area2D>("Area2D");
        lookahead = GetNode<RayCast2D>("RayCast2D");
        attackTimer = GetNode<Timer>("AttackDelay");
        
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
        velocity.X = speedCurrent * -GlobalPosition.Sign().X;
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
    }
    
    public virtual void Attack()
    {
        foreach(var target in attackHitbox.GetOverlappingBodies())
        {
            if (target.HasMethod("TakeDamage"))
            {
                target.Call("TakeDamage", damage);
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
