using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backJump : MonoBehaviour
{
    private BossAIMove moveBoss;            //BossAIMoveコンポーネント取得
    public SerchBoss AttackWaveFlag;        //SerchBossコンポーネント取得
    public bool backJumpFlag;               //バックジャンプフラグ

    // Start is called before the first frame update
    void Start()
    {
        moveBoss = GetComponentInParent<BossAIMove>();  //BossAIMoveコンポーネント取得
        AttackWaveFlag = GetComponent<SerchBoss>();     //SerchBossコンポーネント取得
        backJumpFlag = false;                           //バックジャンプフラグ
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay(Collider collision)
    {
        if(collision.tag == "Player")           //Playerタグが付いたオブジェクトに触れた時
        {
            BossAIMove.BossState state = moveBoss.GetState();   //BossAIMove.BossStateに情報を送る
            backJumpFlag = true;                                //バックジャンプフラグを立てる
            if(state == BossAIMove.BossState.Walk || state == BossAIMove.BossState.Chase)   //AIステートが巡回、又は追跡中の時
            {
                moveBoss.SetState(BossAIMove.BossState.BackJump, collision.transform);      //バックジャンプステートに切り替え
            }
        }
    }
    void OnTriggerExit(Collider collision)
    {
        backJumpFlag = false;       //バックジャンプフラグをオフ
    }
}
