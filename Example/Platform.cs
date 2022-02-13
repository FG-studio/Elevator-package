using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FGElevator;

public class Platform : ElevatorPlatform
{
    Rigidbody2D _body;
    bool _isRunning;
    GameObject _maxHeight;
    GameObject _minHeight;
    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_isRunning)
        {
            //Debug.Log(transform.position.y + ", " + _maxHeight.transform.position.y + ", " + _minHeight.transform.position.y);
            if(transform.position.y >= _maxHeight.transform.position.y || transform.position.y <= _minHeight.transform.position.y)
            {
                _controller.OnBlocked();
            } 
        }
    }

    public override void Move(float spd)
    {
        _body.velocity = new Vector2(0, spd);
        _isRunning = true;
    }

    public override void Stop()
    {
        _body.velocity = new Vector2(0, 0);
        _isRunning = false;
    }

    public void SetAmplitude(GameObject maxPoint, GameObject minPoint) 
    {
        _maxHeight = maxPoint;
        _minHeight = minPoint;
    }
}
