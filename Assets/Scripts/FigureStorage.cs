using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureStorage: MonoBehaviour
{
    [SerializeField]
    public List<BaseFigure> figures = new List<BaseFigure>(10);

    /*
    private void Subscribe()
    {
        for (int i = 0; i < figures.Count; i++)
            if (figures[i])
            {
                figures[i].OnDrawFigure +=
                    RespondToDraw;
            }
    }
    */


    private void Start()
    {
        //Subscribe();
    }

    public BaseFigure FindFigure(in string name)
    {
        foreach (var figure in figures)
        {
            if(figure)
                if (figure.GetName().Equals(name))
                {
                    return figure;
                }
        }

        return null;
    }
}
