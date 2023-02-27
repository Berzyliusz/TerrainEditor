using UnityEngine;

namespace Assets.Scripts
{
    public interface ITerrainModifier
    {
        void ModifyTerrain(TerrainPos pos, bool raiseTerrain, TerrainBrush brush);
    }

    public class TerrainModifier : ITerrainModifier
    {
        TerrainData terrainData;

        public TerrainModifier(TerrainData terrainData)
        {
            this.terrainData = terrainData;
        }

        public void ModifyTerrain(TerrainPos pos, bool raiseTerrain, TerrainBrush brush)
        {
            int minX = Mathf.Max(pos.x - brush.Size, 0);
            int minY = Mathf.Max(pos.z - brush.Size, 0);
            int width = Mathf.Min(terrainData.heightmapResolution - pos.x + brush.Size, brush.Size * 2);
            int height = Mathf.Min(terrainData.heightmapResolution - pos.z + brush.Size, brush.Size * 2);

            float[,] heights = terrainData.GetHeights(minX, minY, width, height);

            float maxStrength = raiseTerrain ? brush.Strength : -brush.Strength;

            for (int x = 0; x < width; x++)
            {
                for(int z = 0; z < height; z++)
                {
                    var distanceFromCenter = CalculateDistanceFromCenter(brush, x, z);

                    if (distanceFromCenter > brush.Size)
                        continue;

                    var strengthMultiplier = CaclulateStrength(distanceFromCenter, brush.Size);

                    heights[z, x] = heights[z, x] += maxStrength * strengthMultiplier;
                }
            }

            terrainData.SetHeights(minX, minY, heights);
        }

        private static float CalculateDistanceFromCenter(TerrainBrush brush, int x, int z)
        {
            return Vector2.Distance(new Vector2(x, z), new Vector2(brush.Size, brush.Size));
        }

        float CaclulateStrength(float distance, float maxDistance)
        {
            return 1 - distance / maxDistance;
        }
    }
}