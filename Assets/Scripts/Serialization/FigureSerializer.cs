using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using UnityEngine;
using UsefulScripts.NetScripts.Data;

public class FigureSerializer: MonoBehaviour 
{
    const string path = @"F:\1.txt";

    public string Path { get { return path; }  }


    public delegate void LoadFigures(List<FigureData> figures);
    public event LoadFigures OnFiguresDeserialized;

    public delegate void SaveFigures(string path);
    public event SaveFigures OnFiguresSerialized;

    //[SerializeField]
    //private Graphics _graphics;
    //[SerializeField]
    //private Archivator _archivator;

    private List<FigureData> loadedObjects = new List<FigureData>(10);

    void Start()
    {
        //_graphics.OnSaveFigures += SerializeFigures;
        //пришлось тут изменить
        //_archivator.OnFileDearchivated += DeserializeFigures;
    }

    public void SerializeFigures(in List<FigureData> figures)
    {
        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            BinaryFormatter serializer = new BinaryFormatter();

            serializer.Serialize(fs, figures);

        }

        OnFiguresSerialized?.Invoke(path);
    }

    public void DeserializeFigures(string newPath)
    {
        if (File.Exists(newPath))
        {
            using (FileStream fs = new FileStream(newPath, FileMode.Open))
            {
                if (fs.Length > 0)
                {
                    try
                    {
                        BinaryFormatter serializer = new BinaryFormatter();

                        loadedObjects = (List<FigureData>)serializer.Deserialize(fs);
                    }
                    catch
                    {
                        Debug.Log("Файл некорректен");
                    }    
                }
                else
                    Debug.Log("Файл загрузки пустой");
            }
        }else
            Debug.Log("Файл по указанному пути не существует");

        OnFiguresDeserialized?.Invoke(loadedObjects);
    }
}
