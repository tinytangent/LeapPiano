using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using System;

public class KeyboardPitchIntervalBehaviour : MonoBehaviour {

    public GameObject WhiteKeyTemplate;
    public GameObject BlackKeyTemplate;
    public Material NormalMaterial;
    public Material BlackMaterial;

    protected float left;
    protected float right;
    protected float spacingScale;
    protected float blackKeyScale;
    protected float whiteKeyWidth;
    protected float blackKeyWidth;

    public const int whiteKeyCount = 7;
    public const int blackKeyCount = 5;
    public readonly int[] blackKeyPosition = { 0, 1, 3, 4, 5 };

    protected List<GameObject> whiteKeyObjects = new List<GameObject>();
    protected List<GameObject> blackKeyObjects = new List<GameObject>();

    public String selectedLeftObject = null;
    public String selectedRightObject = null;

    // Use this for initialization
    void Start () {
        Assert.AreNotEqual(WhiteKeyTemplate, null);
        Assert.AreNotEqual(BlackKeyTemplate, null);
        CheckChildrenExistence();
    }
	
    public void SetScale(float left, float right, float spacingScale, float blackKeyScale)
    {
        CheckChildrenExistence();
        this.left = left;
        this.right = right;
        this.spacingScale = spacingScale;
        this.blackKeyScale = blackKeyScale;
        whiteKeyWidth = (right - left) / (whiteKeyCount + spacingScale * whiteKeyCount);
        blackKeyWidth = whiteKeyWidth * blackKeyScale;
        UpdateChildren();
    }

    void CheckChildrenExistence()
    {
        if(whiteKeyObjects.Count == 0)
        {
            for (int i = 0; i < 7; i++)
            {
                GameObject newObject = Instantiate(WhiteKeyTemplate);
                newObject.transform.parent = this.transform;
                newObject.transform.localPosition = new Vector3(i * 0.26f, 0, 0);
                newObject.transform.localScale = new Vector3(0.02f, 0.1f, 0.1f);
                newObject.name = "" + (char)('C' + i);
                whiteKeyObjects.Add(newObject);
            }
        }
        if(blackKeyObjects.Count == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject newObject = Instantiate(BlackKeyTemplate);
                newObject.transform.parent = this.transform;
                newObject.transform.localPosition = new Vector3(0.13f + blackKeyPosition[i] * 0.26f, 0.1f, 0);
                newObject.transform.localScale = new Vector3(0.02f, 0.1f, 0.1f);
                newObject.name = "" + (char)('C' + blackKeyPosition[i]) + "#";
                blackKeyObjects.Add(newObject);
            }
        }
    }

    void UpdateChildren()
    {
        float offset = (right - left) / 2;
        for (int i = 0; i < 7; i++)
        {
            float keyLeft = whiteKeyWidth * ((1.0f + spacingScale) * i + spacingScale / 2) - offset;
            float keyRight = keyLeft + whiteKeyWidth;
            whiteKeyObjects[i].transform.localPosition = new Vector3((keyLeft + keyRight) / 2, 0, 0);
            whiteKeyObjects[i].transform.localScale = new Vector3(0.1f * whiteKeyWidth, 0.1f, 0.3f);
        }


        for (int i = 0; i < 5; i++)
        {
            float keyCenter = whiteKeyWidth * (1.0f + spacingScale) * (blackKeyPosition[i] + 1) - offset;
            float keyLeft = keyCenter - blackKeyWidth / 2;
            float keyRight = keyCenter + blackKeyWidth / 2;
            blackKeyObjects[i].transform.localPosition = new Vector3((keyLeft + keyRight) / 2, 0.2f, 0.3f);
            blackKeyObjects[i].transform.localScale = new Vector3(0.1f * blackKeyWidth, 0.1f, 0.2f);
        }
    }

	// Update is called once per frame
	void Update () {
    
	}

    public void SetPressedPositions(List<float> positions, bool isLeft)
    {
        for (int i = 0; i < 7; i++)
        {
            if(positions.Count > 0 && 
                Math.Abs(positions[0] - whiteKeyObjects[i].transform.position.x) < whiteKeyWidth / 2)
            {
                if (isLeft)
                    selectedLeftObject = whiteKeyObjects[i].name;
                else
                    selectedRightObject = whiteKeyObjects[i].name;
                whiteKeyObjects[i].GetComponent<MeshRenderer>().material = BlackMaterial;
            }
            else
            {
                if (isLeft
                    && selectedRightObject != whiteKeyObjects[i].name)
                    whiteKeyObjects[i].GetComponent<MeshRenderer>().material = NormalMaterial;
                if (!isLeft
                    && selectedLeftObject != whiteKeyObjects[i].name)
                    whiteKeyObjects[i].GetComponent<MeshRenderer>().material = NormalMaterial;
            }
            if (positions.Count == 0)
            {
                if (isLeft)
                    selectedLeftObject = "";
                else
                    selectedRightObject = "";
            }
        }
    }

    void SetGlobalPosition()
    {

    }
}
