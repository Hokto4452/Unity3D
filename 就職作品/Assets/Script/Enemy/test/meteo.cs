using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meteo : MonoBehaviour
{
    //public GameObject splashPB;
    //public GameObject posSplash;
    //public float splashSpeed;
    //public int splashCount = 12;
    //private float splashInterval;
    //private float reloadInterval;
    //public Rigidbody projectile;
    //public float upSpeed;

    //bool notSplash;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    notSplash = false;
    //    projectile = GetComponent<Rigidbody>();
    //    upSpeed = 20;
    //    splashSpeed = 10;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    Rigidbody clone;
    //    if (splashCount == 0)
    //    {
    //        notSplash = true;
    //    }
    //    if (Input.GetKey(KeyCode.Alpha1))
    //    {
    //        reloadInterval += Time.deltaTime;
    //        if (splashCount > 0)
    //        {
    //            splashCount -= 12;
    //            GameObject bullet = Instantiate(splashPB, posSplash.transform.position, Quaternion.identity);
    //            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
    //            //bulletRb.velocity = new Vector3(bulletRb.velocity.x, upSpeed, bulletRb.velocity.z);
    //            bulletRb.AddForce(transform.forward * splashSpeed);
    //            //clone = Instantiate(projectile, posSplash.transform.position, transform.rotation) as Rigidbody;
    //            //clone.velocity = new Vector3(clone.velocity.x, upSpeed, clone.velocity.z);
    //        }
    //    }
    //}

    public GameObject rockPrefab;
    public AudioClip sound;
    public float InTime;
    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            GameObject rock = Instantiate(rockPrefab, transform.position, Quaternion.identity);
            Rigidbody rockRb = rock.GetComponent<Rigidbody>();
            //rockRb.isKinematic = true;
            rockRb.AddForce(transform.forward * 1200);
            Destroy(rock, 3.5f);
            //AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position);

            InTime += Time.deltaTime;
            if(InTime<0.2f)
            { 
}

        }
    }
    
    void OnTriggerEnter(Collider col)
    {
        BoxCollider _renderer;
        if(col.gameObject.tag =="Ground")
        {
            Debug.Log("aaaaaaaaaaaaaaaaa");
        }
        if (col.gameObject.tag == "bullet")
        {
            _renderer = rockPrefab.GetComponent<BoxCollider>();
            _renderer.isTrigger = false;
        }
    }
    void OnTriggerExit(Collider other)
    {
        BoxCollider _renderer;
        if(other.tag == "bullet")
        {
            _renderer = rockPrefab.GetComponent<BoxCollider>();
            _renderer.isTrigger = true;
        }
    }

}
