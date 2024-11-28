public static class BitmaskExtensions
{
    public static uint SetBitFlag(this uint flags, uint FLAG, bool value)
    {
        if (value)
        {
            flags |= FLAG;
        }
        else
        {
            flags &= ~FLAG;
        }

        return flags;
    }

    public static bool LayerActive(this uint mask, int layer)
    {
        return (mask & layer) > 0;
    }

    public static uint WithLayer(this uint layerMask, int layer)
    {
        return layerMask | (uint)(1 << layer);
    }

    public static uint WithoutLayer(this uint layerMask, int layer)
    {
        return layerMask & ~ (uint)(1 << layer);
    }
}