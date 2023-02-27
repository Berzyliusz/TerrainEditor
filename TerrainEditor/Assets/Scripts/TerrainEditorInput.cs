using UnityEngine;

namespace Assets.Scripts
{
    public interface ITerrainEditorInput
    {
        bool RaiseTerrain { get; }
        bool LowerTerrain { get; }
    }

    public class TerrainEditorInput : ITerrainEditorInput
    {
        public bool RaiseTerrain => Input.GetMouseButton(0) && !anyShiftPressed;

        public bool LowerTerrain => Input.GetMouseButton(0) && anyShiftPressed;

        bool anyShiftPressed => Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
    }
}
