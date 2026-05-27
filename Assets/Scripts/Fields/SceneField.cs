using System;
using ThisProject.Fields.ClickHandlers;
using ThisProject.Fields.NodeMovers;
using ThisProject.Inputs;
using ThisProject.Nodes;
using ThisProject.ObjectsStorages;
using ThisProject.Views;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ThisProject.Fields
{
    public abstract class SceneField<T, V, TId> : MonoBehaviour, IVisibleField<T, V, TId>, IClickableField<T>, IMovableNodeField<T>
        where T : class, INode<TId>
        where V : class, IView<TId>
    {
        public abstract IObjectsStorage<T, TId> Nodes { get; }
        public abstract IObjectsStorage<V, TId> Views { get; }
        public abstract IClickHandler ClickHandler { get; }
        public abstract INodeMover NodeMover { get; }

        public event Action<T, PointerEventData.InputButton, InputSnapshot> NodeClicked;
        public event Action<PointerEventData.InputButton, InputSnapshot> FieldClicked;
        public event Action<T, Vector2> NodeMoved;
        public event Action FieldChanged;

        public abstract BoxCollider2D Box { get; }

        public T GetNodeById(TId id) => Nodes.GetById(id);
        public V GetViewById(TId id) => Views.GetById(id);


        protected void NotifyNodeClicked(T node, PointerEventData.InputButton btn, InputSnapshot input)
        {
            NodeClicked?.Invoke(node, btn, input);
        }

        protected void NotifyFieldClicked(PointerEventData.InputButton btn, InputSnapshot input)
        {
            FieldClicked?.Invoke(btn, input);
        }

        protected void NotifyNodeMoved(T node, Vector2 pos)
        {
            NodeMoved?.Invoke(node, pos);
            NotifyFieldChanged();
        }

        protected void NotifyFieldChanged()
        {
            FieldChanged?.Invoke();
        }
    }
}