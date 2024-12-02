using Godot;
using System;

public partial class Arrow : CharacterBody2D
{
    AnimatedSprite2D sprite;
    private Area2D attackHitbox;
    [Export] float moveSpeed = 0.2f;
    [Export] float despawnTime = 2f;
    private Timer despawnTimer;
    public int damage;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        attackHitbox = GetNode<Area2D>("Area2D");
        despawnTimer = GetNode<Timer>("DespawnTimer");
        despawnTimer.WaitTime = despawnTime;
        despawnTimer.OneShot = true;
        despawnTimer.Timeout += QueueFree;
        
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

        if (IsOnFloor())
        {
            Despawn();
            Velocity = Vector2.Zero;
        }
        else
        {
            Move(delta);
        }
    }

    public virtual void Move(double delta)
    {
        var velocity = Velocity;

        // velocity.X = moveSpeed * -GlobalPosition.Sign().X;
        velocity.Y += 9.8f;
        if (IsOnFloor())
        {
            velocity.Y = 0;
        }

        SetVelocity(velocity);
        MoveAndSlide();
    }

    private void DecideFrame()
    {
        if (despawnTimer.IsStopped())
        {
            var degrees = 90 + Mathf.RadToDeg(Velocity.Angle());
            sprite.Frame = (degrees / (180 / 25.0)).FloorToInt();
        }
    }

    public virtual void Attack(Node2D body)
    {
        if (body.HasMethod("TakeDamage"))
        {
            body.Call("TakeDamage", damage);
        }

        if (body is Building building)
        {
            building.TakeDamage(damage);
        }

        if (body is Unit { isHostile: false } unit)
        {
            unit.TakeDamage(damage);
        }

        var currFrame = sprite.Frame;
        Velocity = Vector2.Zero;
        sprite.Frame = currFrame;
        Reparent(GetParent().GetParent());
        Despawn();
    }

    private void Despawn()
    {
        despawnTimer.Start();
        attackHitbox.Monitoring = false;
        
        // attackHitbox.GetNode<CollisionShape2D>("AttackHitbox").Disabled = true;
    }
}