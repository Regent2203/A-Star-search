using UnityEditor;
using UnityEngine;

namespace Cells
{
    public class CellSprites : ScriptableObject
    {
        [Header("Sprites")]
        public Sprite Normal;
        public Sprite Blocked;
        public Sprite Start;
        public Sprite Finish;
        public Sprite Way;

#if UNITY_EDITOR
        [MenuItem("Assets/Create/ScriptableObject/CellSprites")]
        public static void CreateMyAsset()
        {
            var asset = ScriptableObject.CreateInstance<CellSprites>();

            AssetDatabase.CreateAsset(asset, "Assets/CellSprites.asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }
#endif
    }
}