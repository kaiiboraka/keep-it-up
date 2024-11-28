extends Camera2D

@export_category("settings")
@export var gameSize : float = 1;

func change_viewport_resolution(size: Vector2) -> void:
	get_viewport().size = size * gameSize
	zoom *= gameSize

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
#	change_viewport_resolution(Vector2(640,320))
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass
