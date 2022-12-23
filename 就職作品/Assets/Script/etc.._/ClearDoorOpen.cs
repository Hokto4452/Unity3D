using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearDoorOpen : MonoBehaviour
{
    public void DoorMove()
    {
        StartCoroutine("DoorRotate");
    }

    IEnumerator DoorRotate()
    {
        for (int pos = 0; pos < 6; pos++)
        {
            transform.Translate(0, 1f, 0);
            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(100.0f);

        for (int pos = 0; pos < 6; pos++)
        {
            transform.Translate(0, -1f, 0);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
