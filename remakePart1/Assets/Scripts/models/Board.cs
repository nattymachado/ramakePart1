using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    public int[,] mainBoard = new int[Constants.Rows, Constants.Columns];

    public Board()
    {
        for (int row = 0; row < Constants.Rows; row++)
        {
            for (int column = 0; column < Constants.Columns; column++)
            {
                mainBoard[row,column] = 0;
            }
        }
    }

}
