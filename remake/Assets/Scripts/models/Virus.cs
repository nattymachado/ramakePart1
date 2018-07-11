using System.Collections.Generic;
using UnityEngine;

public class Virus : GridItem
{
    public Color VirusColor { get; set; }
    public Sprite VirusSprite { get; set; }
    public int PositionRow { get; set; }
    public int PositionColumn { get; set; }
    public VirusBehaviour Behaviour { get; set; }
    public bool IsDestroyed { get; set; }

    public bool FinalizedMoviment() {
            return true;
    }

    public bool OnlyDownMoviment()
    {
        return false;
    }

    public Virus(Color virusColor, Transform parent, Dictionary<string, int> position, Transform self, VirusBehaviour behaviuor) {

        VirusColor = virusColor;
        PositionRow = position["row"];
        PositionColumn = position["column"];
        Behaviour = behaviuor;
        Vector3 pos = new Vector3(Constants.VirusSize * (position["column"]+1) + Constants.InitPositionColumns,
            Constants.VirusSize * (position["row"] +1) + Constants.InitPositionRows, 0);
        self.position = pos;
        self.parent = parent;
        IsDestroyed = false;
    }

    public Color GetColor()
    {
        return VirusColor;
    }

    public void UpdateToTransparent()
    {
        Behaviour.UpdateSprite();
    }

    public void DestroyItem()
    {
        Behaviour.Destroy();
    }

    public void SetAsDestroyed()
    {
    }

    public int GetPositionRow()
    {
        return PositionRow;
    }

    public int GetPositionColumn()
    {
        return PositionColumn;
    }

    public string GetGridItemType()
    {
        return "virus";
    }



}
