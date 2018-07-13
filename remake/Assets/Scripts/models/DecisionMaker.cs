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
    }

    public int DecideNewColor(bool isFirst, Grid grid, int level)
    {
        int colorCode = 0;
        if (isFirst)
        {
            colorCode = CheckTheGrid(grid, level);
        } else
        {
            colorCode = Random.Range(0, Constants.ColorDefinitionsKeys.Count);
        }
        
        return colorCode;
    }

    public int CheckTheGrid(Grid grid, int level)
    {
        List<int> pointColors = grid.GetColorPoints();
        float random = Random.value;

        if (level < 5)
        {
            if (random > 0.5f)
            {
                return pointColors.IndexOf(pointColors.Max(x => x));
            } else
            {
                return Random.Range(0, Constants.ColorDefinitionsKeys.Count);
            }
        } else  if (level >= 5 && level < 10)
        {
            if (random > 0.7f)
            {
                return pointColors.IndexOf(pointColors.Max(x => x));
            }
            else
            {
                return Random.Range(0, Constants.ColorDefinitionsKeys.Count);
            }
        }
        else if (level >= 10 && level < 15)
        {
            return Random.Range(0, Constants.ColorDefinitionsKeys.Count);
        }
        else if (level >= 15 && level < 20)
        {
            if (random > 0.3f)
            {
                return pointColors.IndexOf(pointColors.Min(x => x));
            }
            else
            {
                return Random.Range(0, Constants.ColorDefinitionsKeys.Count);
            }
        }
        else
        {
            return Random.Range(0, Constants.ColorDefinitionsKeys.Count);
        }
         
    }

}
