using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class PlayerSelector : MonoBehaviour {

    private AudioSource _hearth_sound = null;

    // Use this for initialization
    void Start () {

        _hearth_sound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.DownArrow) && transform.position.y != -2.65f)
        {
            transform.position = new Vector3(transform.position.x, -2.65f, transform.position.z);
            _hearth_sound.Play();
        } else if (Input.GetKeyDown(KeyCode.UpArrow) && transform.position.y != -1.975f)
        {
            transform.position = new Vector3(transform.position.x, -1.975f, transform.position.z);
            _hearth_sound.Play();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (transform.position.y == -1.975f)
            {
                SceneManager.LoadScene("single_player_menu");
            }
            else
            {
                SceneManager.LoadScene("multiplayer_menu");
            }
        }
    }
}

