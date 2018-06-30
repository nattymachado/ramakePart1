using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Grid
{
    private int[,] _data = new int[Constants.Rows, Constants.Columns];

    public Grid()
    {
        for (int row = 0; row < Constants.Rows; row++)
        {
            for (int column = 0; column < Constants.Columns; column++)
            {
                _data[row,column] = 0;
            }
        }
    }

    public void IncludePositionsOnBoard(int positionRow, int positionColumn, string color)
    {
        _data[positionRow, positionColumn] = Constants.ColorToInt[color];
    }

    public bool IsPositionEmpty(int horizontal, int vertical)

    {

        if (horizontal < 0 || horizontal >= Constants.Rows || vertical < 0 || vertical >= Constants.Columns)
        {
            return false;
        }
        else
        {
            return _data[horizontal, vertical] == 0;
        }
    }

    public Dictionary<string, int> GetEmptyPosition()
    {
        Dictionary<string, int> position = GetPosition();
        while (_data[position["row"], position["column"]] != 0)
        {
            position = GetPosition();
        }
        return position;
    }

    private Dictionary<string, int> GetPosition()
    {
        Dictionary<string, int> position = new Dictionary<string, int> { { "row", 0 }, { "column", 0 } };
        position["row"] = (Random.Range(0, (Constants.Rows - 5)));
        position["column"] = (Random.Range(0, (Constants.Columns - 1)));
        return position;
    }

}
