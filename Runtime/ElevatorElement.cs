using System;
using UnityEngine;

namespace FGElevator
{
    public class ElevatorSwitch : MonoBehaviour
    {
        protected ElevatorController _controller;
        protected bool _isDisable = false;
        public void SwitchToggle()
        {
            // Debug.Log("is disabled " + _isDisable);
            if(!_isDisable && _controller != null)
            {
                _controller.OnSwitchToggle();
            }
        }

        public void DisableToggle(bool isDisable)
        {
            _isDisable = isDisable;
        }

        public void Register(ElevatorController controller)
        {
            _controller = controller;
        }
    }

    public abstract class ElevatorPlatform : MonoBehaviour
    {
        protected ElevatorController _controller;
        public abstract void Move(float speed);
        public abstract void Stop();

        public void Register(ElevatorController controller)
        {
            _controller = controller;
        }
    }
}
