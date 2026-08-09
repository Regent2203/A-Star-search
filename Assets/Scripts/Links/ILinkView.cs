using UnityEngine;

namespace EasyField.Links
{
    public interface ILinkView
    {
        public void UpdatePositions(Vector2 posFrom, Vector2 posTo);
    }

    public interface ILinkView<TId> : ILinkView
    {
        public DualKey<TId> Id { get; }
        public TId From { get; }
        public TId To { get; }
    }
}
