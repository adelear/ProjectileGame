using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) 
        {
            Destroy(collision.gameObject);
        }

        else
        {
            StartCoroutine(DestroyObjectAfterDelay(2.0f));  
        }
    }

    IEnumerator DestroyObjectAfterDelay(float delay) 
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject); 
    } 
}
