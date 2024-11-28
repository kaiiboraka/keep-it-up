using Godot;

public static class AnimationExtensions
{
    public static string AnimName(this Animation anim) => anim.GetName();

    public static void SetResourceName(this Animation anim)
    {
        // path = anim.ResourcePath;
        // path = path
        // .Substring(path.LastIndexOf('/') +1 )
        // .Substring(path.LastIndexOf('_') +1 )
        // .Substring(0, path.Length - 4);

        var path = anim.ResourcePath; // path://to/file/Name_Anim.res
        path = path[(path.LastIndexOf('/') + 1)..]; // Name_Anim.res
        path = path[(path.LastIndexOf('_') + 1)..]; // Anim.res
        path = path[..^4]; // Anim
        anim.ResourceName = path;
    }
    //+ "_" + anim.GetInstanceId();


}