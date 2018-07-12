using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed class Configuration
{
    private static readonly Configuration instance = new Configuration();
    private int _speed;
    private float _speedPills;
    private string _speedName;
    public Dictionary<int, Color> LevelColor = new Dictionary<int, Color>(){ { 0, Color.white }, { 1, Color.yellow }, { 2, Color.blue }, { 3, Color.red },
        { 4, Color.white }, { 5, Color.yellow }, { 6, Color.blue }, { 7, Color.red }, { 8, Color.white }, { 9, Color.yellow },
        { 10, Color.blue }, { 11, Color.red }, { 12, Color.white }, { 13, Color.yellow }, { 14, Color.blue }, { 15, Color.red },
        { 16, Color.white }, { 17, Color.yellow }, { 18, Color.blue }, { 19, Color.yellow }  };

    public int Level { get; set; }
    public float SpeedPills
    {
        get
        {
            return _speedPills;
        }
    }
    
    public int Speed {
        get
        {
            return _speed;
        }
        set
        {
            _speed = value;
            if (_speed == 0)
            {
                _speedName = "LOW";
                _speedPills = 1f;
            }
            else if (_speed == 1)
            {
                _speedName = "MED";
                _speedPills = 0.4f;
            }
            else
            {
                _speedName = "HI";
                _speedPills = 0.2f;
            }
        }
    }
    
    public string SpeedName
    {
        get
        {
            return _speedName;
        }
    }
    public int Music { get; set; }

    public static Configuration Instance {
        get
        {
            return instance;
        }
    }

    

    private Configuration()
    {
        
    }

    

}
