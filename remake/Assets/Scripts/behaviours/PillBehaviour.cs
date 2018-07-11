﻿using
    
    System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillBehaviour : MonoBehaviour {



    public Animator DrMarioAnimator { get; set; }
    
    private GameObject _board;
    public GameObject Board
    {
        get
        {
            return _board;
        }
        set
        {
            _board = value;
            _boardBehaviour = _board.GetComponent<BoardBehaviour>();
            _grid = _boardBehaviour.BoardGrid;
        }
    }
    
    public Constants.UpdatePointsDelegate UpdatePoints;


    private BoardBehaviour _boardBehaviour;
    private Grid _grid;
    private float _totalTime;
    private float _period = 0;
    private bool _finishedAnimation = false;
    public bool finishedMoviment = false;
    public bool onlyDownMoviment = false;
    private Pill _pill;
    private GridItem _nextItem1, _nextItem2 = null;
    //private int positionsDown = 0;

    

    // Use this for initialization
    void Start () {
        _pill = new Pill(Time.frameCount, _board.transform, transform, _grid);
        UpdatePoints = UpdatePointsOnBoard;
    }
	
	// Update is called once per frame
	void Update () {

        _totalTime += Time.deltaTime;
        if (_finishedAnimation == true && !finishedMoviment)
        {
            MovimentDownAutomatic();
            if (!onlyDownMoviment)
            {
                MovimentDownWithKey();
                MovimentRotate();
                MovimentLeftOrRight();
            }
            
        }
    }

    public void UpdatePositionsPill(int newRow0, int newColumn0, int newRow1, int newColumn1)
    {
        _grid.CleanPositionsOnBoard(_pill.PillParts["first"]);
        if (_pill.PillParts["second"] != null)
        {
            _grid.CleanPositionsOnBoard(_pill.PillParts["second"]);
        } 
        _pill.PillParts["first"].PositionRow += newRow0;
        _pill.PillParts["first"].PositionColumn += newColumn0;
        _grid.IncludePositionsOnBoard(_pill.PillParts["first"]);
        if (_pill.PillParts["second"]  != null)
        {
            _pill.PillParts["second"].PositionRow += newRow1;
            _pill.PillParts["second"].PositionColumn += newColumn1;
            _grid.IncludePositionsOnBoard(_pill.PillParts["second"]);
        }

        
    }

    public void UpdatePointsOnBoard(int virusQuantity)
    {
        _boardBehaviour.UpdatePoints(virusQuantity);
    }

    public void MovimentLeftOrRight()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MovimentLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MovimentRight();

        }
    }

    private bool CheckNewPositionLeft()
    {
        bool isPositionEmpty = false;
        _nextItem1 = null;
        _nextItem2 = null;

        Dictionary<string, object> nextPositionResults = new Dictionary<string, object>();
        if (_pill.State == PillState.VERTICAL)
        {
            if (_pill.PillParts["second"] != null)
            {
                isPositionEmpty = (_grid.IsPositionEmpty(_pill.PillParts["second"].PositionRow, _pill.PillParts["second"].PositionColumn - 1) &&
                    _grid.IsPositionEmpty(_pill.PillParts["first"].PositionRow, _pill.PillParts["first"].PositionColumn - 1));
            }
            else
            {
                isPositionEmpty = (_grid.IsPositionEmpty(_pill.PillParts["first"].PositionRow, _pill.PillParts["first"].PositionColumn - 1));
            }
        }
        else
        {
            isPositionEmpty = (_grid.IsPositionEmpty(_pill.PillParts["first"].PositionRow, _pill.PillParts["first"].PositionColumn - 1));
        }
        return isPositionEmpty;
    }

    private bool CheckNewPositionRight()
    {
        bool isPositionEmpty = false;
        _nextItem1 = null;
        _nextItem2 = null;

        Dictionary<string, object> nextPositionResults = new Dictionary<string, object>();
        if (_pill.State == PillState.VERTICAL)
        {
            if (_pill.PillParts["second"] != null)
            {
                isPositionEmpty = (_grid.IsPositionEmpty(_pill.PillParts["second"].PositionRow, _pill.PillParts["second"].PositionColumn + 1) &&
                    _grid.IsPositionEmpty(_pill.PillParts["first"].PositionRow, _pill.PillParts["first"].PositionColumn + 1));
            }
            else
            {
                isPositionEmpty = (_grid.IsPositionEmpty(_pill.PillParts["first"].PositionRow, _pill.PillParts["first"].PositionColumn + 1));
            }
        }
        else
        {
            if (_pill.PillParts["second"] != null)
            {
                isPositionEmpty = (_grid.IsPositionEmpty(_pill.PillParts["second"].PositionRow, _pill.PillParts["second"].PositionColumn + 1));
            }
            else
            {
                isPositionEmpty = (_grid.IsPositionEmpty(_pill.PillParts["first"].PositionRow, _pill.PillParts["first"].PositionColumn + 1));
            }
            
        }
        return isPositionEmpty;
    }

    public void MovimentLeft()
    {
        if (CheckNewPositionLeft())
        {
            transform.position = new Vector3(transform.position.x - Constants.PillSize, transform.position.y, 0f);
            UpdatePositionsPill(0, -1, 0, -1);
        }
    }

    public void MovimentRight()
    {
        if (CheckNewPositionRight())
        {
            transform.position = new Vector3(transform.position.x + Constants.PillSize, transform.position.y, 0f);
            UpdatePositionsPill(0, 1, 0, 1);
        }
    }


    private void MovimentDown()
    {
        UpdatePositionsPill(-1, 0, -1, 0);
        transform.position = new Vector3(transform.position.x, transform.position.y - Constants.PillSize, 0f);
        
        
    }

    private Dictionary<string, object> GetNextItemDown(PillPart currentItem, bool bypassFinalizeMoviment)
    {
        int positionsDown = 1;
        GridItem nextItem = null;
        if (!bypassFinalizeMoviment)
        {
            while ((currentItem.PositionRow - positionsDown >= 0) && (_grid.GetItem(currentItem.PositionRow - positionsDown, currentItem.PositionColumn) != null)
            && (!_grid.GetItem(currentItem.PositionRow - positionsDown, currentItem.PositionColumn).FinalizedMoviment()))
            {
                positionsDown += 1;
            }
        }        

        if (currentItem.PositionRow - positionsDown >= 0)
        {
            nextItem = _grid.GetItem(currentItem.PositionRow - positionsDown, currentItem.PositionColumn);
        }
        Dictionary<string, object> results = new Dictionary<string, object>();
        results.Add("nextItem", nextItem);
        results.Add("positionsDown", positionsDown);
        return results;
    }

    private bool IsValidPosition(int positionsDown, PillPart current)
    {
        if (current.GetPositionRow() - positionsDown >= 0)
            return true;
        else
            return false;
    }

    private bool CheckNewPositionDown()
    {
        bool isPositionEmpty = false;
        _nextItem1 = null;
        _nextItem2 = null;

        Dictionary<string, object> nextPositionResults = new Dictionary<string, object>();
        if (_pill.State == PillState.VERTICAL)
        {
            if (_pill.PillParts["second"] != null)
            {
                nextPositionResults = GetNextItemDown(_pill.PillParts["second"], false);
                _nextItem2 = (GridItem) nextPositionResults["nextItem"];
                isPositionEmpty = _nextItem2 == null && IsValidPosition((int)nextPositionResults["positionsDown"], _pill.PillParts["second"]);
            }
            else
            {
                nextPositionResults = GetNextItemDown(_pill.PillParts["first"], false);
                _nextItem1 = (GridItem)nextPositionResults["nextItem"];
                isPositionEmpty = _nextItem1 == null && IsValidPosition((int)nextPositionResults["positionsDown"], _pill.PillParts["first"]);
            }
        }
        else
        {
            if (_pill.PillParts["second"] != null)
            {
                nextPositionResults = GetNextItemDown(_pill.PillParts["second"], false);
                _nextItem2 = (GridItem)nextPositionResults["nextItem"];

            }
            nextPositionResults = GetNextItemDown(_pill.PillParts["first"], false);
            _nextItem1 = (GridItem)nextPositionResults["nextItem"];
            isPositionEmpty = _nextItem2 == null && _nextItem1 == null && IsValidPosition((int)nextPositionResults["positionsDown"], _pill.PillParts["first"]);
        }
        return isPositionEmpty;
    }

    public void MovimentDownAutomatic()
    {
        
        bool isPositionEmpty = CheckNewPositionDown();
        if (isPositionEmpty)
        {
            if (_period > Constants.WaitForMoviment)
            {
                MovimentDown();
                _period = 0;
            }
            else
            {
                _period += Time.deltaTime;
            }
        } else if (!finishedMoviment)
        {
            if ((_nextItem1 == null || _nextItem1.FinalizedMoviment()) && (_nextItem2 == null || _nextItem2.FinalizedMoviment()))
            {
                FinalizeMoviment();
            }
            
        }
    }

    public void MovimentDownWithKey()
    {
        _nextItem1 = null;
        _nextItem2 = null;
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Dictionary<string, object> nextPositionResults = new Dictionary<string, object>();
            if (_pill.State == PillState.VERTICAL)
            {
                if (_pill.PillParts["second"] != null)
                {
                    nextPositionResults = GetNextItemDown(_pill.PillParts["second"], true);
                    _nextItem2 = (GridItem)nextPositionResults["nextItem"];
                }
                else
                {
                    nextPositionResults = GetNextItemDown(_pill.PillParts["first"], true);
                    _nextItem1 = (GridItem)nextPositionResults["nextItem"];
                }
            }
            else
            {
                if (_pill.PillParts["second"] != null)
                {
                    nextPositionResults = GetNextItemDown(_pill.PillParts["second"], true);
                    _nextItem2 = (GridItem)nextPositionResults["nextItem"];

                }
                nextPositionResults = GetNextItemDown(_pill.PillParts["first"], true);
                _nextItem1 = (GridItem)nextPositionResults["nextItem"];
            }
            if (_nextItem1 == null && _nextItem2 == null)
            {
                _period = Constants.WaitForMoviment;
            } 
            
        }
    }

    public void MovimentAfterStop()
    {
        if ((_pill.PillParts["first"] != null || _pill.PillParts["second"] != null) && (finishedMoviment == true))
        {
            MovimentDownAutomatic();
            finishedMoviment = false;
            onlyDownMoviment = true;
            _period = Constants.WaitForMoviment;
        }
    }

    public void MovimentRotate()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            switch (_pill.State)
            {
                case PillState.HORIZONTAL:
                    MovimentRotateVertical();
                    break;
                case PillState.VERTICAL:
                    MovimentRotateHorizontal();
                    break;
            }
        }
    }

    public void MovimentRotateHorizontal()
    {
        if (_grid.IsPositionEmpty(_pill.PillParts["second"].PositionRow + 1, _pill.PillParts["second"].PositionColumn + 1))
        {
            GetHorizontalPill(false);
            UpdatePositionsPill(0, 0, 1, 1);
            _pill.State = PillState.HORIZONTAL;
        } else if (_pill.PillParts["second"].PositionColumn == (Constants.Columns-1) && _grid.IsPositionEmpty(_pill.PillParts["first"].PositionRow, _pill.PillParts["first"].PositionColumn - 1) &&
            _grid.IsPositionEmpty(_pill.PillParts["second"].PositionRow - 1, _pill.PillParts["second"].PositionColumn))
        {
            GetHorizontalPill(true);
            UpdatePositionsPill(0, -1, 1, 0);
            _pill.State = PillState.HORIZONTAL;
        }
    }

    public void MovimentRotateVertical()
    {

        if (_grid.IsPositionEmpty(_pill.PillParts["second"].PositionRow - 1, _pill.PillParts["second"].PositionColumn - 1))
        {

            GetVerticalPill();
            SpriteRenderer firstRenderer = _pill.PillParts["first"].Behaviour.GetComponent<SpriteRenderer>();
            Color firstColor = firstRenderer.color;
            SpriteRenderer secondRenderer = _pill.PillParts["second"].Behaviour.GetComponent<SpriteRenderer>();
            firstRenderer.color = secondRenderer.color;
            secondRenderer.color = firstColor;
            UpdatePositionsPill(0, 0, -1, -1);
            _pill.State = PillState.VERTICAL;
        }
    }

    public void GetVerticalPill()
    {
        _pill.PillParts["first"].Behaviour.UpdateSprite("up");
        _pill.PillParts["second"].Behaviour.UpdateSprite("down");
        _pill.PillParts["second"].Behaviour.UpdatePosition(new Vector3(-0.9f, -0.9f, 0));
    }

    public void GetHorizontalPill(bool isLastColumn)
    {
        _pill.PillParts["first"].Behaviour.UpdateSprite("left");
        _pill.PillParts["second"].Behaviour.UpdateSprite("right");
        if (isLastColumn)
        {
            _pill.PillParts["second"].Behaviour.UpdatePosition(new Vector3(0, 0.9f, 0));
            _pill.PillParts["first"].Behaviour.UpdatePosition(new Vector3(-0.9f, 0, 0));
        } else
        {
            _pill.PillParts["second"].Behaviour.UpdatePosition(new Vector3(0.9f, 0.9f, 0));
        }
        
    }
    

    private void FinalizeMoviment()
    {
        if (!onlyDownMoviment && !_grid.IsGameOver)
        {
            _board.GetComponent<BoardBehaviour>().ThrowPill(true);
        }
        finishedMoviment = true;
        Dictionary<string, int> firstPosition = new Dictionary<string, int> { { "row", _pill.PillParts["first"].PositionRow }, { "column", _pill.PillParts["first"].PositionColumn } };
        Dictionary<string, int> secondPosition = null;
        if (_pill.PillParts["second"] != null)
        {
            secondPosition = new Dictionary<string, int> { { "row", _pill.PillParts["second"].PositionRow }, { "column", _pill.PillParts["second"].PositionColumn } };
        }

         
    
    StartCoroutine(_grid.CheckMatches(new Dictionary<string, Dictionary<string, int>> { { "first", firstPosition }, { "second", secondPosition } }, UpdatePoints));
    }

    public void PillAnimationEndEvent()
    {
        transform.GetComponent<Animator>().enabled = false;
        Destroy(transform.GetComponent<Animator>());
        _pill.PillParts["first"].Behaviour.GetComponent<BoxCollider2D>().enabled = true;
        _pill.PillParts["second"].Behaviour.GetComponent<BoxCollider2D>().enabled = true;
        _pill.PillParts["first"].Behaviour.GetComponent<Rigidbody2D>().simulated = true;
        _pill.PillParts["second"].Behaviour.GetComponent<Rigidbody2D>().simulated = true;
        _finishedAnimation = true;
        _board.GetComponent<BoardBehaviour>().CreateNewPill(false);
    }

    public void PillAnimationStartEvent()
    {
        DrMarioAnimator.SetInteger("MarioState", 1);
    }

    public void ChangeFirstPillPart()
    {
        _pill.PillParts["first"] = _pill.PillParts["second"];
    }

    public void ClearSecondPillPart()
    {
        _pill.PillParts["second"] = null;
    }
}
