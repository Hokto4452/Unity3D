using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eruptionAttack : MonoBehaviour
{
    private BossAIMove moveBoss;        //BossAIMoveコンポーネント
    public SerchBoss AttackWaveFlag;    //SerchBossコンポーネント
    public bool eruptionAttackFlag;     //噴火攻撃フラグ

    // Start is called before the first frame update
    void Start()
    {
        moveBoss = GetComponentInParent<BossAIMove>();  //BossAIMoveコンポーネント取得
        AttackWaveFlag = GetComponent<SerchBoss>();     //SerchBossコンポーネント取得
        eruptionAttackFlag = false;                     //噴火攻撃フラグをオフ
    }

    public void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")  //Playerタグのついたオブジェクトに触れた時
        {
            BossAIMove.BossState state = moveBoss.GetState();                           //BossAIMove.BossStateに情報を送る
            eruptionAttackFlag = true;                                                  //噴火攻撃フラグオン
            if(state == BossAIMove.BossState.Walk||state==BossAIMove.BossState.Chase)   //巡回、又は追跡中ステートの時
            {
                moveBoss.SetState(BossAIMove.BossState.EruptionAttack, other.transform);//噴火攻撃ステートに移行
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        eruptionAttackFlag = false;     //噴火攻撃フラグオフ
    }
}
