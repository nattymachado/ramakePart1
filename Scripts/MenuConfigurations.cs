using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuConfigurations : MonoBehaviour
{
    public AudioSource _beep_vertical = null;
    public AudioSource _beep_horizontal = null;

    private int _option;
    private int _option_music;
    private int _option_speed;
    private int _option_level;
    
    public Sprite virus_level_highlight;
    public Sprite virus_level_no_highlight;
    public Sprite speed_highlight;
    public Sprite speed_no_highlight;
    public Sprite music_type_highlight;
    public Sprite music_type_no_higlight;
    public Sprite menu_fever_outlined;
    public Sprite menu_fever;
    public Sprite menu_chill_outlined;
    public Sprite menu_chill;
    public Sprite menu_off_outlined;
    public Sprite menu_off;
    public Sprite menu_arrow_down;
    public Sprite menu_arrow_down_large;

    private void Start()
    {
        _option = 0;
        _option_level = 0;
        _option_speed = 1;
        _option_music = 0;
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.DownArrow) && _option < 2)
        {
            _option += 1;
            _beep_vertical.Play();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && _option > 0)
        {
            _option -= 1;
            _beep_vertical.Play();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && _option == 0 && _option_level < 20)
        {
            _option_level += 1;
            _beep_horizontal.Play();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && _option == 0 && _option_level > 0)
        {
            _option_level -= 1;
            _beep_horizontal.Play();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && _option == 1 && _option_speed < 2)
        {
            _option_speed += 1;
            _beep_horizontal.Play();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && _option == 1 && _option_speed > 0)
        {
            _option_speed -= 1;
            _beep_horizontal.Play();
        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow) && _option == 2 && _option_music < 2)
        {
            _option_music += 1;
            _beep_horizontal.Play();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && _option == 2 && _option_music > 0)
        {
            _option_music -= 1;
            _beep_horizontal.Play();
        }

        if (_option == 0)
        {
            GameObject.Find("menu_virus_level").GetComponent<SpriteRenderer>().sprite = virus_level_highlight;
        }
        else
        {
            GameObject.Find("menu_virus_level").GetComponent<SpriteRenderer>().sprite = virus_level_no_highlight;
        }

        if (_option == 1)
        {
            GameObject.Find("menu_speed").GetComponent<SpriteRenderer>().sprite = speed_highlight;
        }
        else
        {
            GameObject.Find("menu_speed").GetComponent<SpriteRenderer>().sprite = speed_no_highlight;
        }

        if (_option == 2)
        {
            GameObject.Find("menu_music_type").GetComponent<SpriteRenderer>().sprite = music_type_highlight;
        }
        else
        {
            GameObject.Find("menu_music_type").GetComponent<SpriteRenderer>().sprite = music_type_no_higlight;
        }

        GameObject.Find("menu_arrow_down").transform.position = new Vector3(-1.82f + (_option_level * 0.18f), 1.63f, 0);

        GameObject.Find("menu_arrow_down_large").transform.position = new Vector3(-1.2f + (_option_speed * 1.3f), -0.82f, 0);

        if (_option_music == 0)
        {
            GameObject.Find("menu_fever").GetComponent<SpriteRenderer>().sprite = menu_fever_outlined;
        }
        else
        {
            GameObject.Find("menu_fever").GetComponent<SpriteRenderer>().sprite = menu_fever;
        }

        if (_option_music == 1)
        {
            GameObject.Find("menu_chill").GetComponent<SpriteRenderer>().sprite = menu_chill_outlined;
        }
        else
        {
            GameObject.Find("menu_chill").GetComponent<SpriteRenderer>().sprite = menu_chill;
        }

        if (_option_music == 2)
        {
            GameObject.Find("menu_off").GetComponent<SpriteRenderer>().sprite = menu_off_outlined;
        }
        else
        {
            GameObject.Find("menu_off").GetComponent<SpriteRenderer>().sprite = menu_off;
        }
    }
}
