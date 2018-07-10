using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BoardBehaviour : MonoBehaviour
{

    public GameObject drMario;
    public GameObject bigVirusBlue;
    public GameObject bigVirusRed;
    public GameObject bigVirusYellow;
    public GameObject blueVirusPrefab;
    public GameObject redVirusPrefab;
    public GameObject yellowVirusPrefab;
    public GameObject pillPrefab;
    public Sprite transparentPill;
    public Sprite marioGettingPill;
    public Sprite endGameBanner;
    private Dictionary<string, GameObject> _virusPrefab;
    
    private GameObject _gameBanner;
    private Animator _drMarioAnimator;
    private Text _topValue;
    private Text _scoreValue;
    private GameObject _waitingPill;
    private bool _isGameOver = false;
    private bool _isGameEnded = false;
    private int _quantityBlueVirus;
    private int _quantityRedVirus;
    private int _quantityYellowVirus;
    private int _points;


    public Grid BoardGrid { get; set; }

    public void Start()
    {
        _quantityBlueVirus = Random.Range(2, 3);
        _quantityRedVirus = Random.Range(1, 3);
        _quantityYellowVirus = Random.Range(1, 1);
        _virusPrefab = new Dictionary<string, GameObject> {{ "yellow", yellowVirusPrefab },
            { "red", redVirusPrefab}, { "blue", blueVirusPrefab}};
        _gameBanner = GameObject.Find("gameBanner");
        _gameBanner.GetComponent<SpriteRenderer>().enabled = false;
        _drMarioAnimator = drMario.GetComponent<Animator>();
        _topValue = GameObject.Find("TopValueText").GetComponent<Text>();
        _topValue.text = "0010000";
        _scoreValue = GameObject.Find("ScoreValueText").GetComponent<Text>();
        BoardGrid = new Grid();
        CreateAllVirus();
        CreateNewPill(true);
        
    }

    public void CreateNewPill(bool isThrowing)
    {
        if (!_isGameOver && !_isGameEnded)
        {
            if (BoardGrid.IsGameOver == true)
            {
                OverGame();
            } else
            {
                _drMarioAnimator.SetInteger("MarioState", 0);
                _waitingPill = Instantiate(pillPrefab, new Vector3(50, 50, 0), Quaternion.identity); 
                _waitingPill.transform.parent = transform;
                _waitingPill.GetComponent<Animator>().enabled = true;
                PillBehaviour pillBehaviour = _waitingPill.GetComponent<PillBehaviour>();
                pillBehaviour.Board = transform.gameObject;
                pillBehaviour.DrMarioAnimator = _drMarioAnimator;
                ThrowPill(isThrowing);
            }  
        } else
        {
            _waitingPill = null;
        }
    }

    public void UpdatePoints(int virusQuantity)
    {
        _points += (virusQuantity * 100);
        string points = _points.ToString();
        _scoreValue.text = points.PadLeft(7, '0');
    }

    public void OverGame()
    {
        _isGameOver = true;
        _drMarioAnimator.SetInteger("MarioState", 99);
        _gameBanner.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void EndGame()
    {
        _isGameEnded = true;
        SpriteRenderer renderer = _gameBanner.GetComponent<SpriteRenderer>();
        renderer.sprite = endGameBanner;
        renderer.enabled = true;

    }

    public void ThrowPill(bool isThrowing)
    {
        if (_waitingPill)
        {
            _waitingPill.GetComponent<Animator>().SetBool("isThrowing", isThrowing);
        }
    }

    public void Update()
    {

    }

    private void CreateAllVirus()
    {
        BigVirusBehaviour bigVirusBlueBehaviour = bigVirusBlue.GetComponent<BigVirusBehaviour>();
        BigVirusBehaviour bigVirusRedBehaviour = bigVirusRed.GetComponent<BigVirusBehaviour>();
        BigVirusBehaviour bigVirusYellowBehaviour = bigVirusYellow.GetComponent<BigVirusBehaviour>();
        VirusBehaviour virusBehaviour = null;
        for (int i = 0; i < _quantityBlueVirus; i++)
        {
            GameObject virus = GameObject.Instantiate(_virusPrefab["blue"], new Vector3(0,0,0), Quaternion.Euler(0, 0, 0));
            virusBehaviour = virus.GetComponent<VirusBehaviour>();
            virusBehaviour.SetBigVirusAndBoard(bigVirusBlueBehaviour, transform.gameObject);
            
        }
        for (int i = 0; i < _quantityRedVirus; i++)
        {
            GameObject virus = GameObject.Instantiate(_virusPrefab["red"], new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
            virusBehaviour = virus.GetComponent<VirusBehaviour>();
            virusBehaviour.SetBigVirusAndBoard(bigVirusRedBehaviour, transform.gameObject);
        }
        for (int i = 0; i < _quantityYellowVirus; i++)
        {
            GameObject virus = GameObject.Instantiate(_virusPrefab["yellow"], new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
            virusBehaviour = virus.GetComponent<VirusBehaviour>();
            virusBehaviour.SetBigVirusAndBoard(bigVirusYellowBehaviour, transform.gameObject);

        }
        
    }

    public void UpdateVirusQuantity(string virusColor)
    {
        switch(virusColor)
        {
            case "blue":
                _quantityBlueVirus -= 1;
                break;
            case "red":
                _quantityRedVirus -= 1;
                break;
            case "yellow":
                _quantityYellowVirus -= 1;
                break;
            default:
                Debug.Log("Something is wrong");
                break;
        }

        if (_quantityBlueVirus == 0 && _quantityRedVirus == 0 && _quantityYellowVirus == 0)
        {
            EndGame();
        }
    }

    public bool CheckVirusQuantity(string virusColor)
    {
        switch (virusColor)
        {
            case "blue":
                return _quantityBlueVirus == 0;
            case "red":
                return _quantityRedVirus == 0;
            case "yellow":
                return _quantityYellowVirus == 0;
            default:
                return false;
        }
   }
}