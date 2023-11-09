using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{

    [SerializeField] AudioManager asm;
    [SerializeField] AudioClip PenguinDie;
    [SerializeField] AudioClip PenguinHit;
    [SerializeField] AudioClip IglooHit;

    public void PlayEnemyDeath()
    {
        asm.PlayOneShot(PenguinDie, false); 
    }

    public void PlayEnemyHit()
    {
        asm.PlayOneShot(PenguinHit, false); 
    }

    public void PlayEnemyAttackIgloo()
    {
        asm.PlayOneShot(IglooHit, false);
    }
}
