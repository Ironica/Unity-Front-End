using System;
using Resources.Scripts;
using UnityEngine;

/**
 * Source: https://blog.csdn.net/amcp9/article/details/79576173
 */
public class MainCameraController : MonoBehaviour
{

    public Vector3 selfPos;
    public Quaternion selfRot;
    
    public float maxScroll;
    public float minScroll;
    public float maxYRotateAngles = 70;
    public float minYRotateAngles = 5;
    public float rotateSpeed = 5;
    public float scrollSpeed = 1;

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
        // transform.LookAt(playerTransform);
        offsetPlayerPos = transform.position - playerTransform.position;
    }

    public void Reset()
    {
        transform.position = selfPos;
        transform.rotation = selfRot;
    }


    void Update()
    {
        transform.position = playerTransform.position + offsetPlayerPos;
        RotateCamera();
        ScrollView();
    }
    void RotateCamera()
    {
        if (Input.GetMouseButton(0))
        {
 
            transform.RotateAround(playerTransform.position, playerTransform.up, Input.GetAxis("Mouse X") * rotateSpeed);
            //更新玩家与摄像机的偏移信息
            offsetPlayerPos = transform.position - playerTransform.position;
            transform.position = playerTransform.position + offsetPlayerPos;
            //限制最大角度
            if (transform.localEulerAngles.x < maxYRotateAngles && Input.GetAxis("Mouse Y") < 0)
            {
                transform.RotateAround(playerTransform.position, transform.right, -Input.GetAxis("Mouse Y") * rotateSpeed);
                //更新玩家与摄像机的偏移信息
                offsetPlayerPos = transform.position - playerTransform.position;
                transform.position = playerTransform.position + offsetPlayerPos;
            }
            //限制最小角度
            if (transform.localEulerAngles.x > minYRotateAngles && Input.GetAxis("Mouse Y") > 0)
            {
                transform.RotateAround(playerTransform.position, transform.right, -Input.GetAxis("Mouse Y") * rotateSpeed);
                //更新玩家与摄像机的偏移信息
                offsetPlayerPos = transform.position - playerTransform.position;
                transform.position = playerTransform.position + offsetPlayerPos;
            }
 
        }
    }
    void ScrollView()
    {
        //限制最大缩放
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && offsetPlayerPos.magnitude < maxScroll)
        {
            //获取偏移量的模
            distance = offsetPlayerPos.magnitude;
            distance += System.Math.Abs(Input.GetAxis("Mouse ScrollWheel")) * scrollSpeed;
            //通过获得单位向量*新的偏移模更新偏移信息
            offsetPlayerPos = offsetPlayerPos.normalized * distance;
        }
        //限制最小缩放
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && offsetPlayerPos.magnitude > minScroll)
        {
            //获取偏移量的模
            distance = offsetPlayerPos.magnitude;
            distance += -System.Math.Abs(Input.GetAxis("Mouse ScrollWheel")) * scrollSpeed;
            //通过获得单位向量*新的偏移模更新偏移信息
            offsetPlayerPos = offsetPlayerPos.normalized * distance;
        }
    }

    private GameObject GetMiddle(GameObject go)
    {
        return go.transform.GetChild(go.transform.childCount / 2).gameObject;
    }
}
