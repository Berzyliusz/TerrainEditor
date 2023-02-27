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
        TerrainBrush brush;

        public TerrainModifier(Terrain terrain, TerrainBrush brush)
        {
            this.brush= brush;
            this.terrain = terrain;
            terrainData = terrain.terrainData;
        }

        public void ModifyTerrain(TerrainPos pos, bool raiseTerrain)
        {
            float[,] heights = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);

            float maxStrength = raiseTerrain ? brush.Strength : -brush.Strength;

            for(int x = -brush.Size; x < brush.Size; x++)
            {
                for(int z = -brush.Size; z < brush.Size; z++)
                {
                    // We need to validate if this is inside the actual terrain?
                    // also make it round?
                    var xToApply = pos.x + x;
                    var zToApply = pos.z + z;

                    if(xToApply < 0 && zToApply < 0)
                    {
                        continue;
                    }

                    heights[zToApply, xToApply] = heights[zToApply, xToApply] += maxStrength;
                    
                }
            }

            terrainData.SetHeights(0, 0, heights);
        }
    }
}