using ThisProject.Nodes;
using ThisProject.ObjectsStorages;
using ThisProject.Views;
using UnityEngine;
using Zenject;

namespace ThisProject.Fields
{
    public abstract class VisibleField<T, V, TId> : MonoBehaviour, IVisibleField<T, V, TId>
        where T : class, INode<TId>
        where V : class, IView<TId>
    {
        public abstract IObjectsStorage<T, TId> Nodes { get; }
        public abstract IObjectsStorage<V, TId> Views { get; }

        public abstract BoxCollider2D Box { get; }

        public T GetNodeById(TId id) => Nodes.GetById(id);
        public V GetViewById(TId id) => Views.GetById(id);

        protected V _viewPrefab;


        [Inject]
        public void Construct(V viewPrefab)
        {
            _viewPrefab = viewPrefab;
        }

        public bool CheckAndAdjustPoint(ref Vector2 pos)
        {
            if (!Box.OverlapPoint(pos))
            {
                return false;
            }

            var bounds = Box.bounds;
            var size = _viewPrefab.GetSize() / 2;
            //X
            var distL = pos.x - bounds.min.x;
            var distR = bounds.max.x - pos.x;

            if (distL < size.x)
                pos.x = bounds.min.x + size.x;
            else if (distR < size.x)
                pos.x = bounds.max.x - size.x;

            //Y
            var distB = pos.y - bounds.min.y;
            var distT = bounds.max.y - pos.y;

            if (distB < size.y)
                pos.y = bounds.min.y + size.y;
            else if (distT < size.y)
                pos.y = bounds.max.y - size.y;

            return true;
        }
    }
}