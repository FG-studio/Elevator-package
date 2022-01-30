using System;
using System.Collections.Generic;
using UnityEngine;

namespace FGElevator
{
    public class ElevatorController : IElevatorObserver
    {

        ElevatorStateType _initState = ElevatorStateType.LOWEST;
        ElevatorContext _ctx;
        ElevatorPlatform _platform;
        List<ElevatorSwitch> _switchList = new List<ElevatorSwitch>();
        float _speed = 0;
        public void init(float speed, ElevatorPlatform platform, ElevatorSwitch[] switchList)
        {
            _speed = speed;
            _platform = platform;
            _platform.Register(this);

            foreach(ElevatorSwitch _switch in switchList)
            {
                _switch.Register(this);
                _switchList.Add(_switch);
            }
            _ctx = new ElevatorContext(_initState, this);
        }

        public void OnChangeState(ElevatorStateType state, bool triggerState)
        {
            foreach (ElevatorSwitch _switch in _switchList)
            {
                _switch.DisableToggle(triggerState);
            }
            switch(state)
            {
                case ElevatorStateType.HIGH_TRANSFORM:
                    Move(_speed);
                    break;
                case ElevatorStateType.LOW_TRANSFORM:
                    Move(-1 * _speed);
                    break;
                case ElevatorStateType.HIGHEST:
                case ElevatorStateType.LOWEST:
                    Stop();
                    break;
                default: break;
            }
        }

        public void OnSwitchToggle()
        {
            _ctx.ChangeState();
        }

        public void OnBlocked()
        {
            _ctx.ChangeState();
        }

        public void Move(float spd)
        {
            _platform.Move(spd);
        }

        public void Stop()
        {
            _platform.Stop();
        }
    }
}
