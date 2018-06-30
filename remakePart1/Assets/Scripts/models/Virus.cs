using System.Collections.Generic;
using UnityEngine;

public class Virus
{
    public Color VirusColor { get; set; }
    public Sprite VirusSprite { get; set; }
    public int PositionRow { get; set; }
    public int PositionColumn { get; set; }

    public Virus(Color virusColor, Transform parent, Dictionary<string, int> position, Transform self) {

        VirusColor = virusColor;
        PositionRow = position["row"];
        PositionColumn = position["column"];
        Vector3 pos = new Vector3(Constants.VirusSize * position["column"] + Constants.InitPositionColumns,
            Constants.VirusSize * position["row"] + Constants.InitPositionRows, 0);
        self.position = pos;
        self.parent = parent;
    }

    
    
}
