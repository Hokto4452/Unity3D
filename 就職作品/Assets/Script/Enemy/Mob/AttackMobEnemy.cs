using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMobEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("������");
            col.GetComponent<PlayerController>().TakeDamage(transform.root);
        }
    }
}