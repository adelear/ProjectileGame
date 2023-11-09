using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField] EnemyAudio enemyAudio;
    Animator animator; 
    
    private int health = 5; 
    private float speed = 1.0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        enemyAudio = GameObject.Find("EnemyAudio").GetComponent<EnemyAudio>();  
        SetSpeed(Random.Range(1.0f, 5.0f));
        SetHealth(Random.Range(1, 5)); 
        int enemyLayer = LayerMask.NameToLayer("EnemyLayer"); 
        Physics2D.IgnoreLayerCollision(enemyLayer, enemyLayer);
        UpdateScale();
    } 

    private void Update()
    {
        Vector3 newPosition = transform.position + new Vector3(-1, 0, 0) * speed * Time.deltaTime;
        transform.position = newPosition;

        if (health <= 0)
        {
            StartCoroutine(DestroyObjectAfterDelay(0.5f));
        }
    }

    public void SetSpeed(float spd)
    {
        speed = spd; 
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Igloo"))
        {
            collision.gameObject.GetComponent<Animator>().SetTrigger("IglooHit"); 
            GameManager.Instance.Lives--;
            enemyAudio.PlayEnemyAttackIgloo(); 
            Destroy(gameObject); 
        } 
    }

    public void HitBySnowball()
    {
        health -= 1;
        enemyAudio.PlayEnemyHit(); 
    }
    
    public void SetHealth(int h)
    {
        health = h; 
    }

    private void UpdateScale()
    {
        float scaleMultiplier = 0.25f * (health - 1) + 0.5f;
        Vector3 newScale = new Vector3(scaleMultiplier, scaleMultiplier, 1.0f);
        transform.localScale = newScale;
    }

    IEnumerator DestroyObjectAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetTrigger("EnemyHit");
        enemyAudio.PlayEnemyDeath();
        GameManager.Instance.Score++; 
        Destroy(gameObject);
    }
}
