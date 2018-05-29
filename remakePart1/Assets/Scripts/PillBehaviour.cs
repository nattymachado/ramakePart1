﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillBehaviour : MonoBehaviour {

    private float _totalTime;
    private float _pillPartSize = 0;
    private SpriteRenderer[] _pills = null;
    private BoardBehaviour _parent_script = null;
    private SpriteRenderer _parent_sprite = null;

    // Use this for initialization
    public void Start () {
        _pills = GetComponentsInChildren<SpriteRenderer>();
        _parent_script = transform.parent.GetComponent<BoardBehaviour>();
        _parent_sprite = transform.parent.GetComponent<SpriteRenderer>();
        _pillPartSize = ((_pills[0].size.x / 2f));
    }

    // Update is called once per frame
    public void Update () {
        _totalTime += Time.deltaTime;
        
        if (_parent_script.pill != null)
        {
            if (transform.parent.localPosition.y < (transform.localPosition.y - _parent_script.speed))
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - _parent_script.speed, 0f);
                HorizontalMove();
                VerticalMove();
            }

            
        }
        
        
    }

    public void HorizontalMove()
    {
        float one_pill_size = 0;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (transform.rotation.z != 90 && transform.rotation.z != -90)
            {
                one_pill_size = (_pillPartSize * 2);
            }  else
            {
                one_pill_size = (_pillPartSize);
            }
            //if (transform.parent.localPosition.x < (transform.localPosition.x - one_pill_size))
            //{
                transform.localPosition = new Vector3(transform.localPosition.x - _pillPartSize, transform.localPosition.y, 0f);
            //}
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (transform.rotation.z != 90 && transform.rotation.z != -90)
            {
                one_pill_size = (_pillPartSize * 2);
            }
            else
            {
                one_pill_size = (_pillPartSize);
            }
            //if (_parent_sprite.size.x > (transform.localPosition.x + one_pill_size))
            //{
            transform.localPosition = new Vector3(transform.localPosition.x + _pillPartSize, transform.localPosition.y, 0f);
            //}
        }
    }

    public void VerticalMove()
    {

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.Rotate(0, 0, 90);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, -90);
        }
    }
}
