using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxEffect : MonoBehaviour
{
    public Transform camera; 
    Vector3 camStartPos;
    float distance; // distance between camera start position and camera current position
    GameObject[] backgrounds;
    Material[] material;
    float[] backSpeed;
    float fartherstBack;
    int backCount;
    [Range(0.01f, 0.2f)]
    public float parallaxSpeed;

    private void Start()
    {
        //camera = Camera.main.transform;


        backCount = transform.childCount;
        material = new Material[backCount];
        backSpeed = new float[backCount];
        backgrounds = new GameObject[backCount];
        
        for (int i = 0; i < backCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
            material[i] = backgrounds[i].GetComponent<Renderer>().material;
        }
        Invoke(nameof(Delay), 3f);
    }

    private void LateUpdate()
    {
        camera = GameManager.Instance.camera;
        distance = camera.position.x - camStartPos.x;
        transform.position = new Vector3(camera.position.x, transform.position.y, 0);
        if (backgrounds != null)
        {
            for (int i = 0; i < backgrounds.Length; i++)
            {
                float speed = backSpeed[i] * parallaxSpeed;
                material[i].SetTextureOffset("_MainTex", new Vector2(distance, 0) * speed);
            }
        }
       
    }

    private void BackSpeedCalculate(int backCount)
    {
        for (int i = 0; i < backCount; i++) // find the farthest background
        { 
            if ((backgrounds[i].transform.position.z - camera.position.z) > fartherstBack)
            {
                fartherstBack = backgrounds[i].transform.position.z - camera.position.z;
            }
        }

        for (int i = 0;i < backCount; i++) // set the speed of background
        {
            backSpeed[i] = 1 - (backgrounds[i].transform.position.z - camera.position.z) / fartherstBack;
        }
    }

    private void Delay()
    {
        BackSpeedCalculate(backCount);
        camStartPos = camera.position;

    }

}
