using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board {

    public SpriteRenderer[,] mainBoard = new SpriteRenderer[Constants.Rows, Constants.Columns];
    private Sprite transparentPill;
    public int killedVirus = 0;
    

    public Board(Sprite transparentPill)
    {
        this.transparentPill = transparentPill;
    }

    private void GetMatchesHorizontalRight(int row, int column, Color pillColor, List<int[]> matches)
    {
        column += 1;
        for (;column < Constants.Columns; column++)
        {
            if (mainBoard[row,column] != null)
            {
                Color itemColor = mainBoard[row, column].color;
                if (itemColor == Color.white)
                {
                    itemColor = mainBoard[row, column].GetComponent<VirusBehaviour>().color;
                }
                if (itemColor == pillColor)
                {
                    matches.Add(new int[2] { row, column });
                }
                else
                {
                    break;
                }
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
            if (mainBoard[row, column] != null)
            {
                Color itemColor = mainBoard[row, column].color;
                if (itemColor == Color.white)
                {
                    itemColor = mainBoard[row, column].GetComponent<VirusBehaviour>().color;
                }
                if (itemColor == pillColor)
                {
                    matches.Add(new int[2] { row, column });
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }

    }

    private void GetMatchesVerticalDown(int row, int column, Color pillColor, List<int[]> matches)
    {
        row -= 1;
        for (; row >= 0; row--)
        {
            if (mainBoard[row, column] != null)
            {
                Color itemColor = mainBoard[row, column].color;
                if (itemColor == Color.white)
                {
                    itemColor = mainBoard[row, column].GetComponent<VirusBehaviour>().color;
                }
                if (itemColor == pillColor)
                {
                    matches.Add(new int[2] { row, column });
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }

    }

    private void GetMatchesVerticalUp(int row, int column, Color pillColor, List<int[]> matches)
    {
        row += 1;
        for (; row < Constants.Rows; row++)
        {
            if (mainBoard[row, column] != null )
            {
                Color itemColor = mainBoard[row, column].color;
                if (itemColor == Color.white)
                {
                    itemColor = mainBoard[row, column].GetComponent<VirusBehaviour>().color;
                }
                if (itemColor == pillColor)
                {
                    matches.Add(new int[2] { row, column });
                } else
                {
                    break;
                }

            } else
                {
                    break;
                }
            }

    }



    private List<int[]> GetMatchesHorizontal(int row, int column)
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

    private List<int[]> GetMatchesVertical(int row, int column)
    {

        List<int[]> matches = new List<int[]>();
        if (mainBoard[row, column])
        {
            matches.Add(new int[2] { row, column });
            Color pillColor = mainBoard[row, column].color;
            GetMatchesVerticalUp(row, column, pillColor, matches);
            GetMatchesVerticalDown(row, column, pillColor, matches);
        }
        if (matches.Count >= 3)
        {
            return matches;
        }
        else
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
            if (mainBoard[matches[index][0], matches[index][1]] != null)
            {
                PillPartBehaviour pillPart = mainBoard[matches[index][0], matches[index][1]].GetComponent<PillPartBehaviour>();
                if (pillPart == null)
                {
                    mainBoard[matches[index][0], matches[index][1]].GetComponent<VirusBehaviour>().Destroy();
                    killedVirus += 1;
                }

                mainBoard[matches[index][0], matches[index][1]] = null;

                if (pillPart != null)
                {
                    PillBehaviour pillBehaviour = pillPart.transform.parent.GetComponent<PillBehaviour>();
                    pillPart.Destroy();
                }
            }
            
        }
    }
    

    public IEnumerator CheckMatches(int[,] positions)
    {
        for (int index = 0; index < positions.GetLength(0); index++)
        {
            List<int[]> matchesHorizontal = GetMatchesHorizontal(positions[index, 0], positions[index, 1]);
            List<int[]> matchesVertical = GetMatchesVertical(positions[index, 0], positions[index, 1]);
            
            if (matchesHorizontal != null)
            {
                UpdateSpriteOfMatches(matchesHorizontal);
                yield return new WaitForSeconds(Constants.WaitBeforePotentialMatchesCheck);
                RemoveMatchPills(matchesHorizontal);
            }

            if (matchesVertical != null)
            {
                UpdateSpriteOfMatches(matchesVertical);
                yield return new WaitForSeconds(Constants.WaitBeforePotentialMatchesCheck);
                RemoveMatchPills(matchesVertical);
            }

        }
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
       
        if (horizontal < 0 || horizontal >= Constants.Rows || vertical < 0 || vertical >= Constants.Columns)
        {
            return false;
        } else
        {
            return mainBoard[horizontal,vertical] ==  null;
        }
    }
}
