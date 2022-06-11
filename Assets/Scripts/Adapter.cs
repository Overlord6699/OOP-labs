using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adapter : Star4, IPlugin, PluginBase.IPlugin
{
    public void ChangeFigureNumber(in List<GameObject> activeFigures)
    {
    }

    public void Connect()
    {
    }

    public List<PluginBase.FloatPair> DrawStar()
    {
        return GetPoints();
        
    }
}

