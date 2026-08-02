using ThisProject.Links;
using ThisProject.Links.Factories;
using ThisProject.Links.Implementations;
using ThisProject.Links.Providers;
using ThisProject.Links.ViewMovers;
using ThisProject.ObjectsStorages;

namespace ThisProject.Implementations.Vertexes
{
    public class VertexesLinksBuilder : LinksBuilder<VertexData, VertexView, int>
    {
        public VertexesLinksBuilder(LinkDataFactory<VertexData, int> linkDatasFactory, LinkViewFactory<int> linkViewsFactory,
            StoredLinksProvider<LinkData<int>, int> linksProvider, LinkViewCoordinator<VertexView, int> linkViewCoordinator,
            DictTypeStorage<LinkData<int>, LinkKey<int>> linkDatas, DictTypeStorage<LinkView<int>, LinkKey<int>> linkViews,
            LinkDataPool<int> linkDatasPool, LinkViewPool<int> linkViewsPool,
            IObjectsStorage<VertexView, int> nodeViews)
            : base(linkDatasFactory, linkViewsFactory, linksProvider, linkViewCoordinator, linkDatas, linkViews, linkDatasPool, linkViewsPool, nodeViews)
        {
        }
    }
}