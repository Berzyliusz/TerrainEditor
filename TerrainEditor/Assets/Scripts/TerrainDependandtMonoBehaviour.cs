using UnityEngine;
using System;

namespace Assets.Scripts
{
    public class TerrainDependandtMonobehaviour  : MonoBehaviour
    {
        [SerializeField] protected Terrain terrain;

        protected virtual void Awake()
        {
            if (!terrain)
            {
                terrain = FindObjectOfType<Terrain>();

                if (!terrain)
                {
                    throw new NullReferenceException($"Terrain not found at {this.gameObject.name}. " +
                        $"Please ensure there is a terrain plugged in the inspector.");
                }
            }
        }
    }
}