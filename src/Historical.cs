using System;

[Serializable]
public class Historical
{
    public string userId;
    public string game;
    public string date;
    public int level;
    public int time;
    public int blocksGenerated;
    public int score;
    //num blocks moved with error
    public int numErrors;
    public bool handR;
    public bool autogrip;
    private string _separator = ";";
    private string _trackingFile ;
    public Historical()
    {
        game = "Box And Block";
        date = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        level = 1;
        handR = true;
        blocksGenerated = 0;
        this._trackingFile = "";
    }

    public string Game
    {
        get => game;
        set => game = value;
    }

    public string Date
    {
        get => date;
        set => date = value;
    }

    public int Level
    {
        get => level;
        set => level = value;
    }

    public int Time
    {
        get => time;
        set => time = value;
    }

  

  
    public int Score
    {
        get => score;
        set => score = value;
    }

    public int NumErrors
    {
        get => numErrors;
        set => numErrors = value;
    }

    public bool Hand
    {
        get => handR;
        set => handR = value;
    }



    public bool Autogrip
    {
        get => autogrip;
        set => autogrip = value;
    }

    public string UserId
    {
        get => userId;
        set => userId = value;
    }

    public string TrackingFile
    {
        get => _trackingFile;
        set => _trackingFile = value;
    }

    public int BlocksGenerated
    {
        get => blocksGenerated;
        set => blocksGenerated = value;
    }

    public override string ToString()
    {
        return userId + _separator + game + _separator + date + _separator + level.ToString() + _separator 
               + time.ToString() + _separator  + blocksGenerated.ToString()  + _separator 
               + score.ToString() + _separator 
               + numErrors.ToString() + _separator + handR.ToString() + _separator
               + autogrip.ToString() + _separator+ _trackingFile.ToString();
    }
}