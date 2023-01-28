using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eruptionAttack : MonoBehaviour
{
    private BossAIMove moveBoss;
    public SerchBoss AttackWaveFlag;
    public bool eruptionAttackFlag;

    // Start is called before the first frame update
    void Start()
    {
        moveBoss = GetComponentInParent<BossAIMove>();
        AttackWaveFlag = GetComponent<SerchBoss>();
        eruptionAttackFlag = false;
    }

    public void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            BossAIMove.BossState state = moveBoss.GetState();
            eruptionAttackFlag = true;
            if(state == BossAIMove.BossState.Walk||state==BossAIMove.BossState.Chase)
            {
                moveBoss.SetState(BossAIMove.BossState.EruptionAttack, other.transform);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        eruptionAttackFlag = false;
    }
}
