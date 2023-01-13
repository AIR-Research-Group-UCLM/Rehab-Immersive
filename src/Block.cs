using System;

using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;
using Random = UnityEngine.Random;

public class Block : MonoBehaviour
{
 
    [SerializeField] private SOGameConfiguration gameConfig;

    public bool goodWall;
    
    private bool _autogrip;
   
    public bool isgrab;
    readonly  object _locker = new object();
    protected bool hasEntered;
    [SerializeField]
    public HandGrabInteractor handGrab;
    [SerializeField] public string identifier;
  

    private readonly Color[] colors = {Color.red, Color.yellow, Color.blue, Color.green};
  


    void Start()
    {
       
        identifier = Guid.NewGuid().ToString();
        _autogrip = gameConfig.gameConfiguration.autogrip;
        //level easy: hand can go through the walls of the box
        isgrab = false;
        //With easy level the wall doesn't exist
        goodWall = gameConfig.gameConfiguration.levelEasy;
        HasEntered = false;
        
        //set the block color's (random): red, yelow or green 
        InteractableColorVisual[] colorVisual = GetComponentsInChildren<InteractableColorVisual>();

        if (colorVisual != null && colorVisual.Length > 0)
        {
            InteractableColorVisual.ColorState colorDefault = new InteractableColorVisual.ColorState
            {
                Color = colors[Random.Range(0, colors.Length)]
            };
            colorVisual[0].InjectOptionalNormalColorState(colorDefault);
        }
     
       
    }

    /**
     * Set hand grab interactor (right or left)
     */
    public void SetHandGrabInteractor(HandGrabInteractor hb)
    {
        this.handGrab = hb;

    }

    public void Update()
    {
        //If the autogrip is enabled and the hand collides with a block,
        //then set the block's position close to the hand.
        if (_autogrip  && gameConfig.grabIdentier.Equals(identifier) 
            && !HasEntered)
        {
       
            Vector3 palm = handGrab.HandGrabApi.GetPalmCenter();
            var transform1 = transform;
            transform1.position = palm;
            transform1.rotation = handGrab.HandGrabApi.transform.rotation;
        }
    }
   

    private void OnCollisionEnter(Collision other)
    {
       
        //if autogrip (true) and the block collision with finger's capsules, then grab object
        if (_autogrip && gameConfig.grabIdentier.Equals("") && other.collider.gameObject != 
            null && other.collider.gameObject.name.EndsWith("CapsuleCollider")
            && !HasEntered  && handGrab != null)
        {
            //set the actual block is grabbed
            gameConfig.grabIdentier = identifier;
        



        }
        
        

    }
   


    public bool HasEntered
    {
        get
        {
            lock (_locker)
            {
                return hasEntered;
            }
                
        }
        set
        {
            lock (_locker)
            {
                
                hasEntered = value;
            }
                
        }
    }

   
}