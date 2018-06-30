using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusBehaviour : MonoBehaviour
{

    Virus VirusObject { get; set; }
    private GameObject _board;
    private BoardBehaviour _boardBehaviour;
    private Grid _grid;
    public string keyColor;



    public void Start()
    {
        _board = GameObject.Find("board");
        _boardBehaviour = _board.GetComponent<BoardBehaviour>();
        _grid = _boardBehaviour.BoardGrid;
        VirusObject = new Virus(Constants.ColorsDefinitions[keyColor], _board.transform, _grid.GetEmptyPosition(), transform);
    }

}