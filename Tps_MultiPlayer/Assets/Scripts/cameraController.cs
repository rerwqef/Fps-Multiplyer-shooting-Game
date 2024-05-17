using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Photon.Pun;

public class cameraController : MonoBehaviour
{
    public float Yaxis;
    public float Xaxis;
    public float Cencityvity = 8f;
    public float min;
    public float max;
    public Transform player;
    public float offsetX;
    public float offsetY;
  public  PhotonView view;

    private void Start()
    {
        view=GetComponentInParent<PhotonView>();
    }
    public void Update()
    {
        if (view.IsMine)
        {
            Yaxis += Input.GetAxis("Mouse X") * Cencityvity;
            Xaxis -= Input.GetAxis("Mouse Y") * Cencityvity;

            // Clamp the Xaxis rotation to restrict vertical movement
            Xaxis = Mathf.Clamp(Xaxis, min, max);

            // Create a Quaternion based on the X and Y axis rotations
            Quaternion targetRotation = Quaternion.Euler(Xaxis, Yaxis, 0f);

            // Set the rotation of the camera to the target rotation
            transform.rotation = targetRotation;

            // Calculate the offset position
            Vector3 offset = new Vector3(offsetX, offsetY, -5f); // Custom offsets in X and Y axis

            // Apply rotation to the offset
            offset = transform.rotation * offset;

            // Set the position of the camera relative to the player with the offset
            transform.position = player.position + offset;
        }
    }
}
