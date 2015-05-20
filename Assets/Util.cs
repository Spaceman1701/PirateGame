using UnityEngine;
using System.Collections;

public class Util {

    private Util() { }


    public static float NormalizeValue(float value, float min, float max)
    {
        float newValue = (value - min) / (max - min);
        return newValue;
    }


    public static float PerlinNoise(float x, float y)
    {
        return Mathf.Clamp(Mathf.PerlinNoise(x, y), 0, 1);
    }

}
