using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoSplit : MonoBehaviour
{
    public GameObject tomatoUncut;
    public GameObject left;
    public GameObject middle;
    public GameObject right;
    public float forceToCut;
    public float maxAngleError;
    public float maxHorizontalDisplacementError;
    public MeshRenderer wrapMesh;
    public CookStatus wrapCookStatus;

    public bool isBlade;
    public bool isSwing;
    public Vector3 originalPosition;
    public Quaternion originalRotation;
    public float cutForce;
    public float cutAngle;
    public float cutDisplacement;

    void Awake()
    {
        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;
    }

    // Use this for initialization
    void Start()
    {
        isBlade = false;
        isSwing = false;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.localPosition = originalPosition;
        //transform.localRotation = originalRotation;
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider col = collision.collider;

        if (col.transform.name == "BladeEdgeA" || col.transform.name == "BladeEdgeB" || col.transform.name == "BladeEdge")
        {

            BladeLogic blade = col.GetComponent<BladeLogic>();

            //foreach (ContactPoint contact in collision.contacts)
            //{
            //    Debug.DrawRay(contact.point, contact.normal, Color.white);
            //}

            if (isBlade)
            {
                return;
            }

            isBlade = true;
            //print("blade edge: " + col.transform.name + ", collider rotation: " + transform.up + ", blade rotation: " + col.transform.up + ", blade velocity: " + blade.bladeVelocity);

            cutForce = Mathf.Abs(blade.bladeVelocity.y);

            if (Mathf.Abs(transform.up.y) > 0.5f)
            {
                if (Mathf.Abs(transform.up.y - col.transform.up.y) <= 1)
                {
                    cutAngle = Mathf.Abs(Mathf.Abs(transform.up.x) - Mathf.Abs(col.transform.up.x));
                }
                else
                {
                    cutAngle = 2f - Mathf.Abs(Mathf.Abs(transform.up.x) - Mathf.Abs(col.transform.up.x));
                }
            }
            else
            {
                if (Mathf.Abs(transform.up.x - col.transform.up.x) <= 1)
                {
                    cutAngle = Mathf.Abs(Mathf.Abs(transform.up.y) - Mathf.Abs(col.transform.up.y));
                }
                else
                {
                    cutAngle = 2f - Mathf.Abs(Mathf.Abs(transform.up.y) - Mathf.Abs(col.transform.up.y));
                }
            }

            cutDisplacement = Vector3.Distance(collision.contacts[0].point, transform.position);
            //print("Distance: " + cutDisplacement);

            if (true)
            {

                //print("cut direction: " + Mathf.Abs(transform.up.x - col.transform.up.x) + ", cut# " + blade.cutCount);

                split();

            }

            Destroy(gameObject);

            //print("Angle rating: " + cutAngleRating + ", Force rating: " + cutForceRating + ", Swing rating: " + cutHorizontalSwingRating + ", Displacement rating" + cutDisplacementRating);
        }
    }

    void split()
    {
        if (left != null)
        {
            left.GetComponent<CookStatus>().isGood = wrapCookStatus.isGood; //If the food wrap is already burnt before it's been cut, it's sliced should keep the same status.


            foreach(MeshRenderer mesh in left.GetComponentsInChildren<MeshRenderer>())
            {
                mesh.material.color = wrapMesh.material.color;
            }

            left.SetActive(true);
            left.transform.parent = null;
        }

        if (right != null)
        {
            left.GetComponent<CookStatus>().isGood = wrapCookStatus.isGood;

            foreach (MeshRenderer mesh in right.GetComponentsInChildren<MeshRenderer>())
            {
                mesh.material.color = wrapMesh.material.color;
            }

            right.SetActive(true);
            right.transform.parent = null;
        }

        if (middle != null)
        {
            left.GetComponent<CookStatus>().isGood = wrapCookStatus.isGood;

            foreach (MeshRenderer mesh in middle.GetComponentsInChildren<MeshRenderer>())
            {
                mesh.material.color = wrapMesh.material.color;
            }

            middle.SetActive(true);
            middle.transform.parent = null;
        }

        left.GetComponent<Rigidbody>().AddForce(-tomatoUncut.transform.right * 0.5f, ForceMode.Impulse);
        right.GetComponent<Rigidbody>().AddForce(tomatoUncut.transform.right * 0.5f, ForceMode.Impulse);
        Destroy(tomatoUncut);
    }
}
