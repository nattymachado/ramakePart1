using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBehaviour : MonoBehaviour
{

    
    public GameObject blueVirusPrefab;
    public GameObject redVirusPrefab;
    public GameObject yellowVirusPrefab;
    public GameObject pillPrefab;
    public Sprite transparentPill;
    public Sprite marioGettingPill;
    private Dictionary<string, GameObject> _virusPrefab;
    private GameObject _drMario;
    private Animator _drMarioAnimator;
    private GameObject _waitingPill;

    public Grid BoardGrid { get; set; }

    public void Start()
    {
                
        _virusPrefab = new Dictionary<string, GameObject> {{ "yellow", yellowVirusPrefab },
            { "red", redVirusPrefab}, { "blue", blueVirusPrefab}};
        _drMario = GameObject.Find("gameMarioThrowingPill");
        _drMarioAnimator = _drMario.GetComponent<Animator>();
        BoardGrid = new Grid();
        CreateAllVirus();
        CreateNewPill(true);
        
    }

    public void CreateNewPill(bool isThrowing)
    {
        _drMarioAnimator.SetInteger("MarioState", 0);
        _waitingPill = GameObject.Instantiate(pillPrefab, new Vector3(50f, 50f, 0), Quaternion.Euler(0, 0, 0));
        ThrowPill(isThrowing);
    }

    public void ThrowPill(bool isThrowing)
    {
        _waitingPill.GetComponent<Animator>().SetBool("isThrowing", isThrowing);
    }

    public void Update()
    {

    }

    private void CreateAllVirus()
    {
        int quantityBlueVirus = Random.Range(1, 3);
        int quantityRedVirus = Random.Range(1, 3);
        int quantityYellowVirus = Random.Range(1, 3);

        for (int i = 0; i < quantityBlueVirus; i++)
        {
            GameObject virus = GameObject.Instantiate(_virusPrefab["blue"], new Vector3(0,0,0), Quaternion.Euler(0, 0, 0));
            
        }
        for (int i = 0; i < quantityRedVirus; i++)
        {
            GameObject virus = GameObject.Instantiate(_virusPrefab["red"], new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
            
        }
        for (int i = 0; i < quantityYellowVirus; i++)
        {
            GameObject virus = GameObject.Instantiate(_virusPrefab["yellow"], new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
            
        }
    }

    

    

}