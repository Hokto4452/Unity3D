using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAngle : MonoBehaviour
{
    [SerializeField] GameObject head;           //観測視のオブジェクト指定
    [SerializeField] float headAngle;           //回転角度
    [SerializeField] Vector3 startHeadAngle;    //初期角度
    bool neckSwitch = true;                     //回転中用

    //--------------- 初期
    void Start()
    {
        startHeadAngle = head.transform.localEulerAngles;    //初期角度を現在の角度に代入
    }

    //--------------- 更新
    void LateUpdate()
    {
        AngleAI();      //'void AnglAI()'関数の呼び出し
    }

    //--------------- 回転用関数
    void AngleAI()
    {
        if (headAngle >= 45)
        {
            neckSwitch = false;     //左に向く
        }
        else if (headAngle <= -45)
        {
            neckSwitch = true;      //右に向く
        }
        if (neckSwitch)
        {
            headAngle += Time.deltaTime * 10;       //回転速度(右)
        }
        else
        {
            headAngle -= Time.deltaTime * 10;       //回転速度(左)
        }

        head.transform.localEulerAngles = new Vector3(
            startHeadAngle.x,               //X軸回転処理
            startHeadAngle.y + headAngle,   //Y軸回転処理
            startHeadAngle.z                //Z軸回転処理
            );
    }
}
