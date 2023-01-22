using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class SearchCharacter : MonoBehaviour
{
    private EnemyMovrAI3 moveEnemy;

    // Start is called before the first frame update
    void Start()
    {
        moveEnemy = GetComponentInParent<EnemyMovrAI3>();
    }

    void OnTriggerStay(Collider col)
    {
        //　プレイヤーキャラクターを発見
        if (col.tag == "Player")
        {
            //　敵キャラクターの状態を取得
            EnemyMovrAI3.EnemyState state = moveEnemy.GetState();
            //　敵キャラクターが追いかける状態でなければ追いかける設定に変更
            if (state == EnemyMovrAI3.EnemyState.Walk||state ==EnemyMovrAI3.EnemyState.Wait)
            {
                //Debug.Log("プレイヤー発見");
                //moveEnemy.SetState(EnemyMovrAI3.EnemyState.Chase, col.transform);
                moveEnemy.SetState(EnemyMovrAI3.EnemyState.Chase, col.transform);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("見失う");
            moveEnemy.SetState(EnemyMovrAI3.EnemyState.Wait);
        }
    }
}
