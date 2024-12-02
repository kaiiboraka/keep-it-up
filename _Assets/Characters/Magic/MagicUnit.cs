using Godot;
using System;

public partial class MagicUnit : RangedUnit
{
    protected override void FireProjectile()
    {
        // base.FireProjectile();
        GD.Print("FireProjectile");
    }
}
