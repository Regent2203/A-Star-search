using System;
using UnityEngine;
using Zenject;

namespace Core.Installers
{
    [CreateAssetMenu(fileName = "SpritesLibrary", menuName = "Installers/SpritesLibrary")]
    public class SpritesLibraryInstaller : ScriptableObjectInstaller<SpritesLibraryInstaller>
    {
        //public SomeConfig Something1;

        public override void InstallBindings()
        {
            //Container.BindInstance(Something1).AsSingle();
        }
    }


    [Serializable]
    public class SomeConfig //todo
    {
        [SerializeField]
        private Sprite _someSprite;
        
        public Sprite SomeSprite => _someSprite;
    }
}