using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Grid
{
    private GridItem[,] _data = new GridItem[Constants.Rows, Constants.Columns];

    public Grid()
    {
        for (int row = 0; row < Constants.Rows; row++)
        {
            for (int column = 0; column < Constants.Columns; column++)
            {
                _data[row,column] = null;
            }
        }
    }

    public void IncludePositionsOnBoard(GridItem item)
    {
        _data[item.GetPositionRow(), item.GetPositionColumn()] = item;
    }

    public void CleanPositionsOnBoard(GridItem item)
    {
        _data[item.GetPositionRow(), item.GetPositionColumn()] = null;
    }

    public bool IsPositionEmpty(int horizontal, int vertical)

    {

        if (horizontal < 0 || horizontal >= Constants.Rows || vertical < 0 || vertical >= Constants.Columns)
        {
            return false;
        }
        else
        {
            return _data[horizontal, vertical] == null;
        }
    }

    public Dictionary<string, int> GetEmptyPosition()
    {
        Dictionary<string, int> position = GetPosition();
        while (_data[position["row"], position["column"]] != null)
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

    public IEnumerator CheckMatches(Dictionary<string, Dictionary<string, int>> positions)
    {
        List<string> keys = new List<string>(positions.Keys);
        for (int index = 0; index < keys.Count; index++)
        {
            if (positions[keys[index]] != null)
            {
                List<int[]> matchesHorizontal = GetMatchesHorizontal(positions[keys[index]]["row"], positions[keys[index]]["column"]);
                List<int[]> matchesVertical = GetMatchesVertical(positions[keys[index]]["row"], positions[keys[index]]["column"]);

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
    }

    private void RemoveMatchPills(List<int[]> matches)
    {
       List<GridItem> items = new List<GridItem>();
        matches.Reverse();
        for (int index = 0; index < matches.Count; index++)
        {
            if (_data[matches[index][0], matches[index][1]] != null)
            {
                items.Add(_data[matches[index][0], matches[index][1]]);
                items[index].SetAsDestroyed();
                _data[matches[index][0], matches[index][1]] = null;
            }
            
            
        }

        for (int index = 0; index < items.Count;  index++)
        {
            items[index].DestroyItem();
        }
    }

    private void UpdateSpriteOfMatches(List<int[]> matches)
    {

        for (int index = 0; index < matches.Count; index++)
        {
            if (_data[matches[index][0], matches[index][1]] != null)
            {
                _data[matches[index][0], matches[index][1]].UpdateToTransparent();
            }
        }
    }

    private List<int[]> GetMatchesHorizontal(int row, int column)
    {

        List<int[]> matches = new List<int[]>();
        if (_data[row, column] != null)
        {
            matches.Add(new int[2] { row, column });
            GetMatchesHorizontalRight(row, column, _data[row, column].GetColor(), matches);
            GetMatchesHorizontalLeft(row, column, _data[row, column].GetColor(), matches);
        }
        if (matches.Count >= 4)
        {
            return matches;
        }
        else
        {
            return null;
        }
    }

    private List<int[]> GetMatchesVertical(int row, int column)
    {

        List<int[]> matches = new List<int[]>();
        if (_data[row, column] != null)
        {
            matches.Add(new int[2] { row, column });
            GetMatchesVerticalUp(row, column, _data[row, column].GetColor(), matches);
            GetMatchesVerticalDown(row, column, _data[row, column].GetColor(), matches);
        }
        if (matches.Count >= 4)
        {
            return matches;
        }
        else
        {
            return null;
        }
    }

    private void GetMatchesHorizontalRight(int row, int column, Color value, List<int[]> matches)
    {
        column += 1;
        for (; column < Constants.Columns; column++)
        {
            if (_data[row, column] != null)
            {
                Color itemValue = _data[row, column].GetColor();
                if (itemValue == value)
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

    private void GetMatchesHorizontalLeft(int row, int column, Color value, List<int[]> matches)
    {
        column -= 1;
        for (; column >= 0; column--)
        {
            if (_data[row, column] != null)
            {
                Color itemValue = _data[row, column].GetColor();
                if (itemValue == value)
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


    private void GetMatchesVerticalDown(int row, int column, Color value, List<int[]> matches)
    {
        row -= 1;
        for (; row >= 0; row--)
        {
            if (_data[row, column] != null)
            {
                Color itemValue = _data[row, column].GetColor();
                if (itemValue == value)
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

    private void GetMatchesVerticalUp(int row, int column, Color value, List<int[]> matches)
    {
        row += 1;
        for (; row < Constants.Rows; row++)
        {
            if (_data[row, column] != null )
            {
                Color itemValue = _data[row, column].GetColor();
                if (itemValue == value)
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



}
