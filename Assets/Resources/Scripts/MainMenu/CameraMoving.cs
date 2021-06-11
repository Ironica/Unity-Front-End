using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class CameraMoving : MonoBehaviour
{
    [SerializeField]
    private Vector2 min;
    [SerializeField]
    private Vector2 max;

    [SerializeField] [Range(0.01f, 0.1f)] private float lerpSpeed = 0.05f;
    private Vector3 _newPosition;

    private void Awake()
    {
        _newPosition = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _newPosition, Time.deltaTime * lerpSpeed);

        if (Vector3.Distance(transform.position, _newPosition) < 1f)
        {
            GetNewPosition();
        }
    }

    void GetNewPosition()
    {
        var xPos = Random.Range(min.x, max.x);
        var zPos = Random.Range(min.y, max.y);
        _newPosition = new Vector3(xPos, zPos, 0);
    }
}
