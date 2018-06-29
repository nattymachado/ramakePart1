using UnityEngine;
using System.Collections.Generic;

public static class Constants {
    public static float WaitBeforePotentialMatchesCheck = 0.5f;
    public static int Rows = 16;
    public static int Columns = 8;
    private static Color yellowColor = new Color(1, 0.6313726f, 0.1058824f);
    private static Color blueColor = new Color(0, 0.5686275f, 0.9843137f);
    private static Color redColor = new Color(1, 0.172549f, 0.3490196f); 
    public static Dictionary<string, Color> ColorsDefinitions = new Dictionary<string, Color> {{ "yellow", yellowColor}, { "red", redColor }, { "blue", blueColor } };
    public static List<string>  ColorDefinitionsKeys = new List<string>(Constants.ColorsDefinitions.Keys);
    public static float VirusSize = 0.9f;
    public static float InitPositionRows = 3.3f;
    public static float InitPositionColumns = -2.7f;
    public static float WaitForMoviment = 0.5f;
}