using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;


public class PlayerSelector : MonoBehaviour {

    private AudioSource _hearth_sound = null;
    private AsyncOperation _async;

    // Use this for initialization
    void Start () {

        _hearth_sound = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {

        
        if (Input.GetKeyDown(KeyCode.DownArrow) && transform.position.y != -2.6f)
        {
            transform.position = new Vector3(transform.position.x, -2.6f, transform.position.z);
            _hearth_sound.Play();
        } else if (Input.GetKeyDown(KeyCode.UpArrow) && transform.position.y != -1.97f)
        {
            transform.position = new Vector3(transform.position.x, -1.97f, transform.position.z);
            _hearth_sound.Play();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (transform.position.y == -1.97f)
            {
                SceneManager.LoadScene("SinglePlayerMenu");
            }
            else
            {
                SceneManager.LoadScene("CreditsScene");
            }
        }
    }
}

