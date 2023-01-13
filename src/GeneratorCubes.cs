using System.Xml.Schema;
using Oculus.Interaction.HandGrab;
using UnityEngine;
using UnityEngine.UIElements;

public class GeneratorCubes : MonoBehaviour
{
    // Reference to block prefab
    [SerializeField] public GameObject cube;

    // Total cubes to be generated
    public int totalCubes = 3;


    [SerializeField] public BoxCollider initBoxCollider;
    [SerializeField] public BoxCollider goalBoxCollider;
    [SerializeField] public GameObject box;
    [SerializeField] public SOGameConfiguration gameConfig;
    private float _xMin, _xMax, _zMin, _zMax, _yPosition;
    private int _cubesThrown;
    private HandGrabInteractor _handGrab;

    public void AddCubes()
    {
        _cubesThrown = 0;
        GenerateCubes();
    }
    

    /**
     * Initialize block with the hand grab indicated and the 3D position
     */
    public void Intilialize(HandGrabInteractor handGrab, float xMin, float xMax, float yPosition, float zMin,
        float zMax)
    {
        _handGrab = handGrab;
        _xMin = xMin;
        _xMax = xMax;
        _yPosition = yPosition;
        _zMin = zMin;
        _zMax = zMax;
        GenerateCubes();
    }

    private void GenerateCubes()
    {
        _cubesThrown = 0;
        GenerateCubesCoroutine();
    }

    private void GenerateCubesCoroutine()
    {
            while (_cubesThrown < totalCubes)
            {
                var c = Instantiate(cube);
                c.GetComponent<Block>().SetHandGrabInteractor(_handGrab);

                c.transform.position =
                    new Vector3(Random.Range(_xMin, _xMax), _yPosition,
                        Random.Range(_zMin, _zMax)); 
                _cubesThrown++;
            }
       
    }

    


}