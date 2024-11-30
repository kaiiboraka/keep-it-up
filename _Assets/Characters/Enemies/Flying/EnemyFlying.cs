using Godot;
using System;

public partial class EnemyFlying : Unit
{
    [Export] public PackedScene SceneToSpawn;
    public override void OnDeath()
    {
        base.OnDeath();
        if (SceneToSpawn == null) return;
        Node instance = SceneToSpawn.Instantiate();
        AddSibling(instance);
    }

    public override void Move(double delta)
    {
        var velocity = Velocity;
        
        velocity.Y = (float)Mathf.Sin(GlobalPosition.X * Math.PI / 180 * 20 ) * 50;
        
        SetVelocity(velocity);
        
        base.Move(delta);
        
    }
}
