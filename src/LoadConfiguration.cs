using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using UnityEngine;

public class LoadConfiguration : MonoBehaviour
{
    private string idUser = "";

    //read command line arguments
    [SerializeField] public SOGameConfiguration gameConfigSO;

    void Start()
    {
        LoadUserConfig();

    }


    /**
     * Load extra arguments from ADB
     */
    string LoadExtraArgumentsData()
    {
        string idUser = RehabConstants.DefaultUser;
        try
        {
            AndroidJavaClass UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            if (UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity") != null)
            {
                AndroidJavaObject currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                AndroidJavaObject intent = currentActivity.Call<AndroidJavaObject>("getIntent");
                AndroidJavaObject extras = intent.Call<AndroidJavaObject>("getExtras");
                idUser = extras.Call<string>("getString", "user");
            }
        }
        catch (Exception e)
        {
            Debug.Log("No user name input detected, default value (DefaultUser) will be used. " + e.Message);
        }

        idUser = idUser != null && idUser.Length > 0 ? idUser : RehabConstants.DefaultUser;
        return idUser;
    }

    private void LoadUserConfig()
    {
        gameConfigSO.resetAll();
        string idUser = LoadExtraArgumentsData();

        //loadUserConfiguration
        gameConfigSO.gameConfiguration.userId = idUser;
        //search idUser into path to load configuration
        DirectoryInfo dirParent = Directory.GetParent(Application.persistentDataPath);
        if (dirParent != null)
        {
            string parentPath = dirParent.ToString();
            string userPath = Path.Combine(parentPath, RehabConstants.DirBbt, idUser);
            gameConfigSO.userPath = userPath;
            String userFileConfiguration = Path.Combine(gameConfigSO.userPath, RehabConstants.FileConfiguration);
       
            if (!Directory.Exists(userPath))
            {
                try
                {
                    //create new user path
                   Directory.CreateDirectory(userPath);
                }
                catch (Exception e)
                {
                    Debug.LogError("Could not create user configuration folder: " + e.Message);
                }
            }
            else if (File.Exists(userFileConfiguration))
            {
                LoadConfigurationFromFile(userFileConfiguration);
            }
        }
       
    }

    private void LoadConfigurationFromFile(string userFileConfiguration)
    {
        try
        {
            //loadConfiguration
            GameConfiguration gameConfiguration =
                JsonUtility.FromJson<GameConfiguration>(File.ReadAllText(userFileConfiguration));
            gameConfigSO.gameConfiguration = gameConfiguration;
        }
        catch (Exception e)
        {
            Debug.LogError("Cannot load configuration from " + userFileConfiguration + ": " + e.Message);
        }
    }
}