using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] private Transform cloudSpawner;
    [SerializeField] private Transform cloudDestroy;
    private float cloudSpeed = 2.0f; 

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = transform.position + new Vector3(1, 0, 0) * cloudSpeed * Time.deltaTime;
        transform.position = newPosition;   

        if (transform.position.x >= cloudDestroy.position.x)
        {
            transform.position = cloudSpawner.transform.position; 
        }
    }
} 
