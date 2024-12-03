using Godot;
using System;

public partial class UnitData : Resource
{
    [Export] public string UnitName = "";
    [Export(PropertyHint.Enum)] public UnitType UnitType;
    [Export] public SpriteFrames CharacterSprites;
    [Export] public bool isHostile = false;
    
    [Export] public int HealthMax = 100;
    [Export] public int SpeedMax = 10;
    [Export(PropertyHint.Enum)] public CharacterBody2D.MotionModeEnum MotionMode = CharacterBody2D.MotionModeEnum.Grounded;
    [Export(PropertyHint.Enum)] public AttackRange AttackRange;
    [Export] public int Damage = 10;
    [Export] public float AttackDelay = 2f;
    [Export] public Shape2D AttackHitbox;
    [Export] public Vector2 AttackHitboxOffset;
    [Export] public CircleShape2D DetectionRadius;
    [Export] public Vector2 DetectionRadiusOffset;

    [ExportCategory("Collision")]
    [Export(PropertyHint.ResourceType)] public CollisionData CharacterCollision;
    [Export(PropertyHint.ResourceType)] public CollisionData AttackCollision;
    // [ExportGroup("Character Collision")]
    // [Export(PropertyHint.Layers2DPhysics)] public uint CollisionLayer = 0;
    // [Export(PropertyHint.Layers2DPhysics)] public uint CollisionMask = 0;
    
    // [ExportGroup("Attack Collision")]
    // [Export(PropertyHint.Layers2DPhysics)] public uint AttackCollisionLayer = 0;
    // [Export(PropertyHint.Layers2DPhysics)] public uint AttackCollisionMask = 0;
}


public enum AttackRange
{
    Melee,
    Ranged,
}

public enum UnitType
{
    Soldier,
    Archer,
    Mage,
    Flying,
}