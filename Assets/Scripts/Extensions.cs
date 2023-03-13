using System;

public static class Extensions
{
    public static bool IndexExists(this Array array, int index, int dimension = 0)
    {
        return index >= 0 && index < array.GetLength(dimension);
    }
}
