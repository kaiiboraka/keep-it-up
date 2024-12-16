using Godot;

public partial class SpawningBuilding : Building
{
    [Export] private int UnitCost = 50;

    [Export] public UnitType UnitToSpawn;
    
    private Button rightSpawnButton;
    private Button leftSpawnButton;
    private Spawner rightSpawner;
    private Spawner leftSpawner;
    private Label rightSpawnerLabel;
    private Label leftSpawnerLabel;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
        rightSpawnButton = GetNode<Button>("%RightSpawnButton");
        leftSpawnButton = GetNode<Button>("%LeftSpawnButton");

        rightSpawnButton.Pressed += SpawnRight;
        leftSpawnButton.Pressed += SpawnLeft;
        
        rightSpawner = GetNode<Spawner>("%SpawnerRight");
        leftSpawner = GetNode<Spawner>("%SpawnerLeft");
        
        leftSpawner.UnitToSpawn = UnitToSpawn;
        rightSpawner.UnitToSpawn = UnitToSpawn;
        
        rightSpawnerLabel = GetNode<Label>("%SpawnRightLabel");
        leftSpawnerLabel = GetNode<Label>("%SpawnLeftLabel");

        rightSpawnerLabel.Text = UnitCost.ToString() + " g";
        leftSpawnerLabel.Text = UnitCost.ToString() + " g";
    }

    protected override void UpdateUpgradeCost()
    {
        upgradeCost = (1+rankCurrent) * UnitCost; 
        upgradeCostLabel.Text = MaxRank ? "MAX" : upgradeCost + " g";
    }
    

    public void SpawnRight()
    {
        if (Tower.Instance.Gold < UnitCost)
        {
            GD.Print("Not enough gold: SPAWN RIGHT");
            return;
        }

        Tower.Instance.Gold -= UnitCost;

        rightSpawner.ForceSpawn(UnitToSpawn);
    }

    public void SpawnLeft()
    {
        if (Tower.Instance.Gold < UnitCost)
        {
            GD.Print("Not enough gold: SPAWN LEFT");
            return;
        }

        Tower.Instance.Gold -= UnitCost;

        leftSpawner.ForceSpawn(UnitToSpawn);
    }
}