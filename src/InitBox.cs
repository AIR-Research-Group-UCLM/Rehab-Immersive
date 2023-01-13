
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class InitBox : MonoBehaviour
{
    [SerializeField] private SOGameConfiguration gameConfig;
    private int _blocksGenerated;

    private bool _startMoreCubes;
   
    private int _totalCubes ;

    private void Awake()
    {
        _blocksGenerated = 0;
        _totalCubes = 1;
        _startMoreCubes = false;
    }
    
    
   
   
   
       private void OnTriggerEnter(Collider other)
       {
           if (other.gameObject.CompareTag(RehabConstants.BlockMesh))
           {
               _totalCubes++;
               Debug.LogWarning("Total cubes On enter " + _totalCubes);
           }
       }
   
       private void OnTriggerExit(Collider other)
       {
           
              
           if (other.gameObject.CompareTag(RehabConstants.BlockMesh))
           {
             
               _totalCubes--; 
               Debug.LogWarning("Total cubes On exit " + _totalCubes);
               if (_totalCubes == 1 && !_startMoreCubes)
               {
                   _startMoreCubes = true;
                   StartCoroutine(WaitAndGenerate());
                  
               }
           }
       }

 
    private IEnumerator WaitAndGenerate()
    {
        yield return new WaitForSeconds(1.0f);
        GameObject.Find("GeneratorCube").GetComponent<GeneratorCubes>().AddCubes();
        BlockUpdater.Instance.AddGeneratedBlocks(3);
        _startMoreCubes = false;
    }
}