using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Oculus.Interaction.GrabAPI;
using Oculus.Interaction.HandGrab;
using Oculus.Interaction.Input;
using Oculus.Interaction.Throw;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Slider = UnityEngine.UI.Slider;


public class StartGame : MonoBehaviour
{
    [SerializeField] private TMP_Text countGoal;
    [SerializeField] private TMP_Text finishText;
    [SerializeField] GameObject block;

    [FormerlySerializedAs("gameConfigSO")] [SerializeField]
    private SOGameConfiguration gameConfigSo;

    [FormerlySerializedAs("_ovrSkeleton")] [SerializeField]
    private OVRCustomSkeleton ovrSkeleton;

    [SerializeField] GameObject desk;
    [SerializeField] public GameObject woodBox;
    [SerializeField] public Slider slider;
    [SerializeField] public AudioSource gameSound;
    [SerializeField] public AudioSource gameEnd;
    [SerializeField] private OVRCameraRig ovrCameraRig;
    [SerializeField] private HandGrabInteractor handGrabInteractorL;
    [SerializeField] private HandGrabInteractor handGrabInteractorR;


    // current Time
    private float _currentTime;
    private readonly List<GameObject> _blockList = new List<GameObject>();
    private bool _hasEntered;
    private bool _autogrip;
    private bool _end;
    private float _extraTimer = 5f;

    

    //Historical object
    private Historical _historical;
    private String scriptGenerator = "GeneratorCube";


    private HandGrabInteractor _handGrab;

    //grabbing rules
    private GrabbingRule _pinchGrabRules;
    private GrabbingRule _palmGrabRules;
    
    private bool _handInitialized;
    private bool _handDetected;
    //box and colliders position
    private float _xMin;
    private float _xMax;
    private float _yPosition;
    private float _zMin;
    private float _zMax;
    private readonly float offsetPosition = 0.08f;
    private readonly float offsetPositionX = 0.02f;

    private readonly float _maxTimer = 60f;

    //partial timer (game scene started)
    private float _partialTimer;
    private int _partialFrameCount;
    private int _frameCountBefore;
    private bool _saveData;
    //last frame
    private int _lastFrame;

    private OVRInput.Controller _handController;

    //last hand position 
    private Vector3 _lastposition;

    //new hand position
    private Vector3 _newposition;

    //bone name prefix
    private String _bonePrefix;

    //bone palm 
    private String _bonePalm;

    //bone wrist
    private String _boneWrist;

    private void Awake()
    {
      
        _handDetected = false;
        _currentTime = 0;
        _handInitialized = false;
 
        UpdateTimeAndGoal();
        finishText.text = "";
        _handController = gameConfigSo.gameConfiguration.handRight
            ? OVRInput.Controller.RHand
            : OVRInput.Controller.LHand;
        _end = false;
        _lastposition = Vector3.zero;
        _partialTimer = Time.realtimeSinceStartup;
        _partialFrameCount = 0;
        _frameCountBefore = 0;
        _lastFrame = Time.frameCount;
        _autogrip = gameConfigSo.gameConfiguration.autogrip;
        //clean string list and set header for first time wuite
        TrackingDataWriter.Instance.ResetAll();
    }

    private void Start()

    {
        BlockUpdater.Instance.ResetAll();
        if (gameConfigSo.userPath == "")
        {
            DirectoryInfo dirParent = Directory.GetParent(Application.persistentDataPath);
            if (dirParent != null)
            {
                string parentPath = dirParent.ToString();
                gameConfigSo.userPath =
                    Path.Combine(parentPath, RehabConstants.DirBbt, RehabConstants.DefaultUser);
            }
        }

        _saveData = true;
        slider.maxValue = _maxTimer;
        TrackingDataWriter.Instance.SetPathWritter(gameConfigSo.userPath);

        _bonePrefix = gameConfigSo.gameConfiguration.handRight ? "Right" : "Left";
        _bonePalm = _bonePrefix + "HandPalm";
        _boneWrist = _bonePrefix + "HandWrist";

        if (!gameConfigSo.gameConfiguration.handRight)
        {
            handGrabInteractorL.enabled = true;
            woodBox.transform.eulerAngles = new Vector3(woodBox.transform.eulerAngles.x, 180, 0);
             _handGrab = handGrabInteractorL;
               //disable right hand grab
               handGrabInteractorR.enabled = false;
        }
        else
        {
             handGrabInteractorR.enabled = true;
              _handGrab = handGrabInteractorR;
              //disable left hand grab
              handGrabInteractorL.enabled = false;
        }


        //set desk position
        desk.transform.position = new Vector3(gameConfigSo.gameConfiguration.boxPosition.x,
            gameConfigSo.gameConfiguration.boxPosition.y - 0.018f, gameConfigSo.gameConfiguration.boxPosition.z);

        woodBox.transform.position = gameConfigSo.gameConfiguration.boxPosition;
        SetBoxAndColliders();
        var hgi = block.GetComponentInChildren<HandGrabInteractable>();
        //Pinch and palm grab rules
        _pinchGrabRules = hgi.PinchGrabRules;
        _palmGrabRules = hgi.PalmGrabRules;

        _historical = new Historical
        {
            time =  gameConfigSo.gameConfiguration.timer,
            userId = gameConfigSo.gameConfiguration.userId,
            autogrip = gameConfigSo.gameConfiguration.autogrip,
            handR = gameConfigSo.gameConfiguration.handRight,
            level = gameConfigSo.gameConfiguration.levelEasy ? 1 : 2
        };
    }




    private void SetTimer()
    {
        if (_handInitialized)
        {
            _currentTime += Time.deltaTime;
            slider.value = _currentTime;
            UpdateTimeAndGoal();
            if (_currentTime >= _maxTimer)
            {
                _end = true;
            }
        }
    }


    void SetBoxAndColliders()
    {
        BoxCollider initBoxCollider = GameObject.FindWithTag("InitBox").GetComponent<BoxCollider>();
        BoxCollider goalBoxCollider = GameObject.FindWithTag("GoalBox").GetComponent<BoxCollider>();
        if (gameConfigSo.gameConfiguration.handRight)
        {
            var max = initBoxCollider.bounds.max;
            var position = woodBox.transform.position;
            _xMin = position.x + offsetPositionX;
            _xMax = position.x + max.x - offsetPositionX;
        }
        else
        {
            var position = woodBox.transform.position;
            _xMax = position.x - offsetPositionX;
            _xMin = position.x - Mathf.Abs(goalBoxCollider.bounds.min.x) + offsetPositionX;
       
        }


        var bounds = initBoxCollider.bounds;
        float wBox = (bounds.max.z - bounds.min.z) / 2;
        var position1 = woodBox.transform.position;
        _zMin = position1.z - wBox + offsetPosition;
        _zMax = position1.z + wBox - offsetPosition;
        _yPosition = position1.y + 0.4f;
    }

    private void FixedUpdate()
    {
        //save tracking data each 5 frames
        bool saveData = Time.frameCount > _lastFrame && (Time.frameCount % 5 == 0);
        if (!_end  )
        {
            _handDetected = _handGrab != null && _handGrab.Hand.IsConnected;
            SetTimer();
            _newposition = OVRInput.GetLocalControllerPosition(_handController);
        
            Vector3 speed = VelocityCalculatorUtilMethods.ToLinearVelocity(_lastposition, _newposition,
                Time.deltaTime);
            //update previous positions
            _lastposition = _newposition;
            if (saveData)
            {
                HandTracking handTracking;
                // HandGrabInteractor is detected and the hand is connected 
                if (_handGrab != null && (_handInitialized || _handGrab.Hand.IsConnected))
                {
                    if (!_handInitialized)
                    {
                        //first time hands are detected
                        GameObject.Find(scriptGenerator).GetComponent<GeneratorCubes>().Intilialize(_handGrab,
                            _xMin, _xMax, _yPosition, _zMin, _zMax);
                        _handInitialized = true;
                    }

                    handTracking = GetTrackingData();
                }
                else
                {
                    bool connected;
                    bool confidence;
                    if (_handGrab == null)
                    {
                        connected = false;
                        confidence = false;
                    }
                    else
                    {
                        connected = _handGrab.Hand.IsConnected;
                        confidence = _handGrab.Hand.IsHighConfidence;
                    }

                    SetPartialTimeAndFrame();

                    handTracking = new HandTracking(Time.frameCount,
                        _currentTime, gameConfigSo.gameConfiguration.handRight, 
                        connected,
                        confidence, ovrCameraRig.centerEyeAnchor.position,
                        Vector3.zero);
                    handTracking.isAutogripGrabbing = _autogrip;
                }



                handTracking.handVelocity = speed;
                SetBodyPosition(handTracking);
                TrackingDataWriter.Instance.SaveTrackingData(handTracking);
            }
        }
        else
        {
            EndBbt();
        }
    }
    

    private void EndBbt()
    {
        if (_saveData)
        {
            TrackingDataWriter.Instance.WriteTrackingData();
            _historical.score = BlockUpdater.Instance.GoalBlocks;
            _historical.numErrors = BlockUpdater.Instance.ErrorBlocks;
            _historical.blocksGenerated = BlockUpdater.Instance.GeneratedBlock;
            _historical.TrackingFile = TrackingDataWriter.Instance.filePath;
            HistoricalWritter historicalW = new HistoricalWritter(gameConfigSo.userPath);
            historicalW.WriteHistorical(_historical);

            _saveData = false;
        }

        if (_extraTimer <= 0f)
        {
            SceneManager.LoadScene("StartMenu");
        }
        else if (Math.Abs(_extraTimer - 5f) < 0.1f)
        {
            if (gameSound.isPlaying)
            {
                gameSound.Stop();
                gameEnd.Play();

                //starts extra time to show results
                slider.gameObject.SetActive(false);
                countGoal.text = "";
                finishText.SetText("Congratulations, \n you have moved " + BlockUpdater.Instance.GoalBlocks + " blocks");
            }
        }

        _extraTimer -= Time.deltaTime;
    }


    private void SetBodyPosition(HandTracking handTracking)
    {
        Vector3 palm = Vector3.zero;
        Vector3 wrist = Vector3.zero;


        foreach (var bone in ovrSkeleton.Bones)
        {
            Debug.LogWarning(bone.Id.ToString());
            string boneName = bone.Id.ToString();
           
            if (boneName.Contains(_bonePalm))
            {
                palm = bone.Transform.position;
                handTracking.handPosition = palm.ToString("f4");
            }
            else if (boneName.Contains(_boneWrist))
            {
                wrist = bone.Transform.position;
            }
        }

      
        Vector3.Magnitude(wrist);
        handTracking.wristTwist = Vector3.Angle(wrist, palm) * 180 / (float) Math.PI;
    }


    private void SetPartialTimeAndFrame()
    {
        _partialTimer = ( Time.realtimeSinceStartup >= _partialTimer) ? Time.realtimeSinceStartup - _partialTimer : Time.realtimeSinceStartup;
        if (_partialFrameCount == 0)
        {
            _frameCountBefore = Time.frameCount;
        }
        else
        {
            int difference = Time.frameCount - _partialFrameCount;
            _partialFrameCount = difference > 0 ? _partialFrameCount + difference : _frameCountBefore;
            _frameCountBefore = Time.frameCount;
        }
       
    }
    private HandTracking GetTrackingData()
    {
       SetPartialTimeAndFrame();
        HandTracking handData = new HandTracking(Time.frameCount,
            _currentTime, gameConfigSo.gameConfiguration.handRight,
            _handDetected, _handGrab.Hand.IsHighConfidence,
            ovrCameraRig.centerEyeAnchor.position,
            OVRInput.GetLocalControllerPosition(_handController))
        {
            isAutogripGrabbing = _autogrip
        };

        //get grabbing data
        if (_handGrab.IsGrabbing || (gameConfigSo.gameConfiguration.autogrip && !gameConfigSo.grabIdentier.Equals("")))
        {
            for (int i = 0; i < Constants.NUM_FINGERS; i++)
            {
                HandFinger finger = (HandFinger) i;

                if (gameConfigSo.gameConfiguration.autogrip)
                {
                    
                    SaveFingerStrength(handData, finger);
                    SavePalmStrength(handData, finger);
                }
                else if (_handGrab.HandGrabApi.IsHandPinchGrabbing(_pinchGrabRules)) //pinch
                {
                    SaveFingerStrength(handData, finger);
                }
                //palm grip
                else if (_handGrab.HandGrabApi.IsHandPalmGrabbing(_palmGrabRules))
                {
                    SavePalmStrength(handData, finger);
                }
            }
        }

        return handData;
    }


    private void SavePalmStrength(HandTracking handData, HandFinger finger)
    {
        handData.isPalmGrabbing = true;
        var actualStrength = _handGrab.HandGrabApi.GetFingerPalmStrength(finger);
            handData.SetStrength(finger, actualStrength, true);
    }

    private void SaveFingerStrength(HandTracking handData, HandFinger finger)
    {
        handData.isPalmGrabbing = true;
        var actualStrength = _handGrab.HandGrabApi.GetFingerPinchStrength(finger);
         handData.SetStrength(finger, actualStrength, false);
    }


    private void UpdateTimeAndGoal()
    {
        countGoal.text = "\n     Time left  " + (_maxTimer - _currentTime).ToString("f0") +
                         "                                              Blocks moved correctly: "
                         + BlockUpdater.Instance.GoalBlocks.ToString() ;

   
    }
}