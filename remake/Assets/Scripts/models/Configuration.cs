using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed class Configuration
{
    private static readonly Configuration instance = new Configuration();
    public int Level { get; set; }
    private int _speed;
    private string _speedName;
    public int Speed {
        get
        {
            return _speed;
        }
        set {
            _speed = value;
            if (_speed == 0)
            {
                _speedName = "LOW";
            } else if (_speed == 1) {
                _speedName = "MED";
            } else {
                _speedName = "HI";
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
