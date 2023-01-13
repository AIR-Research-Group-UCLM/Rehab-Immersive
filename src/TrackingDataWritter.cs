using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;


public class TrackingDataWriter
{
    private static readonly string _filename = "OculusTracking";

    private List<String> _trackToWrite;

    private static readonly Lazy<TrackingDataWriter> _instance =
        new Lazy<TrackingDataWriter>(() => new TrackingDataWriter());

    private const string DataHeader =
        "Frame;Time;HeadPosition;HandDetected;HighConfidence;" +
        "HandPosition;HandVelocity;IsPinchGrabbing;IsPalmGrabbing;IsAutogripGrabbing;" +
        "StrengthThumbIndex;StrengthThumbMiddle;" +
        "StrengthThumbRing;StrengthThumbPinky;" +
        "StrengthPalmIndex;StrengthPalmMiddle;StrengthPalmRing;StrengthPalmPinky;WristTwist";

    
    

    private const int Batch = 10;
    public string filePath;

    private bool _setHead;


    private TrackingDataWriter()
    {
        ResetAll();
    }

    public void ResetAll()
    {
        //First write -- add header
        _setHead = true;
        _trackToWrite = new List<string>();
    }

    public void SetPathWritter(String userPath)
    {
        string trackingData = Path.Combine(userPath, RehabConstants.DirTracking);
        filePath = Path.Combine(trackingData, _filename + DateTime.Now.ToString(RehabConstants.DateFormat) 
                                                        + RehabConstants.ExtensionCsv);

        if (!Directory.Exists(trackingData))
        {
            try
            {
                //create new user path
                Directory.CreateDirectory(trackingData);
            }
            catch (Exception e)
            {
                Debug.LogError("Error creating user directory to save data tracking: " + e.Message);
            }
        }
    }


    public static TrackingDataWriter Instance => _instance.Value;


    public void SaveTrackingData(HandTracking handTracking)
    {
        try
        {
            _trackToWrite.Add(handTracking.ToString());


            if (_trackToWrite.Count > Batch)
            {
                //write to file
                WriteTrackingData();
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error saving tracking data: " + e.Message);
        }
    }

    /**
     * Write tracking data into file
     */
    public void WriteTrackingData()
    {
        try
        {

            using (StreamWriter file = new StreamWriter
                       (filePath, true, Encoding.UTF8))
            {
                if (_setHead)
                {
                    //write header first time
                    file.WriteLine(DataHeader);
                    _setHead = false;
                }


                file.Write(String.Join("", this._trackToWrite));
            }
        }
        catch (IOException e)
        {
            Debug.LogError("Error writing tracking data: " + e.Message);
        }

        _trackToWrite = new List<String>();
    }

    public string FilePath
    {
        get => filePath;
        set => filePath = value;
    }
}