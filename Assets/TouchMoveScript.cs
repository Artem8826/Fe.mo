using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class TouchMoveScript : MonoBehaviour
{
    public Text Text;
    public LayerMask InteractLayerMask;
    public InteractionEffect InteractionEffectProp;

    private float deltaX;
    private float deltaY;
    private float ForceColliderSize = .5f;

    private Rigidbody _rb;
    private Collider _dragCollider;
    private bool _initGravityState;

    private bool moveAllowed = false;


    // Start is called before the first frame update
    void Start()
    {
        _dragCollider = GetComponent<Collider>();
        _rb = GetComponent<Rigidbody>();
        _initGravityState = _rb.useGravity;
        _rb.useGravity = false;
        _dragCollider.isTrigger = true;

        if (_dragCollider is SphereCollider sphereCollider)
        {
            sphereCollider.radius = ForceColliderSize;
        }

        //        Text.text = "Start game";

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

                    _rb.freezeRotation = true;
                    _rb.velocity = new Vector3(0, 0, 0);
                    _rb.useGravity = false;

                    Debug.Log("Began phase locked");
                }
            }
        }

        if ((Input.GetMouseButton(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) && moveAllowed)
        {
            Vector3 pointer = Input.touchCount > 0 ? new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0) : Input.mousePosition;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(pointer), out RaycastHit hit, 15))
            {
                Vector3 touchPos = hit.point;

                _rb.MovePosition(new Vector3(touchPos.x - deltaX, touchPos.y - deltaY, transform.position.z));

                Debug.Log("Movement phase locked");
            }
        }
        
        if ((Input.GetMouseButtonUp(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) && moveAllowed)
        {
            moveAllowed = false;
            _rb.freezeRotation = false;
            _rb.useGravity = _initGravityState;

            IfUponHoldCell();
            IfUponIPutable();

            Debug.Log("Ended phase locked");
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

    private void IfUponIPutable()
    {
        Vector3 pointer = Input.touchCount > 0 ? new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0) : Input.mousePosition;

//        if (InteractionEffectProp == InteractionEffect.Wet)
//            if (Physics.Raycast(Camera.main.ScreenPointToRay(pointer), out RaycastHit hit, 25, LayerMask.GetMask("IDryMakableElement")))
//            if (Physics.Raycast(Camera.main.ScreenPointToRay(pointer), out RaycastHit hit, 25, (1 << 10)))
            if (Physics.Raycast(Camera.main.ScreenPointToRay(pointer), out RaycastHit hit, 25, InteractLayerMask))
            {
                FunctionEffect.Call(hit.collider, InteractionEffectProp);
//                if (hit.collider.TryGetComponent(out IPutable putable))
//                {
//                    if (InteractionEffectProp == InteractionEffect.Wet)
//                    {
//                        putable.OnWet();
//                    } 
//                    else if (InteractionEffectProp == InteractionEffect.Dry)
//                    {
//                        putable.OnDry();
//                    }
//
//                }

            //                    Debug.Log("I upon OnWet object " + LayerMask.LayerToName(hit.collider.gameObject.layer));
            transform.position = hit.collider.transform.position;
            }
    }

//    private void IfDry()
//    {
//        Vector3 pointer = Input.touchCount > 0 ? new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0) : Input.mousePosition;
//
//        Debug.Log(Convert.ToString(InteractLayerMask.value, 2).PadLeft(32, '0'));
//
//        if (InteractionEffectProp == InteractionEffect.Dry)
//        {
////            if (Physics.Raycast(Camera.main.ScreenPointToRay(pointer), out RaycastHit hit, 25, LayerMask.GetMask("IDryMakableElement")))
////            if (Physics.Raycast(Camera.main.ScreenPointToRay(pointer), out RaycastHit hit, 25, (1 << 10)))
//                if (Physics.Raycast(Camera.main.ScreenPointToRay(pointer), out RaycastHit hit, 25, InteractLayerMask))
//                {
//                if (hit.collider.TryGetComponent(out IPutable putable))
//                {
//                    putable.OnDry();
//
//                    Debug.Log("I upon OnDry object " + LayerMask.LayerToName(hit.collider.gameObject.layer));
//
//                    transform.position = hit.collider.transform.position;
//                }
//            }
//        }
//            
//    }

    private void GravitationStop()
    {
        _rb.freezeRotation = true;
        _rb.velocity = new Vector3(0, 0, 0);
        _rb.useGravity = false;
    }

    private void UniversalRayCastFromScreenPoint()
    {

    }
}

