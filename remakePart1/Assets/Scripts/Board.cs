using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board {

    public SpriteRenderer[,] mainBoard = new SpriteRenderer[Constants.Rows, Constants.Columns];
    private Sprite transparentPill;

    public Board(Sprite transparentPill)
    {
        this.transparentPill = transparentPill;
    }

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

    private void GetMatchesHorizontalRight(int row, int column, Color pillColor, List<int[]> matches)
    {
        column += 1;
        for (;column < Constants.Columns; column++)
        {
            if (mainBoard[row,column] != null && mainBoard[row, column].color == pillColor)
            {
                matches.Add(new int[2] { row, column });
            } else
            {
                break;
            }
        }
        
    }

    private void GetMatchesHorizontalLeft(int row, int column, Color pillColor, List<int[]> matches)
    {
        column -= 1;
        for (; column >= 0; column--)
        {
            if (mainBoard[row, column] != null && mainBoard[row, column].color == pillColor)
            {
                matches.Add(new int[2] { row, column });
            }
            else
            {
                break;
            }
        }

    }


    private List<int[]> GetMatches(int row, int column)
    {

        List<int[]> matches = new List<int[]>();
        if (mainBoard[row, column])
        {
            matches.Add(new int[2] { row, column });
            Color pillColor = mainBoard[row, column].color;
            GetMatchesHorizontalRight(row, column, pillColor, matches);
            GetMatchesHorizontalLeft(row, column, pillColor, matches);
        }
        if (matches.Count >= 3)
        {
            return matches;
        } else
        {
            return null;
        }
        
    }

    private void UpdateSpriteOfMatches(List<int[]> matches)
    {
        
        for ( int index=0; index < matches.Count; index++)
        {
            mainBoard[matches[index][0], matches[index][1]].sprite = this.transparentPill;
        }
    }

    private void RemoveMatchPills(List<int[]> matches)
    {

        for (int index = 0; index < matches.Count; index++)
        {
            //Transform pillTransform = mainBoard[row, column].transform.parent;
            //pillTransform.GetComponent<PillBehaviour>().Destroy();
            //mainBoard[matches[index][0], matches[index][1]].sprite = null;
            mainBoard[matches[index][0], matches[index][1]].GetComponent<PillPartBehaviour>().Destroy();
            mainBoard[matches[index][0], matches[index][1]] = null;
        }
    }
    

    public IEnumerator CheckMatches(int[,] positions)
    {
        for (int index=0; index < positions.GetLength(0); index++)
        {
            List<int[]> matches = GetMatches(positions[index,0], positions[index, 1]);
            if (matches != null)
            {
                UpdateSpriteOfMatches(matches);
                yield return new WaitForSeconds(Constants.WaitBeforePotentialMatchesCheck);
                RemoveMatchPills(matches);
            } 
            
        }
        
        /*potentialMatches = Utilities.GetPotentialMatches(shapes);
        if (potentialMatches != null)
        {
            while (true)
            {

                AnimatePotentialMatchesCoroutine = Utilities.AnimatePotentialMatches(potentialMatches);
                StartCoroutine(AnimatePotentialMatchesCoroutine);
                yield return new WaitForSeconds(Constants.WaitBeforePotentialMatchesCheck);
            }
        }*/
    }

    public void CheckCombinations()
    {

        Debug.Log("Checking combinations");
        /*List<int[]> positionsToDestroyHorizontal = new List<int[]>();
        positionsToDestroyHorizontal.Add(new int[2] { horizontal_position, vertical_position });
        CheckUpCombinations(horizontal_position, vertical_position, positionsToDestroyHorizontal);
        CheckDownCombinations(horizontal_position, vertical_position, positionsToDestroyHorizontal);

        List<int[]> positionsToDestroyVertical = new List<int[]>();
        positionsToDestroyVertical.Add(new int[2] { horizontal_position, vertical_position });
        CheckRightCombinations(horizontal_position, vertical_position, positionsToDestroyVertical);
        CheckLeftCombinations(horizontal_position, vertical_position, positionsToDestroyVertical);

        if (positionsToDestroyHorizontal.Count < 3)
        {
            positionsToDestroyHorizontal.Clear();
        }

        if (positionsToDestroyVertical.Count < 3)
        {
            positionsToDestroyVertical.Clear();
        }

        positionsToDestroyHorizontal.AddRange(positionsToDestroyVertical);

        return positionsToDestroyHorizontal;*/

    }

    public void RefreshBoard(List<int[]> positionsToDestroy)
    {

        positionsToDestroy.Sort(
            delegate (int[] p1, int[] p2)
            {
                if (p1[0] == p2[0])
                    return p1[1].CompareTo(p2[1]);
                else
                    return p1[0].CompareTo(p2[0]);
            });

        /*for (int line = 0; line < positionsToDestroy.Count; line++)
        {
            Debug.Log(positionsToDestroy[line][0] + "X" + positionsToDestroy[line][1]);
            int positionLineCheck = positionsToDestroy[line][0] + 1;
            while (positionLineCheck < 16 && mainBoard[positionLineCheck, positionsToDestroy[line][1]] == null)
            {
                positionLineCheck += 1;
            }
            if (positionLineCheck < 16 && mainBoard[positionLineCheck, positionsToDestroy[line][1]] != null)
            {
                mainBoard[positionLineCheck, positionsToDestroy[line][1]].transform.position = new Vector3(mainBoard[positionLineCheck, positionsToDestroy[line][1]].transform.position.x, mainBoard[positionLineCheck, positionsToDestroy[line][1]].transform.position.y - 0.9f, mainBoard[positionLineCheck, positionsToDestroy[line][1]].transform.position.z);
            }
        }*/

        int line = 0;
        if (line < positionsToDestroy.Count)
        {
            Debug.Log(positionsToDestroy[line][0] + "X" + positionsToDestroy[line][1]);
            int positionLineCheck = positionsToDestroy[line][0] + 1;
            while (positionLineCheck < 16 && mainBoard[positionLineCheck, positionsToDestroy[line][1]] == null)
            {
                positionLineCheck += 1;
            }
            if (positionLineCheck < 16 && mainBoard[positionLineCheck, positionsToDestroy[line][1]] != null)
            {
                int newPosition = (positionLineCheck - positionsToDestroy[line][0]);
                SpriteRenderer pill = mainBoard[positionLineCheck, positionsToDestroy[line][1]];
                mainBoard[positionLineCheck, positionsToDestroy[line][1]] = null;
                pill.transform.position = 
                    new Vector3(pill.transform.position.x,
                    pill.transform.position.y - (newPosition * 0.9f),
                    pill.transform.position.z);
                mainBoard[newPosition, positionsToDestroy[line][1]] = pill;


            }

        }

        //int initLine = positionsToDestroy[0]


        /*
         * if (mainBoard[i, verticalToCheck] != null)
                {
                    Debug.Log("Achei a proxima com algo");
                    mainBoard[i, verticalToCheck].transform.position = new Vector3(mainBoard[i, verticalToCheck].transform.position.x, mainBoard[i, verticalToCheck].transform.position.y - 1f, mainBoard[i, verticalToCheck].transform.position.z);
                }*/


    }

    public void CheckUpCombinations(int horizontalPosition, int verticalPosition, List<int[]> positionsToDestroy)
    {
        int horizontal_index = horizontalPosition + 1;
        bool isSameColor = true;
        Color color = mainBoard[horizontalPosition,verticalPosition].color;
        
        while (horizontal_index < mainBoard.GetLength(0) && (isSameColor == true))
        {
            if (mainBoard[horizontal_index, verticalPosition] != null && mainBoard[horizontal_index, verticalPosition].color == color)
            {
                positionsToDestroy.Add(new int[2] { horizontal_index, verticalPosition });
            } else
            {
                isSameColor = true;
            }
            horizontal_index += 1;
        }
    }

    public void CheckDownCombinations(int horizontalPosition, int verticalPosition, List<int[]> positionsToDestroy)
    {
        int horizontalIndex = horizontalPosition - 1;
        bool is_same_color = true;
        Color color = mainBoard[horizontalPosition, verticalPosition].color;
        while (horizontalIndex >= 0 && (is_same_color == true))
        {
            if (mainBoard[horizontalIndex, verticalPosition] != null && mainBoard[horizontalIndex, verticalPosition].color == color)
            {
                positionsToDestroy.Add(new int[2] { horizontalIndex, verticalPosition });
            }
            else
            {
                is_same_color = false;
            }
            horizontalIndex -= 1;
        }
    }

    public void CheckLeftCombinations(int horizontalPosition, int verticalPosition, List<int[]> positionsToDestroy)
    {
        int verticalIndex = verticalPosition + 1;
        bool is_same_color = true;
        Color color = mainBoard[horizontalPosition, verticalPosition].color;
        while (verticalIndex < 8 && (is_same_color == true))
        {
            if (mainBoard[horizontalPosition, verticalIndex] != null && mainBoard[horizontalPosition, verticalIndex].color == color)
            {
                positionsToDestroy.Add(new int[2] { horizontalPosition, verticalIndex });
            }
            else
            {
                is_same_color = false;
            }
            verticalIndex += 1;
        }
    }

    public void CheckRightCombinations(int horizontalPosition, int verticalPosition, List<int[]> positionsToDestroy)
    {
        int verticalIndex = verticalPosition - 1;
        bool is_same_color = true;
        Color color = mainBoard[horizontalPosition, verticalPosition].color;
        while (verticalIndex >= 0 && (is_same_color == true))
        {
            if (mainBoard[horizontalPosition, verticalIndex] != null && mainBoard[horizontalPosition, verticalIndex].color == color)
            {
                positionsToDestroy.Add(new int[2] { horizontalPosition, verticalIndex });
            }
            else
            {
                is_same_color = false;
            }
            verticalIndex -= 1;
        }
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
