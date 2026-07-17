using System;

namespace ThisProject.SaveSystem.Dto
{
    [Serializable]
    public class NodeDataDto<TId>
    {
        public TId Id;
        public Vector2Dto NodePosition;

        public NodeDataDto(TId id, Vector2Dto nodePosition)
        {
            Id = id;
            NodePosition = nodePosition;
        }
    }
}