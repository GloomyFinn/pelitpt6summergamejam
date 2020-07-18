using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneController : MonoBehaviour
{
    public Rigidbody2D craneLoadRB; // Connect to Crane Load Rigidbody. Windforce affects this Rigidbody.



    [SerializeField]
    private int windForce = 1;

    [SerializeField]
    private int windBlastMinWaitForSec = 1;

    [SerializeField]
    private int windBlastMaxWaitForSec = 3;

    private bool lastWindBlastDirectionWasRight = false;

    [SerializeField]
    private int containerVelocityDamper = 1; // This is used to decide how much velocity the falling container inherits from the swinging container.

    // Used by ThisIsTheWind() function
    Vector3 craneLoadPrevLoc = Vector3.zero;
    Vector3 craneLoadCurrLoc = Vector3.zero;

    // These will be used to control the cranes' position onscreen
    private float horizontalMovement;
    private float verticalMovement;

    private bool canLoadNewContainer; // This is used to decide when player can drop the next container and when to show container hanging from the crane.

    // Line Renderer uses these two GameObjects to draw the crane cable.
    public GameObject craneTop;
    public GameObject craneLoad;

    public GameObject newContainer; // Container prefab goes here.

    public LineRenderer lineRenderer; // This draws the crane cable.



    void Start()
    {
        canLoadNewContainer = true;
        StartCoroutine(ThisIsTheWind());

        craneLoadPrevLoc = craneLoad.transform.position;
    }

    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

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
        //craneLoadRB.AddForce(transform.right * horizontalMovement * 10); // This was used to emulate wind manually
    }

    IEnumerator ThisIsTheWind()
    {
        craneLoadCurrLoc = (craneLoad.transform.position - craneLoadPrevLoc) / Time.deltaTime; // Crane swing direction. Positive right, negative left.

        // Randomizing the Wind Force for more realistic wind effect
        int windForceMin = windForce;
        int windForceMax = windForce * 5;
        int windFactor = Random.Range(windForceMin, windForceMax);

        if (craneLoadCurrLoc.x > 0)  // Wind blows from right
        {
            craneLoadRB.AddForce(transform.right * windFactor);
            lastWindBlastDirectionWasRight = true;
        }
        else  // Wind blows from left
        {
            craneLoadRB.AddForce(transform.right * -windFactor);
            lastWindBlastDirectionWasRight = false;
        }

        int windBlastTimer = Random.Range(windBlastMinWaitForSec, windBlastMaxWaitForSec);
        yield return new WaitForSeconds(windBlastTimer);
        StartCoroutine(ThisIsTheWind());
    }

    IEnumerator NewContainer()
    {
        GameObject container;
        canLoadNewContainer = false;
        craneLoad.GetComponent<SpriteRenderer>().enabled = false;
        container = Instantiate(newContainer, craneLoad.transform.position, craneLoad.transform.rotation);
        container.GetComponent<Rigidbody2D>().velocity = craneLoad.GetComponent<Rigidbody2D>().velocity / containerVelocityDamper;
        yield return new WaitForSeconds(2);
        craneLoad.GetComponent<SpriteRenderer>().enabled = true;
        canLoadNewContainer = true;
    }
}
