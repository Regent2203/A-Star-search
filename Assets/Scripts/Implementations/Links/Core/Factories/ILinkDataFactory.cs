namespace EasyField.Links.Factories
{
    public interface ILinkDataFactory<TLinkData, TId>
        where TLinkData : ILinkData<TId>
    {
        public TLinkData CreateItem(TId fromId, TId toId, float cost);
        public void DeleteItem(TLinkData item);
    }
}