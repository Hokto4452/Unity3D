using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backJump : MonoBehaviour
{
    private BossAIMove moveBoss;
    public SerchBoss AttackWaveFlag;
    public bool backJumpFlag;

    // Start is called before the first frame update
    void Start()
    {
        moveBoss = GetComponentInParent<BossAIMove>();
        AttackWaveFlag = GetComponent<SerchBoss>();
        backJumpFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Player")
        {
            BossAIMove.BossState state = moveBoss.GetState();
            backJumpFlag = true;
            if(state == BossAIMove.BossState.Walk || state == BossAIMove.BossState.Chase)
            {
                moveBoss.SetState(BossAIMove.BossState.BackJump, collision.transform);
            }
        }
    }
    void OnTriggerExit(Collider collision)
    {
        backJumpFlag = false;
    }
}
