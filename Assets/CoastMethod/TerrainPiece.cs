using UnityEngine;
using System.Collections;

public class TerrainPiece {

    private const float PI = Mathf.PI;

    private readonly Vector2 center;
    private readonly float averageRadius;
    private readonly float variation;

    private readonly long seed;

    private float angleStep;

    private int resolution;

    private int iterations = 20;
    private float falloff = 1;


    private Vector2[] verticies;

    public Vector2[] Verticies
    {
        get
        {
            return verticies;
        }
    }

    public TerrainPiece(Vector2 center, float averageRadius, float variation, long seed, float vertDistance)
    {
        this.center = center;
        this.averageRadius = averageRadius;
        this.variation = variation;

        this.seed = seed;

        angleStep = Mathf.Asin(vertDistance / averageRadius);

        resolution = (int)((2 * PI) / angleStep);

        verticies = new Vector2[resolution];

        GenerateIterativeShape();
    }


    private void GenerateShape()
    {
        float[] distances = new float[resolution];
        for (int i = 0; i < resolution; i++)
        {
            distances[i] = averageRadius;
        }
        for (int j = 0; j < resolution; j++)
        {
            float offset = NextFloat();
            distances[j] += offset;
        }
        for (int k = 0; k < resolution; k++)
        {
            float theta = angleStep * k;
            float r = distances[k];
            verticies[k] = new Vector2(r * Mathf.Cos(theta), r * Mathf.Sin(theta));
        }
    }

    private void GenerateIterativeShape()
    {
        float[] distances = new float[resolution];

        for (int i = 0; i < resolution; i++)
        {
            distances[i] = averageRadius;
        }

        for (int i = 0; i < iterations; i++)
        {
            for (int j = 0; j < resolution; j+= ((iterations)/(i + 1)))
            {
                float offset = NextFloat() * (falloff / (i + 1));
                distances[j] += offset;
            }
        }

        for (int k = 0; k < resolution; k++)
        {
            float theta = angleStep * k;
            float r = distances[k];
            verticies[k] = new Vector2(r * Mathf.Cos(theta), r * Mathf.Sin(theta));
        }
    }


    private float NextFloat()
    {
        return Random.Range(-variation / 2.0f, variation / 2.0f);
    }
}
