using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    private float speed = 1.0f;

    private void Start()
    {
        SetSpeed(Random.Range(1.0f, 5.0f));
        int enemyLayer = LayerMask.NameToLayer("EnemyLayer"); 
        Physics2D.IgnoreLayerCollision(enemyLayer, enemyLayer);
    } 

    private void Update()
    {
        // Calculate the new position of the enemy.
        Vector3 newPosition = transform.position + new Vector3(-1, 0, 0) * speed * Time.deltaTime;
        transform.position = newPosition;
    }

    public void SetSpeed(float spd)
    {
        speed = spd; 
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Igloo"))
        {

            Destroy(gameObject); 
        } 
    }
}
