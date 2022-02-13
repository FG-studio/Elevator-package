using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FGElevator;

public class Switch : ElevatorSwitch, IInteractable
{
    SpriteRenderer _sprite;
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(!_isDisable)
        {
            _sprite.color = Color.green;
        } else
        {
            _sprite.color = Color.red;
        }
    }

    public void OnInteract()
    {
        Debug.Log("on interact switch");
        SwitchToggle();
    }
}
