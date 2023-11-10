using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchManager : MonoBehaviour
{
    [SerializeField] Transform projectilePrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] GameObject line;
    [SerializeField] float launchForce = 10f;
    [SerializeField] float maxDistance = 100f; 
    [SerializeField] int trajectoryStepCount = 15;
    [SerializeField] float gravity = 9.81f;
    [SerializeField] AudioManager asm;
    [SerializeField] AudioClip throwSnowball; 


    private List<Vector3> trajectoryPoints;
    private Vector3 launchDirection;
    private bool isAiming = false;
    private bool onGround = false;
    float currentLaunchForce; 

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
            // max - min power 
            // do a way that takes the mouse position in mind that does a PERCENTAGE of the launch force
            launchDirection = (mousePosition - spawnPoint.position); //  Initial velocity will rely on launch direction when comparing to the spawnpoint of the projectile
            //closer to spawnpoint essentially means theres a smaller initial velocity
            //the further you are away from the spawn point, the greater your velocity is 
            float distancePercentage = Vector3.Distance(spawnPoint.position, mousePosition) / maxDistance; 
            currentLaunchForce = launchForce * Mathf.Clamp01(distancePercentage); 


            if (Input.GetMouseButtonUp(0))
            {
                LaunchProjectile();
                isAiming = false;
                ClearTrajectory();
            }
            else
            {
                //When mouse button is held down, the direction of where the player is aiming will be displayed 
                DrawTrajectory();
            }
        }
        else
        {
            trajectoryPoints.Clear();
        }
        Debug.Log(currentLaunchForce);
    }


    void DrawTrajectory()
    {
        trajectoryPoints.Clear();

        Vector3 currentPosition = spawnPoint.position;
        Vector2 currentVelocity = launchDirection * currentLaunchForce;

        //It will only show the first 2 points of the trajectory to make it a bit more difficult
        for (int i = 0; i < 2; i++)
        {
            float t = i * 0.1f; 
            float x = currentPosition.x + currentVelocity.x * t; // x = x0 + v0 * t  = horizontal component 
            float y = currentPosition.y + currentVelocity.y * t - 0.5f * gravity * t * t; // y = y0 + (v0)(t) - (1/2)(a)(t^2)  = vertical component 
   
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
        // Calculating velocity based on the launch direction and force
        Vector2 velocity = launchDirection * currentLaunchForce;
        asm.PlayOneShot(throwSnowball, false); 
        StartCoroutine(MoveProjectile(pr, velocity));
    }

    IEnumerator MoveProjectile(Transform projectile, Vector2 velocity)
    {
        while (projectile != null)
        {
            if (!onGround) // Check if the projectile hasn't hit the ground yet
            {
                velocity.y -= gravity * Time.deltaTime; // Gravity taking an effect to the vertical component until it hits the ground
            }
            else
            {
                velocity.y = 0; // Stopp further vertical movement when on ground
            }


            Vector3 newPosition = projectile.position + new Vector3(velocity.x, velocity.y, 0) * Time.deltaTime; // im taking the current position of the projectile
            projectile.position = newPosition;  // and adding the displacement vector. which is determined by the horizontal and vertical component of the velocity
            // multiplied by the time between frames

            // line 116 updates the position of the projectile to the calculated one

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
