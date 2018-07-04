using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PillState { HORIZONTAL, VERTICAL};

public class Pill
{
    public int Id { get; set; }
    public Dictionary<string, PillPart> PillParts { get; set; }
    public PillState State { get; set; }

    public Pill(int id, Transform parent, Transform self)
    {
        Id = id;
        PillParts = new Dictionary<string, PillPart> { { "first", null}, { "second", null } };
        PillParts["first"] = new PillPart(id, true);
        PillParts["first"].PositionColumn = Constants.InitPositionColumnPillPart0;
        PillParts["second"] = new PillPart(id, false);
        PillParts["second"].PositionColumn = Constants.InitPositionColumnPillPart1;
        PillParts["first"].PositionRow = PillParts["second"].PositionRow = Constants.Rows - 1;
        self.name = "pill" + Id;
        //self.position = new Vector3(Constants.PillInitPositionX, Constants.PillInitPositionY, 0);
        SpriteRenderer[] allChildren = self.GetComponentsInChildren<SpriteRenderer>();
        List<GameObject> pillParts = new List<GameObject>();
        foreach (SpriteRenderer child in allChildren)
        {
            pillParts.Add(child.gameObject);
        }
        PillParts["first"].Behaviour = pillParts[0].GetComponent<PillPartBehaviour>();
        PillParts["second"].Behaviour = pillParts[1].GetComponent<PillPartBehaviour>();
        PillParts["first"].Behaviour.PillPartObj = PillParts["first"];

        PillParts["second"].Behaviour.PillPartObj = PillParts["second"];
        State = PillState.HORIZONTAL;
    }

}
