using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Struct just as Vector2Int, but instead of Y, we have Z value, to do not confuse them.
    /// </summary>
    public struct TerrainPos
    {
        public int x;
        public int z;

        public TerrainPos(float x, float z)
        {
            this.x = Mathf.RoundToInt(x);
            this.z = Mathf.RoundToInt(z);
        }

        public TerrainPos(int x, int z)
        {
            this.x = x;
            this.z = z;
        }
    }

    public static class TerrainPosExtensions
    {
        public static Vector2 ToVector2(this TerrainPos pos)
        {
            return new Vector2(pos.x, pos.z);
        }
    }
}