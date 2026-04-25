using System;

public static class Extensions
{
    public static bool IndexExists(this Array array, int index, int dimension)
    {
        if (array == null) 
            return false;
        
        if (dimension < 0 || dimension >= array.Rank) 
            return false;

        return index >= 0 && index < array.GetLength(dimension);
    }
}
