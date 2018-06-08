using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board {

    public SpriteRenderer[,] mainBoard = new SpriteRenderer[16, 8];

    public void SeeBoard()
    {
        string data = "";
        Debug.Log(mainBoard.GetLength(1));
        Debug.Log(mainBoard.GetLength(0));
        for (int horizontal = 0; horizontal < mainBoard.GetLength(0); horizontal++)
        {
            data = "";
            for (int vertical = 0; vertical < mainBoard.GetLength(1); vertical++)
            {
                if (mainBoard[horizontal, vertical])
                {
                    data = data + "|" + mainBoard[horizontal, vertical].color;
                } else
                {
                    data = data + "|" + " ";
                }
               
            }
            Debug.Log(data);
        }
    }

    public List<SpriteRenderer> CheckCombinations(int horizontal_position, int vertical_position)
    {
        List<int[]> positionsSameColor = new List<int[]>();
        List<SpriteRenderer> positionsToDestroy = new List<SpriteRenderer>();
        CheckUpCombinations(horizontal_position, vertical_position, positionsSameColor);
        CheckDownCombinations(horizontal_position, vertical_position, positionsSameColor);
        if (positionsSameColor.Count >= 2)
        {
            for (int position = 0; position < positionsSameColor.Count; position++)
            {
                positionsToDestroy.Add(mainBoard[positionsSameColor[position][0], positionsSameColor[position][1]]);
                mainBoard[positionsSameColor[position][0], positionsSameColor[position][1]] = null;
            }
            positionsToDestroy.Add(mainBoard[horizontal_position, vertical_position]);
            mainBoard[horizontal_position, vertical_position] = null;

        }
        positionsSameColor = new List<int[]>();
        CheckRightCombinations(horizontal_position, vertical_position, positionsSameColor);
        CheckLeftCombinations(horizontal_position, vertical_position, positionsSameColor);

        
        if (positionsSameColor.Count >= 2)
        {
            for (int position = 0; position < positionsSameColor.Count; position++)
            {
                positionsToDestroy.Add(mainBoard[positionsSameColor[position][0], positionsSameColor[position][1]]);
                mainBoard[positionsSameColor[position][0], positionsSameColor[position][1]] = null;
            }
            positionsToDestroy.Add(mainBoard[horizontal_position, vertical_position]);
            mainBoard[horizontal_position, vertical_position] = null;

        }



        return positionsToDestroy;

    }

    public void RefreshBoard(List<SpriteRenderer> positionsToDestroy)
    {
        int highestHorizontal = 0;
        int verticalToCheck = 4;
        

        for (highestHorizontal = 0; highestHorizontal < 16; highestHorizontal++)
        {
            if (mainBoard[highestHorizontal, verticalToCheck] == null)
            {
                for (int i = highestHorizontal + 1; i < 16; i++)
                {
                    if (mainBoard[i, verticalToCheck] != null)
                    {
                        Debug.Log("Achei a proxima com algo");
                        mainBoard[i, verticalToCheck].transform.position = new Vector3(mainBoard[i, verticalToCheck].transform.position.x, mainBoard[i, verticalToCheck].transform.position.y - 1f, mainBoard[i, verticalToCheck].transform.position.z);
                    }
                }
            }

        }

        
    }

    public List<int[]> CheckUpCombinations(int horizontalPosition, int verticalPosition, List<int[]> positionsSameColor)
    {
        int horizontal_index = horizontalPosition + 1;
        bool isSameColor = true;
        Debug.Log("Horizontal:" + horizontalPosition);
        Debug.Log("Vertical:" + verticalPosition);
        Color color = mainBoard[horizontalPosition,verticalPosition].color;
        
        while (horizontal_index < mainBoard.GetLength(0) && (isSameColor == true))
        {
            if (mainBoard[horizontal_index, verticalPosition] != null && mainBoard[horizontal_index, verticalPosition].color == color)
            {
                positionsSameColor.Add(new int[2] { horizontal_index, verticalPosition });
            } else
            {
                isSameColor = true;
            }
            horizontal_index += 1;
        }
        return positionsSameColor;
    }

    public void CheckDownCombinations(int horizontalPosition, int verticalPosition, List<int[]> positionsSameColor)
    {
        int horizontalIndex = horizontalPosition - 1;
        bool is_same_color = true;
        Debug.Log("Horizontal:" + horizontalPosition);
        Debug.Log("Vertical:" + verticalPosition);
        Color color = mainBoard[horizontalPosition, verticalPosition].color;
        while (horizontalIndex >= 0 && (is_same_color == true))
        {
            if (mainBoard[horizontalIndex, verticalPosition] != null && mainBoard[horizontalIndex, verticalPosition].color == color)
            {
                positionsSameColor.Add(new int[2] { horizontalIndex, verticalPosition });
            }
            else
            {
                is_same_color = false;
            }
            horizontalIndex -= 1;
        }
    }

    public void CheckLeftCombinations(int horizontalPosition, int verticalPosition, List<int[]> positionsSameColor)
    {
        int verticalIndex = verticalPosition + 1;
        bool is_same_color = true;
        Debug.Log("Horizontal:" + horizontalPosition);
        Debug.Log("Vertical:" + verticalPosition);
        Color color = mainBoard[horizontalPosition, verticalPosition].color;
        while (verticalIndex < 8 && (is_same_color == true))
        {
            if (mainBoard[horizontalPosition, verticalIndex] != null && mainBoard[horizontalPosition, verticalIndex].color == color)
            {
                positionsSameColor.Add(new int[2] { horizontalPosition, verticalIndex });
            }
            else
            {
                is_same_color = false;
            }
            verticalIndex += 1;
        }
    }

    public void CheckRightCombinations(int horizontalPosition, int verticalPosition, List<int[]> positionsSameColor)
    {
        int verticalIndex = verticalPosition - 1;
        bool is_same_color = true;
        Debug.Log("Horizontal:" + horizontalPosition);
        Debug.Log("Vertical:" + verticalPosition);
        Color color = mainBoard[horizontalPosition, verticalPosition].color;
        while (verticalIndex >= 0 && (is_same_color == true))
        {
            if (mainBoard[horizontalPosition, verticalIndex] != null && mainBoard[horizontalPosition, verticalIndex].color == color)
            {
                positionsSameColor.Add(new int[2] { horizontalPosition, verticalIndex });
            }
            else
            {
                is_same_color = false;
            }
            verticalIndex -= 1;
        }
    }



    public List<int[]> CheckCombinationsWithOrientation(int horizontalPosition, int verticalPosition, List<int[]> positionsSameColor)
    {
        int horizontal_index = horizontalPosition + 1;
        bool isSameColor = true;
        Color color = mainBoard[horizontalPosition, verticalPosition].color;

        while (horizontal_index < mainBoard.GetLength(0) && (isSameColor == true))
        {
            if (mainBoard[horizontal_index, verticalPosition] != null && mainBoard[horizontal_index, verticalPosition].color == color)
            {
                positionsSameColor.Add(new int[2] { horizontal_index, verticalPosition });
            }
            else
            {
                isSameColor = true;
            }
            horizontal_index += 1;
        }
        return positionsSameColor;
    }

    public bool IsPositionEmpty(int horizontal, int vertical)

    {
       
        if (horizontal < 0 || horizontal >= mainBoard.GetLength(0) || vertical < 0 || vertical >= mainBoard.GetLength(1))
        {
            return false;
        } else
        {
            return mainBoard[horizontal,vertical] ==  null;
        }
    }
}
