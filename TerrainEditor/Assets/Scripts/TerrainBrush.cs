using UnityEngine;

namespace Assets.Scripts
{
    [System.Serializable]
    public struct TerrainBrush
    {
        [field: SerializeField] public float Strength { get; private set; }
        [field: SerializeField] public int Size { get; private set; }
    }
}