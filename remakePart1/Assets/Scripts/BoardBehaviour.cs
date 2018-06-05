using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBehaviour : MonoBehaviour {

    public GameObject prefab;
    public GameObject pill = null;
    private GameObject pillPart2 = null;
    private SpriteRenderer boardRenderer = null;
    public float wait_for_moviment = 0.5f;
    public Board board = new Board();

    

    // Use this for initialization
    public void Start () {
        boardRenderer = GetComponent<SpriteRenderer>();
        for (int horizontal=0; horizontal < board.mainBoard.GetLength(0); horizontal++ )
        {
            for (int vertical = 0; vertical < board.mainBoard.GetLength(1); vertical++)
            {
                board.mainBoard[horizontal,vertical] = null;
            }
        }
    }
    
	// Update is called once per frame
	public void Update () {
        
        if (pill == null)
        { 
            createNewPill();

        }
        
        

    }

    private void createNewPill()
    {
        Vector3 pos = new Vector3(0, 0, 0);
        Debug.Log("Creating new instance");
        pill = Instantiate(prefab, pos, Quaternion.Euler(0, 0, 0));
        pill.GetComponent<PillBehaviour>().CreatePill(transform);
        
    }
}
