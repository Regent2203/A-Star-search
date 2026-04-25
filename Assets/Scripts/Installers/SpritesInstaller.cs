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
        
        public Sprite Normal => _normal;
        public Sprite Obstacle => _obstacle;
        //todo: swamp, dirt, rough
    }
}