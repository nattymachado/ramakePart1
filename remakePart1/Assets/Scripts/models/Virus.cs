using System.Collections.Generic;
using UnityEngine;

public class Virus
{
    public Color VirusColor { get; set; }
    public Sprite VirusSprite { get; set; }

    public Virus(Color virusColor, Transform parent, GameObject virusPrefab, Dictionary<string, int> position) {

        VirusColor = virusColor;
        Vector3 pos = new Vector3(Constants.VirusSize * position["column"] + Constants.InitPositionColumns,
            Constants.VirusSize * position["row"] + Constants.InitPositionRows, 0);
        GameObject virus = GameObject.Instantiate(virusPrefab, pos, Quaternion.Euler(0, 0, 0));
        virus.transform.parent = parent;
    }

    
    
}
