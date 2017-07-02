﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputType
{
    Keyboard,
    Trackpad
}

/// <summary>
/// A component for applying forces based on trackpad or keyboard input
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {

    [Tooltip("The type of input that will be applied as force")]
	public InputType InputType = InputType.Trackpad;
	[Tooltip("The minimum velocity")]
	public float Velocityclamp = 10f;
	[Tooltip("The scalar applied to the input axis vector")]
	public float AccelterationMultiplier = 1.0f;
	[Tooltip("The minimum input required to apply force")]
	public float XInputBuffer = 0.1f;
	public float YInputBuffer = 0.5f;
	[Tooltip("The decrease rate of the vertical vector to switch rows")]
	public float VerticalDecay = 0.1f;
	[Tooltip("The necessary y velocity to change rows")]
	public float VerticalJumpVelocity = 10f;
    [Tooltip("The Distance the player will lerp (Should be the size of row)")]
	public float VerticalJumpDistance = 0.1f;
	[Tooltip("The time in seconds it will take to lerp")]
	public float VerticalJumpLerp = 1f;
    [Tooltip("The time in seconds before the player may jump again)")]
    public float VerticalJumpCooldown = 1f;

    public LineRenderer DebugLineRenderer;

	private Vector3 _acceleration = Vector3.zero;
    private bool _initialized = false;

//Variable for Vertical Movement
    private bool _lerping;
	private float _lerpTimer;
    private float _lerpCooldownTimer;
    private Vector3 _lerpOrigin;

    private float _verticalAcceleration;
    private float _verticalJumpDistancInternal;
///

    private void Start()
    {
        EventSystem.instance.Connect<GameEvents.InitializeGameEvent>(Initialize);
    }

    private void Initialize(GameEvents.InitializeGameEvent e)
    {
        _initialized = true;
        _verticalJumpDistancInternal = Screen.height * VerticalJumpDistance;
    }

    /// <summary>
    /// Update is separated into two funtions based on input type.
    /// This is mostly for testing purposes
    /// </summary>
	private void FixedUpdate () {

        if (!_initialized) 
            return;

        //This doesn't work right now
        DebugMovement();

        //Update movment based on the type of input (only trackpad/mouse is implemented)
        if (InputType == InputType.Trackpad)
            TrackPadUpdate();
        else
            KeyboardUpdate();

        //Reduce vertical movement cooldown timer if it is active
		if (!_lerping && _lerpCooldownTimer > 0f)
		{
			_lerpCooldownTimer -= Time.fixedDeltaTime;
            if (_lerpCooldownTimer - Time.fixedDeltaTime < 0f)
                _lerpCooldownTimer = 0f;
		}

        //Handle the vertical movement lerp
		if (_lerping)
		{
            //Increment time
			_lerpTimer += Time.fixedDeltaTime;
            var newPos = transform.position;
            //Find direction based on acceleration
            var dir = _verticalAcceleration > 0f ? 1 : -1;
            //Adjust the y position based on lerp timer and jump distance
            newPos.y = Mathf.Lerp(_lerpOrigin.y, _lerpOrigin.y + (_verticalJumpDistancInternal * dir), _lerpTimer / VerticalJumpLerp);
			transform.position = newPos;
            //Reset the timer once we have reached the end
			if (_lerpTimer >= VerticalJumpLerp)
			{
				_lerpTimer = 0f;
				_lerping = false;
                _lerpCooldownTimer = VerticalJumpCooldown;
			}
		}
	}

    protected void TrackPadUpdate()
    {
        //Get the input from the mouse and apply it to the acceleration
		if (Mathf.Abs(Input.GetAxis("Mouse X")) > XInputBuffer)
		{
			_acceleration.x = Input.GetAxis("Mouse X") * AccelterationMultiplier;
		}
        if (Mathf.Abs(Input.GetAxis("Mouse Y")) > YInputBuffer && !_lerping)
		{
			_acceleration.y = Input.GetAxis("Mouse Y") * AccelterationMultiplier;
		}
        //If the player is lerping do not adjust y (because lerping is fixed)
        if (_lerping)
            _acceleration.y = 0f;

        //Add the acceleration force to teh rigid body
		GetComponent<Rigidbody2D>().AddForce(_acceleration);
        //Clamp the velocity so the play is continuously gliding
		if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) <= Velocityclamp)
		{
			GetComponent<Rigidbody2D>().velocity = Vector3.zero;
		}

/////DEBUG
        _verticalAcceleration += Input.GetAxis("Mouse Y") * 2;
        var fuff = 1f;
        fuff -= Time.deltaTime * VerticalDecay;
		_verticalAcceleration *= fuff;
        //////

        if (Mathf.Abs(_verticalAcceleration) >= VerticalJumpVelocity && !_lerping && _lerpCooldownTimer.Equals(0f))
        {
            Debug.Log("DID IT");
            _lerpOrigin = transform.position;
			_lerping = true;
            var vel = GetComponent<Rigidbody2D>().velocity;
            vel.y = 0f;
            GetComponent<Rigidbody2D>().velocity = vel;
        }

		_acceleration = Vector3.zero;
    }
    private void KeyboardUpdate(){}

    /// <summary>
    /// This doesn't work right now
    /// </summary>
    private void DebugMovement()
    {
        var LineRenderer = DebugLineRenderer;
        if (LineRenderer == null)
            return;

        var otherPos = GetComponent<RectTransform>().position;
		LineRenderer.SetPosition(0, otherPos);
        otherPos.y += _verticalAcceleration;

		if ((Mathf.Abs(_verticalAcceleration) > 0) && (Mathf.Abs(_verticalAcceleration) < VerticalJumpVelocity / 2))
		{
            LineRenderer.material.color = Color.red;
		}
		else if ((Mathf.Abs(_verticalAcceleration) > VerticalJumpVelocity / 2) && (Mathf.Abs(_verticalAcceleration) < VerticalJumpVelocity))
		{
			LineRenderer.material.color = Color.yellow;
		}
		else
		{
			LineRenderer.material.color = Color.green;
		}
        LineRenderer.SetPosition(1, otherPos);
    }
}