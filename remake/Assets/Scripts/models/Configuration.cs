using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed class Configuration
{
    private static readonly Configuration instance = new Configuration();
    private int _speed;
    private float _speedPills;
    private string _speedName;
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
                _speedPills = 0.9f;
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
    public string Music { get; set; }

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
