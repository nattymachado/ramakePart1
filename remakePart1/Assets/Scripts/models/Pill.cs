using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pill
{
    public int Id { get; set; }
    public PillPart[] PillParts { get; set; }

    public Pill(int id, Transform parent, Transform self)
    {
        Id = id;
        PillParts = new PillPart[2];
        PillParts[0] = new PillPart(id);
        PillParts[0].PositionColumn = Constants.InitPositionColumnPillPart0;
        PillParts[1] = new PillPart(id);
        PillParts[1].PositionColumn = Constants.InitPositionColumnPillPart1;
        PillParts[0].PositionRow = PillParts[1].PositionRow = Constants.Rows - 1;

        self.parent = parent;
        self.position = new Vector3(Constants.PillInitPositionX, Constants.PillInitPositionY, 0);
        SpriteRenderer[] allChildren = self.GetComponentsInChildren<SpriteRenderer>();
        List<GameObject> pillParts = new List<GameObject>();
        foreach (SpriteRenderer child in allChildren)
        {
            pillParts.Add(child.gameObject);
        }
        pillParts[0].GetComponent<PillPartBehaviour>().PillPartObj = PillParts[0];
        pillParts[1].GetComponent<PillPartBehaviour>().PillPartObj = PillParts[1];
    }

}
