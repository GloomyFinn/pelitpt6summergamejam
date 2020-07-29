using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snap : MonoBehaviour
{

    Collider2D m_Collider;
    Vector3 m_Size;



    private void OnTriggerEnter2D (Collider2D col)
    {
        if (col.tag == ("Bottom"))
            {
           
            Debug.Log("Snapster");
            col.transform.parent.parent = gameObject.transform.parent;
            col.transform.parent.localPosition = new Vector3(0f, 2f, 0f);

           
            //Fetch the Collider from the GameObject
            m_Collider = GetComponentInParent<BoxCollider2D>();

            //Fetch the size of the Collider volume
            m_Size = m_Collider.bounds.size;

            //Output to the console the size of the Collider volume
            Debug.Log("Collider Size : " + m_Size);


        }
        //gameObject.GetComponent<FixedJoint>().connectedBody = col.parent.parent<Rigidbody2D>();
        
    }
  }
    



