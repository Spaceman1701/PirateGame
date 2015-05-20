using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class HeightMap
{
    private const int X = 0;
    private const int Y = 1;

    private float[,] data;

    private float max;
    private float min;

    private bool built;

    public float Max
    {
        get
        {
            return max;
        }
    }

    public float Min
    {
        get
        {
            return min;
        }
    }

    public bool Built
    {
        get
        {
            return built;
        }
    }

    public float[,] Data
    {
        get
        {
            return data;
        }
    }

    public int Width
    {
        get
        {
            return data.GetLength(X);
        }
    }

    public int Height
    {
        get
        {
            return data.GetLength(Y);
        }
    }

    public void SetValue(int x, int y, float value)
    {
        data[x, y] = value;
    }

    public float GetValue(int x, int y)
    {
        if (x < 0)
        {
            x = 0;
        }
        else if (x > data.GetLength(X))
        {
            x = data.GetLength(X) - 1;
        }
        if (y < 0)
        {
            y = 0;
        }
        else if (y > data.GetLength(Y))
        {
            y = data.GetLength(Y) - 1;
        }
        return data[x, y];
    }


    public void Build()
    {
        FindMaxAndMin();
        NormalizeData();
        built = true;
    }

    private void FindMaxAndMin()
    {
        float min = float.MaxValue;
        float max = float.MinValue;
        foreach (float f in data)
        {
            if (f < min)
            {
                min = f;
            }
            if (f > max)
            {
                max = f;
            }
        }

        this.min = min;
        this.max = max;
    }

    private void NormalizeData()
    {
        for (int x = 0; x < data.GetLength(X); x++)
        {
            for (int y = 0; y < data.GetLength(Y); y++)
            {
                data[x, y] = ConvertToUnitRange(data[x, y], min, max);
            }
        }
    }

    private float ConvertToUnitRange(float value, float min, float max)
    {
        float newValue = (value - min) / (max - min);
        return newValue;
    }
}
