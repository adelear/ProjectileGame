using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    [SerializeField] LaunchManager launchManager; 
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) 
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            launchManager.SetOnGround(true); 
            StartCoroutine(DestroyObjectAfterDelay(2.0f));  
        }
        else
        {
            launchManager.SetOnGround(false);
            StartCoroutine(DestroyObjectAfterDelay(2.0f));  
        }
    }

    IEnumerator DestroyObjectAfterDelay(float delay) 
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject); 
    } 
}
