using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


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
    public Image background;
    public Sprite transparentPill;
    public Sprite marioGettingPill;
    public AudioSource audioSource;
    public AudioClip music1, music2;
    private Dictionary<string, GameObject> _virusPrefab;
    
    public GameObject gameBannerGameOver;
    public GameObject gameBannerGameClear;
    private Animator _drMarioAnimator;
    public Text topValue;
    public Text scoreValue;
    public Text levelValue;
    public Text speedValue;
    public Text virusValue;
    private GameObject _waitingPill;
    private bool _isGameOver = false;
    private bool _isGameEnded = false;
    private int _quantityBlueVirus;
    private int _quantityRedVirus;
    private int _quantityYellowVirus;
    private int _points;
    private Configuration _configuration;



    public Grid BoardGrid { get; set; }

    public void Start()
    {
        _configuration = Configuration.Instance;
        background.color = _configuration.LevelColor[_configuration.Level];


        switch (_configuration.Music)
        {
            case 0:
                audioSource.clip = music1;
                break;
            case 1:
                audioSource.clip = music2;
                break;
            default:
                break;
        }

        if (_configuration.Music < 2)
        {
            audioSource.enabled = true;
            audioSource.Play();
        } else
        {
            audioSource.enabled = false;
        }

        levelValue.text = _configuration.Level.ToString().PadLeft(2, '0');
        speedValue.text = _configuration.SpeedName;
        int totalVirus = _configuration.Level * 4;
        totalVirus += 4;
        int totalColors = totalVirus / 3;
        int virusWithMore = Random.Range(0, 2);
        _quantityBlueVirus = totalColors;
        _quantityRedVirus = totalColors;
        _quantityYellowVirus = totalColors;
        switch (virusWithMore)
        {
            case 0:
                _quantityBlueVirus += totalVirus % 3;
                break;
            case 1:
                _quantityRedVirus += totalVirus % 3;
                break;
            case 2:
                _quantityYellowVirus += totalVirus % 3;
                break;
            default:
                break;
        }
        virusValue.text = (_quantityBlueVirus + _quantityRedVirus + _quantityYellowVirus).ToString().PadLeft(2, '0');
        _virusPrefab = new Dictionary<string, GameObject> {{ "yellow", yellowVirusPrefab },
            { "red", redVirusPrefab}, { "blue", blueVirusPrefab}};
        gameBannerGameOver.GetComponent<SpriteRenderer>().enabled = false;
        gameBannerGameClear.GetComponent<SpriteRenderer>().enabled = false;
        _drMarioAnimator = drMario.GetComponent<Animator>();
        topValue.text = "0010000";
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
        scoreValue.text = points.PadLeft(7, '0');
    }

    public void OverGame()
    {
        _isGameOver = true;
        _drMarioAnimator.SetInteger("MarioState", 99);
        gameBannerGameOver.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void EndGame()
    {
        _isGameEnded = true;
        gameBannerGameClear.GetComponent<SpriteRenderer>().enabled = true;

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
        if (Input.GetKeyDown(KeyCode.Return) && (_isGameOver  || _isGameEnded))
        {
            SceneManager.LoadScene("SinglePlayerMenu");
        }
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