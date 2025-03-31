using UnityEngine;

public class Grapple : MonoBehaviour
{
    public LayerMask grappleLayer;   // Define what can be grappled
    public Transform gunTip;         // The tip where the grapple starts
    public float maxGrappleDistance = 20f;
    public float grappleSpeed = 10f;
    public LineRenderer lineRenderer; // Optional visual effect
    public KeyCode grappleKey = KeyCode.Mouse1; // Right Mouse Button

    private Rigidbody rb;
    private Vector3 grapplePoint;
    private bool isGrappling = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (lineRenderer != null)
            lineRenderer.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(grappleKey))
        {
            StartGrapple();
        }
        if (Input.GetKeyUp(grappleKey))
        {
            StopGrapple();
        }
        if (isGrappling)
        {
            DrawRope();
        }
    }

    void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(gunTip.position, gunTip.forward, out hit, maxGrappleDistance, grappleLayer))
        {
            grapplePoint = hit.point;
            isGrappling = true;

            if (lineRenderer != null)
            {
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, gunTip.position);
                lineRenderer.SetPosition(1, grapplePoint);
            }
        }
    }

    void FixedUpdate()
    {
        if (isGrappling)
        {
            Vector3 direction = (grapplePoint - transform.position).normalized;
            rb.linearVelocity = direction * grappleSpeed;

            if (Vector3.Distance(transform.position, grapplePoint) < 1f)
            {
                StopGrapple();
            }
        }
    }

    void StopGrapple()
    {
        isGrappling = false;
        if (lineRenderer != null) lineRenderer.enabled = false;
        rb.linearVelocity = Vector3.zero; // Stop movement when reaching the point
    }

    void DrawRope()
    {
        if (lineRenderer != null)
        {
            lineRenderer.SetPosition(0, gunTip.position);
            lineRenderer.SetPosition(1, grapplePoint);
        }
    }
}
