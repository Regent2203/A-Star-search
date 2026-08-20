using EasyField.ObjectsStorages;
using EasyField.PathDrawers;
using EasyField.PathFinders;
using EasyField.PathSetters;
using EasyField.SceneControllers;
using UnityEngine;

namespace EasyField.Implementations.Cells
{
    public class CellsPathfindRunner : PathfindRunner<CellData, CellView, Vector2Int>
    {
        public CellsPathfindRunner(PathSetter<CellData> pathSetter, PathFinder<CellData> pathFinder, IPathDrawer<CellView> pathDrawer, 
            IObjectsStorage<CellView, Vector2Int> nodeViews) : base(pathSetter, pathFinder, pathDrawer, nodeViews)
        {
        }
    }
}