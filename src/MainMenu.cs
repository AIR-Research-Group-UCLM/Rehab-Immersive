using UnityEngine;
using System;
using System.IO;

public class MainMenu : MonoBehaviour
{
    [SerializeField] public GameObject mainMenuGameObject;
    [SerializeField] public GameObject handMenuGameObject;
    [SerializeField] public GameObject calibrationMenuGameObject;
    [SerializeField] public GameObject boxMenuGameObject;
    [SerializeField] public GameObject blockMenuGameObject;
    [SerializeField] public SOGameConfiguration gameConfig;
    [SerializeField] public GameObject difficultyMenuGameObject;

    private const string HandConfig = "HandConfiguration";

    //hand configuration
    private const string HandRight = "RHand";

    private const string HandLeft = "LHand";

    //Level
    private const string Easy = "Easy";

    private const string Hard = "Hard";

    //Configuration manu
    private const string Calibration = "Calibration";
    private const string BoxCalibration = "Box";
    private const string SaveBoxCalibration = "SaveBox";
    private const string SaveBlockCalibration = "SaveBlock";
    private const string BlockCalibration = "Block";
    private const string DifficultyCalibration = "Difficulty";
    private GameObject _desk;

    public void Start()
    {
        /*string[] arguments = Environment.GetCommandLineArgs();
        if (arguments.Length > 0)
        {
            Console.WriteLine("GetCommandLineArgs: {0}", string.Join(", ", arguments));
        }*/
        //gameConfig.resetAll();
        DesactivateAll();
        mainMenuGameObject.SetActive(true);
    }


    private void DesactivateAll()
    {
        handMenuGameObject.SetActive(false);
        calibrationMenuGameObject.SetActive(false);
        boxMenuGameObject.SetActive(false);
        blockMenuGameObject.SetActive(false);
        difficultyMenuGameObject.SetActive(false);
    }


    //Detect push button and load menu
    public void DetectButton(string buttonText)
    {
        switch (buttonText)
        {
            case SaveBoxCalibration:

                calibrationMenuGameObject.SetActive(false);
                _desk = GetDeskGameObject();
                if (_desk != null)
                {
                    gameConfig.gameConfiguration.boxPosition = _desk.transform.position;
                }

                boxMenuGameObject.SetActive(false);
                ActiveMainMenu();
                break;

            case SaveBlockCalibration:
                blockMenuGameObject.SetActive(false);
                ActiveMainMenu();
                break;

            case BoxCalibration:
                calibrationMenuGameObject.SetActive(false);
                mainMenuGameObject.SetActive(false);
                boxMenuGameObject.SetActive(true);
                _desk = GetDeskGameObject();
                _desk.transform.position = gameConfig.gameConfiguration.boxPosition;

                break;
            case BlockCalibration:
                calibrationMenuGameObject.SetActive(false);
                mainMenuGameObject.SetActive(false);
                blockMenuGameObject.SetActive(true);
                
                //blockMenuGameObject.GetComponent<BlockCalibration>().enabled = true;
                blockMenuGameObject.GetComponent<BlockCalibration>().Initialize();
                blockMenuGameObject.GetComponent<BlockCalibration>().stopCalibration = false;

                break;
            case DifficultyCalibration:
                mainMenuGameObject.SetActive(false);
                calibrationMenuGameObject.SetActive(false);
                difficultyMenuGameObject.SetActive(true);
                break;


            case HandConfig:
                mainMenuGameObject.SetActive(false);
                handMenuGameObject.SetActive(true);
                break;
            case HandRight:
                gameConfig.gameConfiguration.handRight = true;
                ActiveMainMenu();

                break;
            case HandLeft:
                gameConfig.gameConfiguration.handRight = false;
                ActiveMainMenu();

                break;
            case Easy:
                gameConfig.gameConfiguration.levelEasy = true;
                ActiveMainMenu();
                break;
            case Hard:
                gameConfig.gameConfiguration.levelEasy = false;
                ActiveMainMenu();
                break;


            case Calibration:
                mainMenuGameObject.SetActive(false);
                calibrationMenuGameObject.SetActive(true);
                break;
            default:
                ActiveMainMenu();
                break;
        }
    }

    /**
     * Save user configuration
     */
    private void SaveConfiguration()
    {
        try
        {
            String userFileConfiguration = Path.Combine(gameConfig.userPath, RehabConstants.FileConfiguration);

            string dataString = JsonUtility.ToJson(gameConfig.gameConfiguration);
            File.WriteAllText(userFileConfiguration, dataString);
        }
        catch (Exception e)
        {
            Debug.Log("Error saving user configuration " + e.Message);
        }
    }

    private void ActiveMainMenu()
    {
        calibrationMenuGameObject.SetActive(false);
        boxMenuGameObject.SetActive(false);
        handMenuGameObject.SetActive(false);
        difficultyMenuGameObject.SetActive(false);
        mainMenuGameObject.SetActive(true);
        SaveConfiguration();
    }

    private GameObject GetDeskGameObject()
    {
        return GameObject.FindGameObjectWithTag("BoxObject");
    }
}