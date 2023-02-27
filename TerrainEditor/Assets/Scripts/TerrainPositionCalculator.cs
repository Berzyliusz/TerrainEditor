using UnityEngine;

namespace Assets.Scripts
{
    public interface ITerrainPositionCalculator
    {
        TerrainPos CalculateTerrainPosition(Vector3 worldPosition);
    }

    public class TerrainPositionCalculator : ITerrainPositionCalculator
    {
        Terrain terrain;
        TerrainData terrainData;

        public TerrainPositionCalculator(Terrain terrain)
        {
            this.terrain = terrain;
            terrainData = terrain.terrainData;
        }

        public TerrainPos CalculateTerrainPosition(Vector3 worldPosition)
        {
            float relativeHitTerX = (worldPosition.x - terrain.transform.position.x) / terrainData.size.x;
            float relativeHitTerZ = (worldPosition.z - terrain.transform.position.z) / terrainData.size.z;

            float relativeTerCoordX = terrainData.heightmapResolution * relativeHitTerX;
            float relativeTerCoordZ = terrainData.heightmapResolution * relativeHitTerZ;

            return new TerrainPos(relativeTerCoordX, relativeTerCoordZ);
        }
    }
}