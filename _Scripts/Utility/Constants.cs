using Godot;
using System;

public class Constants
{
	public const int LAYERMASK_PLATFORM = 4;
	
	public const float TILES_PER_PX = 1f / 16f;
	public const int PX_PER_TILE = 16;
	public const float PX_PER_IN = 4f / 3f;
	public const float IN_PER_PX = 3f / 4f;
	public const float IN_PER_M = 39.3701f;
	public const float PX_PER_M = PX_PER_IN * IN_PER_M;
	public const float TILES_PER_M = TILES_PER_PX * PX_PER_M;
	public const float M_PER_TILE = 1f / TILES_PER_M;
	public const float GRAVITY_MAG = 9.81f;
	public const float GRAVITY_DESIRED = 15f;
	public const float GRAV_SCALAR = GRAVITY_DESIRED / GRAVITY_MAG;
	public const float GRAVITY_PX = GRAVITY_MAG * PX_PER_M;
	public const float MS_TO_MPH = 2.23694f;
	public const float MPH_TO_MS = 0.44704f;
	public const float MPH_TO_TPS = 15f / 22f;
	public const float FPS_TO_MS = 125f / 381f;
	public const float MS_TO_TPS = 381f / 125f;

	public const float FALL_SPEED_MAX = 1000f;
	public const float MOVE_SPEED_MAX = 10000f;
}
