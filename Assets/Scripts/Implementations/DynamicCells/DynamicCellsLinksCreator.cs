using EasyField.Links;
using EasyField.Links.Factories;
using EasyField.Links.Implementations;
using EasyField.Links.Providers;
using EasyField.Links.ViewMovers;
using EasyField.ObjectsStorages;
using UnityEngine;

namespace EasyField.Implementations.Cells.DynamicCells
{
    public class DynamicCellsLinksCreator : LinksCreator<CellData, CellView, LinkData<Vector2Int>, Vector2Int>
    {
        public DynamicCellsLinksCreator(SmartLinkDataFactory<CellData, LinkData<Vector2Int>, Vector2Int> linkDatasFactory,
            LinkViewFactory<Vector2Int> linkViewsFactory,
            StoredLinksProvider<LinkData<Vector2Int>, Vector2Int> linksProvider, LinkViewCoordinator<CellView, Vector2Int> linkViewCoordinator,
            DictTypeStorage<LinkData<Vector2Int>, DualKey<Vector2Int>> linkDatas, DictTypeStorage<LinkView<Vector2Int>, DualKey<Vector2Int>> linkViews, 
            bool useDual)
            : base(linkDatasFactory, linkViewsFactory, linksProvider, linkViewCoordinator, linkDatas, linkViews, useDual)
        { 
        }
    }
}