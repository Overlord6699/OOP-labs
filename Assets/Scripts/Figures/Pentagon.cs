using System;
using System.Collections.Generic;
using UnityEngine;

public class Pentagon : BaseFigure
{
    [SerializeField]
    private Vector2 firstPoint = new Vector2(500, 375);
    [SerializeField]
    private Vector2 secondPoint = new Vector2(600, 400);
    [SerializeField]
    private Vector2 thirdPoint = new Vector2(700, 375);
    [SerializeField]
    private Vector2 fourthtPoint = new Vector2(650, 300);
    [SerializeField]
    private Vector2 fifthPoint = new Vector2(550, 300);

    private void Awake()
    {
        _name = "Pentagon";
    }

    public void CreateFigure(in Vector2 point1, in Vector2 point2,
        in Vector2 point3, in Vector2 point4, in Vector2 point5)
    {
        firstPoint = point1;

        secondPoint = point2;

        thirdPoint = point3;

        fourthtPoint = point4;

        fifthPoint = point5;

    }

    protected override void WritePoints()
    {
        _points.Add(firstPoint);
        _points.Add(secondPoint);
        _points.Add(thirdPoint);
        _points.Add(fourthtPoint);
        _points.Add(fifthPoint);
        _points.Add(firstPoint);
    }

    public override void Draw(in bool writingFigurePoints = true)
    {
        base.Draw(writingFigurePoints);
    }
}
