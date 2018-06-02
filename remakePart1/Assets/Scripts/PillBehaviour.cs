using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillBehaviour : MonoBehaviour {

    private float _totalTime;
    private float _pillPartSize = 0;
    private bool _stop_moviment = false;
    private SpriteRenderer[] _pills = null;
    private BoardBehaviour _parent_script = null;
    private SpriteRenderer _parent_sprite = null;
    private Quaternion _original_rotation;
    private int _pill_state = 0;
    private Vector3 _vertical_scale = new Vector3(0.5f, 0.46f, 1f);
    private Vector3 _horizontal_scale = new Vector3(0.58f, 0.4f, 1f);
    private float _period = 0;
    private int[] _horizontal_position = new int[2];

    // Use this for initialization
    public void Start () {
        _pills = GetComponentsInChildren<SpriteRenderer>();
        _parent_script = transform.parent.GetComponent<BoardBehaviour>();
        _parent_sprite = transform.parent.GetComponent<SpriteRenderer>();
        _original_rotation = transform.rotation;
        _stop_moviment = false;
        transform.localScale = _horizontal_scale;
        _horizontal_position[0] = 16;
        _horizontal_position[1] = 16;
        _pillPartSize = 1;
    }

    // Update is called once per frame
    public void Update () {
        _totalTime += Time.deltaTime;
        
        if (_parent_script.pill.transform == transform)
        {
            
            if (_stop_moviment == false)
            {
                
                if (_horizontal_position[0] > 0 && _horizontal_position[1] > 0)
                {
                    if (_period > _parent_script.wait_for_moviment)
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y - _pillPartSize, 0f);
                        _horizontal_position[0] -= 1;
                        _horizontal_position[1] -= 1;
                        _period = 0;
                    } else
                    {
                        _period += Time.deltaTime;
                    }
                } else
                {
                    _parent_script.pill = null;
                    _parent_script.board.mainBoard[0, _horizontal_position[0]] = Color.blue;
                    _parent_script.board.mainBoard[0, _horizontal_position[1]] = Color.yellow;
                    _stop_moviment = true;
                }

                HorizontalMove();
                VerticalMove();
            } else
            {
                _parent_script.pill = null;
            }
               
        }
    }

    public void HorizontalMove()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            float pill_next_x = 0;
            if (_pill_state == 0)
            {
                pill_next_x = (transform.position.x - (2 * _pillPartSize));
            } else if (_pill_state == 1 || _pill_state == 3 || _pill_state == 2)
            {
                pill_next_x = (transform.position.x - (_pillPartSize));
            }
            
            if ((transform.parent.position.x ) < (pill_next_x))
            {
                transform.position = new Vector3(transform.position.x - _pillPartSize, transform.position.y, 0f);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            float pill_next_x = 0;
            if (_pill_state == 0 || _pill_state == 3)
            {
                pill_next_x = transform.position.x + _pillPartSize;
            }
            else if ((_pill_state == 2))
            {
                pill_next_x = transform.position.x + (2*_pillPartSize);
            }
            else if (_pill_state == 1)
            {
                pill_next_x = transform.position.x + _pillPartSize;
            }

            if ((_parent_sprite.bounds.size.x) > (pill_next_x))
            {
                transform.position = new Vector3(transform.position.x + _pillPartSize, transform.position.y, 0f);

            }

        }
    }

    public void VerticalMove()
    {
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            

            if (_pill_state == 0)
            {
                transform.rotation = _original_rotation;
                transform.Rotate(0, 0, 90);
                transform.position = new Vector3(transform.position.x - (_pillPartSize*0.73f), transform.position.y + (_pillPartSize * 0.45f), transform.position.z);
                transform.localScale = _vertical_scale;
                _horizontal_position[0] -= 1;
                _pill_state = 1;
            } else if (_pill_state == 1)
            {
                transform.rotation = _original_rotation;
                transform.Rotate(0, 180, 0);
                transform.position = new Vector3(transform.position.x - (_pillPartSize * 0.3f), transform.position.y-(_pillPartSize * 0.3f), transform.position.z);
                transform.localScale = _horizontal_scale;
                _horizontal_position[0] += 1;
                _pill_state = 2;
            } else if (_pill_state == 2)
            {
                transform.rotation = _original_rotation;
                transform.Rotate(0, 0, 90);
                transform.Rotate(0, 180, 0);
                transform.position = new Vector3(transform.position.x + (_pillPartSize * 0.3f), transform.position.y - (_pillPartSize * 0.7f), transform.position.z);
                transform.localScale = _vertical_scale;
                _horizontal_position[1] -= 1;
                _pill_state = 3;
            } else if (_pill_state == 3)
            {
                transform.rotation = _original_rotation;
                transform.position = new Vector3(transform.position.x + (_pillPartSize * 0.73f), transform.position.y + (_pillPartSize * 0.75f), transform.position.z);
                transform.localScale = _horizontal_scale;
                _horizontal_position[1] += 1;
                _pill_state = 0;
            }
        }
        

    }

    public void OnTriggerEnter2D()
    {
        Debug.Log("OnTriggerEnter2D");
        _stop_moviment = true;
    }

    public void OnTriggerStay2D()
    {
        Debug.Log("OnTriggerStay2D");
       
    }

    public void OnTriggerExit2D()
    {
        Debug.Log("OnTriggerExit2D");
    }
}
