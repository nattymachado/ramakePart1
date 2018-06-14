using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PillPartBehaviour : MonoBehaviour {

    public Sprite pillOnePartSprite = null;

    public void OnTriggerExit2D(Collider2D other)
    {
        //The pill is only one part now
        SpriteRenderer spriteRenderer = transform.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = pillOnePartSprite;
    }

    public void Destroy()
    {
        //Destroy(this);
        Destroy(transform.gameObject);
    }

}
