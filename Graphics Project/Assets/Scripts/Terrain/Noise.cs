using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise {

    public static float[,] GeneratNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octives, float persistance, float lacunarity, Vector2 offset)
    {
        System.Random prng = new System.Random(seed);
        Vector2[] octiveOffSets = new Vector2[octives];

        for (int i = 0; i < octives; i++)
        {
            float offsetx = prng.Next(-10000, 10000) + offset.x;
            float offsety = prng.Next(-10000, 10000) + offset.y;
            octiveOffSets[i] = new Vector2(offsetx, offsety);

        }

        if (scale < 0 || scale == 0)
        {
            scale = 0.0001f;
        }
               
        float[,] noiseMap = new float[mapWidth, mapHeight];

        float halfWidth = mapWidth / 2;
        float halfHeight = mapHeight / 2;

        float maxNoiseHeight = float.MinValue;
        float minNiseHeight = float.MaxValue;

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float amplatude = 1;
                float frequancy = 1;
                float noiseHight = 0;

                float sampleX;
                float sampleY;

                for (int i = 0; i < octives; i++)
                {
                    sampleX = (x - halfWidth) / scale * frequancy + octiveOffSets[i].x;
                    sampleY = (y - halfHeight) / scale * frequancy + octiveOffSets[i].y;

                    float perlinVale = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHight += perlinVale * amplatude;

                    amplatude *= persistance;
                    frequancy *= lacunarity;

                }

                if (noiseHight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHight;
                }
                if (noiseHight < minNiseHeight)
                {
                    minNiseHeight = noiseHight;
                }

                noiseMap[x, y] = noiseHight;
            }
        }

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNiseHeight, maxNoiseHeight, noiseMap[x,y]);
            }
        }

        return noiseMap;
    }

}
