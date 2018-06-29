using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pill
{
    public int Id { get; set; }
    public PillPart[] PillParts { get; set; }

    public Pill(int id, GameObject pillPrefab, Transform parent)
    {
        Id = id;
        PillParts = new PillPart[2];
        PillParts[0] = new PillPart(id);
        PillParts[1] = new PillPart(id);

        GameObject pill = GameObject.Instantiate(pillPrefab, new Vector3(2.5f, 2.2f, 0), Quaternion.Euler(0, 0, 0));
        pill.transform.parent = parent;
        SpriteRenderer[] allChildren = pill.GetComponentsInChildren<SpriteRenderer>();
        List<GameObject> pillParts = new List<GameObject>();
        foreach (SpriteRenderer child in allChildren)
        {
            pillParts.Add(child.gameObject);
        }
        pillParts[0].GetComponent<PillPartBehaviour>().PillPartObj = PillParts[0];
        pillParts[1].GetComponent<PillPartBehaviour>().PillPartObj = PillParts[1];
    }

}
