using System;

namespace FGElevator
{
    class ElevatorSwitch
    {
        ElevatorController _controller;
        bool _isDisable = false;
        public void SwitchToggle()
        {
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

    abstract class ElevatorPlatform
    {
        ElevatorController _controller;
        public abstract void Move(float speed);
        public abstract void Stop();

        public void Register(ElevatorController controller)
        {
            _controller = controller;
        }
    }
}
