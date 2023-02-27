using UnityEngine;

namespace Assets.Scripts
{
    public interface ITerrainModifier
    {
        void ModifyTerrain(TerrainPos pos, bool raiseTerrain);
    }

    public class TerrainModifier : ITerrainModifier
    {
        Terrain terrain;
        TerrainData terrainData;

        public TerrainModifier(Terrain terrain)
        {
            this.terrain = terrain;
            terrainData = terrain.terrainData;

            // We need some "brush" data and strength passed
        }

        public void ModifyTerrain(TerrainPos pos, bool raiseTerrain)
        {
            float[,] heights = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);
            // Also modify the neighbouring positions, according to "brush size"

            heights[pos.z, pos.x] = heights[pos.z, pos.x] * 10;
            terrainData.SetHeights(0, 0, heights);
        }
    }
}