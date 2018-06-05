using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PillBehaviour : MonoBehaviour {

    private Color[] _colors = new Color[] { Color.red, Color.yellow, Color.cyan };
    public Vector3 initPillPosition = new Vector3();
    public Vector3 horizontalStateScale = new Vector3();
    public Vector3 verticalStateScale = new Vector3();

    private float _totalTime;
    private float _pillPartSize = 0;
    private SpriteRenderer[] _pills = null;
    private BoardBehaviour _parent_script = null;
    private SpriteRenderer _parent_sprite = null;
    private Quaternion _original_rotation;
    private PillState _pill_state = 0;
    private float _period = 0;
    private int _horizontal_lenght = 0;
    private int _vertical_lenght = 0;
    private int[] _horizontal_position = new int[2];
    private int[] _vertical_position = new int[2];

    enum PillState { HORIZONTAL_ORIGINAL, VERTICAL_ORIGINAL, HORIZONTAL_INVERTED, VERTICAL_INVERTED };

    // Use this for initialization
    public void Start () {
        _pills = GetComponentsInChildren<SpriteRenderer>();
        _parent_script = transform.parent.GetComponent<BoardBehaviour>();
        _parent_sprite = transform.parent.GetComponent<SpriteRenderer>();
        _original_rotation = transform.rotation;
        transform.localScale = horizontalStateScale;
        //Init positions
        _horizontal_lenght = _parent_script.board.mainBoard.GetLength(0);
        _vertical_lenght = _parent_script.board.mainBoard.GetLength(1);
        _horizontal_position[0] = _horizontal_lenght;
        _horizontal_position[1] = _horizontal_lenght;
        _vertical_position[0] = 3;
        _vertical_position[1] = 4;
        _horizontal_lenght -= 1;
        _vertical_lenght -= 1;
        _pillPartSize = 1f;
    }

    public void CreatePill(Transform parent)
    {
        SpriteRenderer[] pillParts = GetComponentsInChildren<SpriteRenderer>();
        pillParts[0].color = _colors[Random.Range(0, _colors.Length)];
        pillParts[1].color = _colors[Random.Range(0, _colors.Length)];
        transform.parent = parent;
        transform.position = initPillPosition;
    }


    public void includeValuesOnBoard()
    {
        
        
        _parent_script.board.mainBoard[_horizontal_position[0], _vertical_position[0]] = _pills[0];
        _parent_script.board.mainBoard[_horizontal_position[1], _vertical_position[1]] = _pills[1];
        _parent_script.board.CheckCombinations(_horizontal_position[0], _vertical_position[0]);
        _parent_script.board.CheckCombinations(_horizontal_position[1], _vertical_position[1]);
    }

    

    public int[] CopyPositions(int[] original_positions)
    {
        int[] new_positions = new int[2];

        new_positions[1] = original_positions[1];
        new_positions[0] = original_positions[0];

        return new_positions;
    }

    public void MovimentDown()
    {
        int[] nextHorizontalPosition = CopyPositions(_horizontal_position);
        int[] nextVerticalPosition = CopyPositions(_vertical_position);
        nextHorizontalPosition[0] -= 1;
        nextHorizontalPosition[1] -= 1;
        if (_parent_script.board.IsPositionEmpty(nextHorizontalPosition[0], nextVerticalPosition[0]) &&
            _parent_script.board.IsPositionEmpty(nextHorizontalPosition[1], nextVerticalPosition[1]))
        {
            if (_period > _parent_script.wait_for_moviment)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - _pillPartSize, 0f);
                _horizontal_position[0] = nextHorizontalPosition[0];
                _horizontal_position[1] = nextHorizontalPosition[1];
                _period = 0;
            }
            else
            {
                _period += Time.deltaTime;
            }
        }
        else
        {
            includeValuesOnBoard();
            Debug.Log(_horizontal_position[0] +"x"+ _vertical_position[0]);
            Debug.Log(_horizontal_position[1] + "x" + _vertical_position[1]);
            _parent_script.pill = null;
        }
    }

    public void MovimentLeft()
    {
        int[] nextHorizontalPosition = CopyPositions(_horizontal_position);
        int[] nextVerticalPosition = CopyPositions(_vertical_position);
        nextVerticalPosition[0] -= 1;
        nextVerticalPosition[1] -= 1;
        if (_parent_script.board.IsPositionEmpty(nextHorizontalPosition[0], nextVerticalPosition[0]) &&
            _parent_script.board.IsPositionEmpty(nextHorizontalPosition[1], nextVerticalPosition[1]))
        {
            transform.position = new Vector3(transform.position.x - _pillPartSize, transform.position.y, 0f);
            _vertical_position[0] = nextVerticalPosition[0];
            _vertical_position[1] = nextVerticalPosition[1];
        }
    }

    public void MovimentRight()
    {
        int[] nextHorizontalPosition = CopyPositions(_horizontal_position);
        int[] nextVerticalPosition = CopyPositions(_vertical_position);
        nextVerticalPosition[0] += 1;
        nextVerticalPosition[1] += 1;
        if (_parent_script.board.IsPositionEmpty(nextHorizontalPosition[0], nextVerticalPosition[0]) &&
            _parent_script.board.IsPositionEmpty(nextHorizontalPosition[1], nextVerticalPosition[1]))
        {
            transform.position = new Vector3(transform.position.x + _pillPartSize, transform.position.y, 0f);
            _vertical_position[0] = nextVerticalPosition[0];
            _vertical_position[1] = nextVerticalPosition[1];
        }
    }

    // Update is called once per frame
    public void Update () {
        _totalTime += Time.deltaTime;
        
        if ( _parent_script.pill != null &&_parent_script.pill.transform == transform)
        {
            
            MovimentDown();
            HorizontalMove();
            VerticalMove();
              
        }
        //Debug.Log(_horizontal_position[0] +"x"+ _vertical_position[0]);
        //Debug.Log(_horizontal_position[1] + "x" + _vertical_position[1]);
        //_parent_script.board.SeeBoard();
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

    public void MovimentRotateVerticalOriginal()
    {
        int[] nextHorizontalPosition = CopyPositions(_horizontal_position);
        int[] nextVerticalPosition = CopyPositions(_vertical_position);
        nextHorizontalPosition[0] -= 1;
        nextVerticalPosition[1] -= 1;
        if (_parent_script.board.IsPositionEmpty(nextHorizontalPosition[0], nextVerticalPosition[0]) &&
            _parent_script.board.IsPositionEmpty(nextHorizontalPosition[1], nextVerticalPosition[1]))
        {
           
            transform.rotation = _original_rotation;
            transform.Rotate(0, 0, 90);
            transform.position = new Vector3(transform.position.x - (_pillPartSize * 0.75f), transform.position.y + (_pillPartSize * 0.38f), transform.position.z);
            transform.localScale = verticalStateScale;
            _horizontal_position[0] = nextHorizontalPosition[0];
            _vertical_position[1] = nextVerticalPosition[1];
            _pill_state = PillState.VERTICAL_ORIGINAL;
        }
    }

    public void MovimentRotateHorizontalInverted()
    {
        int[] nextHorizontalPosition = CopyPositions(_horizontal_position);
        int[] nextVerticalPosition = CopyPositions(_vertical_position);
        nextHorizontalPosition[0] += 1;
        nextVerticalPosition[0] += 1;
        if (_parent_script.board.IsPositionEmpty(nextHorizontalPosition[0], nextVerticalPosition[0]) &&
            _parent_script.board.IsPositionEmpty(nextHorizontalPosition[1], nextVerticalPosition[1]))
        {
            transform.rotation = _original_rotation;
            transform.Rotate(0, 180, 0);
            transform.position = new Vector3(transform.position.x - (_pillPartSize * 0.3f), transform.position.y - (_pillPartSize * 0.4f), transform.position.z);
            transform.localScale = horizontalStateScale;
            _horizontal_position[0] = nextHorizontalPosition[0];
            _vertical_position[0] = nextVerticalPosition[0];
            _pill_state = PillState.HORIZONTAL_INVERTED;
        }
    }

    public void MovimentRotateVerticalInverted()
    {
        int[] nextHorizontalPosition = CopyPositions(_horizontal_position);
        int[] nextVerticalPosition = CopyPositions(_vertical_position);
        nextHorizontalPosition[1] -= 1;
        nextVerticalPosition[0] -= 1;
        if (_parent_script.board.IsPositionEmpty(nextHorizontalPosition[0], nextVerticalPosition[0]) &&
            _parent_script.board.IsPositionEmpty(nextHorizontalPosition[1], nextVerticalPosition[1]))
        {
            transform.rotation = _original_rotation;
            transform.Rotate(0, 0, 90);
            transform.Rotate(0, 180, 0);
            transform.position = new Vector3(transform.position.x + (_pillPartSize * 0.3f), transform.position.y - (_pillPartSize * 0.7f), transform.position.z);
            transform.localScale = verticalStateScale;
            _vertical_position[0] = nextVerticalPosition[0];
            _horizontal_position[1] = nextHorizontalPosition[1];
            _pill_state = PillState.VERTICAL_INVERTED;
        }
    }

    public void MovimentRotateHorizontalOriginal()
    {
        int[] nextHorizontalPosition = CopyPositions(_horizontal_position);
        int[] nextVerticalPosition = CopyPositions(_vertical_position);
        nextHorizontalPosition[1] += 1;
        nextVerticalPosition[1] += 1;
        if (_parent_script.board.IsPositionEmpty(nextHorizontalPosition[0], nextVerticalPosition[0]) &&
            _parent_script.board.IsPositionEmpty(nextHorizontalPosition[1], nextVerticalPosition[1]))
        {
            transform.rotation = _original_rotation;
            transform.position = new Vector3(transform.position.x + (_pillPartSize * 0.73f), transform.position.y + (_pillPartSize * 0.75f), transform.position.z);
            transform.localScale = horizontalStateScale;
            _vertical_position[1] = nextVerticalPosition[1];
            _horizontal_position[1] = nextHorizontalPosition[1];
            _pill_state = PillState.HORIZONTAL_ORIGINAL;
        }
    }

    public void VerticalMove()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Entrei 1");
            switch (_pill_state)
            {
                case PillState.HORIZONTAL_ORIGINAL:
                    MovimentRotateVerticalOriginal();
                    break;
                case PillState.HORIZONTAL_INVERTED:
                    MovimentRotateVerticalInverted();
                    break;
                case PillState.VERTICAL_ORIGINAL:
                    MovimentRotateHorizontalInverted();
                    break;
                case PillState.VERTICAL_INVERTED:
                    MovimentRotateHorizontalOriginal();
                    break;
            }

            Debug.Log("Fim ---------------------------------------------");
            

        }
        

    }

    
}
