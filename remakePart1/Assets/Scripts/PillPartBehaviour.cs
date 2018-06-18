using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PillPartBehaviour : MonoBehaviour {

    public Sprite pillOnePartSprite = null;
    public int pillId ;
    public int pillPartIndex;
    private ContactPoint2D[] _contactPoints = new ContactPoint2D[4];
    private Coroutine _movimentRoutine = null;
    public bool isDestroyed = false;
    
public void OnTriggerExit2D(Collider2D other)
    {
        //The pill is only one part now

        if (!isDestroyed)
        {
            PillBehaviour pillBehaviour = transform.parent.GetComponent<PillBehaviour>();

            if (other.GetComponent<PillPartBehaviour>().pillId == transform.GetComponent<PillPartBehaviour>().pillId)
            {
                SpriteRenderer spriteRenderer = transform.GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = pillOnePartSprite;
                pillBehaviour.alone = true;
                if (pillPartIndex == 1)
                {
                    pillBehaviour._pills[0] = pillBehaviour._pills[1];
                    pillBehaviour._positions[0, 0] = pillBehaviour._positions[1, 0];
                    pillBehaviour._positions[0, 1] = pillBehaviour._positions[1, 1];

                }
                pillBehaviour._pills[1] = null;
                Debug.Log("Pill id:" + pillId);
                Debug.Log("Pill Index:" + pillPartIndex);
                Debug.Log("IsDestroyed: " + isDestroyed);
                
            }
            StartCoroutine(pillBehaviour.MovimentAfterStop());

        }
    }
        
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (_movimentRoutine != null)
        {
            StopCoroutine(_movimentRoutine);
        }
        

    }

    public void Destroy()
    {
        //Destroy(this);
        isDestroyed = true;
        Destroy(transform.gameObject);
    }

}
