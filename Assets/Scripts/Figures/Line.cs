using UnityEngine;
using System.Collections.Generic;
using System;

public class Line : BaseFigure
{
    [SerializeField]
    private Vector2 firstPoint = new Vector2(200, 200);
    [SerializeField]
    private Vector2 secondPoint = new Vector2(800, 400);

    private void Awake()
    {
        _name = "Line";
    }

    public override void CreateFigure(params Vector2[] points)
    {
        firstPoint = points[0];
        secondPoint = points[1];
    }

    protected override void WritePoints()
    {
        _points.Add(firstPoint);
        _points.Add(secondPoint);
    }

    public override void Draw(in bool writingFigurePoints = true)
    {
        base.Draw(writingFigurePoints); 
    }
}
