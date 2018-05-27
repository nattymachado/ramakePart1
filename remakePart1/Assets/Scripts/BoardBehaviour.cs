using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBehaviour : MonoBehaviour {

    public GameObject prefab;
    private Camera camera;
    public GameObject pill = null;
    private GameObject pillPart2 = null;
    private SpriteRenderer board = null;


    // Use this for initialization
    public void Start () {

        board = GetComponent<SpriteRenderer>();
        Debug.Log(board.transform.localPosition);
        Debug.Log(board.size);
        Debug.Log(board.size.y/2);
        camera = Camera.main;
    }
    
	// Update is called once per frame
	public void Update () {
        
        if (pill == null)
        {
            createNewPill(1);

        } else
        {
            if (board.transform.localPosition.y > (pill.transform.localPosition.y - 0.05f))
            {
                pill = null;
            }
            //Debug.Log(pill.transform.position);
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Debug.Log("Left");
            }
        }
        
        

    }

    private void createNewPill(int id)
    {

        
        Vector3 pos = new Vector3(0, board.size.y, 0);
        pill = Instantiate(prefab, pos, Quaternion.Euler(0, 0, 0));
        SpriteRenderer[] pillParts =  pill.GetComponentsInChildren<SpriteRenderer>();
        Debug.Log(pillParts[0].size);
        pillParts[0].color = Color.yellow;
        pillParts[1].color = Color.red;

        float initial_y = board.size.y;
        float initial_x = - (board.size.x - (pillParts[0].size.x * 2.4f));
        Vector3 newPosition = new Vector3(initial_x, initial_y, 0);

        pill.transform.parent = board.transform;
        pill.transform.localPosition = newPosition;



    }
}
