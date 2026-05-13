using Core.Nodes;
using UnityEngine;
using Zenject;

namespace Core.Implementations.VisualLinks
{
    public class VisualLinksFactory<T> where T : class, INode
    {
        private readonly VisualLink<T> _prefab;
        private readonly IInstantiator _instantiator;

        public VisualLinksFactory(VisualLink<T> prefab, IInstantiator instantiator) 
        {
            _prefab = prefab;
            _instantiator = instantiator;
        }

        public VisualLink<T> Create()
        {
            var visualLink = _instantiator.InstantiatePrefabForComponent<VisualLink<T>>(_prefab, Vector2.zero, Quaternion.identity, null);

            return visualLink;
        }
    }
}
