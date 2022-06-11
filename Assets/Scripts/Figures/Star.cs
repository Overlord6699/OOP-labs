using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//пришлось сделать из-за Панева
public class Star : BaseFigure
{
    [SerializeField]
    private float XAdding = 100;
    [SerializeField]
    private float YAdding = 100;

    private void Awake()
    {
        _name = "Star";
    }

    public override void CreateFigure(params Vector2[] points)
    {
        base.CreateFigure();
    }

    protected override void WritePoints()
    {
        Adapter adapter = new Adapter();
        List<PluginBase.FloatPair> points = adapter.DrawStar();
       
        foreach(PluginBase.FloatPair pair in points)
        {
            _points.Add(new Vector2(pair.x + XAdding, pair.y + YAdding));
        }
    }

    public override void Draw(in bool writingFigurePoints = true)
    {
        base.Draw(writingFigurePoints);
    }
}
