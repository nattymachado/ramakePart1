using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed class Configuration
{
    private static readonly Configuration instance = new Configuration();
    public int Level { get; set; }
    public int Speed { get; set; }
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
