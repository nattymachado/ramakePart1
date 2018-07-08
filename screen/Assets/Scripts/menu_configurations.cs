using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_configurations : MonoBehaviour {

    private int option;
    public Sprite virus_level_highlight;
    public Sprite virus_level_no_highlight;
    public Sprite speed_highlight;
    public Sprite speed_no_highlight;
    public Sprite music_type_highlight;
    public Sprite music_type_no_higlight;
    public Sprite menu_fever_outlined;
    public Sprite menu_fever;
    public Sprite chill_outlined;
    public Sprite chill;
    public Sprite off_outlined;
    public Sprite off;
    public Sprite menu_arrow_down;
    public Sprite menu_arrow_down_large;

    private void Start()
    {
        option = 0;
    }
    void Update () {

        if (Input.GetKeyDown(KeyCode.DownArrow) && option<2)
        {
            option += 1;
            //_hearth_sound.Play();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && option>0)
        {
            option -= 1; 
        }

        if (option == 0)
        {
            GameObject.Find("menu_virus_level").GetComponent<SpriteRenderer>().sprite = virus_level_highlight;
        } else
        {
            GameObject.Find("menu_virus_level").GetComponent<SpriteRenderer>().sprite = virus_level_no_highlight;
        }

        if (option == 1)
        {
            GameObject.Find("menu_speed").GetComponent<SpriteRenderer>().sprite = speed_highlight;
        }
        else
        {
            GameObject.Find("menu_speed").GetComponent<SpriteRenderer>().sprite = speed_no_highlight;
        }

        if (option == 2)
        {
            GameObject.Find("menu_music_type").GetComponent<SpriteRenderer>().sprite = music_type_highlight;        
        }
        else
        {
            GameObject.Find("menu_music_type").GetComponent<SpriteRenderer>().sprite = music_type_no_higlight;
        }



    }
}
