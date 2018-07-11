using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillPart : GridItem
{
    public int ParentId { get; set; }
    public int PositionRow { get; set; }
    public int PositionColumn { get; set; }
    public bool IsDestroyed { get; set; }
    public bool IsAlone { get; set; }
    public bool IsFirst { get; set; }
    public Color PillPartColor { get; set; }
    public PillPartBehaviour Behaviour { get; set; }

    public Color GetColor()
    {
        if (!Behaviour.PillPartObj.IsDestroyed)
        {
            return Behaviour.GetComponent<SpriteRenderer>().color;
        }
        return Color.black;
       
    }

    public int GetPositionRow()
    {
        return PositionRow;
    }

    public int GetPositionColumn()
    {
        return PositionColumn;
    }

    public void UpdateToTransparent()
    {
       Behaviour.UpdateSprite("transparent");
    }

    public void DestroyItem()
    {
        Behaviour.Destroy();
    }

    public void SetAsDestroyed()
    {
        IsDestroyed = true;
    }

    public string GetGridItemType()
    {
        return "pillPart";
    }

    public PillPart(int parentId, bool isFirst, Grid grid)
    {
        IsDestroyed = false;
        IsAlone = false;
        IsFirst = isFirst;
        ParentId = parentId;
        int keyIndex = DecisionMaker.Instance.DecideNewColor(isFirst, grid);
        string colorKey = Constants.ColorDefinitionsKeys[keyIndex];
        PillPartColor = Constants.ColorsDefinitions[colorKey];
    }

    public bool FinalizedMoviment()
    {
        return Behaviour.GetComponentInParent<PillBehaviour>().finishedMoviment;
    }






}
