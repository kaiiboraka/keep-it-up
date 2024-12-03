using Godot;

public partial class CollisionData : Resource
{
    [ExportCategory("Collision")]
    [Export(PropertyHint.Layers2DPhysics)] public uint Layer = 0;
    [Export(PropertyHint.Layers2DPhysics)] public uint Mask = 0;
}