using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class OutsideBox : MonoBehaviour
{
  
    [SerializeField] private SOGameConfiguration gameConfig;
    [SerializeField] private AudioSource audioFail;
    
    private void OnTriggerEnter(Collider other)
    {
        //block has moved outside 
        if (other.CompareTag(RehabConstants.BlockMesh))

            {
                audioFail.Play();
                Destroy(other.GetComponentInParent<Block>().gameObject);

            }
    }

}
  
      
    

