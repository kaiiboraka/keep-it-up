using Godot;
using System;

public partial class PropertyEntry : HBoxContainer
{

	public string PropertyText
	{
		get => PropertyLabel.Text;
		set => PropertyLabel.Text = value;
	}
	public string ValueText
	{
		get => ValueLabel.Text;
		set => ValueLabel.Text = value;
	}
	
	private Label PropertyLabel;
	private Label ValueLabel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PropertyLabel = GetNode<Label>("%PropertyLabel");
		ValueLabel = GetNode<Label>("%ValueLabel");
	}

	public void UpdateValueText(string which, string value)
	{
		if (which != PropertyText) return;
		ValueText = value;
	}

}
