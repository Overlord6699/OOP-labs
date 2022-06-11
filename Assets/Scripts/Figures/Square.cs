using System;
using System.Collections.Generic;
using UnityEngine;

public class Square : Rectangle
{
    private void Awake()
    {
        _name = "Square";

        //другие точки по умолчанию
        leftPoint = new Vector2(400, 400);
        rightPoint = new Vector2(600,200);
    }

    public override void CreateFigure(params Vector2[] points)
    {
        base.CreateFigure();
    }

    protected override void CalculatePoints()
    {
        _points.Add(new Vector2(rightPoint.x, leftPoint.y));
        _points.Add(rightPoint);
        _points.Add(new Vector2(leftPoint.x, rightPoint.y));
    }

    protected override void WritePoints()
    {
        _points.Add(leftPoint);

        CalculatePoints();

        _points.Add(leftPoint);
    }

    public override void Draw(in bool writingFigurePoints = true)
    {
        base.Draw(writingFigurePoints);
    }
}
