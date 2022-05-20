using System;
using UnityEngine;

namespace FogFormer
{
    public class PlayerFlipper : MonoBehaviour
    {
        private PlayerController _controller;
        private WallGrabber _grabber;
        private void Awake()
        {
            _controller = GetComponent<PlayerController>();
            _grabber = GetComponent<WallGrabber>();
            _lastLookDir = _controller.LookDirection;
        }

        private int _lastLookDir;
        private void Update()
        {
            int lookDir = _controller.LookDirection;
            if (_lastLookDir != lookDir)
            {
                Vector2 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
            }
            _lastLookDir = lookDir;
        }   
    }
}