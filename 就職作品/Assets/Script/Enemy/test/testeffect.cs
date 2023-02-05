using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testeffect : MonoBehaviour
{
    public GameObject rockPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GameObject rock = Instantiate(rockPrefab, transform.position, Quaternion.identity);
            Rigidbody rockRb = rock.GetComponent<Rigidbody>();
            //rockRb.isKinematic = true;
            rockRb.AddForce(transform.up * 1200);
            Destroy(rock, 3.5f);
        }
    }
}
