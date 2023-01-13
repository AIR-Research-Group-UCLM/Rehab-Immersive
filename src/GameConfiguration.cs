using System;
using UnityEngine;

[Serializable]
public class GameConfiguration
{
    //hand right or left
    public bool handRight;
    public int timer;
    public int level;
    public bool autogrip;
    public string userId;
  
    public Vector3 boxPosition;
    //without wall
    public bool levelEasy;
   

    //Default values
    public GameConfiguration()
    {
        timer = 60;
        level = 1;
        boxPosition =  new Vector3(0.0f, 0.489f, 0.2f);
        autogrip = false;
        handRight = true;
        levelEasy = false;
        userId = RehabConstants.DefaultUser;
        level = 1;
        autogrip = false;
        handRight = true;
       
    
    }
    
  
}