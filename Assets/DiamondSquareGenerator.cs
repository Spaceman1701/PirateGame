using UnityEngine;
using System.Collections;

public class DiamondSquareGenerator : HeightMapGenerator
{

    private HeightMap map;

    public float cornerHeight;

    public float roughness;

    private int max;


    public void Start()
    {
        map = HeightMap;
    }

    public override void GenerateMap()
    {
        Random.seed = 10;
        max = map.Size - 1;
        Divide(max);
        map.Init();
    }

    private void Divide(int size)
    {
        int half = size / 2;
        float fHalf = (float)size / 2;
        float scale = roughness * size;

        if (fHalf < 1)
        {
            return;
        }

        for (int y = half; y < max; y += size)
        {
            for (int x = half; x < max; x += size)
            {
                Square(x, y, half, NextFloat() * scale * 2 - scale);
            }
        }

        for (int y = 0; y <= max; y += half)
        {
            for (int x = (y + half) % size; x <= max; x += size)
            {
                Diamond(x, y, half, NextFloat() * scale * 2 - scale);
            }
        }

        Divide(size / 2);
    }

    private void Square(int x, int y, int size, float offset)
    {
        float average = Average(map.GetValue(x - size, y - size), map.GetValue(x - size, y + size), map.GetValue(x + size, y + size), map.GetValue(x + size, y - size));
        map.SetValue(x, y, average + offset);
    }

    private void Diamond(int x, int y, int size, float offset)
    {
        float average = Average(map.GetValue(x, y - size), map.GetValue(x + size, y), map.GetValue(x, y + size), map.GetValue(x - size, y));
        map.SetValue(x, y, average + offset);
    }

    private float Average(float a, float b, float c, float d)
    {
        return (a + b + c + d) / 4.0f;
    }



    private void SetCorners()
    {
        map.SetValue(0, 0, max / 2);
        map.SetValue(0, map.Size - 1, max / 2);
        map.SetValue(map.Size - 1, 0, max / 2);
        map.SetValue(map.Size - 1, map.Size - 1, max / 2);
    }


}
