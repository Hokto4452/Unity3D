using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimEvent : MonoBehaviour
{
    private EnemyMovrAI3 enemy;
    [SerializeField] private SphereCollider sphereCollider;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<EnemyMovrAI3>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AttackStart()
    {
        sphereCollider.enabled = true;
    }

    public void AttackEnd()
    {
        sphereCollider.enabled = false;
    }

    public void StateEnd()
    {
        enemy.SetState(EnemyMovrAI3.EnemyState.Freeze);
    }

    public void EndDamage()
    {
        enemy.SetState(EnemyMovrAI3.EnemyState.Walk);
    }
}
