using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]        //オブジェクトにNavMeshAgentコンポーネントを設置

public class EnemyMoveAI : MonoBehaviour
{
    //--------------- 変数宣言 --------------------
    public Transform[] points;                  //
    [SerializeField] int destPoint = 0;         //
    private NavMeshAgent agent;                 //

    Vector3 playerPos;
    public GameObject player;
    public GameObject bullet;
    float distance;
    [SerializeField] float trackingRange = 6f;
    [SerializeField] float quitRange = 10f;
    [SerializeField] float attackRange = 8f;
    [SerializeField] bool tracking = false;
    
    //----------------ステート駆動
    public enum EnemyAIState
    {
        CHACE,          //追いかける
        ATTACK,         //攻撃する
        ROUND,          //巡回する
        DO_NOTHING      //何もしない
    }

    EnemyAIState currentState = EnemyAIState.ROUND;
    bool stateEnter = true;

    void ChangeState(EnemyAIState newState)
    {
        currentState = newState;             //ステートの切り替え
        stateEnter = true;                   //ステートの切り替え合図
    }

    //----------------初期化関数
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        //GotoNextPoint();
        player = GameObject.Find("Player");
    }

    //----------------更新関数
    void Update()
    {
        StateAI();
    }
    

    void StateAI()
    {
        switch(currentState)
        {
            case EnemyAIState.DO_NOTHING:
                if (stateEnter)
                {
                    stateEnter = false;
                }
                break;
            case EnemyAIState.ATTACK:
                break;
            case EnemyAIState.CHACE:
                ChaceAI();
                break;
            case EnemyAIState.ROUND:
                GotoNextPoint();
                break;
        }
    }

    //----------------追跡関数
    void ChaceAI()
    {
        #region
        //    playerPos = player.transform.position;                              //Playerの現在地
        //    distance = Vector3.Distance(this.transform.position, playerPos);    //Playerとこのオブジェクトの距離を測る

        //    if (tracking)       //追跡中処理
        //    {
        //        if (distance <= quitRange)          //追跡区域内
        //        {
        //            agent.destination = playerPos;  //Playerを目標とする
        //            if(distance<=attackRange)
        //            {
        //                //attckPoint.GetComponent<EnemyShot>().BallShot();

        //            }
        //        }
        //        else if (distance > quitRange)      //追跡の時、quitRangeより距離が離れたら中止
        //        {
        //            tracking = false;
        //        }


        //    }
        //    else                 //追跡外処理
        //    {
        //        if (distance < trackingRange)   //PlayerがtrackingRangeより近づいたら追跡開始
        //        {
        //            tracking = true;            //
        //        }

        //        if (!agent.pathPending && agent.remainingDistance < 0.5f)   // エージェントが現目標地点に近づいてきたら、次の目標地点を選択します
        //        {
        //            GotoNextPoint();            //巡回処理
        //        }
        //    }
        #endregion
        playerPos = player.transform.position;                              //Playerの現在地
        distance = Vector3.Distance(this.transform.position, playerPos);    //Playerとこのオブジェクトの距離を測る
        if (distance <= quitRange)          //追跡区域内
        {
            agent.destination = playerPos;  //Playerを目標とする
        }
        else if (distance > quitRange)      //追跡の時、quitRangeより距離が離れたら中止
        {
            ChangeState(EnemyAIState.ROUND);
        }
    }

    //----------------巡回関数
    void GotoNextPoint()
    {
        if (points.Length == 0)                          // 地点がなにも設定されていないときに返します
        {
            return;
        }

        agent.destination = points[destPoint].position; // エージェントが現在設定された目標地点に行くように設定します

        destPoint = (destPoint + 1) % points.Length;    // 配列内の次の位置を目標地点に設定し、必要ならば出発地点にもどります




        playerPos = player.transform.position;                              //Playerの現在地
        distance = Vector3.Distance(this.transform.position, playerPos);    //Playerとこのオブジェクトの距離を測る

        if (distance < trackingRange)       //追跡中処理
        {
            ChangeState(EnemyAIState.CHACE);
        }
    }

    //----------------範囲関数
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;                                   //色を赤
        Gizmos.DrawWireSphere(transform.position, trackingRange);   //フレーム範囲を表示
        
        Gizmos.color = Color.blue;                                  //色を青
        Gizmos.DrawWireSphere(transform.position, quitRange);       //フレーム範囲を表示

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    
}
