using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillBehaviour : MonoBehaviour {

    private float _totalTime;

	// Use this for initialization
	public void Start () {
		
	}
	
	// Update is called once per frame
    public void Update () {
        _totalTime += Time.deltaTime;
        BoardBehaviour parent = transform.parent.GetComponent<BoardBehaviour>();
        Debug.Log(parent.pill);
        if (parent.pill != null)
        {
            if (transform.parent.localPosition.y < (transform.localPosition.y - 0.05f))
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 0.05f, 0f);
            }
        }
        
        
    }
}
