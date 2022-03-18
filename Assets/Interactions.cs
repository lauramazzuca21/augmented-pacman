using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    //ref to GM
    public GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("XXXXXX");
        //if (collision.transform.gameObject.CompareTag("Player"))
        //{
        //    Debug.Log("arrestato");
        //}
    }


}
