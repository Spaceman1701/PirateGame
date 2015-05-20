using UnityEngine;
using System.Collections;


public class PerlinHeightMap : MonoBehaviour {

    public float scale;

    public int resolution;

    private Texture2D tex;

    public float cuttoff;

    public float steepness;

    public float biomeOffset;
    public float biomeScale;
    public float biomeEffect;

    public float hfOffset;
    public float hfScale;
    public float hfEffect;


    private float xOffset;
    private float yOffset;

    private float xOffsetScale;
    private float yOffsetScale;

    public int seed = 1;
    private int cashedSeed;

    // Use this for initialization
    void Start () {
        tex = new Texture2D(resolution, resolution);/*
        for (int x = 0; x < resolution; x++)
        {
            for (int y = 0; y < resolution; y ++)
            {
                float value = Mathf.PerlinNoise(((float)x) / (resolution*scale), ((float)y) / (resolution*scale)) * steepness;
                value = Mathf.PingPong(value, 1);
                if (value < cuttoff)
                {
                    value = 0;
                }
                tex.SetPixel(x, y, new Color(value, value, value, 1));
            }
        }
        tex.filterMode = FilterMode.Point;
        tex.Apply(); */
        Random.seed = seed;
        cashedSeed = seed;
        SetRandoms();
        
	}

    void Update()
    {
        if (cashedSeed != seed)
        {
            Random.seed = seed;
            cashedSeed = seed;
            SetRandoms();
            Debug.Log("Randomizing");
        }
        AjustScale();
        if (tex.width != resolution)
        {
            tex = new Texture2D(resolution, resolution);
        }
        for (int x = 0; x < resolution; x++)
        {
            for (int y = 0; y < resolution; y++)
            {
                float ix = (x + xOffsetScale) * scale;
                float iy = (y + xOffsetScale) * scale;

                float value = getValue(ix, iy, 1);
                float biome = getValue(ix + biomeOffset, iy + biomeOffset, biomeScale) * biomeEffect;
                float highfrequency = getValue(ix + hfOffset, iy + hfOffset, hfScale) * hfEffect;
                value = Util.NormalizeValue(value + biome + highfrequency, 0, 1 + biomeEffect);
                if (value < cuttoff)
                {
                    value = 0;
                }
                tex.SetPixel(x, y, new Color(value, value, value, 1));
            }
        }
        tex.filterMode = FilterMode.Point;
        tex.Apply();
    }

    private void AjustScale()
    {
        xOffsetScale = xOffset / scale;
        yOffsetScale = yOffset / scale;
    }

    private void SetRandoms()
    {
        Debug.Log("Set randoms!");
        xOffset = 100000 * Random.Range(-2000f, 2000f) / resolution;
        yOffset = 100000 * Random.Range(-2000f, 2000f) / resolution;
    }

    private float getValue(float x, float y, float scale)
    {
        float x2 = x / resolution;
        float y2 = y / resolution;

        return Util.PerlinNoise(x2 * scale, y2 * scale);
    }
	
	void OnGUI () {
	    if (tex != null)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), tex);
        }
	}
}
