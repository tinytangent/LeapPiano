using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class HandBehaviour : MonoBehaviour {

    public Leap.Hand handData;

    public GameObject FingerTemplate;
    public GameObject Keyboard;
    public GameObject Cam;
    public GameObject OpenSphere;
    public List<GameObject> fingerObjects = new List<GameObject>();

    // Use this for initialization
    void Start () {
        if (fingerObjects.Count == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject newFinger = Instantiate(FingerTemplate);
                newFinger.transform.parent = this.transform;
                fingerObjects.Add(newFinger);
            }
        }
        for (int i = 0; i < 5; i++)
        {
            fingerObjects[i].SetActive(true);
        }
        FingerTemplate.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        int havePressedButton = 0;
        Vector2 pressPos = Vector2.zero;
        // Show hand
        if (handData != null)
        {
            float z = handData.PalmPosition.z;
            float deltax = 0f;
            if (z > 90f)
                z = 45f;
            else if (z < 50f)
                z = 5f;
            else
                z = z - 45f;
            if (handData.IsLeft)
                deltax = 120f;
            Vector3 originalVector = new Vector3(handData.PalmPosition.x + deltax, 20.0f, -z) / 20.0f;
            Vector3 newVector = Quaternion.AngleAxis(45, Vector3.left) * originalVector;
            transform.localPosition = newVector;
            // Method 1
            // transform.localPosition =
            //    new Vector3(handData.PalmPosition.x+deltax ,20.0f, -z) / 20.0f;
            // Method 2
            //transform.position = transform.parent.position + new Vector3(handData.PalmPosition.x + deltax, 20.0f, -z) / 20.0f;
        }
        else return;
        // Show fingers
        List<Leap.Finger> fingers = handData.Fingers;
        for(int i = 0; i < 5; i++)
        {
            if (i >= fingers.Count) break;
            Vector3 relativePosition = new Vector3(
                fingers[i].TipPosition.x - handData.PalmPosition.x,
                fingers[i].TipPosition.y - handData.PalmPosition.y,
                -(fingers[i].TipPosition.z - handData.PalmPosition.z)
                );
            fingerObjects[i].transform.localPosition = relativePosition / 20.0f;

            if (Vector3.Distance(fingerObjects[i].transform.position, OpenSphere.transform.position)
                < 1)
                OpenSphere.GetComponent<SphereBehaviour>().countToStart();
                
            if(fingers[i].Type == Leap.Finger.FingerType.TYPE_THUMB && Math.Abs(fingers[i].Direction.Dot(handData.PalmNormal)) > 0.4
                || Math.Abs(fingers[i].Direction.Dot(handData.PalmNormal)) > 0.7)
            {
                havePressedButton++;
                pressPos = new Vector2(fingerObjects[i].transform.position.x, fingerObjects[i].transform.position.z);
                fingerObjects[i].transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            }
            else
            {
                fingerObjects[i].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
        }
        List<Vector2> positions = new List<Vector2>();
        if(havePressedButton > 4)
        {
            Cam.GetComponent<CameraBehaviour>().setAngle(CameraBehaviour.START);
            Debug.Log("Exit now!!");
        }
        else if(havePressedButton > 0)
        {
            positions.Add(pressPos);
        }
        Keyboard.GetComponent<KeyboardBehaviour>().SetPressedPositions(positions, handData.IsLeft);
    }
}
