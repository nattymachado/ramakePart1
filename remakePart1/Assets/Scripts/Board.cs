using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board {

    public SpriteRenderer[,] mainBoard = new SpriteRenderer[16, 8];

    public void SeeBoard()
    {
        string data = "";
        for (int horizontal = 0; horizontal < mainBoard.GetLength(0); horizontal++)
        {
            data = "";
            for (int vertical = 0; vertical < mainBoard.GetLength(1); vertical++)
            {
               data = data + "X" + mainBoard[horizontal, vertical].color;
            }
            Debug.Log(data);
        }
    }

    public List<SpriteRenderer> CheckCombinations(int horizontal_position, int vertical_position)
    {
        List<int[]> positions_with_same_color = CheckUpCombinations(horizontal_position, vertical_position);
        positions_with_same_color.AddRange(CheckDownCombinations(horizontal_position, vertical_position));
        List<SpriteRenderer> positionsToDestroy = new List<SpriteRenderer>();
        if (positions_with_same_color.Count >= 3)
        {
            for(int position=0; position < positions_with_same_color.Count; position++)
            {
                Debug.Log(positions_with_same_color[position][0]);
                Debug.Log(positions_with_same_color[position][1]);
                positionsToDestroy.Add(mainBoard[positions_with_same_color[position][0], positions_with_same_color[position][1]]);
                mainBoard[positions_with_same_color[position][0], positions_with_same_color[position][1]] = null;
            }
            Debug.Log(horizontal_position);
            Debug.Log(vertical_position);
            mainBoard[horizontal_position, vertical_position].sprite = null;
            mainBoard[horizontal_position, vertical_position] = null;

        }
        return positionsToDestroy;

    }

    public List<int[]> CheckUpCombinations(int horizontal_position, int vertical_position)
    {
        int horizontal_index = horizontal_position + 1;
        bool is_same_color = true;
        Color color = mainBoard[horizontal_position,vertical_position].color;
        List<int[]> positions_with_same_color = new List<int[]>();
        while (horizontal_index < mainBoard.GetLength(0) && (is_same_color == true))
        {
            if (mainBoard[horizontal_index, vertical_position] != null && mainBoard[horizontal_index, vertical_position].color == color)
            {
                positions_with_same_color.Add(new int[2] { horizontal_index, vertical_position });
            } else
            {
                is_same_color = false;
            }
            horizontal_index += 1;
        }
        return positions_with_same_color;
    }

    public List<int[]> CheckDownCombinations(int horizontal_position, int vertical_position)
    {
        int horizontal_index = horizontal_position - 1;
        bool is_same_color = true;
        Color color = mainBoard[horizontal_position, vertical_position].color;
        List<int[]> positions_with_same_color = new List<int[]>();
        while (horizontal_index >= 0 && (is_same_color == true))
        {
            if (mainBoard[horizontal_index, vertical_position] != null && mainBoard[horizontal_index, vertical_position].color == color)
            {
                positions_with_same_color.Add(new int[2] { horizontal_index, vertical_position });
            }
            else
            {
                is_same_color = false;
            }
            horizontal_index -= 1;
        }
        return positions_with_same_color;
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
