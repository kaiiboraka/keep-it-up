using Godot;

public partial class RangedUnitData : UnitData
{
    [Export(PropertyHint.ResourceType)] public PackedScene ProjectilePrefab;
    [Export(PropertyHint.ResourceType)] public CollisionData ProjectileCollisionData;    
    // [ExportCategory("Collision")]
    // [ExportGroup("Projectile Collision")]
    // [Export(PropertyHint.Layers2DPhysics)] public uint ProjectileCollisionLayer = 0;
    // [Export(PropertyHint.Layers2DPhysics)] public uint ProjectileCollisionMask = 0;
}