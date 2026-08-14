using EasyField.Nodes;
using EasyField.ObjectsStorages;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Extensions
{
    public static bool IsIndexWithinBounds<T>(this T[,] grid, int i, int j)
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

    public static bool TryGetHitObject(this PointerEventData eventData, out GameObject hitObject)
    {
        hitObject = eventData.pointerCurrentRaycast.gameObject;
        Debug.Log(hitObject.transform.parent.name, hitObject);
        return hitObject != null;
    }


    public static void NodesToViewsNonAlloc<TNodeData, TNodeView, TId>(this IObjectsStorage<TNodeView, TId> views, IList<TNodeData> nodesList, IList<TNodeView> outViewsList)
        where TNodeData : class, INodeData<TId>
        where TNodeView : class, INodeView<TId>
    {
        outViewsList.Clear();

        for (int i = 0; i < nodesList.Count; i++)
        {
            outViewsList.Add(views.GetItem(nodesList[i].Id));
        }
    }
}
