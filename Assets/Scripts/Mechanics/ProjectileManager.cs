using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private Transform projectileTransform;
    private bool isDragging = false;
    private Vector3 offset;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //SetProjectileScale();
    }

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
        rb.velocity = Vector3.zero;
        
        slingshotRegion.SetActive(false); 
    }
}
