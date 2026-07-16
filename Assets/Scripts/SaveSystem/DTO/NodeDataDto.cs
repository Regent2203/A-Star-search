namespace ThisProject.SaveSystem.Dto
{
    public abstract class NodeDataDto<TId>
    {
        public TId Id;
        public Vector2Dto NodePosition;
    }
}