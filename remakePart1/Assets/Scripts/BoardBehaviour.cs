using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBehaviour : MonoBehaviour {

    public GameObject prefab;
    public GameObject blueVirusPrefab;
    public GameObject redVirusPrefab;
    public GameObject yellowVirusPrefab;
    public Sprite transparentPill;
    public GameObject pill = null;
    private GameObject pillPart2 = null;
    private SpriteRenderer boardRenderer = null;
    public float wait_for_moviment = 0.5f;
    public Board board;

    



    // Use this for initialization
    public void Start () {
        CreateVirus();
        boardRenderer = GetComponent<SpriteRenderer>();
        this.board = new Board(transparentPill);
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
            CreateNewPill();

        }
    }

    

    private void CreateVirus()
    {
        Vector3 pos = new Vector3(0f, 10f, 0);
        GameObject blueVirus = Instantiate(blueVirusPrefab, pos, Quaternion.Euler(0, 0, 0));

        pos = new Vector3(2f, 2f, 0);
        GameObject redVirus = Instantiate(redVirusPrefab, pos, Quaternion.Euler(0, 0, 0));

        pos = new Vector3(4f, 4f, 0);
        GameObject yellowVirus = Instantiate(yellowVirusPrefab, pos, Quaternion.Euler(0, 0, 0));
    }


    private void CreateNewPill()
    {
        Vector3 pos = new Vector3(0, 0, 0);
        pill = Instantiate(prefab, pos, Quaternion.Euler(0, 0, 0));
        pill.GetComponent<PillBehaviour>().CreatePill(transform);
        
    }

    public void IncludeValuesOnBoard(SpriteRenderer[] pills, int[,] positions)
    {


        board.mainBoard[positions[0,0], positions[0, 1]] = pills[0];
        board.mainBoard[positions[1, 0], positions[1, 1]] = pills[1];
    }




}
