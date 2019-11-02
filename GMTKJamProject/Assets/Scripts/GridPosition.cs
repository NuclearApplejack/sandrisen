using System;
using System.Collections.Generic;
using UnityEngine;



public class GridPosition
{
    public int row;
    public int column;

    public GridPosition(int rowS, int columnS)
    {
        row = rowS;
        column = columnS;
    }

    public static bool ComparePos(GridPosition a, GridPosition b)
    {
        if (a.row == b.row && a.column == b.column)
        {
            return true;
        }
        return false;
    }
}

