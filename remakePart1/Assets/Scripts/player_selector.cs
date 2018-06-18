using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class player_selector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.position = new Vector3(transform.position.x, -2.65f, transform.position.z);
        } else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.position = new Vector3(transform.position.x, -1.975f, transform.position.z);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (transform.position.y == -1.975f)
            {
                SceneManager.LoadScene("BoardGame");
            }
            else
            {
                SceneManager.LoadScene("multiplayer_menu");
            }
        }
    }
}

