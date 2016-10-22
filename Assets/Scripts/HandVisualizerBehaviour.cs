using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HandVisualizerBehaviour : MonoBehaviour {

    public GameObject handTemplate;
    public GameObject LeapMotionDataSource;
    protected LeapMotionDataSourceBehaviour leapMotionDataSource;
    protected List<GameObject> handObjects;

	// Use this for initialization
	void Start () {
        handTemplate.SetActive(false);
        leapMotionDataSource = LeapMotionDataSource.GetComponent<LeapMotionDataSourceBehaviour>();
        handObjects = new List<GameObject>();
    }

    void UpdateChildObjectCount()
    {
        List<Leap.Hand> hands = leapMotionDataSource.GetHands();
        int maxSize = Mathf.Max(hands.Count, handObjects.Count);
        for(int i = 0; i < hands.Count; i++)
        {
            if(i < handObjects.Count)
            {
                handObjects[i].SetActive(true);
            }
            else
            {
                handTemplate.SetActive(true);
                handObjects.Add(Instantiate(handTemplate));
                handObjects[i].transform.parent = this.transform;
                handTemplate.SetActive(false);
            }
        }
        for(int i = hands.Count; i < handObjects.Count; i++)
        {
            handObjects[i].SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
        UpdateChildObjectCount();
        List<Leap.Hand> hands = leapMotionDataSource.GetHands();
        for (int i = 0; i < hands.Count; i++)
        {
            handObjects[i].GetComponent<HandBehaviour>().handData = hands[i];
        }
    }
}
