using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using UnityEngine;

public class Archivator: MonoBehaviour
{
    public delegate void DearchivateFile(string path);
    public event DearchivateFile OnFileDearchivated;

    public string SourceFile { get; set; }
    private string compressedFile; 
    private string targetFile; 

    //[SerializeField]
    //private FigureSerializer _serializer;
    //[SerializeField]
    //private Graphics _graphics;

    private void Awake()
    {
        Subscribe();

        //sourceFile = _serializer.Path;

        var from = SourceFile.LastIndexOf('\\') + 1;
        var to = SourceFile.IndexOf('.');
        var length = to - from;

        targetFile = SourceFile.Replace(SourceFile.Substring(from, length), "new");

        compressedFile = SourceFile.Replace(".txt", ".gz");
    }

    private void Subscribe()
    {
        //_serializer.OnFiguresSerialized += Archivate;
        //_graphics.OnLoadFigures += Dearchivate;
    }

    private void Unsubscribe()
    {
        //_serializer.OnFiguresSerialized -= Archivate;
        //_graphics.OnLoadFigures -= Dearchivate;
    }

    public async void Archivate(string path)
    {
        SourceFile = path;

        await CompressAsync(SourceFile, compressedFile);
    }

    public async void Dearchivate()
    {
        await DecompressAsync(compressedFile, targetFile);
    }

    async Task CompressAsync(string sourceFile, string compressedFile)
    {
        using FileStream sourceStream = new FileStream(sourceFile, FileMode.OpenOrCreate);
        using FileStream targetStream = File.Create(compressedFile);

        using GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress);

        await sourceStream.CopyToAsync(compressionStream);

        Debug.Log($"Завершено сжатие файла {sourceFile}.");
        Debug.Log($"Размер файла до сжатия: {sourceStream.Length}  ,после: {targetStream.Length}");
    }

    async Task DecompressAsync(string compressedFile, string targetFile)
    {
        using FileStream sourceStream = new FileStream(compressedFile, FileMode.OpenOrCreate);
        using FileStream targetStream = File.Create(targetFile);

        using GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress);

        await decompressionStream.CopyToAsync(targetStream);

        Debug.Log($"Восстановлен файл: {targetFile}");

        targetStream.Dispose();
        OnFileDearchivated?.Invoke(targetFile);
    }
}