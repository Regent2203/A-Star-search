using System;

public static class Extensions
{
    public static bool IsWithinBounds(this Array grid, int i, int j)
    {
        return i >= 0 && i < grid.GetLength(0) && j >= 0 && j < grid.GetLength(1);
    }
}
