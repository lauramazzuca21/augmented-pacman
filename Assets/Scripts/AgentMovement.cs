using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMovement : MonoBehaviour
{
    //ref to momevent
    public NavMeshAgent navMeshAgent;

    //ref to arrest player
    public Transform playerTransform;
    public PlayerController playerController;
    [SerializeField] bool isTimeToArrest = false;
    [SerializeField] float minDistToArrest = 15.0f;

    //ref to random movement
    [SerializeField] bool isTravelStart = false;
    [SerializeField] Vector3 randomDestination;
    [SerializeField] GameObject[] randomPoints;

    //ref to CheckDistance function
    [SerializeField] float checkDistance = 0.5f;
    [SerializeField] float distanceFromShark = 25f;
    [SerializeField] float recalculateDistance = 5f; //dovrebbe cercare un altro percorso, non ottimale ma va bhe

    // Start is called before the first frame update
    void Start()
    {
        randomPoints = GameObject.FindGameObjectsWithTag("RandomPoint");
    }

    // Update is called once per frame
    void Update()
    {

        //transform.position = new Vector3(transform.position.x, 10.1f, transform.position.z);

        //controllo sulla distanza per attivare l'inseguimento
        //Debug.Log(Vector3.Distance(transform.position, playerTransform.position));

        if (Vector3.Distance(transform.position, playerTransform.position) <= minDistToArrest)
        {
            isTimeToArrest = true;
        }
        else
        {
            isTimeToArrest = false;
        }

        if (isTimeToArrest && !playerController.IsPowerupActive)
        {
            //devo arrestare il player
            navMeshAgent.destination = playerTransform.position;
        }
        else if (!isTimeToArrest && !playerController.IsPowerupActive)
        {
            //scelgo una destinazione random
            Move();

        }
        else if (playerController.IsPowerupActive)
        {
            //RUN FORREST!

            EscapeFromShark();

            if(CheckDIstance(transform.position, playerTransform.position, recalculateDistance))
            {
                CheckPickPosition();
            }
        }
    }

    private void Move()
    {
        if (!isTravelStart)
        {
            //se non sono ancora in viaggio calcolo la nuova destinazione
            isTravelStart = true;
            PickRandomPoint();
        }
        else
        {
            //controllo se non sono arrivato
            if (CheckDIstance(transform.position, randomDestination, checkDistance))
            {
                isTravelStart = false;
            }
        }

        navMeshAgent.destination = randomDestination;
    }

    private void PickRandomPoint()
    {
        //Vector3 randDest = RandomNavSphere(transform.position, rayDistance, -1);

        //randDest.x = Mathf.Round(randDest.x);
        //randDest.y = 0f;
        //randDest.z = Mathf.Round(randDest.z);

        int dim = randomPoints.Length - 1;
        int rand = Random.Range(0, dim);
     
        randomDestination = randomPoints[rand].transform.position;
        //Debug.Log("Sono a: " + transform.position + " Sto andando a: " + randomPoints[rand].transform.position + " indx: " + rand);
    }

    private bool CheckDIstance(Vector3 point1, Vector3 point2, float maxRange)
    {
        float range = maxRange;

        float dist = Vector3.Distance(point1, point2);
        //Debug.Log("la distanza è: " + dist);

        if (dist <= range)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void EscapeFromShark()
    {
        //if shark mode is active, RUN!
        Move();

        CheckPickPosition();
    }

    private void CheckPickPosition()
    {
        while (CheckDIstance(randomDestination, playerTransform.position, distanceFromShark))
        {
            //pick a position far away
            PickRandomPoint();
        }
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("ARRESTALO!");
    //    if (other.CompareTag("Player"))
    //    {
    //        isTimeToArrest = true;
    //    }
    //}

    //void OnTriggerExit(Collider other)
    //{
    //    Debug.Log("E' SCAPPATO!");
    //    if (other.CompareTag("Player"))
    //    {
    //        isTimeToArrest = false;
    //    }
    //}

    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        return navHit.position;
    }

}
