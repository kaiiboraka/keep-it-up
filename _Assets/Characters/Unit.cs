using Godot;
using System;

public partial class Unit : CharacterBody2D
{
    [Export] int health = 100;
    [Export] int damage = 10;
    [Export] int speed = 10;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
