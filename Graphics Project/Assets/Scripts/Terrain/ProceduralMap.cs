using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralMap : MonoBehaviour {

    public enum DrawState { NOISEMAP, MESH }

    public DrawState drawMode;

    public float meshHeightMultiplyer;

    public int mapWidth;
    public int mapHeight;
    public float noiseScale;

    public int octives;

    [Range(0,1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public bool autoUpdate;

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GeneratNoiseMap(mapWidth, mapHeight, seed, noiseScale, octives, persistance, lacunarity, offset);

        GenerateMap display = FindObjectOfType<GenerateMap>();

        if (drawMode == DrawState.NOISEMAP)
        {
            display.DrawNoiseMap(noiseMap);
        }
        else if (drawMode == DrawState.MESH)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplyer));
        }
    }

    private void OnValidate()
    {
        if (mapWidth < 1)
        {
            mapHeight = 1;
        }
        if (mapHeight < 1)
        {
            mapHeight = 1;
        }

        if (lacunarity < 1)
        {
            lacunarity = 1;
        }

        if (octives < 0)
        {
            octives = 1;
        }
    }
}
