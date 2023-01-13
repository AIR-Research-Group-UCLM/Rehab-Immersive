using UnityEngine;

public class GoodWall : MonoBehaviour
{
    [SerializeField] private SOGameConfiguration gameConfig;

    private bool _levelEasy;
    private bool _autoGrip;
    

    void Start()
    {
        _levelEasy = gameConfig.gameConfiguration.levelEasy;
        _autoGrip = gameConfig.gameConfiguration.autogrip;
    }

    private void OnTriggerExit(Collider other)
    {
        //the block cross wall in hard level
        if (!_levelEasy && 
        other.gameObject.CompareTag(RehabConstants.BlockMesh))
        {
            
            var grabPosses = other.GetComponentInParent<Oculus.Interaction.Grabbable>().GrabPoints;
            if (!_autoGrip)
            {
                //cross good wall with the block grabbed
                other.GetComponentInParent<Block>().goodWall = !(grabPosses == null || grabPosses.Count == 0);
            }
            else
            {
                //cross good wall always with block grabbed (autogrip)
                other.GetComponentInParent<Block>().goodWall = true;
            }
        }
           
    
    }
}