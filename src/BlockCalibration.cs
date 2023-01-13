using System.Collections;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class BlockCalibration : MonoBehaviour
{
    [SerializeField] private SOGameConfiguration gameConfiguration;
    [SerializeField] HandGrabInteractor handGrabR;
    [SerializeField] HandGrabInteractor handGrabL;
    [SerializeField] private GameObject ButtonSave;
    [SerializeField] private Canvas _canvas;
    [SerializeField] public GameObject cube;
    
    public bool stopCalibration;

    private HandGrabInteractor handGrab;
    private GameObject canvas;
    
    // current Time
    private float currentTime;
    private bool autoGrip;
    private TMP_Text textMP;
    private UnityEvent eventMainMenu;




 


  public void Initialize()
  {
     
      ButtonSave.SetActive(false);
      gameConfiguration.gameConfiguration.autogrip = false;
      _canvas.gameObject.SetActive(true);
      textMP = (TMP_Text) _canvas.GetComponentInChildren(typeof(TMP_Text));
      textMP.text = "Calibration begins. Try to grab the block as tightly as possible.";
      handGrab = gameConfiguration.gameConfiguration.handRight ? handGrabR : handGrabL;
      autoGrip = false;
      GameObject desk = GameObject.Find("Desk");
      desk.transform.position = new Vector3(0f, gameConfiguration.gameConfiguration.boxPosition.y + 0.05f, 0.1f);
    //  GameObject goBlock = GameObject.Find("Block");
     // goBlock.transform.position = new Vector3(0.0f, 0.8f, gameConfiguration.gameConfiguration.boxPosition.z);
     var c = Instantiate(cube);
     c.GetComponent<Block>().SetHandGrabInteractor(handGrab);

     c.transform.position =
         new Vector3(.0f, 0.8f, gameConfiguration.gameConfiguration.boxPosition.z); 
      currentTime = Time.time;
      stopCalibration = false;
      autoGrip = true;
  }




  private IEnumerator WaitUntillStarts()
    {
        yield return new WaitForSeconds(3);
        stopCalibration = false;
    }

    public void Update()
    {
        if (!stopCalibration)
        {

            if (!autoGrip)
            {
                textMP.text = "Grip detected correctly.";
                SaveResult();
            }else if (Time.time - currentTime < 15f )
            {
                
                
                if (InteractorState.Select == handGrab.State && autoGrip)
                {
                    autoGrip = false;
                }
               
            }
            else
            {
                textMP.text = "Invalid grip detected, auto-grip will be used";
                SaveResult();


            }
        }
    }

    private void SaveResult()
    {
        gameConfiguration.gameConfiguration.autogrip = autoGrip;
        stopCalibration = true;
    
        StartCoroutine(WaitAndShowButton());
       
        
    }

    IEnumerator WaitAndShowButton()
    {
       
        yield return new WaitForSeconds(3);
        _canvas.gameObject.SetActive(false);
        ButtonSave.SetActive(true);

    }

    
}