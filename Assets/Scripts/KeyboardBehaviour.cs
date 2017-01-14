using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class KeyboardBehaviour : MonoBehaviour
{
    public GameObject Cam;
    public GameObject PitchIntervalPrefab;
    public AudioSource music;

    protected float left;
    protected float right;

    protected List<GameObject> pitchIntervalObjects = new List<GameObject>();

    protected String[] oldSelectedLeftObject = new String[4];
    protected String[] oldSelectedRightObject = new String[4];

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject newObject = Instantiate(PitchIntervalPrefab);
            newObject.transform.localPosition = new Vector3(i * 7 * 0.26f, 0, 0);
            //newObject.transform.localScale = new Vector3(0.02f, 0.1f, 0.1f);
            newObject.transform.parent = this.transform;
            newObject.name = "" + i;
            pitchIntervalObjects.Add(newObject);
        }
        setScale(-10.0f, 10.0f, 4);
    }

    void setScale(float left, float right, int numPitchInterval)
    {
        this.left = left;
        this.right = right;
        transform.localPosition = new Vector3((left + right) / 2, 0, 0);
        float pitchIntervalWidth = (right - left) / 4;
        for(int i = 0; i < 4; i++)
        {
            float intervalLeft = left + pitchIntervalWidth * i;
            float intervalRight = intervalLeft + pitchIntervalWidth;
            pitchIntervalObjects[i].transform.localPosition = 
                new Vector3((intervalLeft + intervalRight) / 2, 0, 0);
            pitchIntervalObjects[i].GetComponent<KeyboardPitchIntervalBehaviour>().
                SetScale(intervalLeft, intervalRight, 0.1f, 0.9f);
        }
    }

    public void SetPressedPositions(List<Vector2> positions, bool isLeft)
    {
        if (Cam.GetComponent<CameraBehaviour>().angle == CameraBehaviour.START)
            return;
        String[] oldSelectedObject;
        if (isLeft)
            oldSelectedObject = oldSelectedLeftObject;
        else
            oldSelectedObject = oldSelectedRightObject;
        for (int i = 0; i < 4; i++)
        {
            String pitchSelectedObject;
            if (isLeft)
                pitchSelectedObject = pitchIntervalObjects[i].GetComponent<KeyboardPitchIntervalBehaviour>().selectedLeftObject;
            else
                pitchSelectedObject = pitchIntervalObjects[i].GetComponent<KeyboardPitchIntervalBehaviour>().selectedRightObject;
            pitchIntervalObjects[i].GetComponent<KeyboardPitchIntervalBehaviour>().SetPressedPositions(positions, isLeft);
            if (!pitchSelectedObject.Equals(oldSelectedObject[i]))
            {
                //print("Before " + (isLeft ? "Left" : "Right") + ", interval " + i + ", pressed " + pitchSelectedObject + ".");
                oldSelectedObject[i] = pitchSelectedObject;
                //print("Now " + (isLeft ? "Left" : "Right") + ", interval " + i + ", pressed " + pitchSelectedObject + ".");
                if (!pitchSelectedObject.Equals(""))
                {
                    print("Now " + (isLeft ? "Left" : "Right") + ", interval " + i + ", pressed " + pitchSelectedObject + ".");
                    music.pitch = 1;
                    int j = 0;
                    if (oldSelectedObject[i].Length < 2)
                        switch (oldSelectedObject[i][0])
                        {
                            case 'H': j = 9; break;
                            case 'I': j = 11; break;
                            case 'C': j = 0; break;
                            case 'D': j = 2; break;
                            case 'E': j = 4; break;
                            case 'F': j = 5; break;
                            case 'G': j = 7; break;
                        }
                    else
                        switch (oldSelectedObject[i][0])
                        {
                            case 'C': j = 1; break;
                            case 'D': j = 3; break;
                            case 'F': j = 6; break;
                            case 'G': j = 8; break;
                            case 'H': j = 10; break;
                        }
                    switch (i)
                    {
                        case 0: music.pitch = music.pitch * 0.25f; break;
                        case 1: music.pitch = music.pitch * 0.5f; break;
                        case 3: music.pitch = music.pitch * 2; break;
                    }
                    for (int k = 0; k < j; ++k)
                        music.pitch = music.pitch * 1.05946f;
                    music.Play();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
