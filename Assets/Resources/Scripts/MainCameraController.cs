using System;
using Resources.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

/**
 * Source: https://blog.csdn.net/amcp9/article/details/79576173
 */
public class MainCameraController : MonoBehaviour
{

    public Vector3 selfPos = new Vector3(10, 10, 10);
    public Quaternion selfRot;
    
    public float maxScroll;
    public float minScroll;
    public float maxPan;
    public float maxYRotateAngles = 70;
    public float minYRotateAngles = 5;
    public float rotateSpeed = 5;
    public float scrollSpeed = 1;
    public float panSpeed = 0.5f;

    public GameObject target;

    private Transform playerTransform;
    private Vector3 offsetPlayerPos;
    private float distance;
    private Quaternion currentRotation;
 
    void Start()
    {
        target = GameObject.Find("Tiles");
        playerTransform = GetMiddle(target).transform;
        selfPos = transform.position;
        selfRot = transform.rotation;
        transform.LookAt(playerTransform);
        offsetPlayerPos = transform.position - playerTransform.position;
    }

    public void OnClickReset()
    {
        target = GameObject.Find("Tiles");
        playerTransform = GetMiddle(target).transform;
        transform.position = selfPos;
        transform.rotation = selfRot;
        transform.LookAt(playerTransform);
        offsetPlayerPos = transform.position - playerTransform.position;
    }

    void Update()
    {
        transform.position = playerTransform.position + offsetPlayerPos;
        RotateCamera();
        ScrollView();
        Pan();
    }
    void RotateCamera()
    {
        if (Global.mouseActivated && Input.GetMouseButton(0))
        {
 
            transform.RotateAround(playerTransform.position, playerTransform.up, Input.GetAxis("Mouse X") * rotateSpeed);
            
            offsetPlayerPos = transform.position - playerTransform.position;
            transform.position = playerTransform.position + offsetPlayerPos;
            
            if (transform.localEulerAngles.x < maxYRotateAngles && Input.GetAxis("Mouse Y") < 0)
            {
                transform.RotateAround(playerTransform.position, transform.right, -Input.GetAxis("Mouse Y") * rotateSpeed);
                
                offsetPlayerPos = transform.position - playerTransform.position;
                transform.position = playerTransform.position + offsetPlayerPos;
            }
            
            if (transform.localEulerAngles.x > minYRotateAngles && Input.GetAxis("Mouse Y") > 0)
            {
                transform.RotateAround(playerTransform.position, transform.right, -Input.GetAxis("Mouse Y") * rotateSpeed);
                
                offsetPlayerPos = transform.position - playerTransform.position;
                transform.position = playerTransform.position + offsetPlayerPos;
            }
 
        }
    }
    void ScrollView()
    {
        
        if (Global.mouseActivated && Input.GetAxis("Mouse ScrollWheel") < 0 && offsetPlayerPos.magnitude < maxScroll)
        {
            
            distance = offsetPlayerPos.magnitude;
            distance += System.Math.Abs(Input.GetAxis("Mouse ScrollWheel")) * scrollSpeed;
            
            offsetPlayerPos = offsetPlayerPos.normalized * distance;
        }
        
        if (Global.mouseActivated && Input.GetAxis("Mouse ScrollWheel") > 0 && offsetPlayerPos.magnitude > minScroll)
        {
            
            distance = offsetPlayerPos.magnitude;
            distance += -System.Math.Abs(Input.GetAxis("Mouse ScrollWheel")) * scrollSpeed;
            
            offsetPlayerPos = offsetPlayerPos.normalized * distance;
        }
    }

    void Pan()
    {
        if (Global.mouseActivated && Input.GetKey("up") && offsetPlayerPos.magnitude < maxPan)
        {
            transform.Translate(0, panSpeed, 0);
            offsetPlayerPos = transform.position - playerTransform.position;
            if (offsetPlayerPos.magnitude > maxPan)
            {
                transform.Translate(0, -panSpeed, 0);
                offsetPlayerPos = transform.position - playerTransform.position;
            }
        }
        if (Global.mouseActivated && Input.GetKey("down") && offsetPlayerPos.magnitude < maxPan)
        {
            transform.Translate(0, -panSpeed, 0);
            offsetPlayerPos = transform.position - playerTransform.position;
            if (offsetPlayerPos.magnitude > maxPan)
            {
                transform.Translate(0, panSpeed, 0);
                offsetPlayerPos = transform.position - playerTransform.position;
            }
        }
        if (Global.mouseActivated && Input.GetKey("left") && offsetPlayerPos.magnitude < maxPan)
        {
            transform.Translate(-panSpeed, 0, 0);
            offsetPlayerPos = transform.position - playerTransform.position;
            if (offsetPlayerPos.magnitude > maxPan)
            {
                transform.Translate(panSpeed, 0, 0);
                offsetPlayerPos = transform.position - playerTransform.position;
            }
        }
        if (Global.mouseActivated && Input.GetKey("right") && offsetPlayerPos.magnitude < maxPan)
        {
            transform.Translate(panSpeed, 0, 0);
            offsetPlayerPos = transform.position - playerTransform.position;
            if (offsetPlayerPos.magnitude > maxPan)
            {
                transform.Translate(-panSpeed, 0, 0);
                offsetPlayerPos = transform.position - playerTransform.position;
            }
        }
    }

    private GameObject GetMiddle(GameObject go)
    {
        return go.transform.GetChild(go.transform.childCount / 2).gameObject;
    }
}
