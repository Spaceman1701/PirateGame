using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Layer
{
    private int[,] data;

    private float height;

    public Layer(int width, int height, float layerHeight)
    {
        data = new int[width, height];
        this.height = layerHeight;
    }


    public void SetValue(int x, int y, int value)
    {
        data[x, y] = value;
    }

    public int[,] Data
    {
        get
        {
            return data;
        }
    }


    public bool InsideBound(float height)
    {
        return height >= this.height;
    }
}

