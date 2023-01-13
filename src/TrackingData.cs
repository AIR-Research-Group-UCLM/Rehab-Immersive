using System;
using Oculus.Interaction.Input;
using UnityEngine;

[Serializable]
public class HandTracking
{
    public int frame;
    public double time;
    public bool handDetected;
    public bool highConfidence;
    public string handPosition;
    public bool isPinchGrabbing;
    public bool isPalmGrabbing;
    public bool isAutogripGrabbing;
    public float strengthThumbIndex;
    public float strengthThumbMiddle;
    public float strengthThumbRing;
    public float strengthThumbPinky;


    public float strengthPalmIndex;
    public float strengthPalmMiddle;
    public float strengthPalmRing;
    public float strengthPalmPinky;
    public Vector3 headPosition;
    public Vector3 handVelocity;
    public float wristTwist;

    
    public HandTracking() : this(0, 0, true, false, false, 
        new Vector3().ToString(), false, false, false, 
        0.0f, 0.0f, 0.0f, 0.0f,
        0.0f, 0.0f,
        0.0f, 0.0f, Vector3.zero, 
        Vector3.zero, 0.0f)

    {
    }

    public HandTracking(int frame, double time, bool isRight, bool handDetected, bool highConfidence,
        String handPosition,
        bool isPinchGrabbing,
        bool isPalmGrabbing, bool isAutogrip, 
        float grabScoreThumbMiddle, float grabScoreThumbRing, float grabScoreThumbPinky, float grabScoreThumb,
        float grabScorePalmIndex, float grabScorePalmMiddle, float grabScorePalmRing, float grabScorePalmPinky, 
        Vector3 headPosition,
        Vector3 handVelocity, float wristTwist
    )
    {
        this.frame = frame;
        this.time = time;
        this.handDetected = handDetected;
        this.highConfidence = highConfidence;
        this.handPosition = handPosition;
        this.isPinchGrabbing = isPinchGrabbing;
        this.strengthThumbMiddle = grabScoreThumbMiddle;
        this.strengthThumbRing = grabScoreThumbRing;
        this.strengthThumbPinky = grabScoreThumbPinky;
        this.isPalmGrabbing = isPalmGrabbing;
        this.isAutogripGrabbing = isAutogrip;
        this.strengthPalmIndex = grabScorePalmIndex;
        this.strengthPalmMiddle = grabScorePalmMiddle;
        this.strengthPalmRing = grabScorePalmRing;
        this.strengthPalmPinky = grabScorePalmPinky;
        this.handVelocity = handVelocity;
        this.headPosition = headPosition;
        this.wristTwist = wristTwist;
        
    }

    public HandTracking(int frame, double time, bool isRight, bool handDetected, bool highConfidence, 
        Vector3 headPosition, Vector3 handPosition) : 
        this(frame, time, isRight, handDetected,highConfidence, 
        handPosition.ToString("f3"), false, false, false,  0.0f, 0.0f,
        0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
        0.0f, headPosition, Vector3.zero, 0.0f)
    {
    }

    /**
     * Set finger/hand strength 
     */
    public void SetStrength(HandFinger finger, float actualStregth, bool palm)
    {
        switch (finger)
        {
            case HandFinger.Index:
                if (palm)
                    strengthPalmIndex = actualStregth;
                else
                {
                    strengthThumbIndex = actualStregth;
                }

                break;
            case HandFinger.Middle:
                if (palm)
                    strengthPalmMiddle = actualStregth;
                else
                {
                    strengthThumbMiddle = actualStregth;
                }

                break;
            case HandFinger.Pinky:
                if (palm)
                    strengthPalmPinky = actualStregth;
                else
                {
                    strengthThumbPinky = actualStregth;
                }

                break;
            case HandFinger.Ring:
                if (palm)
                    strengthPalmRing = actualStregth;
                else
                {
                    strengthThumbRing = actualStregth;
                }

                break;
        }
    }

    public Vector3 HeadPosition
    {
        get => headPosition;
        set => headPosition = value;
    }

    public int Frame
    {
        get => frame;
        set => frame = value;
    }

    public double Time
    {
        get => time;
        set => time = value;
    }

    public float StrengthThumbIndex
    {
        get => strengthThumbIndex;
        set => strengthThumbIndex = value;
    }

    public float StrengthThumbMiddle
    {
        get => strengthThumbMiddle;
        set => strengthThumbMiddle = value;
    }

    public float StrengthThumbRing
    {
        get => strengthThumbRing;
        set => strengthThumbRing = value;
    }

    public float StrengthThumbPinky
    {
        get => strengthThumbPinky;
        set => strengthThumbPinky = value;
    }


    public float StrengthPalmIndex
    {
        get => strengthPalmIndex;
        set => strengthPalmIndex = value;
    }

    public float StrengthPalmMiddle
    {
        get => strengthPalmMiddle;
        set => strengthPalmMiddle = value;
    }

    public float StrengthPalmRing
    {
        get => strengthPalmRing;
        set => strengthPalmRing = value;
    }

    public bool HandDetected
    {
        get => handDetected;
        set => handDetected = value;
    }

    public float StrengthPalmPinky
    {
        get => strengthPalmPinky;
        set => strengthPalmPinky = value;
    }


    public string HandPosition
    {
        get => handPosition;
        set => handPosition = value;
    }

    public bool IsPinchGrabbing
    {
        get => isPinchGrabbing;
        set => isPinchGrabbing = value;
    }

    public bool IsPalmGrabbing
    {
        get => isPalmGrabbing;
        set => isPalmGrabbing = value;
    }

    public bool IsAutogripGrabbing
    {
        get => isAutogripGrabbing;
        set => isAutogripGrabbing = value;
    }

    public override string ToString()
    {
       
   
        return frame.ToString()
               + ";" + time.ToString() + ";" 
               + headPosition.ToString("f3") + ";"
               + handDetected.ToString() + ";"
               + highConfidence.ToString() + ";"
               + handPosition+ ";"
               + handVelocity.ToString("f4") + ";"
               + isPinchGrabbing.ToString() + ";"
               + isPalmGrabbing.ToString() + ";"
               + isAutogripGrabbing.ToString() + ";"
               + strengthThumbIndex.ToString() + ";"
               + strengthThumbMiddle.ToString() + ";"
               + strengthThumbRing.ToString() + ";" 
               + strengthThumbPinky.ToString() + ";"
               + strengthPalmIndex.ToString() + ";"
               + strengthPalmMiddle.ToString() + ";"
               + strengthPalmRing.ToString() + ";" 
               + strengthPalmPinky.ToString() + ";" 
               + wristTwist.ToString("f3")+ ";" 
               + "\n";
    }
}