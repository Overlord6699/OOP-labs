using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trapezoid : BaseFigure
{
    [SerializeField]
    private Vector2 firstPoint = new Vector2(400,400);
    [SerializeField]
    private Vector2 secondPoint = new Vector2(600, 400);
    [SerializeField]
    private Vector2 thirdPoint = new Vector2(800, 200);
    [SerializeField]
    private Vector2 fourthPoint = new Vector2(200, 200);

    private void  Awake()
    {
        _name = "Trapezoid";
    }
    
    public void CreateFigure(in Vector2 point1, in Vector2 point2, in Vector2 point3,
        in Vector2 point4)
    {
        firstPoint = point1;
        secondPoint = point2;
        thirdPoint = point3;
        fourthPoint = point4;
    }

    protected override void WritePoints()
    {
        _points.Add(firstPoint);
        _points.Add(secondPoint);
        _points.Add(thirdPoint);
        _points.Add(fourthPoint);
        _points.Add(firstPoint);
    }

    public override void Draw(in bool writingFigurePoints = true)
    {
        base.Draw(writingFigurePoints);
    }

}
