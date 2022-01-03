using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;
    [Tooltip("Add maxHitPoint to enemy when they dies.")]
    [SerializeField] int difficultyRamp = 1;
    int currentHP = 0;

    Enemy enemy;
    // Start is called before the first frame update
    void OnEnable()
    {
        currentHP = maxHitPoints;
    }
    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void OnParticleCollision(GameObject other)
    {
        ProccessHit();    
    }
    void ProccessHit()
    {
        currentHP--;
        if(currentHP <= 0)
        {            
            gameObject.SetActive(false);
            maxHitPoints += difficultyRamp;
            enemy.RewardGold();
        }
    }
}
