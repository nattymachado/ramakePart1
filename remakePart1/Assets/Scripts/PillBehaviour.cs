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

    // Use this for initialization
    public void Start () {
        _pills = GetComponentsInChildren<SpriteRenderer>();
        _parent_script = transform.parent.GetComponent<BoardBehaviour>();
        _parent_sprite = transform.parent.GetComponent<SpriteRenderer>();
        _original_rotation = transform.rotation;

        _pillPartSize = 1;
    }

    // Update is called once per frame
    public void Update () {
        _totalTime += Time.deltaTime;
        
        if (_parent_script.pill != null)
        {
            if (_stop_moviment == false)
            {
                if (transform.parent.localPosition.y < (transform.localPosition.y - _parent_script.speed))
                {
                    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - _parent_script.speed, 0f);
                    HorizontalMove();
                    VerticalMove();
                }
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
            Debug.Log(_parent_sprite.bounds.size.x);
            Debug.Log(transform.position.x - _pillPartSize);
            if (transform.parent.position.x < (transform.position.x - _pillPartSize))
            {
            transform.position = new Vector3(transform.position.x - _pillPartSize, transform.position.y, 0f);
            
                
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if ((_parent_sprite.bounds.size.x-_pillPartSize) > (transform.position.x + _pillPartSize))
            {
                transform.position = new Vector3(transform.position.x + _pillPartSize, transform.position.y, 0f);
            
            }
        }
    }

    public void VerticalMove()
    {
        Debug.Log(transform.rotation.x);
        if (Input.GetKeyDown(KeyCode.X))
        {
            
            if (_pill_state == 0)
            {
                transform.rotation = _original_rotation;
                transform.Rotate(0, 0, 90);
               // transform.Rotate(0, 180, 0);
                _pill_state = 1;
            } else if (_pill_state == 1)
            {
                transform.rotation = _original_rotation;
                transform.Rotate(0, 180, 0);
                _pill_state = 2;
            } else if (_pill_state == 2)
            {
                transform.rotation = _original_rotation;
                transform.Rotate(0, 0, 90);
                transform.Rotate(0, 180, 0);
                _pill_state = 3;
            } else if (_pill_state == 3)
            {
                transform.rotation = _original_rotation;
                _pill_state = 0;
            }


            //float angleRotation = transform.rotation.z;

        }

        if (Input.GetKeyDown(KeyCode.Z))
        {

            transform.Rotate(0, 180, 0);
            

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
