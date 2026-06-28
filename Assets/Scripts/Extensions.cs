using System;
using System.Collections.Generic;
using ThisProject.Nodes;
using ThisProject.ObjectsStorages;
using ThisProject.Views;

public static class Extensions
{
    public static bool IsIndexWithinBounds(this Array grid, int i, int j)
    {
        return i >= 0 && i < grid.GetLength(0) && j >= 0 && j < grid.GetLength(1);
    }
    
    public static void NodesToViewsNonAlloc<T, V, TId>(this IObjectsStorage<V, TId> views, IList<T> nodesList, IList<V> viewsList)
        where T : class, INode<TId>
        where V : class, IView<TId>
    {
        viewsList.Clear();

        for (int i = 0; i < nodesList.Count; i++)
        {
            viewsList.Add(views.GetItemById(nodesList[i].Id));
        }
    }
}
