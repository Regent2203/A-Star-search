using EasyField.Links;
using EasyField.Links.Factories;
using EasyField.Links.Implementations;
using EasyField.Links.Providers;
using EasyField.Links.ViewMovers;
using EasyField.ObjectsStorages;

namespace EasyField.Implementations.Vertexes
{
    public class VertexesLinksBuilder : LinksBuilder<VertexData, VertexView, int>
    {
        public VertexesLinksBuilder(LinkDataFactory<VertexData, int> linkDatasFactory, LinkViewFactory<int> linkViewsFactory,
            StoredLinksProvider<LinkData<int>, int> linksProvider, LinkViewCoordinator<VertexView, int> linkViewCoordinator,
            DictTypeStorage<LinkData<int>, LinkKey<int>> linkDatas, DictTypeStorage<LinkView<int>, LinkKey<int>> linkViews)
            : base(linkDatasFactory, linkViewsFactory, linksProvider, linkViewCoordinator, linkDatas, linkViews)
        { }
    }
}