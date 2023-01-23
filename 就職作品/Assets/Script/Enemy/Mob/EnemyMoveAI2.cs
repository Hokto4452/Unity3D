using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]        //オブジェクトにNavMeshAgentコンポーネントを設置

public class EnemyMoveAI2 : MonoBehaviour
{
    [SerializeField] int destPoint = 0;         //
    private NavMeshAgent agent;                 //
    [SerializeField] GameObject head;           //
    [SerializeField] float headAngle;           //
    [SerializeField] Vector3 startHeadAngle;    //
    bool neckSwitch = true;                     //

    [SerializeField] float quitRange = 5f;      //
    [SerializeField] bool tracking = false;     //

    Vector3 playerPos;                          //
    GameObject player;                          //
    float distance;                             //

    //---------------初期化
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();               //コンポーネントの代入
        agent.autoBraking = false;                          //

        player = GameObject.Find("Player");                 //Playerという名のオブジェクトを指定

        startHeadAngle = head.transform.localEulerAngles;   //現在の視点方向を代入
    }

    //----------------更新
    void LateUpdate()
    {
        playerPos = player.transform.position;                                  //プレイヤーの現在地の更新
        distance = Vector3.Distance(this.transform.position, playerPos);        //プレイヤーの位置からenemyまでの距離を計算

        //レーダーを生成し、プレイヤーが当たると....
        if (Physics.Raycast(head.transform.position, head.transform.up * 3, 3)) //生成位置、距離
        {
            tracking = true;
        }

        //
        if (tracking)
        {
            //Agentを動かす
            agent.isStopped = false;        //
            //追跡の時、quitRangeより距離が離れたら中止
            if (distance > quitRange)       //
                tracking = false;           //

            //Playerを目標とする
            agent.destination = playerPos;  //
        }
        else
        {
            //Agentを止める
            agent.isStopped = true;         //

            //首を左右どちらに動かすか
            if (headAngle >= 45)
            {
                neckSwitch = false;         //
            }
            else if (headAngle <= -45)
            {
                neckSwitch = true;          //
            }

            //首の角度に加える数値を増減させる
            if (neckSwitch)
            {
                headAngle += Time.deltaTime * 10;   //
            }
            else
            {
                headAngle -= Time.deltaTime * 10;   //
            }

            //首のローカルの角度に初期角度とX軸の首の傾きの数値を加える
            head.transform.localEulerAngles = new Vector3(
                startHeadAngle.x + headAngle,   //
                startHeadAngle.y,               //
                startHeadAngle.z                //
                );
        }
    }

    //----------------
    void OnDrawGizmosSelected()
    {
        //頭から出ているRayの線を赤いワイヤーフレームで示す
        Gizmos.color = Color.red;                                               //
        Vector3 direction = head.transform.position + head.transform.up * 3;    //
        Gizmos.DrawLine(head.transform.position, direction);                    //

        //quitRangeの範囲を青いワイヤーフレームで示す
        Gizmos.color = Color.blue;                                              //
        Gizmos.DrawWireSphere(transform.position, quitRange);                   //
    }
}
