using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerchBoss : MonoBehaviour
{
    private BossAIMove moveBoss;
    public bool AttackWaveFlag;

    // Start is called before the first frame update
    void Start()
    {
        moveBoss = GetComponentInParent<BossAIMove>();
        AttackWaveFlag = false;
    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag =="Player")
        {
            BossAIMove.BossState state = moveBoss.GetState();
            AttackWaveFlag = true;
            if (state == BossAIMove.BossState.Walk || state == BossAIMove.BossState.Wait)
            {
                moveBoss.SetState(BossAIMove.BossState.Chase, other.transform);

            }
        }
    }
    //void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        moveBoss.SetState(BossAIMove.BossState.Wait);
    //    }
    //}
}
