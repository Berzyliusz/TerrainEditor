using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class TerrainEditor : MonoBehaviour
    {
        ITerrainEditorInput input;

        void Awake()
        {
            input = new TerrainEditorInput();
        }

        void Update()
        {
            if (!input.LowerTerrain && !input.RaiseTerrain)
                return;
            var mousePos = Input.mousePosition;
            var ray = Camera.main.ScreenPointToRay(mousePos);

            if (!Physics.Raycast(ray, out RaycastHit hit))
                return;
                

            AlterTerrain();
        }

        private static void AlterTerrain()
        {
            
            // Check if cursor is over terrain
            // Calculate what position to lower
            // Lower it
        }
    }
}