using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePosition : MonoBehaviour
{
    //　目的地
    private Vector3 destination;
    //　スタート位置
    private Vector3 startPosition;

    [SerializeField] private GameObject _prefab;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        SetDestination(transform.position);
    }

    //　ランダムな位置の作成
    public void CreateRandomPosition()
    {
        //　ランダムなVector2の値を得る
        var randDestination = Random.insideUnitCircle * 10;
        //　現在地にランダムな位置を足して目的地とする
        SetDestination(startPosition + new Vector3(randDestination.x, 5, randDestination.y));


        ////var spawnPos = new Vector3(startPosition.x + randDestination.x, 5, startPosition.y + randDestination.y);
        //var spawnPos = new Vector3(startPosition.x, 5, startPosition.y);
        //Instantiate(_prefab, spawnPos, Quaternion.identity);
    }

    //　目的地を設定する
    public void SetDestination(Vector3 position)
    {
        destination = position;
    }

    //　目的地を取得する
    public Vector3 GetDestination()
    {
        return destination;
    }
}
