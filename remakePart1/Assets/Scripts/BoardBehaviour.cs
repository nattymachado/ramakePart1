using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBehaviour : MonoBehaviour {

    public GameObject prefab;

    // Use this for initialization
    void Start () {
        int i;
        for (i=0; i < 20; i++)
        {
            Debug.Log(i);
            float y = i * 1;
            float x = i * Random.value;
            Debug.Log(x);
            Vector3 pos = new Vector3(x, y, 0);
            var myNewObject = Instantiate(prefab, pos, Quaternion.Euler(0, 0, 180));
            myNewObject.GetComponent<SpriteRenderer>().color = Color.red;
            Vector3 pos2 = new Vector3(x+0.3f, y, 0);
            var myNewObject2 = Instantiate(prefab, pos2, Quaternion.Euler(0, 0, 0));
            myNewObject2.GetComponent<SpriteRenderer>().color = Color.yellow;
        }


    }
	
	// Update is called once per frame
	void Update () {

       

    }
}
