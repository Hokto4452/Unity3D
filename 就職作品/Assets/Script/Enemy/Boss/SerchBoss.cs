using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerchBoss : MonoBehaviour
{
    private BossAIMove moveBoss;
    // Start is called before the first frame update
    void Start()
    {
        moveBoss = GetComponent<BossAIMove>();
    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag =="Player")
        {
            BossAIMove.BossState state = moveBoss.GetState();
            if(state == BossAIMove.BossState.Walk||state == BossAIMove.BossState.Wait)
            {
                moveBoss.SetState(BossAIMove.BossState.Chase, other.transform);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        moveBoss.SetState(BossAIMove.BossState.Wait);
    }
}
