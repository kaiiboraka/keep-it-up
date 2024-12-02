using Godot;

public partial class SpawningBuilding : Building
{
    [Export] private int UnitCost = 50;

    [Export] public PackedScene UnitToSpawn;
    
    private Button rightSpawnButton;
    private Button leftSpawnButton;
    private Spawner rightSpawner;
    private Spawner leftSpawner;
    private Label rightSpawnerLabel;
    private Label leftSpawnerLabel;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        rightSpawnButton = GetNode<Button>("%RightSpawnButton");
        leftSpawnButton = GetNode<Button>("%LeftSpawnButton");

        rightSpawnButton.Pressed += SpawnRight;
        leftSpawnButton.Pressed += SpawnLeft;
        
        rightSpawner = GetNode<Spawner>("%SpawnerRight");
        leftSpawner = GetNode<Spawner>("%SpawnerLeft");

        rightSpawner.SceneToSpawn = UnitToSpawn;
        leftSpawner.SceneToSpawn = UnitToSpawn;

        rightSpawnerLabel = GetNode<Label>("%SpawnRightLabel");
        leftSpawnerLabel = GetNode<Label>("%SpawnLeftLabel");

        rightSpawnerLabel.Text = UnitCost.ToString() + " g";
        leftSpawnerLabel.Text = UnitCost.ToString() + " g";
    }

    public override void _Process(double delta)
    {
        UpdateHUDValues();
    }
    
    protected override void UpdateUpgradeCost()
    {
        upgradeCost = (1+rankCurrent) * UnitCost; 
        upgradeCostLabel.Text = upgradeCost.ToString() + " g";
    }

    public void SpawnRight()
    {
        GD.Print("SpawnRight");
        if (Tower.Instance.Gold < UnitCost)
        {
            GD.Print("Not enough gold: SPAWN RIGHT");
            return;
        }

        Tower.Instance.Gold -= UnitCost;

        rightSpawner.SpawnScene(UnitToSpawn, false);
    }

    public void SpawnLeft()
    {
        GD.Print("SpawnLeft");
        if (Tower.Instance.Gold < UnitCost)
        {
            GD.Print("Not enough gold: SPAWN LEFT");
            return;
        }

        Tower.Instance.Gold -= UnitCost;

        leftSpawner.SpawnScene(UnitToSpawn, false);
    }
}