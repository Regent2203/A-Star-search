using EasyField.Links;
using EasyField.Links.Factories;
using EasyField.Links.Implementations;
using EasyField.Links.Providers;
using EasyField.Links.ViewMovers;
using EasyField.ObjectsStorages;

namespace EasyField.Implementations.Vertexes
{
    public class VertexesLinksCreator : LinksCreator<VertexData, VertexView, int>
    {
        public VertexesLinksCreator(SmartLinkDataFactory<VertexData, int> linkDatasFactory, LinkViewFactory<int> linkViewsFactory,
            StoredLinksProvider<LinkData<int>, int> linksProvider, LinkViewCoordinator<VertexView, int> linkViewCoordinator,
            DictTypeStorage<LinkData<int>, DualKey<int>> linkDatas, DictTypeStorage<LinkView<int>, DualKey<int>> linkViews, bool useDual)
            : base(linkDatasFactory, linkViewsFactory, linksProvider, linkViewCoordinator, linkDatas, linkViews, useDual)
        { }
    }
}