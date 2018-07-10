using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillPartBehaviour : MonoBehaviour
{

    private PillPart _pillPartObj;
    public Sprite pillAlone;
    public Sprite pillTransparent;
    public Sprite pillRight;
    public Sprite pillLeft;
    public Sprite pillUp;
    public Sprite pillDown;
    public Dictionary<string, Sprite> sprites ;


    public PillPart PillPartObj {
        get {
            return this._pillPartObj;
        }
        set
        {
            this._pillPartObj = value;
            this.UpdatePrefabParameters();
        }
    }

    public void UpdateSprite(string type)
    {
        if (!_pillPartObj.IsDestroyed)
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            renderer.sprite = sprites[type];
        }
    }

    public void UpdatePosition(Vector3 newPosition)
    {
        if (!_pillPartObj.IsDestroyed)
        {
            transform.position = new Vector3(transform.position.x + newPosition.x, transform.position.y + newPosition.y, 0);
        }
    }

    private void UpdatePrefabParameters()
    {
        SpriteRenderer pillPartSpriteRender = GetComponent<SpriteRenderer>();
        pillPartSpriteRender.color = this._pillPartObj.PillPartColor;
    }

    public void Start()
    {
        sprites = new Dictionary<string, Sprite> { { "up", pillUp },
        { "down", pillDown }, { "left", pillLeft }, { "right", pillRight }, { "transparent", pillTransparent }, { "alone", pillAlone } };
    }

    public void Destroy()
    {
        _pillPartObj.IsDestroyed = true;
        Destroy(transform.gameObject);
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        PillBehaviour pillBehaviour = transform.parent.GetComponent<PillBehaviour>();
        PillPartBehaviour otherPill = other.collider.GetComponent<PillPartBehaviour>();
        if (_pillPartObj.IsDestroyed != true)
        {
            if (otherPill._pillPartObj.ParentId == _pillPartObj.ParentId)
            {
                UpdateSprite("alone");
                _pillPartObj.IsAlone = true;
                if (!_pillPartObj.IsFirst)
                {
                    pillBehaviour.ChangeFirstPillPart();
                }
                pillBehaviour.ClearSecondPillPart();

            }
            if (transform.GetComponentInParent<PillBehaviour>().finishedMoviment)
            {
                transform.GetComponentInParent<PillBehaviour>().stopTemporaryMoviment = false;
                StartCoroutine(pillBehaviour.MovimentAfterStop());
            }
            
        }
        
    }

    /*public void OnCollisionEnter2D(Collision2D other)
    {
        PillBehaviour pillBehaviour = transform.parent.GetComponent<PillBehaviour>();
        PillPartBehaviour otherPill = other.collider.GetComponent<PillPartBehaviour>();
        if (_pillPartObj.IsDestroyed != true)
        {
            if (otherPill._pillPartObj.ParentId != _pillPartObj.ParentId && !otherPill.GetComponentInParent<PillBehaviour>().finishedMoviment &&
                !transform.GetComponentInParent<PillBehaviour>().finishedMoviment)
            {
                PillBehaviour pillBh = transform.GetComponentInParent<PillBehaviour>();
                if (!pillBh.onlyDownMoviment)
                {
                    pillBh.stopTemporaryMoviment = true;
                }
                
            }
        }
    }*/

}