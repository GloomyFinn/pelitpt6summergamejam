using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatBehavior : MonoBehaviour
{
    public GameObject boat;
    public float sinkSpeed;
    public bool sinking = false;

    void FixedUpdate()
    {
        if (sinking == true)
        {
            BoatSink();
        }
    }

    private void OnTriggerStay2D (Collider2D other)
    {
        if(other.CompareTag("BoatFront") || (other.CompareTag("BoatBack")))
        {
            Debug.Log("Sinking");
            sinking = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        sinking = false;
    }

    void BoatSink()
    {

            Rigidbody2D bbb = boat.GetComponent<Rigidbody2D>();
            bbb.mass += sinkSpeed * Time.deltaTime;            
    }
}
