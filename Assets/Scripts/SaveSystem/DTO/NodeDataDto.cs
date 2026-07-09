using System;

namespace ThisProject.SaveSystem.Dto
{
    [Serializable]
    public abstract class NodeDataDto<TId>
    {
        public TId Id;
        public Vector2Dto NodePosition;
    }
}