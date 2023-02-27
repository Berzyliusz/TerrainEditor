using UnityEngine;

namespace Assets.Scripts
{
    public class TerrainEditor : TerrainDependandtMonobehaviour
    {
        [SerializeField] TerrainBrush brush;

        ITerrainEditorInput input;
        ITerrainPositionCalculator positionCalculator;
        ITerrainModifier terrainModifier;

        protected override void Awake()
        {
            base.Awake();

            input = new TerrainEditorInput();
            positionCalculator = new TerrainPositionCalculator(terrain);
            terrainModifier = new TerrainModifier(terrain, brush);
        }

        void Update()
        {
            if (!input.LowerTerrain && !input.RaiseTerrain)
                return;

            var mousePos = input.MousePosition;
            var ray = Camera.main.ScreenPointToRay(mousePos);

            RaycastHit hit;
            if (IsCursorOutsideOfTerrain(ray, out hit))
                return;

            TerrainPos terrainHitPos = positionCalculator.CalculateTerrainPosition(hit.point);
            terrainModifier.ModifyTerrain(terrainHitPos, input.RaiseTerrain);
        }

        bool IsCursorOutsideOfTerrain(Ray ray, out RaycastHit hit)
        {
            return !Physics.Raycast(ray, out hit);
        }
    }
}