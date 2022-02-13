using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FGElevator;

public class ElevatorManager : MonoBehaviour
{
    ElevatorController _controller = new ElevatorController();
    [SerializeField] Switch _highSwitch;
    [SerializeField] Switch _lowSwitch;
    [SerializeField] Switch _middleSwitch;
    [SerializeField] Platform _platform;
    [SerializeField] float _speed = 4.0f;
    [SerializeField] GameObject _maxHeight;
    [SerializeField] GameObject _minHeight;
    // Start is called before the first frame update
    void Start()
    {
        ElevatorSwitch[] list = { _highSwitch, _lowSwitch, _middleSwitch };
        _platform.SetAmplitude(_maxHeight, _minHeight);
        _controller.init(_speed, _platform, list);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
