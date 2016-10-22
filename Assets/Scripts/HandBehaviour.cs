using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class HandBehaviour : MonoBehaviour {

    public Leap.Hand handData;

    public GameObject FingerTemplate;
    public GameObject Keyboard;
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
        bool havePressedButton = false;
        float pressPos = 0.0f;
        if (handData != null)
        {
            transform.localPosition =
                new Vector3(handData.PalmPosition.x,20.0f, 0) / 20.0f;
        }
        else return;
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
            if(Math.Abs(fingers[i].Direction.Dot(handData.PalmNormal)) > 0.7)
            {
                havePressedButton = true;
                pressPos = fingerObjects[i].transform.position.x;
                fingerObjects[i].transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            }
            else
            {
                fingerObjects[i].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
        }
        List<float> positions = new List<float>();
        if(havePressedButton)
        {
            positions.Add(pressPos);
        }
        Keyboard.GetComponent<KeyboardBehaviour>().SetPressedPositions(positions);
    }
}
