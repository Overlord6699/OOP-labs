using UnityEngine;

//класс является цепочкой между ui и сервисами
//также определет порядок событий загрузки\сохранения
public class ButtonController : MonoBehaviour
{
    [SerializeField]
    private Graphics graphics;
    [SerializeField]
    private Archivator archivator;
    [SerializeField]
    private FigureSerializer serializer;


    private void Awake()
    {
        archivator.SourceFile = serializer.Path;
    }

    public void DrawFigure(string name)
    {
        graphics.DrawNeededFigure(name);
    }



    //это можно было бы вынести в отдельный класс
    public void LoadFigures()
    {
        archivator.OnFileDearchivated += serializer.DeserializeFigures;
        serializer.OnFiguresDeserialized += graphics.LoadFigures;

        archivator.Dearchivate();
    }

    public void SaveFigures()
    {
        graphics.OnSaveFigures += serializer.SerializeFigures;
        serializer.OnFiguresSerialized += archivator.Archivate;

        graphics.SaveFigures();
    }
}
