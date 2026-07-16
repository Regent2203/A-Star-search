namespace ThisProject.SaveSystem.Dto
{
    public interface INodeDataDto<TId>
    {
        public TId Id { get; }
        public Vector2Dto NodePosition { get; }
    }
}