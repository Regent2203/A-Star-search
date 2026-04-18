using System;
using UnityEngine;
using Zenject;

namespace Core.Installers
{
    [CreateAssetMenu(fileName = "SpritesLibrary", menuName = "Installers/SpritesLibrary")]
    public class SpritesLibraryInstaller : ScriptableObjectInstaller<SpritesLibraryInstaller>
    {
        public CellSprites Cells;

        public override void InstallBindings()
        {
            Container.BindInstance(Cells).AsSingle();
        }
    }


    [Serializable]
    public class CellSprites
    {
        [SerializeField]
        private Sprite _normal;
        [SerializeField]
        private Sprite _blocked;
        [SerializeField]
        private Sprite _start;
        [SerializeField]
        private Sprite _finish;
        [SerializeField]
        private Sprite _way;

        public Sprite Normal => _normal;
        public Sprite Blocked => _blocked;
        public Sprite Start => _start;
        public Sprite Finish => _finish;
        public Sprite Way => _way;
    }
}