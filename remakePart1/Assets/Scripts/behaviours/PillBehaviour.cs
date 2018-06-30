using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillBehaviour : MonoBehaviour {

    private GameObject _drMario;
    private GameObject _board;
    private BoardBehaviour _boardBehaviour;
    private Grid _grid;
    private Animator _drMarioAnimator;
    private Animator _pillAnimator;
    private float _totalTime;
    private float _period = 0;
    private bool _finishedAnimation = false;
    private bool _finishedMoviment = false;
    private Pill _pill;

    

    // Use this for initialization
    void Start () {
        _drMario = GameObject.Find("gameMarioThrowingPill");
        _drMarioAnimator = _drMario.GetComponent<Animator>();
        _board = GameObject.Find("board");
        _boardBehaviour = _board.GetComponent<BoardBehaviour>();
        _grid = _boardBehaviour.BoardGrid;
        _pill = new Pill(Time.frameCount, _board.transform, transform);
    }
	
	// Update is called once per frame
	void Update () {

        _totalTime += Time.deltaTime;
        if (_finishedAnimation == true)
        {
            MovimentDown();
            
        }
    }

    public void UpdatePositionsPill(int newRow0, int newColumn0, int newRow1, int newColumn1)
    {
        _pill.PillParts[0].PositionRow += newRow0;
        _pill.PillParts[0].PositionColumn += newColumn0;
        _pill.PillParts[1].PositionRow += newRow1;
        _pill.PillParts[1].PositionColumn += newColumn1;
    }

    public void MovimentDown()
    {
        if (_grid.IsPositionEmpty(_pill.PillParts[0].PositionRow - 1, _pill.PillParts[0].PositionColumn) &&
              _grid.IsPositionEmpty(_pill.PillParts[1].PositionRow - 1, _pill.PillParts[1].PositionColumn))
        {
            if (_period > Constants.WaitForMoviment)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - Constants.PillSize, 0f);
                UpdatePositionsPill(-1, 0, -1, 0);
                _period = 0;
            }
            else
            {
                _period += Time.deltaTime;
            }
        } else if (!_finishedMoviment)
        {
            FinalizeMoviment();
        }
    }

    


    private void FinalizeMoviment()
    {
        _board.GetComponent<BoardBehaviour>().ThrowPill(true);
        _finishedMoviment = true;
        if (_pill.PillParts[0] != null)
            _grid.IncludePositionsOnBoard(_pill.PillParts[0].PositionRow, _pill.PillParts[0].PositionColumn, "blue");
        if (_pill.PillParts[1] != null)
            _grid.IncludePositionsOnBoard(_pill.PillParts[1].PositionRow, _pill.PillParts[1].PositionColumn, "blue");
    }
    public void PillAnimationEndEvent()
    {
        transform.GetComponent<Animator>().enabled = false;
        _finishedAnimation = true;
        _board.GetComponent<BoardBehaviour>().CreateNewPill(false);
    }

    public void PillAnimationStartEvent()
    {
        _drMarioAnimator.SetInteger("MarioState", 1);
    }
}
