using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using UnityEngine;


public class HistoricalWritter
{
    private static readonly string _filename = "Historical.csv";


    private string _dataHeader =
        "USER;GAME;DATE;LEVEL;TIME;GENERATED_BLOCKS;SCORE_BLOCKS;" +
        "ERROR_BLOCKS;HAND_RIGHT;AUTOGRIP;TRACKING_FILE";

    private string _directoryName;
    private readonly string _filePath;
    


    public HistoricalWritter(string userPath)
    {
       
        _filePath = Path.Combine(userPath, _filename);
    }


    

    public void WriteHistorical(Historical historicalData)
    {
        bool exists = File.Exists(_filePath);
        using StreamWriter file = new StreamWriter(_filePath, true, Encoding.UTF8);
        if (!exists)
        {
            file.WriteLine(_dataHeader);
        }

        file.WriteLine(historicalData + "\n");
    }
}