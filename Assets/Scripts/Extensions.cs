using System;
using System.Collections;
using System.Collections.Generic;
using ThisProject.Nodes;
using ThisProject.ObjectsStorages;
using UnityEngine;

public static class Extensions
{
    public static bool IsIndexWithinBounds(this Array grid, int i, int j)
    {
        return i >= 0 && i < grid.GetLength(0) && j >= 0 && j < grid.GetLength(1);
    }


    public static Vector2 Clamp(this Vector2 value, Bounds bounds)
    {
        return (Vector2)Clamp((Vector3)value, bounds);
    }

    public static Vector2 Clamp(this Vector2 value, Bounds bounds, Vector2 offset)
    {
        return (Vector2)Clamp((Vector3)value, bounds, (Vector3)offset);
    }

    public static Vector3 Clamp(this Vector3 value, Bounds bounds)
    {
        return value.Clamp(bounds, Vector3.zero);
    }

    public static Vector3 Clamp(this Vector3 value, Bounds bounds, Vector3 offset)
    {
        return new Vector3(
            Mathf.Clamp(value.x, bounds.min.x + offset.x, bounds.max.x - offset.x),
            Mathf.Clamp(value.y, bounds.min.y + offset.y, bounds.max.y - offset.y),
            value.z
        );
    }


    public static void NodesToViewsNonAlloc<T, V, TId>(this IObjectsStorage<V, TId> views, IList<T> nodesList, IList<V> outViewsList)
        where T : class, INodeData<TId>
        where V : class, INodeView<TId>
    {
        outViewsList.Clear();

        for (int i = 0; i < nodesList.Count; i++)
        {
            outViewsList.Add(views.GetItemById(nodesList[i].Id));
        }
    }
}
