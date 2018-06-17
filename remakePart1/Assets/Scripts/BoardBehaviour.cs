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
        
        boardRenderer = GetComponent<SpriteRenderer>();
        this.board = new Board(transparentPill);
        for (int horizontal=0; horizontal < board.mainBoard.GetLength(0); horizontal++ )
        {
            for (int vertical = 0; vertical < board.mainBoard.GetLength(1); vertical++)
            {
                board.mainBoard[horizontal,vertical] = null;
            }
        }
        CreateVirus();
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
        int quantityBlueVirus = Random.Range(1, 4);
        float virusSize = 0.9f;
        float initPositionRows = 3.3f;
        float initPositionColumns = -2.7f;
        int positionRow = 0;
        int positionColumn = 0;
        for (int i=0; i<quantityBlueVirus; i++)
        {
            positionColumn = (Random.Range(0, (Constants.Columns - 1)));
            positionRow = (Random.Range(0, (Constants.Rows - 1)));
            Vector3 pos = new Vector3(virusSize * positionColumn + initPositionColumns, virusSize * positionRow + initPositionRows, 0);
            GameObject blueVirus = Instantiate(blueVirusPrefab, pos, Quaternion.Euler(0, 0, 0));
            blueVirus.transform.parent = transform;

            board.mainBoard[positionRow, positionColumn] = blueVirus.GetComponent<SpriteRenderer>();
        }
        

        /*pos = new Vector3(2f, 2f, 0);
        GameObject redVirus = Instantiate(redVirusPrefab, pos, Quaternion.Euler(0, 0, 0));

        pos = new Vector3(4f, 4f, 0);
        GameObject yellowVirus = Instantiate(yellowVirusPrefab, pos, Quaternion.Euler(0, 0, 0));
        */
    }


    private void CreateNewPill()
    {
        Vector3 pos = new Vector3(0, 0, 0);
        pill = Instantiate(prefab, pos, Quaternion.Euler(0, 0, 0));
        pill.GetComponent<PillBehaviour>().CreatePill(transform);
        
    }

    public void IncludeValuesOnBoard(SpriteRenderer[] pills, int[,] positions)
    {
        
        if (pills[0] != null)
        {
            Debug.Log("Part 1" + positions[0, 0] + "-" + positions[0, 1]);
            board.mainBoard[positions[0, 0], positions[0, 1]] = pills[0];
        }
        if (pills[1] != null)
        {
            Debug.Log("Part 2" + positions[1, 0] + "-" + positions[1, 1]);
            board.mainBoard[positions[1, 0], positions[1, 1]] = pills[1];
        }
    }




}
