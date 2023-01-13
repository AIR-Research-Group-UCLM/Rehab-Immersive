
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GoalBox : MonoBehaviour
{
    [SerializeField] private SOGameConfiguration gameConfig;
    private List<GameObject> _blockList;
    [FormerlySerializedAs("_audioGoal")] [SerializeField] private AudioSource audioGoal;
    [FormerlySerializedAs("_audioFail")] [SerializeField] private AudioSource audioFail;
  


    private void Awake()
    {
        _blockList = new List<GameObject>();

    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag(RehabConstants.BlockMesh) && !other.GetComponentInParent<Block>().HasEntered)

        {
            //block has moved into goal
            Block block;
            if (!gameConfig.gameConfiguration.autogrip)
            {
                var grabPosses = other.GetComponentInParent<Oculus.Interaction.Grabbable>().GrabPoints;
                //checks if the user release pinch
                //goodWall
                if ((grabPosses == null || grabPosses.Count == 0))
                {
                    block = other.GetComponentInParent<Block>();
                    block.HasEntered = true;
                    if (block.goodWall)
                    {
                        //update the number of blocks moved correctly
                        UpdateGoalBoxCorrectly(block.gameObject);
                    }
                    else
                    {
                        audioFail.Play();
                        BlockUpdater.Instance.AddErrorBlocks();
                        UpdateBlocksGoal(block.gameObject);
                    }
                }
            }
            else
            {
                block = other.GetComponentInParent<Block>();
                block.HasEntered = true;
                //release block
                block.isgrab = false;
                gameConfig.grabIdentier = "";
                //update the number of blocks moved correctly
                UpdateGoalBoxCorrectly(block.gameObject);
            }
        }
    }

    /**
     * Update the number of blocks moved correctly and play a sound
     */
    private void UpdateGoalBoxCorrectly(GameObject block)
    {
        audioGoal.Play();
       
        BlockUpdater.Instance.AddGoalBlocks();
        UpdateBlocksGoal(block);
    }


    private void UpdateBlocksGoal(GameObject block)
    {
        _blockList.Add(block);
        StartCoroutine(RemoveBlocks(block));
    }

    /**
     * If the number of blocks moved correctly is a multiple of three,
     * they are removed from goal box.
     */
    private IEnumerator RemoveBlocks(GameObject block)
    {
        if (BlockUpdater.Instance.TotalBlocks % 3 == 0)
        {
            block.GetComponentInChildren<ParticleSystem>().Play();
            yield return new WaitForSeconds(1.5f);
            foreach (var blockElement in _blockList) Destroy(blockElement);
            _blockList.Clear();
        }
    }
}