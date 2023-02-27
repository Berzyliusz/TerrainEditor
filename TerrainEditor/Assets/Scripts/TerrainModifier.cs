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

        float maxPossibleDistance;

        public TerrainModifier(TerrainData terrainData)
        {
            this.terrainData = terrainData;
        }

        public void ModifyTerrain(TerrainPos pos, bool raiseTerrain, TerrainBrush brush)
        {
            float[,] heights = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);

            float maxStrength = raiseTerrain ? brush.Strength : -brush.Strength;

            maxPossibleDistance = Vector2.Distance(Vector2.zero, new Vector2(brush.Size, 0));

            for (int x = -brush.Size; x < brush.Size; x++)
            {
                for(int z = -brush.Size; z < brush.Size; z++)
                {
                    var xToApply = pos.x + x;
                    var zToApply = pos.z + z;

                    if (IsOutsideOfTerrain(xToApply, zToApply))
                        continue;

                    var distanceFromCenter = CalculateDistanceFromCenter(x, z);

                    if (distanceFromCenter > brush.Size)
                        continue;

                    var strengthMultiplier = CaclulateStrength(distanceFromCenter);

                    heights[zToApply, xToApply] = heights[zToApply, xToApply] += maxStrength * strengthMultiplier;
                }
            }

            terrainData.SetHeights(0, 0, heights);
        }

        float CalculateDistanceFromCenter(int x, int z)
        {
            return Vector2.Distance(new Vector2(x,z), new Vector2(0,0));
        }

        float CaclulateStrength(float distance)
        {
            return 1 - distance / maxPossibleDistance;
        }

        bool IsOutsideOfTerrain(int x, int z)
        {
            return x < 0 || z < 0 || x > terrainData.heightmapResolution || z > terrainData.heightmapResolution;
        }
    }
}