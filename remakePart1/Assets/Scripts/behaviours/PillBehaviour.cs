using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillBehaviour : MonoBehaviour {

    private GameObject _drMario;
    private GameObject _board;
    private Animator _drMarioAnimator;
    private float _totalTime;
    private float _period = 0;
    private bool _finishedAnimation = false;

    // Use this for initialization
    void Start () {
        _drMario = GameObject.Find("gameMarioThrowingPill");
        _drMarioAnimator = _drMario.GetComponent<Animator>();
        _board = GameObject.Find("board");

    }
	
	// Update is called once per frame
	void Update () {

        _totalTime += Time.deltaTime;
        if (_finishedAnimation == true)
        {
            MovimentDown();
            
        }
        

    }

    public void MovimentDown()
    {
      if (_period > Constants.WaitForMoviment)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 1f, 0f);
                _period = 0;
            }
            else
            {
                _period += Time.deltaTime;
            }
       

    }

    public void PillAnimationEndEvent(string s)
    {
        Debug.Log("PrintEvent: " + s + " called at: " + Time.time);
        _drMarioAnimator.SetInteger("MarioState", 1);
        transform.GetComponent<Animator>().enabled = false;
        StartCoroutine(_board.GetComponent<BoardBehaviour>().CreateNewPill());
        _finishedAnimation = true;
    }
}
