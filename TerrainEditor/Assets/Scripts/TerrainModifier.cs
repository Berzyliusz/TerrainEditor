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
            int width = brush.Size * 2;
            int height = brush.Size * 2;

            float[,] heights = terrainData.GetHeights(minX, minY, width, height);

            float maxStrength = raiseTerrain ? brush.Strength : -brush.Strength;

            for (int x = 0; x < brush.Size * 2; x++)
            {
                for(int z = 0; z < brush.Size * 2; z++)
                {
                    /*if (IsOutsideOfTerrain(x, z))
                        continue;

                    var distanceFromCenter = CalculateDistanceFromCenter(x, z, pos);

                    if (distanceFromCenter > brush.Size)
                        continue;

                    var strengthMultiplier = CaclulateStrength(distanceFromCenter, brush.Size);*/

                    heights[z, x] = heights[z, x] += maxStrength; // * strengthMultiplier;
                }
            }

            terrainData.SetHeights(minX, minY, heights);
        }

        bool IsOutsideOfTerrain(int x, int z)
        {
            return x < 0 || z < 0 || x > terrainData.heightmapResolution || z > terrainData.heightmapResolution;
        }

        float CalculateDistanceFromCenter(int x, int z, TerrainPos pos)
        {
            return Vector2.Distance(new Vector2(x,z), new Vector2(pos.x, pos.z));
        }

        float CaclulateStrength(float distance, float maxDistance)
        {
            return 1 - distance / maxDistance;
        }
    }
}