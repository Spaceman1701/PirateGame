using UnityEngine;
using System.Collections;

public class DiamondSquareGenerator : MonoBehaviour
{

    private HeightMap map;

    public float cornerHeight;

    public float roughness;

    public int size;

    private int max;

    void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        //Random.seed = 10;

        map = new HeightMap();

        max = size - 1;
        Divide(max);

        map.Build();
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
        map.SetValue(0, max, max / 2);
        map.SetValue(max, 0, max / 2);
        map.SetValue(max, max, max / 2);
    }

    private float NextFloat()
    {
        return Random.Range(0.0f, 1.0f);
    }


}
