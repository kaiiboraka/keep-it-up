using System.Text;
using Godot;

public static class GodotExtensions
{
    public static void RemoveLastPoint(this Curve2D curve)
    {
        curve.RemovePoint(curve.PointCount-1);
    }

    public static void Print(this string text)
    {
        GD.Print(text);
    }

    public static string Repeat(this string s, int n)
    {
        return new StringBuilder(s.Length * n).Insert(0, s, n).ToString();
    }

    // public static void AlignWithY(this Node2D node, Vector2 new_y)
    // {
    //     var xform = node.Transform;
    //     
    //     xform.BasisXform().Y = new_y;
    //     
    //     xform.basis.x = -xform.basis.z.cross(new_y)
    //     xform.basis = xform.basis.orthonormalized()
    // return xform
    // }
}