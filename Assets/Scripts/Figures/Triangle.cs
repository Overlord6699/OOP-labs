using System;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : BaseFigure
{
    [SerializeField]
    private Vector2 firstPoint = new Vector2(600, 400);
    [SerializeField]
    private Vector2 secondPoint = new Vector2(700, 300);
    [SerializeField]
    private Vector2 thirdPoint = new Vector2(500, 300);

    private void Awake()
    {
        _name = "Triangle";
    }

    public override void CreateFigure(params Vector2[] points)
    {
        firstPoint = points[0];

        secondPoint = points[1];

        thirdPoint = points[2];
        
    }


    protected override void WritePoints()
    {
        _points.Add(firstPoint);
        _points.Add(secondPoint);
        _points.Add(thirdPoint);

        _points.Add(firstPoint);
    }
    public override void Draw(in bool writingFigurePoints = true)
    {
        base.Draw(writingFigurePoints);
    }
}
