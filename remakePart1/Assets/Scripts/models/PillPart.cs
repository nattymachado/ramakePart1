using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillPart
{
    public int ParentId { get; set; }
    public bool IsDestroyed { get; set; }
    public bool IsAlone { get; set; }
    public Color PillPartColor { get; set; }

    public PillPart(int parentId)
    {
        IsDestroyed = false;
        IsAlone = false;
        ParentId = parentId;
        int keyIndex = Random.Range(0, Constants.ColorDefinitionsKeys.Count);
        string colorKey = Constants.ColorDefinitionsKeys[keyIndex];
        PillPartColor = Constants.ColorsDefinitions[colorKey];
    }

}
