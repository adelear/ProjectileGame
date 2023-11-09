using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    [SerializeField] LaunchManager launchManager;
    [SerializeField] float knockbackDistance = 1.0f; 

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemies enemy = collision.gameObject.GetComponent<Enemies>();
            enemy.GetComponent<Animator>().SetTrigger("EnemyHit");  
            enemy.HitBySnowball();
            KnockbackEnemy(collision.transform);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
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

    private void KnockbackEnemy(Transform enemyTransform)
    {
        Vector2 knockbackDirection = (enemyTransform.position - transform.position).normalized;
        Vector2 knockbackPosition = (Vector2)enemyTransform.position + knockbackDirection * knockbackDistance;
        enemyTransform.position = knockbackPosition;
    }
}
