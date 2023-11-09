using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System; 

public class ProjectileManager : MonoBehaviour
{
    //The player must pull back slingshot to launch snowballs 
    //The player's pullin ball the slingshot controls the initial velocity 


    // max distance, pull distance 
    // pull distance/max distance determines percecntage release velocity 
    //region box colliders are removed once the projectile is released 

    //pull distance = transform.location.x and transform.location.y 

    // Release velocity will be used to calculate x and y component 
    //release velocity = 100 units (just an example) 
    [SerializeField] GameObject slingshotRegion;
    [SerializeField] Transform EndRegion;

    private Transform projectileTransform;
    private Rigidbody2D rb;
    private Vector3 offset; 

    private float DistanceToX;
    private float DistanceToY;
    private float DistanceToRegion; 

    private float pullDistanceX;
    private float pullDistanceY;
    private float pullDistance; //Hypoteneuse 

    private float initialVelocity;
    private float initialVelocityX;
    private float initialVelocityY;
    private float maxInitialVelocity = 0.5f;
    private float gravity = 9.81f;
    private float angle; 

    private bool isDragging = false;
    private bool isReleased = true;



    /*
    FinalVelocity^2 - InitialVelocity^2 = 2 * Acceleration * Displacement 
    ANGLE = tan^-1(initialVelocity Y / initialVelocity X) 

    VERTICAL COMPONENT:
         y = height of tower + (InitialVelocity^2 * sin(ANGLE) ^2)/2*g

    HORIZONTAL COMPONENT: 
        x = (initialVelocity^2 * sin(2 ANGLE)) / g

    GIVEN: 
        Displacement = Distance between enemy penguin and player
     
     */
    private void CalculateAngle()
    {
        angle = Mathf.Atan(initialVelocityY / initialVelocityX); 

    }

    private void CalculateMotion()
    {
        if (!isDragging && isReleased)
        {
            // Calculate the angle based on the initial velocity components
            CalculateAngle();
            initialVelocityX = DistanceToX * maxInitialVelocity;
            initialVelocityY = DistanceToY * maxInitialVelocity;

            // Setting initial position
            Vector3 position = transform.position;

            // vertical component 
            float verticalComponent = (initialVelocityY * initialVelocityY * Mathf.Sin(2 * angle)) / gravity;

            // time of flight  i dont think i need
            float timeOfFlight = (2 * verticalComponent) / gravity;

            

            // Motion 
            float timeElapsed = 0.0f;
            while (timeElapsed <= timeOfFlight)
            {
                float x = initialVelocityX * timeElapsed;
                float y = initialVelocityY * timeElapsed - 0.5f * gravity * timeElapsed * timeElapsed;

                // Update the position
                position = new Vector3(x, y, 0);

                // Increment time elapsed
                timeElapsed += Time.fixedDeltaTime;

                // Set the new position
                transform.position = position;
            }
        }
    }


    private void Start()
    {
        isReleased = false; 

        rb = GetComponent<Rigidbody2D>();
        //SetProjectileScale();
    }

    private void CalculateDistanceToRegion()
    {
        //This is the percentage of the power that will be used on the cannonball 
        DistanceToX = EndRegion.position.x - transform.position.x;   
        DistanceToY = EndRegion.position.y - transform.position.y;

        if (DistanceToX > 1.0f) DistanceToX = 1.0f;
        if (DistanceToY < 0.0f) DistanceToY = 0.0f;

        //MAY NOT NEED, but its the hypoteneuse 
        /*
        DistanceToRegion = Mathf.Sqrt(Mathf.Pow(DistanceToX, 2) + Mathf.Pow(DistanceToY, 2));
        if (DistanceToRegion > 1.0f) DistanceToRegion = 1.0f; 
        if (DistanceToRegion < 0.0f) DistanceToRegion = 0.0f; 
        */ 
    }

    /*
    private void SetPullDistance()
    {
        if (!isDragging)
        {
            pullDistanceX = transform.position.x / EndRegion.position.x;  
            pullDistanceY = transform.position.y / EndRegion.position.y; 
        }

        pullDistance = Mathf.Sqrt(Mathf.Pow(pullDistanceX, 2) + Mathf.Pow(pullDistanceY, 2));   
    } 
    */

    private void SetProjectileScale()
    {
        float randomScale = Random.Range(0.5f, 1.0f);
        Vector3 newScale = new Vector3(randomScale, randomScale, 1.0f);
        transform.localScale = newScale;
    }

    private void OnMouseDown()
    {
        isDragging = true;
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            rb.velocity = (mousePos + offset - transform.position) * 10;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
        isReleased = true; 
        //rb.velocity = Vector2.zero;  
        slingshotRegion.SetActive(false); 
    }

    private void Update()
    {
        slingshotRegion.SetActive(isDragging);

        if (isDragging)
        {
            CalculateDistanceToRegion();
            Debug.Log("X: " + DistanceToX);
            Debug.Log("Y: " + DistanceToY);
        }

        if (Input.GetMouseButtonDown(0)) 
        {
            // Player starts dragging the projectile.
            isDragging = true;
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0)) 
        {
            // Player releases the projectile.
            isDragging = false;
            isReleased = true;
            initialVelocityX = DistanceToX * maxInitialVelocity;
            initialVelocityY = DistanceToY * maxInitialVelocity;
            rb.velocity = new Vector2(initialVelocityX, initialVelocityY);
            slingshotRegion.SetActive(false);
        }
 

        if (isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            rb.velocity = (mousePos + offset - transform.position) * 10;
        }
    }

}
