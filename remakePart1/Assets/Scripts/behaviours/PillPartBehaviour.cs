using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillPartBehaviour : MonoBehaviour
{

    private PillPart _pillPartObj;


    public PillPart PillPartObj {
        get {
            return this._pillPartObj;
        }
        set
        {
            this._pillPartObj = value;
            this.UpdatePrefabParameters();
        }
    }

    private void UpdatePrefabParameters()
    {
        SpriteRenderer pillPartSpriteRender = GetComponent<SpriteRenderer>();
        pillPartSpriteRender.color = this._pillPartObj.PillPartColor;
        Debug.Log(pillPartSpriteRender.color);
    }

    public void Start()
    {
        Debug.Log("Starting Pill Part...");
    }


}