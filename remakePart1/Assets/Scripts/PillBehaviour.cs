using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PillBehaviour : MonoBehaviour {

    private Color[] _colors = new Color[] { Color.red, Color.yellow, Color.cyan };
    public Vector3 initPillPosition = new Vector3();
    public Sprite pillLeft = null;
    public Sprite pillRight = null;
    public Sprite pillUp = null;
    public Sprite pillDown = null;

    private float _totalTime;
    private float _pillPartSize = 0;
    private SpriteRenderer[] _pills = null;
    private BoardBehaviour _parentScript = null;
    private SpriteRenderer _parent_sprite = null;
    private Quaternion _original_rotation;
    private PillState _pill_state = 0;
    private float _period = 0;
    private int _horizontal_lenght = 0;
    private int _vertical_lenght = 0;
    private int[,] _positions = new int[2, 2];

    enum PillState { HORIZONTAL_ONE, VERTICAL_ONE, HORIZONTAL_INVERTED, VERTICAL_INVERTED };

    delegate void RefreshBoardTrigger();
    RefreshBoardTrigger refreshBoardTrigger;
    private IEnumerator CheckMatchesCoroutine;


    public void StopCheckMatches()
    {
        if (CheckMatchesCoroutine != null)
            StopCoroutine(CheckMatchesCoroutine);
    }

    public void StartCheckMatchesProcess()
    {
        StopCheckMatches();
        StartCoroutine(_parentScript.board.CheckMatches(_positions));
    }

    // Use this for initialization
    public void Start () {
        _pills = GetComponentsInChildren<SpriteRenderer>();
        _parentScript = transform.parent.GetComponent<BoardBehaviour>();
        _parent_sprite = transform.parent.GetComponent<SpriteRenderer>();
        _original_rotation = transform.rotation;
        //Init positions
        _horizontal_lenght = _parentScript.board.mainBoard.GetLength(0);
        _vertical_lenght = _parentScript.board.mainBoard.GetLength(1);
        _positions[0,0] = _horizontal_lenght;
        _positions[1,0] = _horizontal_lenght;
        _positions[0,1] = 3;
        _positions[1,1] = 4;
        _horizontal_lenght -= 1;
        _vertical_lenght -= 1;
        _pillPartSize = 0.9f;

        refreshBoardTrigger += StopPill;
        refreshBoardTrigger += StartCheckMatchesProcess;
    }

    public void CreatePill(Transform parent)
    {
        SpriteRenderer[] pillParts = GetComponentsInChildren<SpriteRenderer>();
        pillParts[0].color = _colors[Random.Range(0, _colors.Length)];
        pillParts[1].color = _colors[Random.Range(0, _colors.Length)];
        transform.parent = parent;
        transform.position = initPillPosition;
    }


    

    public void SumToPositions(int sumWithHorizontal0, int sumWithVertical0, int sumWithHorizontal1, int sumWithVertical1)
    {
        _positions[0,0] += sumWithHorizontal0;
        _positions[1,0] += sumWithHorizontal1;
        _positions[0,1] += sumWithVertical0;
        _positions[1,1] += sumWithVertical1;
    }   

    public void MovimentDown()
    {
        if (_parentScript.board.IsPositionEmpty(_positions[0, 0]-1, _positions[0, 1]) &&
            _parentScript.board.IsPositionEmpty(_positions[1, 0]-1, _positions[1, 1]))
        {
            if (_period > _parentScript.wait_for_moviment)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - _pillPartSize, 0f);
                SumToPositions(- 1, 0, - 1, 0);
                _period = 0;
            }
            else
            {
                  _period += Time.deltaTime;
            }
        }
        else
        {
            refreshBoardTrigger();
        }
        
    }

    public void StopPill()
    {
        _parentScript.IncludeValuesOnBoard(_pills, _positions);
        Rigidbody2D rigidbody =  transform.GetComponent<Rigidbody2D>();
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
        _parentScript.pill = null;
    }

    public void MovimentDownWithKey()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (_parentScript.board.IsPositionEmpty(_positions[0, 0] - 1, _positions[0, 1]) &&
                _parentScript.board.IsPositionEmpty(_positions[1, 0] - 1, _positions[1, 1]))
            {
                //if (_period > _parentScript.wait_for_moviment)
                //{
                transform.position = new Vector3(transform.position.x, transform.position.y - _pillPartSize, 0f);
                //_horizontal_position[0] = nextHorizontalPosition[0];
                //_horizontal_position[1] = nextHorizontalPosition[1];
                SumToPositions(-1, 0, -1, 0);
                _period = 0;
                //}
                //else
                //{
                //  _period += Time.deltaTime;
                //}
            }
            else
            {
                _parentScript.IncludeValuesOnBoard(_pills, _positions);
                _parentScript.board.SeeBoard();
                _parentScript.pill = null;
            }
        }

    }


    public void GetVerticalPill()
    {
        _pills[0].sprite = pillUp;
        _pills[1].sprite = pillDown;
        _pills[1].transform.position = new Vector3(_pills[1].transform.position.x - 0.95f, _pills[1].transform.position.y - 0.9f, _pills[1].transform.position.z);
    }

    public void GetHorizontalPill()
    {
        _pills[0].sprite = pillLeft;
        _pills[1].sprite = pillRight;
        _pills[1].transform.position = new Vector3(_pills[1].transform.position.x + 0.95f, 
            _pills[1].transform.position.y + 0.9f, _pills[1].transform.position.z);
        //BoxCollider2D boxCollider_pills[1].transform.GetComponent<BoxCollider2D>();
    }

    public void MovimentLeft()
    {
        if (_parentScript.board.IsPositionEmpty(_positions[0,0], _positions[0, 1]-1) &&
            _parentScript.board.IsPositionEmpty(_positions[1, 0], _positions[1, 1]-1))
        {
            transform.position = new Vector3(transform.position.x - _pillPartSize, transform.position.y, 0f);
            SumToPositions(0, -1, 0, -1);
        }
    }

    public void MovimentRight()
    {
        if (_parentScript.board.IsPositionEmpty(_positions[0, 0], _positions[0, 1] + 1) &&
            _parentScript.board.IsPositionEmpty(_positions[1, 0], _positions[1, 1] + 1))
        {
            transform.position = new Vector3(transform.position.x + _pillPartSize, transform.position.y, 0f);
            SumToPositions(0, 1, 0, 1);
        }
    }

    // Update is called once per frame
    public void Update () {
        _totalTime += Time.deltaTime;
        
        if ( _parentScript.pill != null &&_parentScript.pill.transform == transform)
        {
            
            MovimentDown();
            HorizontalMove();
            VerticalMove();
              
        }
        //Debug.Log(_horizontal_position[0] +"x"+ _vertical_position[0]);
        //Debug.Log(_horizontal_position[1] + "x" + _vertical_position[1]);
        //_parentScript.board.SeeBoard();
    }

    // Update is called once per frame
    public void Destroy()
    {
        //Destroy(this);
        Destroy(transform.gameObject);
    }

    public void HorizontalMove()
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

    public void MovimentRotateHorizontalOne()
    {
        if (_parentScript.board.IsPositionEmpty(_positions[1,0] + 1, _positions[1, 1] + 1))
        {

            GetHorizontalPill();
            SumToPositions(0, 0, 1, 1);
            _pill_state = PillState.HORIZONTAL_ONE;
        }
    }

    public void MovimentRotateVerticalOne()
    {
        
        if (_parentScript.board.IsPositionEmpty(_positions[1, 0] - 1, _positions[1, 1] - 1))
        {

            GetVerticalPill();

            Color firstColor = _pills[0].color;
            _pills[0].color = _pills[1].color;
            _pills[1].color = firstColor;

            SumToPositions(0, 0, -1, -1);

            _pill_state = PillState.VERTICAL_ONE;
        }
    }   

    public void VerticalMove()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            switch (_pill_state)
            {
                case PillState.HORIZONTAL_ONE:
                    MovimentRotateVerticalOne();
                    break;
                case PillState.VERTICAL_ONE:
                    MovimentRotateHorizontalOne();
                    break;
            }

            Debug.Log("Fim ---------------------------------------------");
            

        }
        

    }

    
}
