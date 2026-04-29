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
        private Sprite _obstacle;
        [SerializeField]
        private Sprite _dirt;
        [SerializeField]
        private Sprite _sand;
        [SerializeField]
        private Sprite _swamp;

        public Sprite Normal => _normal;
        public Sprite Obstacle => _obstacle;
        public Sprite Dirt => _dirt;
        public Sprite Sand => _sand;
        public Sprite Swamp => _swamp;

    }
}