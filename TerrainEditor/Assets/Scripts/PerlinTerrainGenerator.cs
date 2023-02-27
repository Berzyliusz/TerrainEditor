using UnityEngine;

namespace Assets.Scripts
{
    public class PerlinTerrainGenerator : TerrainDependandtMonobehaviour
    {
        [SerializeField] float perlinXScale = 0.01f;
        [SerializeField] float perlinZScale = 0.01f;
        [SerializeField] float heightMultiplier = 10f;
        [SerializeField] float heightOffset = 20f;

        protected override void Awake()
        {
            base.Awake();

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
                    heightMap[x,z] = Mathf.PerlinNoise(x * perlinXScale, z * perlinZScale) * heightMultiplier + heightOffset;
                }
            }

            terrainData.SetHeights(0, 0, heightMap);
        }
    }
}