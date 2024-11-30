using Godot;
using System.Diagnostics;
using Godot.Collections;

public partial class DebugHUD : Control
{
	private Vector2 game_size = new Vector2(
		(float)ProjectSettings.GetSetting("display/window/size/width"),
		(float)ProjectSettings.GetSetting("display/window/size/height")
	);
    
	[Export] private PackedScene PropertyEntryScene;
	private Dictionary<string, string> HUDProperties = new();
	private Array<PropertyEntry> entries = new();

	[Signal] public delegate void PropertyChangedEventHandler(string which, string value);
	
	private VBoxContainer PropertyList;

	private Viewport viewport;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		viewport = GetViewport();
		// viewport.SizeChanged += CenterGame;

		HUDProperties ??= new();

		PropertyList = GetNode<VBoxContainer>("%PropertyList");
		foreach (var child in PropertyList.GetChildren())
		{
			child.QueueFree();
		}

		entries = new();
		foreach (var (label, value) in HUDProperties)
		{
			Debug.Assert(PropertyEntryScene != null, "PropertyEntryScene cannot be null");
			var propertyEntry = PropertyEntryScene.Instantiate<PropertyEntry>();
			
			PropertyList.AddChild(propertyEntry);
			propertyEntry.Owner = this;
			
			propertyEntry.PropertyText = label;
			propertyEntry.ValueText = value;
			
			PropertyChanged += propertyEntry.UpdateValueText;
			entries.Add(propertyEntry);
		}
	}

	public void CenterGame()
	{
		var game_offset = (viewport.GetVisibleRect().Size - game_size) / 2f;

		viewport.CanvasTransform = viewport.CanvasTransform with { Origin = game_offset };
	}

	public void UpdateProperty<T>(string which, T value)
	{
		string propertyValue = value.ToString();
		HUDProperties[which] = propertyValue;
		EmitSignal(SignalName.PropertyChanged, which, propertyValue);
	}
}
