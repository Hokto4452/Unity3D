using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWaveArea : MonoBehaviour
{
    private SerchBoss _AttackArea;
    private BossAIMove moveBoss;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("SearchArea");
        moveBoss = GetComponentInParent<BossAIMove>();
        _AttackArea = obj.GetComponent<SerchBoss>();
    }

    void OnTriggerExit(Collider other)
    {
        _AttackArea.AttackWaveFlag = false;

        if (other.tag == "Player")
        {
            moveBoss.SetState(BossAIMove.BossState.Wait);
        }
    }
}
