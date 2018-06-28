using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBehaviour : MonoBehaviour
{

    public GameObject pillPrefab;
    public GameObject blueVirusPrefab;
    public GameObject redVirusPrefab;
    public GameObject yellowVirusPrefab;
    public Sprite transparentPill;
    public Sprite marioGettingPill;
    private int pillCount = 0;
    private Dictionary<string, GameObject> _virusPrefab;
    private GameObject _drMario;

    public Board board;

    public void Start()
    {
                
        _virusPrefab = new Dictionary<string, GameObject> {{ "yellow", yellowVirusPrefab },
            { "red", redVirusPrefab}, { "blue", blueVirusPrefab}};
        _drMario = GameObject.Find("gameMarioThrowingPill");
        _drMario.GetComponent<SpriteRenderer>().sprite = marioGettingPill;
        board = new Board();
        CreateAllVirus();
        CreateNewPill();
    }

    private void CreateNewPill()
    {

        new Pill(pillCount++, pillPrefab, transform);
    }

    public void Update()
    {

    }

    private void CreateAllVirus()
    {
        int quantityBlueVirus = Random.Range(1, 3);
        int quantityRedVirus = Random.Range(1, 3);
        int quantityYellowVirus = Random.Range(1, 3);

        for (int i = 0; i < quantityBlueVirus; i++)
        {
            new Virus(Constants.ColorsDefinitions["blue"], transform, _virusPrefab["blue"], GetEmptyPosition());
        }
        for (int i = 0; i < quantityRedVirus; i++)
        {
            new Virus(Constants.ColorsDefinitions["red"], transform, _virusPrefab["red"], GetEmptyPosition());
        }
        for (int i = 0; i < quantityYellowVirus; i++)
        {
            new Virus(Constants.ColorsDefinitions["yellow"], transform, _virusPrefab["yellow"], GetEmptyPosition());
        }
    }

    private Dictionary<string, int> GetPosition()
    {
        Dictionary<string, int> position = new Dictionary<string, int> { { "row", 0 }, { "column", 0 } };
        position["row"] = (Random.Range(0, (Constants.Rows - 5)));
        position["column"] = (Random.Range(0, (Constants.Columns - 1)));
        return position;
    }

    private Dictionary<string, int> GetEmptyPosition()
    {
        Dictionary<string, int> position = GetPosition();
        while (board.mainBoard[position["row"], position["column"]] != 0)
        {
            position = GetPosition();
        }
        return position;
    }

}