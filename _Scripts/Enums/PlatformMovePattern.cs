public enum PlatformMovePattern
{
    Frozen,
    LinearCycle,
    Pingpong,
    Instant,
}

// to be continuous but keep easing, set WaitTime to 0
public enum PlatformWaitPattern
{
    EndPoints, // easing and stopping
    AllPathPoints, // easing at stopping
}