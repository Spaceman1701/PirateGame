using UnityEngine;
using System.Collections;

public class LayerMap : MonoBehaviour {

    public float seaLevel;

    public int numLayers;

    private float layerSize;

    private int maxSeaLayer;

    private float[] layerHeights;

    private Layer[] layers;

    public Layer[] Layers
    {
        get
        {
            return layers;
        }
    }

    // Use this for initialization
    void Start() {
        layerSize = 1.0f / numLayers;

        GenerateLayerHeights();
        InitSeaLevel();
    }


    public void GenerateFromHeightMap(HeightMap map)
    {
        GenerateEmptyLayers(map);
        for (int x = 0; x < map.Width; x++)
        {
            for (int y = 0; y < map.Height; y++)
            {   
                foreach (Layer l in layers)
                {
                    if (l.InsideBound(map.Data[x, y]))
                    {
                        l.SetValue(x, y, 1);
                    }
                }
            }
        }
    }
        

    private void GenerateEmptyLayers(HeightMap map)
    {
        layers = new Layer[numLayers];
        for (int i = 0; i < numLayers; i++)
        {
            layers[i] = new Layer(map.Width, map.Height, layerSize);
        }
    }

    private void GenerateLayerHeights()
    {
        layerHeights = new float[numLayers];
        for (int i = 0; i < numLayers; i++)
        {
            layerHeights[i] = i * numLayers;
        }
    }

    private void InitSeaLevel()
    {
        int minLevel = numLayers - 1;
        float minHeight = 2.0f;
        for (int i = 0; i < numLayers; i++)
        {
            if (layerHeights[i] < minHeight)
            {
                minLevel = i;
                minHeight = layerHeights[i];
            }
        }

        maxSeaLayer = minLevel;
    }

}
