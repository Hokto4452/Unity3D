using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniMapCameraMove : MonoBehaviour
{
    private GameObject player;      //プレイヤーオブジェクト取得
    private Vector3 offset;         //移動量

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("Player");                //Playerオブジェクト検索
        offset = transform.position - player.transform.position;//移動した位置　ー　プレイヤーの位置
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;    //移動量
        //transform.rotation = player.transform.rotation;
    }
}
