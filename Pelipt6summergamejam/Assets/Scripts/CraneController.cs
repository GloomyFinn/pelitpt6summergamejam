using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneController : MonoBehaviour
{
    public Rigidbody2D craneLoadRB; // Connect to Crane Load Rigidbody. Windforce affects this Rigidbody.

    public GameObject theCrane; // Connect the whole crane GameObject here

    [SerializeField]
    private int windForce = 1;

    [SerializeField]
    private int windBlastMinWaitForSec = 1;

    [SerializeField]
    private int windBlastMaxWaitForSec = 3;

    [SerializeField]
    private int containerVelocityDamper = 1; // This is used to decide how much velocity the falling container inherits from the swinging container.

    // Used by ThisIsTheWind() function
    Vector3 craneLoadPrevLoc = Vector3.zero;
    Vector3 craneLoadCurrLoc = Vector3.zero;

    // These will be used to control the cranes' position onscreen
    private float horizontalMovement;
    private float verticalMovement;

    [SerializeField]
    private float craneHorizontalMoveSpeed = 1f;

    [SerializeField]
    private float craneVerticalMoveSpeed = 1f;

    private bool canLoadNewContainer; // This is used to decide when player can drop the next container and when to show container hanging from the crane.

    // Line Renderer uses these two GameObjects to draw the crane cable.
    public GameObject craneTop;
    public GameObject craneLoad; // craneLoad is also used to move crane vertically.

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
        CraneMovementControls();

        string test = ContainerFlowController.LoadContainerToCrane("i");
        Debug.Log("load weight: " + test);
    }

    void LateUpdate()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, craneTop.transform.position);
        lineRenderer.SetPosition(1, craneLoad.transform.position);
    }


    private void CraneMovementControls()
    {
        Vector3 craneHorizontalMovement = new Vector3(horizontalMovement, 0f, 0f);
        theCrane.transform.position += craneHorizontalMovement * Time.deltaTime * craneHorizontalMoveSpeed;
        theCrane.transform.position = new Vector3(Mathf.Clamp(theCrane.transform.position.x, -2f, 2f), theCrane.transform.position.y, theCrane.transform.position.z);

        Vector3 craneVerticalMovement = new Vector3(0f, verticalMovement, 0f);
        craneLoad.transform.position += craneVerticalMovement * Time.deltaTime * craneVerticalMoveSpeed;
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
        }
        else  // Wind blows from left
        {
            craneLoadRB.AddForce(transform.right * -windFactor);
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
