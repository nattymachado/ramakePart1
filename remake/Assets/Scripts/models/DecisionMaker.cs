using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

sealed class DecisionMaker
{
    private static readonly DecisionMaker instance = new DecisionMaker();
    Dictionary<string, List<int>> inputs;

    public static DecisionMaker Instance {
        get
        {
            return instance;
        }
    }

    public Dictionary<string, List<int>> Inputs
    {
        get
        {
            return inputs;
        }

        set
        {
            inputs = value;
        }
    }

    private DecisionMaker()
    {
        Inputs = new Dictionary<string, List<int>>();
        /*Inputs["lastColors"] = new List<int>();
        Inputs["lastColor"].AddRange(new [] { 0,0});
        Inputs["newColors"] = new List<int>();
        Inputs["newColors"].AddRange(new[] { 0, 0 });*/
    }

    public int DecideNewColor(bool isFirst, Grid grid)
    {
        int colorCode = 0;
        if (isFirst)
        {
            colorCode = CheckTheGrid(grid);
        } else
        {
            colorCode = Random.Range(0, Constants.ColorDefinitionsKeys.Count);
        }
        
        return colorCode;
    }

    public void IncludeLastColors(int colorCode, bool isFirst)
    {
        if (Inputs["lastColors"] == null)
        {
            Inputs["lastColors"] = new List<int>();
        } else
        {
            if (isFirst)
            {
                Inputs["lastColors"].Insert(0,colorCode);
            } else
            {
                Inputs["lastColors"].Insert(1, colorCode);
            }
        } 
    }

    public void CheckLastColors(int colorCode)
    {
       if (Inputs["lastColors"][0] == colorCode || Inputs["lastColors"][1] == colorCode)
        {

        }
    }

    public int CheckTheGrid(Grid grid)
    {
        List<int> pointColors = grid.GetLastColors();
        return pointColors.IndexOf(pointColors.Max(x => x)); 
    }

}
