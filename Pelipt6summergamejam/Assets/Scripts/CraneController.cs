using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneController : MonoBehaviour
{
    public Rigidbody2D rb;

    public GameObject craneTop;
    public GameObject craneLoad;

    public GameObject newContainer;

    public LineRenderer lineRenderer;

    private bool canLoadNewContainer;

    private float horizontalMovement;


    void Start()
    {
        canLoadNewContainer = true;
    }

    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && canLoadNewContainer)
        {
            StartCoroutine(NewContainer());
        }
    }

    void LateUpdate()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, craneTop.transform.position);
        lineRenderer.SetPosition(1, craneLoad.transform.position);
    }

    void FixedUpdate()
    {
        rb.AddForce(transform.right * horizontalMovement * 10);
    }

    IEnumerator NewContainer()
    {
        GameObject container;
        canLoadNewContainer = false;
        craneLoad.GetComponent<SpriteRenderer>().enabled = false;
        container = Instantiate(newContainer, craneLoad.transform.position, craneLoad.transform.rotation);
        container.GetComponent<Rigidbody2D>().velocity = craneLoad.GetComponent<Rigidbody2D>().velocity / 3;
        yield return new WaitForSeconds(2);
        craneLoad.GetComponent<SpriteRenderer>().enabled = true;
        canLoadNewContainer = true;
    }
}
