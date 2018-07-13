using
    
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
    private Configuration _configuration;

    public Constants.UpdatePointsDelegate UpdatePoints;


    private BoardBehaviour _boardBehaviour;
    private Grid _grid;
    private int _optionRotate = 0;
    private Color[] initColors;
    private float _totalTime;
    private float _period = 0;
    private int _level = 0;
    private bool _finishedAnimation = false;
    public bool finishedMoviment = false;
    public bool onlyDownMoviment = false;
    private bool _isMovingDown = false;
    private Pill _pill;
    private GridItem _nextItem1, _nextItem2 = null;

    

    // Use this for initialization
    void Start () {
        _configuration = Configuration.Instance;
        _pill = new Pill(Time.frameCount, _board.transform, transform, _grid);
        UpdatePoints = UpdatePointsOnBoard;
        initColors = new Color[2];
        initColors[0] = _pill.PillParts["first"].PillPartColor;
        initColors[1] = _pill.PillParts["second"].PillPartColor;

    }
	
	// Update is called once per frame
	void Update () {

        _totalTime += Time.deltaTime;
        if (_finishedAnimation == true && !finishedMoviment)
        {
            if (!onlyDownMoviment)
            {
                //MovimentDownWithKey();
                //MovimentRotate();
                //MovimentLeftOrRight();
            }
            MovimentDownAutomatic();
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
        _isMovingDown = true;
        UpdatePositionsPill(-1, 0, -1, 0);
        transform.position = new Vector3(transform.position.x, transform.position.y - Constants.PillSize, 0f);  
    }

    private IEnumerator WaitMoviment(Vector3 position)
    {
        yield return new WaitForSeconds(0.1f);
        transform.position = position;
    }

    private Dictionary<string, object> GetNextItemDown(PillPart currentItem, bool bypassFinalizeMoviment)
    {
        int positionsDown = 1;
        GridItem nextItem = null;
        if (!bypassFinalizeMoviment)
        {
            while ((currentItem.PositionRow - positionsDown >= 0) && (_grid.GetItem(currentItem.PositionRow - positionsDown, currentItem.PositionColumn) != null)
            && (!_grid.GetItem(currentItem.PositionRow - positionsDown, currentItem.PositionColumn).FinalizedMoviment() &&
            _grid.GetItem(currentItem.PositionRow - positionsDown, currentItem.PositionColumn).OnlyDownMoviment()))
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
                nextPositionResults = GetNextItemDown(_pill.PillParts["first"], false);
                //_nextItem1 = (GridItem)nextPositionResults["nextItem"];
                //isPositionEmpty = _nextItem1 == null && IsValidPosition((int)nextPositionResults["positionsDown"], _pill.PillParts["first"]);
        }
        else
        {
            if (_pill.PillParts["second"] != null)
            {
                nextPositionResults = GetNextItemDown(_pill.PillParts["second"], false);
                //_nextItem2 = (GridItem)nextPositionResults["nextItem"];

            }
            nextPositionResults = GetNextItemDown(_pill.PillParts["first"], false);
            //_nextItem1 = (GridItem)nextPositionResults["nextItem"];
            //isPositionEmpty = _nextItem2 == null && _nextItem1 == null && IsValidPosition((int)nextPositionResults["positionsDown"], _pill.PillParts["first"]);
        }
        return true;
    }

    public void MovimentDownAutomatic()
    {
        
        bool isPositionEmpty = CheckNewPositionDown();
        if (isPositionEmpty)
        {
            if (_period > _configuration.SpeedPills)
            {
                //MovimentDown();
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
                nextPositionResults = GetNextItemDown(_pill.PillParts["first"], true);
                _nextItem1 = (GridItem)nextPositionResults["nextItem"];
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
                _period = _configuration.SpeedPills;
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
            _period = _configuration.SpeedPills;
        }
    }

    public void MovimentRotate()
    {
        bool inputX = Input.GetKeyDown(KeyCode.X);
        bool inputY = Input.GetKeyDown(KeyCode.Z);
        if (inputX || inputY)
        {
            if (inputX)
            {
                _optionRotate += 1;

                if (_optionRotate > 3)
                {
                    _optionRotate = 0;
                }

                
            }
            else
            {
                _optionRotate -= 1;
                if (_optionRotate < -3)
                {
                    _optionRotate = 0;
                }
               
            }




            switch (_optionRotate)
            {
                case -3:
                    initializeColors();
                    break;
                case -2:
                    OppositeColors();
                    break;
                case -1:
                    OppositeColors();
                    break;
                case 0:
                    initializeColors();
                    break;
                case 1:
                    initializeColors();
                    break;
                case 2:
                    OppositeColors();
                    break;
                case 3:
                    OppositeColors();
                    break;

            }



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

    private void initializeColors()
    {
        SpriteRenderer firstRenderer = _pill.PillParts["first"].Behaviour.GetComponent<SpriteRenderer>();
        SpriteRenderer secondRenderer = _pill.PillParts["second"].Behaviour.GetComponent<SpriteRenderer>();
        firstRenderer.color = initColors[0];
        secondRenderer.color = initColors[1];
    }

    private void OppositeColors()
    {
        SpriteRenderer firstRenderer = _pill.PillParts["first"].Behaviour.GetComponent<SpriteRenderer>();
        SpriteRenderer secondRenderer = _pill.PillParts["second"].Behaviour.GetComponent<SpriteRenderer>();
        firstRenderer.color = initColors[1];
        secondRenderer.color = initColors[0];
    }

    private void changeColors()
    {
        SpriteRenderer firstRenderer = _pill.PillParts["first"].Behaviour.GetComponent<SpriteRenderer>();
        Color firstColor = firstRenderer.color;
        SpriteRenderer secondRenderer = _pill.PillParts["second"].Behaviour.GetComponent<SpriteRenderer>();
        firstRenderer.color = secondRenderer.color;
        secondRenderer.color = firstColor;
    }

    public void MovimentRotateHorizontal()
    {
        if (_grid.IsPositionEmpty(_pill.PillParts["second"].PositionRow - 1, _pill.PillParts["second"].PositionColumn + 1))
        {
            GetHorizontalPill(false);
            UpdatePositionsPill(0, 0, -1, 1);
            _pill.State = PillState.HORIZONTAL;
        } else if (_pill.PillParts["second"].PositionColumn == (Constants.Columns-1) && _grid.IsPositionEmpty(_pill.PillParts["first"].PositionRow, _pill.PillParts["first"].PositionColumn - 1) )
        {
            GetHorizontalPill(true);
            UpdatePositionsPill(0, -1, -1, 0);
            _pill.State = PillState.HORIZONTAL;
        }
    }

    public void MovimentRotateVertical()
    {

        if (_grid.IsPositionEmpty(_pill.PillParts["second"].PositionRow + 1, _pill.PillParts["second"].PositionColumn -1))
        {

            GetVerticalPill();
            UpdatePositionsPill(0, 0, 1, -1);
            _pill.State = PillState.VERTICAL;
        }
    }

   
    public void GetVerticalPill()
    {
        _pill.PillParts["first"].Behaviour.UpdateSprite("down");
        //_pill.PillParts["first"].Behaviour.UpdatePosition(new Vector3(0, -0.9f, 0));
        _pill.PillParts["second"].Behaviour.UpdateSprite("up");
        _pill.PillParts["second"].Behaviour.UpdatePosition(new Vector3(-0.9f, 0.9f, 0));
    }

    public void GetHorizontalPill(bool isLastColumn)
    {
        _pill.PillParts["first"].Behaviour.UpdateSprite("left");
        _pill.PillParts["second"].Behaviour.UpdateSprite("right");
        if (isLastColumn)
        {
            _pill.PillParts["second"].Behaviour.UpdatePosition(new Vector3(0, - 0.9f, 0));
            _pill.PillParts["first"].Behaviour.UpdatePosition(new Vector3(- 0.9f, 0, 0));
        } else
        {
            _pill.PillParts["second"].Behaviour.UpdatePosition(new Vector3(0.9f, -0.9f, 0));
        }
        
    }
    

    private void FinalizeMoviment()
    {
        if (!onlyDownMoviment && !_grid.IsGameOver)
        {
            _board.GetComponent<BoardBehaviour>().ThrowPill(true);
        }
        finishedMoviment = true;
        if (_isMovingDown)
        {
            Vector3 position = transform.position;
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.06f, 0f);
            StartCoroutine(WaitMoviment(position));
        }
        _isMovingDown = false;        

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
        _grid.IncludePositionsOnBoard(_pill.PillParts["first"]);
        _grid.IncludePositionsOnBoard(_pill.PillParts["second"]);
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
