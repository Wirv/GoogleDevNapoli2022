using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Wimera.camera
{
    public class WimeraBehaviour : MonoBehaviour
    {
        private GameObject player;

        [Header("Camera Position Offset Settings")]
        public float Height = 2.0f;
        public float Distance = 10.0f;
        public float mLerp = 10.0f;
        public float mouseSensitivity = 100.0f;
        public float clampAngle = 80.0f;
        
        private float rotY = 0.0f; // rotation around the up/y axis
        private float rotX = 0.0f; // rotation around the right/x axis
        private GameObject cameraPivot;
        private void Start()
        {
            if (GameObject.FindWithTag("Player"))
            {
                player = GameObject.FindWithTag("Player");
                cameraPivot = new GameObject("Camera Pivot");
                transform.SetParent(cameraPivot.transform);
                 
                transform.position = new Vector3(player.transform.localPosition.x, 
                    player.transform.localPosition.y + Height,
                    player.transform.localPosition.z - Distance);
            }
            else
            {
                Debug.Log("Can't find Player, anythings in scene have tag Player");
            }

            CameraPos();
        }

        private void CameraPos()
        {
            cameraPivot.transform.position = 
                Vector3.Lerp(cameraPivot.transform.position, player.transform.position, mLerp * Time.deltaTime);
        }

        public void Update()
        {
            CameraPos();

            CameraRotation();
        }

        private void CameraRotation()
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = -Input.GetAxis("Mouse Y");

            rotY += mouseX * mouseSensitivity * Time.deltaTime;
            rotX += mouseY * mouseSensitivity * Time.deltaTime;
            rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

            Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
            cameraPivot.transform.rotation = localRotation;
        }
    }
}

