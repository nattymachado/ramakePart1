using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusBehaviour : MonoBehaviour
{

    public Virus VirusObject { get; set; }
    private GameObject _board;
    private Grid _grid;
    private BoardBehaviour _boardBehaviour;
    public BigVirusBehaviour BigVirusBehaviour { get; set; }

    public GameObject Board {
        get
        {
            return _board;
        }
        set
        {
            _board = value;
            _boardBehaviour = _board.GetComponent<BoardBehaviour>();
            _grid = _boardBehaviour.BoardGrid;
            VirusObject = new Virus(Constants.ColorsDefinitions[keyColor], _board.transform, _grid.GetEmptyPosition(), transform, this);
            _grid.IncludePositionsOnBoard(VirusObject);
        }
    }
    

    
    
    public string keyColor;
    public Sprite virusTransparent;
    public string virusBigAnimationName;


    public void Start()
    {
        
    }

    public void SetBigVirusAndBoard(BigVirusBehaviour bigVirusBehaviour, GameObject board)
    {
        BigVirusBehaviour = bigVirusBehaviour;
        Board = board;
    }

    public void UpdateSprite()
    {
        Animator animator = GetComponent<Animator>();
        if (!VirusObject.IsDestroyed && animator != null)
        {
            animator.enabled = false;
            Destroy(animator);
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            renderer.sprite = virusTransparent;
            renderer.color = Constants.ColorsDefinitions[keyColor];
            BigVirusBehaviour.SetVirusDown();
        }
    }

    public void Destroy()
    {
        VirusObject.IsDestroyed = true;
        _boardBehaviour.UpdateVirusQuantity(keyColor);
        bool isLastVirus = _boardBehaviour.CheckVirusQuantity(keyColor);
        if (isLastVirus)
        {
            BigVirusBehaviour.Destroy();
        }
        Destroy(transform.gameObject);

    }

}