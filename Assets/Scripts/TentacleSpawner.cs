using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TentacleSpawner : MonoBehaviour
{
    public Vector3 tentacleScale;
    public int numberOfTSubdivisions;
    public int tentacleCount;
    public Vector2 startOffset;
    public Vector2 targetOffset;
    public GameObject tentaclePrefab;
    private GameObject[] tentacles;
    private EnamyTentacleControle[] tentacleControler;
    [HideInInspector]
    public float speed;
    [HideInInspector]
    public float tSpeed;
    [HideInInspector]
    public bool foundEnamy;
  //  [HideInInspector]
   // public FOV fov;
    [HideInInspector]
    public float lookForTimer;
    [HideInInspector]
    public float chaseForTimer;
    [HideInInspector]
    public float distanceToPlayer;

   // private EnamyControler.BehState state;
    [HideInInspector]
    public Vector3 target;
    private Vector3 startPosition;

    [SerializeField]
    private Light spot;
    [SerializeField]
    private Light viewLight;
    [SerializeField]
    Color colorIdle;
    [SerializeField]
    Color colorChase;
    [SerializeField]
    Color colorLookFor;
    [HideInInspector]
    public Transform[] idlePath;
    [HideInInspector]
    public float lookForMax;

    [HideInInspector]
    public float killDistance;
    // Start is called before the first frame update
    void Awake()
    {
        lookForTimer = 0;
        chaseForTimer = 0;
        distanceToPlayer = 9999;

        float stepangle = 0;
        tentacles = new GameObject[tentacleCount];
        tentacleControler = new EnamyTentacleControle[tentacleCount];
        foundEnamy = false;

        for (int i = 0; i < tentacleCount; i++)
        {
            
            tentacles[i] = GameObject.Instantiate(tentaclePrefab, transform);
            Vector2 c = new Vector2(Mathf.Cos(stepangle),Mathf.Sin(stepangle));
            tentacleControler[i] = tentacles[i].GetComponent<EnamyTentacleControle>();
            tentacleControler[i].v1 =new Vector3(transform.position.x+c.x*startOffset.x, transform.position.y + startOffset.y,transform.position.z+c.y*startOffset.x);
            tentacleControler[i].v2 =new Vector3(transform.position.x + c.x * targetOffset.x, transform.position.y + targetOffset.y, transform.position.z + c.y * targetOffset.x); ;
            stepangle+= Mathf.Deg2Rad*360 / tentacleCount;
            tentacleControler[i].a = tentacleScale.x;
            tentacleControler[i].c = tentacleScale.y;
            tentacleControler[i].targetMoveThresholdeModifier = tentacleScale.z;
            tentacleControler[i].numPoints = numberOfTSubdivisions;
            tentacleControler[i].direction = new Vector3(c.x, 1, c.y).normalized;

        }

    }
}
