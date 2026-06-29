using ThisProject.Nodes;
using UnityEngine;
using UnityEngine.Pool;

namespace ThisProject.Implementations.VisualLinks
{
    public class VisualLinksPool<T> where T : class, INodeData
    {
        private readonly VisualLinksFactory<T> _factory;
        private readonly ObjectPool<VisualLink<T>> _pool;


        public VisualLinksPool(VisualLinksFactory<T> factory)
        {
            _factory = factory;

            _pool = new ObjectPool<VisualLink<T>>(
                createFunc: _factory.Create,
                actionOnGet: link => link.gameObject.SetActive(true),
                actionOnRelease: link => link.gameObject.SetActive(false),
                actionOnDestroy: link => 
                { 
                    if (link != null) 
                        Object.Destroy(link.gameObject); 
                },
                collectionCheck: true,
                defaultCapacity: 30,
                maxSize: 50
            );
        }

        public VisualLink<T> Get() => _pool.Get();
        public void Release(VisualLink<T> link) => _pool.Release(link);
    }
}
