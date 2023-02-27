using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Wrapper interface. If needed, input can be swapped to new / old / whatever from single place.
    /// </summary>
    public interface ITerrainEditorInput
    {
        bool RaiseTerrain { get; }
        bool LowerTerrain { get; }
        Vector3 MousePosition { get; }
    }

    public class TerrainEditorInput : ITerrainEditorInput
    {
        public bool RaiseTerrain => Input.GetMouseButton(0) && !anyShiftPressed;

        public bool LowerTerrain => Input.GetMouseButton(0) && anyShiftPressed;

        public Vector3 MousePosition => Input.mousePosition;

        bool anyShiftPressed => Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
    }
}
