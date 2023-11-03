using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchManager : MonoBehaviour
{
    [SerializeField] Transform projectilePrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] GameObject line;
    [SerializeField] float launchForce = 1.5f;
    [SerializeField] int trajectoryStepCount = 15;
    [SerializeField] float gravity = 9.81f;

    private List<Vector3> trajectoryPoints;
    private Vector3 launchDirection;
    private bool isAiming = false;
    private bool onGround = false;   

    void Start()
    {
        lineRenderer.positionCount = trajectoryStepCount;
        trajectoryPoints = new List<Vector3>();
        line.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isAiming = true;
            line.SetActive(true);
            trajectoryPoints.Clear();
        }

        if (isAiming)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            launchDirection = (mousePosition - spawnPoint.position).normalized; //  Initial velocity 

            if (Input.GetMouseButtonUp(0))
            {
                LaunchProjectile();
                isAiming = false;
                ClearTrajectory();
            }
            else
            {
                DrawTrajectory();
            }
        }
        else
        {
            trajectoryPoints.Clear();
        }
    }


    void DrawTrajectory()
    {
        trajectoryPoints.Clear();

        Vector3 currentPosition = spawnPoint.position;

        for (int i = 0; i < 5; i++)
        {
            float t = i / (float)(trajectoryStepCount - 1);
            float x = currentPosition.x + launchDirection.x * launchForce * t; 
            float y = currentPosition.y + launchDirection.y * launchForce * t - 0.5f * gravity * t * t;
            trajectoryPoints.Add(new Vector3(x, y, 0));
        }

        lineRenderer.positionCount = trajectoryPoints.Count;
        lineRenderer.SetPositions(trajectoryPoints.ToArray());
    }


    void ClearTrajectory()
    {
        lineRenderer.positionCount = 0;
    }

    void LaunchProjectile()
    {
        Transform pr = Instantiate(projectilePrefab, spawnPoint.position,spawnPoint.rotation);
        // Calculate the velocity based on the launch direction and force
        Vector2 velocity = launchDirection * launchForce;

        // Move the projectile
        StartCoroutine(MoveProjectile(pr, velocity));
    }

    IEnumerator MoveProjectile(Transform projectile, Vector2 velocity)
    {
        while (projectile != null)
        {
            if (!onGround) // Check if the projectile hasn't hit the ground yet
            {
                velocity.y -= gravity * Time.deltaTime;
            }
            else
            {
                velocity.y = 0; // Stop further vertical movement when on the ground
            }

            Vector3 newPosition = projectile.position + new Vector3(velocity.x, velocity.y, 0) * Time.deltaTime;
            projectile.position = newPosition;

            yield return null;
        }
    }



    public void SetOnGround(bool grounded)
    {
        onGround = grounded; 
    }

    public bool GetOnGround()
    {
        return onGround; 
    }
}
