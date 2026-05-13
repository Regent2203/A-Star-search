using Core.Nodes;
using UnityEngine;
using Zenject;

namespace Core.Implementations
{
    public class VisualLinksFactory<T> where T : class, INode
    {
        private VisualLink<T> _prefab;
        private IInstantiator _instantiator;

        public VisualLinksFactory(IInstantiator instantiator) 
        {
            _instantiator = instantiator;
        }

        public void Create()
        {
            var link = _instantiator.InstantiatePrefabForComponent<VisualLink<T>>(_prefab, Vector2.zero, Quaternion.identity, null);
            //link.Bind()
        }
    }
}
