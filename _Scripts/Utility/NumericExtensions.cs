#if GODOT
using MATHLIB = Godot.Mathf;
using MATHCONST = Godot.Mathf;
using Godot;
using Godot.Collections;
#endif

#if UNITY_64
using MATHLIB = UnityEngine.Mathf;
using MATHCONST = System.Single;
using UnityEngine;
#endif

using System;
using System.Linq;



public static class NumericExtensions
{
    /// <summary>
    /// true: 1
    /// <br/>
    /// false: 0
    /// </summary>
    /// <param name="b"></param>
    /// <returns></returns>
    public static int Int(this bool b)
    {
        return b ? 1 : 0;
    }

    /// <summary>
    /// true: 1
    /// <br/>
    /// false: -1
    /// </summary>
    /// <param name="b"></param>
    /// <returns></returns>
    public static int IntNeg(this bool b)
    {
        return b ? 1 : -1;
    }

    public static bool InRange(this int x, int min = 0, int max = Int32.MaxValue, 
                               bool includeMin = true, bool includeMax = false)
    {
        return includeMin switch
        {
            true when includeMax => min <= x && x <= max,
            true when !includeMax => min <= x && x < max,
            false when includeMax => min < x && x <= max,
            false when !includeMax => min < x && x < max
        };
    }

    public static bool IsPos(this float x)
    {
        return x >= Mathf.Epsilon;
    }
    
    public static bool IsPos(this int x)
    {
        return x >= Mathf.Epsilon;
    }

    public static bool IsNeg(this float x)
    {
        return x < -Mathf.Epsilon;
    }
    
    public static bool IsNeg(this int x)
    {
        return x < -Mathf.Epsilon;
    }
    
    public static bool IsEven(this int x)
    {
        return x % 2 == 0;
    }
    
    public static bool IsOdd(this int x)
    {
        return x % 2 != 0;
    }

    public static HorizontalDirection HorizontalDirection(this int x)
    {
        return (HorizontalDirection)x.Sign();
    }

    public static HorizontalDirection HorizontalDirection(this float x)
    {
        return (HorizontalDirection)x.Sign();
    }

    public static HorizontalDirection HorizontalDirection(this double x)
    {
        return (HorizontalDirection)x.Sign();
    }

    public static VerticalDirection VerticalDirection(this int x)
    {
        return (VerticalDirection)x.Sign();
    }

    public static VerticalDirection VerticalDirection(this float x)
    {
        return (VerticalDirection)x.Sign();
    }

    public static VerticalDirection VerticalDirection(this double x)
    {
        return (VerticalDirection)x.Sign();
    }
    
    public static double Lerp(this double num, float to, float weight)
    {
        return MATHLIB.Lerp(num, to, weight);
    }

    public static float Lerp(this float num, float to, float weight)
    {
        return MATHLIB.Lerp(num, to, weight);
    }

    public static double Lerp(this double num, double to, double weight)
    {
        return MATHLIB.Lerp(num, to, weight);
    }
    
    public static float Lerp(this float num, double to, double weight)
    {
        return (float)MATHLIB.Lerp(num, to, weight);
    }

    public static bool IsZero(this float num) => num is > -MATHCONST.Epsilon and < MATHCONST.Epsilon;

    public static bool FloatEqualsApprox(this float x, float y, float tolerance = MATHCONST.Epsilon)
    {
        return MATHLIB.Abs(x - y) <= tolerance;
    }

    public static bool FloatCloseTo(this float num, float closeTo, float byTolerance = MATHCONST.Epsilon)
    {
        return num >= MATHLIB.Abs(closeTo - byTolerance);
    }

    public static float EaseInOut(this float x, float easeAmount)
    {
        if (easeAmount == 0) return x;
        GD.Print("EASING");
        // TODO : clamp for proper inout behavior (need it > 1. 0<>1 is OutIn)
        float a = easeAmount + 1;
        return MATHLIB.Pow(x, a) / (MATHLIB.Pow(x, a) + MATHLIB.Pow(1 - x, a));
    }
    
    public static float RoundToPrecision(this float value, int places = 2)
    {
        var tens = Mathf.Pow(10, Mathf.Max(0, places));
        float rounded = Mathf.Round((value * tens));
        return rounded / tens;
    }
    
    public static double RoundToPrecision(this double value, int places = 2)
    {
        var tens = Mathf.Pow(10, Mathf.Max(0, places));
        double rounded = Mathf.Round((value * tens));
        return rounded / tens;
    }

    public static float Pow(this float x, float pow)
    {
        return Mathf.Pow(x, pow);
    }
    
    public static float Pow(this int x, int pow)
    {
        return Mathf.Pow(x, pow);
    }
    
    public static float Pow(this int x, float pow)
    {
        return Mathf.Pow(x, pow);
    }
    
    public static double Pow(this double x, double pow)
    {
        return Mathf.Pow(x, pow);
    }

    public static float Sqrt(this float x)
    {
        return MATHLIB.Sqrt(x);
    }
    
    public static double Sqrt(this double x)
    {
        return MATHLIB.Sqrt(x);
    }

    public static float Sqrt(this int x)
    {
        return MATHLIB.Sqrt(x);
    }

    public static int Clamp(this int x, int min, int max)
    {
        return MATHLIB.Clamp(x, min, max);
    }

    public static float Clamp(this float x, float min, float max)
    {
        return MATHLIB.Clamp(x, min, max);
    }

    public static double Clamp(this double x, double min, double max)
    {
        return MATHLIB.Clamp(x, min, max);
    }
    
    public static int Clamp01(this int x)
    {
        return MATHLIB.Clamp(x, 0,1);
    }

    public static float Clamp01(this float x)
    {
        return MATHLIB.Clamp(x, 0f,1f);
    }

    public static double Clamp01(this double x)
    {
        return MATHLIB.Clamp(x, 0,1);
    }

    public static float Floor(this float x)
    {
        return MATHLIB.Floor(x);
    }

    public static int FloorToInt(this float x)
    {
        return MATHLIB.FloorToInt(x);
    }

    public static double Floor(this double x)
    {
        return MATHLIB.Floor(x);
    }

    public static int FloorToInt(this double x)
    {
        return MATHLIB.FloorToInt(x);
    }

    public static float Round(this float x)
    {
        return MATHLIB.Round(x);
    }

    public static int RoundToInt(this float x)
    {
        return MATHLIB.RoundToInt(x);
    }

    public static double Round(this double x)
    {
        return MATHLIB.Round(x);
    }

    public static int RoundToInt(this double x)
    {
        return MATHLIB.RoundToInt(x);
    }

    public static double Sign(this double x)
    {
        return Mathf.Sign(x);
    }
    public static float Sign(this float x)
    {
        return Mathf.Sign(x);
    }
    public static int Sign(this int x)
    {
        return Math.Sign(x);
    }

    public static int TrueMod(this int x, int mod)
    {
        return ((x % mod) + mod) % mod;
    }

    public static float Map(this float value, float min1, float max1, float min2, float max2, bool clamp = false)
    {
        float val = min2 + (max2 - min2) * ((value - min1) / (max1 - min1));
        return clamp ? MATHLIB.Clamp(val, MATHLIB.Min(min2, max2), MATHLIB.Max(min2, max2)) : val;
    }
    
}

