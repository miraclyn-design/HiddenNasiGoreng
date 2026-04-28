using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private float cameraZoomSpeed = 4f;

    private Camera mainCam;
    private Transform mainCamTransform;

    private InputSystem_Actions input;
    private Coroutine zoomCoroutine;

    float scrollVal;

    void OnEnable()
    {
        if (input != null) input.Enable();
    }

    void OnDisable()
    {
        if (input != null) input.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCam = Camera.main;
        mainCamTransform = mainCam.transform;

        input = new();
        input.Enable();

        input.NewPlayer.SecondaryTouchContact.started += _ => ZoomStart();
        input.NewPlayer.SecondaryTouchContact.canceled += _ => ZoomEnd();
        input.NewPlayer.PrimaryTouchContact.canceled += _ => ZoomEnd();
    }

    private void ZoomEnd()
    {
        StopCoroutine(zoomCoroutine);
    }

    private void ZoomStart()
    {
        zoomCoroutine = StartCoroutine(ZoomDetection());
    }

    IEnumerator ZoomDetection()
    {
        float prevDistance = 0f;
        float distance = 0f;
        float targetZoom = 0;
        while (true)
        {
            distance = Vector2.Distance(input.NewPlayer.PrimaryFingerPos.ReadValue<Vector2>(), 
                                        input.NewPlayer.SecondaryFingerPos.ReadValue<Vector2>());

            float delta = distance - prevDistance;

            targetZoom -= delta * 0.01f;
            targetZoom = Mathf.Clamp(targetZoom, 2f, 10f);

            mainCam.orthographicSize = Mathf.Lerp(mainCam.orthographicSize, targetZoom, Time.deltaTime * 10f);

            /* if (distance > prevDistance)
            {
                Vector3 targetPos = mainCamTransform.position;
                targetPos.z -= 1;
                mainCamTransform.position = Vector3.Slerp(mainCamTransform.position,
                                                            targetPos,
                                                            Time.deltaTime * cameraZoomSpeed);
            } else if (distance < prevDistance)
            {
                Vector3 targetPos = mainCamTransform.position;
                targetPos.z += 1;
                mainCamTransform.position = Vector3.Slerp(mainCamTransform.position,
                                                            targetPos,
                                                            Time.deltaTime * cameraZoomSpeed);
            } */

            prevDistance = distance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /* if (Input.GetMouseButton(0))
            touchStart = mainCam.ScreenToWorldPoint(Input.mousePosition);
        
        if (Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - mainCam.ScreenToWorldPoint(Input.mousePosition);
            mainCam.transform.position += direction;
        } */
    }
}
