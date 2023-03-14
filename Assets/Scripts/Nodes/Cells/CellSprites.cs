using UnityEditor;
using UnityEngine;

namespace Nodes.Cells
{
    public class CellSprites : ScriptableObject
    {
        [Header("Sprites")]
        [SerializeField]
        private Sprite _normal = default;
        [SerializeField]
        private Sprite _blocked = default;
        [SerializeField]
        private Sprite _start = default;
        [SerializeField]
        private Sprite _finish = default;
        [SerializeField]
        private Sprite _way = default;

        public Sprite Normal => _normal;
        public Sprite Blocked => _blocked;
        public Sprite Start => _start;
        public Sprite Finish => _finish;
        public Sprite Way => _way;

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