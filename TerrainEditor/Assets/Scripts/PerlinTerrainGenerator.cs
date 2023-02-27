using UnityEngine;

namespace Assets.Scripts
{
    public class PerlinTerrainGenerator : MonoBehaviour
    {
        [SerializeField] Terrain terrain;
        [SerializeField] float perlinXScale = 0.01f;
        [SerializeField] float perlinZScale = 0.01f;
        [SerializeField] float heightMultiplier = 10f;

        void Awake()
        {
            if (!terrain)
            {
                terrain = FindObjectOfType<Terrain>();

                if (!terrain)
                {
                    Debug.LogError("Terrain null, pls plug in a terrain for perlin generator.");
                    Destroy(this);
                    return;
                }
            }

            SetupTerrainParameters();
        }

        void Start()
        {            
            GeneratePerlinNoiseTerrain();
        }

        void SetupTerrainParameters()
        {
            terrain.terrainData.size = new Vector3(256, 256, 256);
            terrain.terrainData.heightmapResolution = 2048;
        }

        [ContextMenu("Generate Perlin Terrain")]
        void GeneratePerlinNoiseTerrain()
        {
            var terrainData = terrain.terrainData;
            float[,] heightMap = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);

            for(int x = 0; x < terrainData.heightmapResolution; x++)
            {
                for(int z = 0; z < terrainData.heightmapResolution; z++)
                {
                    heightMap[x,z] = Mathf.PerlinNoise(x * perlinXScale, z * perlinZScale) * heightMultiplier;
                }
            }

            terrainData.SetHeights(0, 0, heightMap);
        }
    }
}