using UnityEngine;
using UnityEngine.Serialization;

public class BadWall : MonoBehaviour
{
  
    [SerializeField] private SOGameConfiguration gameConfig;
    [FormerlySerializedAs("_audioFail")] [SerializeField] private AudioSource audioFail;
    private bool _levelEasy;
 


    void Start()
    {
        _levelEasy = gameConfig.gameConfiguration.levelEasy;
       
    }
  

    private void OnTriggerExit(Collider other)
    {

        //the block cross wall in hard level
        if ( !_levelEasy && other.gameObject.CompareTag(RehabConstants.BlockMesh))
        {
            audioFail.Play();
            other.GetComponentInParent<Block>().goodWall = false;
        }
           
    
    }
}