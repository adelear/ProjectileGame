using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchManager : MonoBehaviour
{

    [SerializeField] Transform projectile;
    [SerializeField] Transform spawnPoint;
    [SerializeField] LineRenderer lineRenderer;

    [SerializeField] float launchForce = 1.5f;
    [SerializeField] float trajectoryTimeStep = 0.05f;
    [SerializeField] int trajectoryStepCount = 15;

    [SerializeField]  Vector2 velocity;
    [SerializeField]  Vector2 startMousPos;
    [SerializeField]  Vector2 currentMousePos;  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startMousPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        }

        if (Input.GetMouseButton(0)) 
        {
            currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            velocity = (startMousPos - currentMousePos) * launchForce;

            DrawTrajectory(); 
        }

        if (Input.GetMouseButtonUp(0))
        {
            FireProjectile(); 
        }
    }

    void DrawTrajectory()
    {
        Vector3[] positions = new Vector3[trajectoryStepCount];  
        for (int i = 0; i < trajectoryStepCount; i++)
        {
            float t = i * trajectoryTimeStep;
            Vector3 pos = (Vector2)spawnPoint.position + velocity * t + 0.5f * Physics2D.gravity * t * t;

            positions[i] = pos; 

        }
        lineRenderer.positionCount = trajectoryStepCount; 
        lineRenderer.SetPositions(positions); 
    }

    void FireProjectile()
    {
        Transform pr = Instantiate(projectile, spawnPoint.position, Quaternion.identity); 
        pr.GetComponent<Rigidbody2D>().velocity = velocity;
    }
}
