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
}
