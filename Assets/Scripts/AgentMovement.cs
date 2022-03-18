using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMovement : MonoBehaviour
{
    //ref to momevent
    public NavMeshAgent navMeshAgent;

    //ref to arrest player
    public GameObject player;
    [SerializeField] bool isTimeToArrest = false;
    [SerializeField] float minDistToArrest = 15.0f;

    //ref to random movement
    [SerializeField] bool isTravelStart = false;
    [SerializeField] float rayDistance = 200.0f;
    [SerializeField] Vector3 randomDestination;
    [SerializeField] GameObject[] randomPoints;

    // Start is called before the first frame update
    void Start()
    {
        randomPoints = GameObject.FindGameObjectsWithTag("RandomPoint");
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);

        //controllo sulla distanza per attivare l'inseguimento
        //Debug.Log(Vector3.Distance(transform.position, player.transform.position));

        if (Vector3.Distance(transform.position, player.transform.position) <= minDistToArrest)
        {
            isTimeToArrest = true;
        }
        else
        {
            isTimeToArrest = false;
        }

        if (isTimeToArrest)
        {
            navMeshAgent.destination = player.transform.position;
        }
        else
        {
            //scelgo una destinazione random
            
            if (!isTravelStart)
            {
                //se non sono ancora in viaggio calcolo la nuova destinazione
                isTravelStart = true;
                PickRandomPoint();
            }
            else
            {
                //controllo se non sono arrivato
                if (CheckDIstance(transform.position, randomDestination))
                {
                    isTravelStart = false;
                }
            }

            navMeshAgent.destination = randomDestination;

        }
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

    private bool CheckDIstance(Vector3 point1, Vector3 point2)
    {
        float range = 0.5f;

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

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("ARRESTALO!");
        if (other.CompareTag("Player"))
        {
            isTimeToArrest = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("E' SCAPPATO!");
        if (other.CompareTag("Player"))
        {
            isTimeToArrest = false;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        return navHit.position;
    }

}
