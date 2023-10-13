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
    private Transform projectileTransform;

    private void Start()
    {
        projectileTransform = transform;
        SetProjectileScale();
    }

    private void SetProjectileScale()
    {
        float randomScale = Random.Range(1.0f, 5.0f);
        Vector3 newScale = new Vector3(randomScale, randomScale, 1.0f);  
        projectileTransform.localScale = newScale;
    }
     
    private void Update()
    {
       
    }
}
