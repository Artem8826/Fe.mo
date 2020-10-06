using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchMoveScript : MonoBehaviour
{
    public Text Text;

    private float deltaX;
    private float deltaY;

    private Rigidbody rb;
    private Collider _dragCollider;

    private bool moveAllowed = false;


    // Start is called before the first frame update
    void Start()
    {
        _dragCollider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        Text.text = "Start game";

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector3 pointer = Input.touchCount > 0 ? new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0) : Input.mousePosition;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(pointer), out RaycastHit hit, 15))
            {
                if (hit.collider == _dragCollider)
                {
                    Vector3 touchPos = hit.point;

                    deltaX = touchPos.x - transform.position.x;
                    deltaY = touchPos.y - transform.position.y;

                    moveAllowed = true;

                    rb.freezeRotation = true;
                    rb.velocity = new Vector3(0, 0, 0);
                    rb.useGravity = false;

                    Text.text = "Began phase locked";
                }
            }
        }

        if ((Input.GetMouseButton(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) && moveAllowed)
        {
            Vector3 pointer = Input.touchCount > 0 ? new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0) : Input.mousePosition;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(pointer), out RaycastHit hit, 15))
            {
                Vector3 touchPos = hit.point;

                rb.MovePosition(new Vector3(touchPos.x - deltaX, touchPos.y - deltaY, 0));

                Text.text = "Movement phase locked";
            }
        }
        
        if ((Input.GetMouseButtonUp(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) && moveAllowed)
        {
            moveAllowed = false;
            rb.freezeRotation = false;
            rb.useGravity = true;

            IfUponHoldCell();   

            Text.text = "Ended phase locked";
        }
    }
     
    private void IfUponHoldCell()
    {
        Vector3 pointer = Input.touchCount > 0 ? new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0) : Input.mousePosition;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(pointer), out RaycastHit hit, 25, LayerMask.GetMask("IHoldElement")))
        {
//            if (hit.collider.GetComponent<HoldCell>())
//            {
                Debug.Log("I upon hold cell " + LayerMask.LayerToName(hit.collider.gameObject.layer) );

                transform.position = hit.collider.transform.position;

                GravitationStop();
//            }
        }
    }

    private void GravitationStop()
    {
        rb.freezeRotation = true;
        rb.velocity = new Vector3(0, 0, 0);
        rb.useGravity = false;
    }

    private void UniversalRayCastFromScreenPoint()
    {

    }
}
