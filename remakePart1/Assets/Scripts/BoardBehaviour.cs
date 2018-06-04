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

    Color[] colors = new Color[] { Color.red, Color.yellow, Color.cyan};

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
        SpriteRenderer[] pillParts =  pill.GetComponentsInChildren<SpriteRenderer>();
        pillParts[0].color = colors[Random.Range(0, colors.Length)];
        pillParts[1].color = colors[Random.Range(0, colors.Length)];

        float initial_y = 16.2f;
        float initial_x = 4.5f;
        Vector3 newPosition = new Vector3(initial_x, initial_y, 0);

        pill.transform.parent = boardRenderer.transform;
        pill.transform.position = newPosition;
        
    }
}
