using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splashAttack : MonoBehaviour
{
    private BossAIMove moveBoss;
    public SerchBoss SplashWaveFlag;
    public bool SplashFlag;
    public int splashCount = 1;
    public float splashMadeCount = 5f;

    public GameObject rockPrefab;
    public float shotSplashTime;

    // Start is called before the first frame update
    void Start()
    {
        moveBoss = GetComponentInParent<BossAIMove>();
        SplashWaveFlag = GetComponent<SerchBoss>();
        SplashFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            BossAIMove.BossState state = moveBoss.GetState();
            SplashFlag = true;
            if (state == BossAIMove.BossState.Walk || state == BossAIMove.BossState.Chase)
            {
                if (splashCount > 0)
                {
                    splashCount -= 1;
                    GameObject target = GameObject.Find("muzzle");
                    GameObject clone = Instantiate(target, this.transform.position, Quaternion.Euler(transform.parent.eulerAngles.x - 45, transform.parent.eulerAngles.y, transform.parent.eulerAngles.z));
                    clone = Instantiate(target, this.transform.position, Quaternion.Euler(-45, 30, 0));
                    clone = Instantiate(target, this.transform.position, Quaternion.Euler(-45, 60, 0));
                    clone = Instantiate(target, this.transform.position, Quaternion.Euler(-45, 90, 0));
                    clone = Instantiate(target, this.transform.position, Quaternion.Euler(-45, 120, 0));
                    clone = Instantiate(target, this.transform.position, Quaternion.Euler(-45, 150, 0));
                    clone = Instantiate(target, this.transform.position, Quaternion.Euler(-45, 180, 0));
                    clone = Instantiate(target, this.transform.position, Quaternion.Euler(-45, 210, 0));
                    clone = Instantiate(target, this.transform.position, Quaternion.Euler(-45, 240, 0));
                    clone = Instantiate(target, this.transform.position, Quaternion.Euler(-45, 270, 0));
                    clone = Instantiate(target, this.transform.position, Quaternion.Euler(-45, 300, 0));
                    clone = Instantiate(target, this.transform.position, Quaternion.Euler(-45, 330, 0));

                    Rigidbody Rb = clone.GetComponent<Rigidbody>();
                    Rb.isKinematic = true;
                    Debug.Log(Rb.isKinematic);

                    shotSplashTime += Time.deltaTime;
                    if (shotSplashTime > 1f)
                    {
                        GameObject rock = Instantiate(rockPrefab, transform.position, Quaternion.identity);
                        Rigidbody rockRb = rock.GetComponent<Rigidbody>();
                        rockRb.AddForce(transform.forward * 1200);
                        Destroy(rock, 3.5f);
                    }

                }

                moveBoss.SetState(BossAIMove.BossState.Splash, other.transform);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        SplashFlag = false;

    }
}
