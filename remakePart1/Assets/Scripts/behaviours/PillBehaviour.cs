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
    private bool _onlyDownMoviment = false;
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
        if (_finishedAnimation == true && !_finishedMoviment)
        {
            MovimentDownAutomatic();
            if (!_onlyDownMoviment)
            {
                MovimentDownWithKey();
                MovimentRotate();
                MovimentLeftOrRight();
            }
            
        }
    }

    public void UpdatePositionsPill(int newRow0, int newColumn0, int newRow1, int newColumn1)
    {
        _pill.PillParts["first"].PositionRow += newRow0;
        _pill.PillParts["first"].PositionColumn += newColumn0;
        if (_pill.PillParts["second"]  != null)
        {
            _pill.PillParts["second"].PositionRow += newRow1;
            _pill.PillParts["second"].PositionColumn += newColumn1;
        }
        
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

    public void MovimentLeft()
    {
        if (_grid.IsPositionEmpty(_pill.PillParts["first"].PositionRow, _pill.PillParts["first"].PositionColumn - 1) &&
            _grid.IsPositionEmpty(_pill.PillParts["second"].PositionRow, _pill.PillParts["second"].PositionColumn - 1))
        {
            transform.position = new Vector3(transform.position.x - Constants.PillSize, transform.position.y, 0f);
            UpdatePositionsPill(0, -1, 0, -1);
        }
    }

    public void MovimentRight()
    {
        if (_grid.IsPositionEmpty(_pill.PillParts["first"].PositionRow, _pill.PillParts["first"].PositionColumn + 1) &&
            _grid.IsPositionEmpty(_pill.PillParts["second"].PositionRow, _pill.PillParts["second"].PositionColumn + 1))
        {
            transform.position = new Vector3(transform.position.x + Constants.PillSize, transform.position.y, 0f);
            UpdatePositionsPill(0, 1, 0, 1);
        }
    }


    private void MovimentDown()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - Constants.PillSize, 0f);
        UpdatePositionsPill(-1, 0, -1, 0);
    }

    public void MovimentDownAutomatic()
    {
        if ((_pill.PillParts["first"] == null || _grid.IsPositionEmpty(_pill.PillParts["first"].PositionRow - 1, _pill.PillParts["first"].PositionColumn)) &&
              (_pill.PillParts["second"] == null ||_grid.IsPositionEmpty(_pill.PillParts["second"].PositionRow - 1, _pill.PillParts["second"].PositionColumn)))
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
        } else if (!_finishedMoviment)
        {
            FinalizeMoviment();
        }
    }

    public void MovimentDownWithKey()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (_grid.IsPositionEmpty(_pill.PillParts["first"].PositionRow - 1, _pill.PillParts["first"].PositionColumn) &&
              _grid.IsPositionEmpty(_pill.PillParts["second"].PositionRow - 1, _pill.PillParts["second"].PositionColumn))
            {
                MovimentDown();
            }
        }
    }

    public IEnumerator MovimentAfterStop()
    {
        yield return new WaitForSeconds(0.1f);
        if ((_pill.PillParts["first"] != null || _pill.PillParts["second"] != null) && (_finishedMoviment == true))
        {
            if (_pill.PillParts["first"] != null)
            {
                _grid.CleanPositionsOnBoard(_pill.PillParts["first"]);
            }
            if (_pill.PillParts["second"] != null)
            {
                _grid.CleanPositionsOnBoard(_pill.PillParts["second"]);
            }
            MovimentDownAutomatic();
            _finishedMoviment = false;
            _onlyDownMoviment = true;
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
        if (!_onlyDownMoviment)
        {
            _board.GetComponent<BoardBehaviour>().ThrowPill(true);
        }
        _finishedMoviment = true;
        if (_pill.PillParts["first"] != null)
            _grid.IncludePositionsOnBoard(_pill.PillParts["first"]);
        if (_pill.PillParts["second"] != null)
            _grid.IncludePositionsOnBoard(_pill.PillParts["second"]);
        Dictionary<string, int> firstPosition = new Dictionary<string, int> { { "row", _pill.PillParts["first"].PositionRow }, { "column", _pill.PillParts["first"].PositionColumn } };
        Dictionary<string, int> secondPosition = null;
        if (_pill.PillParts["second"] != null)
        {
            secondPosition = new Dictionary<string, int> { { "row", _pill.PillParts["second"].PositionRow }, { "column", _pill.PillParts["second"].PositionColumn } };
        }
        
        StartCoroutine(_grid.CheckMatches(new Dictionary<string, Dictionary<string, int>> { { "first", firstPosition }, { "second", secondPosition } }));
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
        _drMarioAnimator.SetInteger("MarioState", 1);
    }

    public void ChangeFirstPillPart()
    {
        _pill.PillParts["first"] = _pill.PillParts["second"];
    }

    public void ClearSecondPillPart()
    {
        _pill.PillParts["second"] = null;
        Debug.Log("Cleaning teh seconds");
    }
}
