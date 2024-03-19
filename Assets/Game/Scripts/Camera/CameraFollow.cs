using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : Singleton<CameraFollow>
{
    public Player target;
    public Vector3 offset;
    public float speed;
    public CinemachineVirtualCamera virtualCamera;


    void FixedUpdate()
    {
        if (target != null)
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, Time.deltaTime * speed);
            virtualCamera.Follow = target.transform;
        }
    }

    public void FindPlayer()
    {
        target = FindObjectOfType<Player>();
        //offset = this.transform.position - player.transform.position;

    }
}
