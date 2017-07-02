using System.Collections;
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
    [Tooltip("Player movement speed (for keyboard controls only)")]
    public float MovementSpeed = 1f;

    public LineRenderer DebugLineRenderer;

	private Vector3 _acceleration = Vector3.zero;
    private bool _initialized = false;

//Variable for Vertical Movement
    private bool _lerping;
	private float _lerpTimer;
    private float _lerpCooldownTimer;
    private Vector3 _lerpOrigin;
    private float _lerpDirection;

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

        //Update movmenet based on the type of input (only trackpad/mouse is implemented)
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
            //Adjust the y position based on lerp timer and jump distance
            newPos.y = Mathf.Lerp(_lerpOrigin.y, _lerpOrigin.y + (_verticalJumpDistancInternal * _lerpDirection), _lerpTimer / VerticalJumpLerp);
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
        //Set the acceleration relative to the input
		if (Mathf.Abs(Input.GetAxis("Mouse X")) > XInputBuffer)
		{
			_acceleration.x = Input.GetAxis("Mouse X") * AccelterationMultiplier;
		}
		//Vertical acceleration is additive and decays over time
		_verticalAcceleration += Input.GetAxis("Mouse Y") * 2;
		var Decay = 1f;
		Decay -= Time.deltaTime * VerticalDecay;
		_verticalAcceleration *= Decay;

       

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

        if (Mathf.Abs(_verticalAcceleration) >= VerticalJumpVelocity && !_lerping && _lerpCooldownTimer.Equals(0f))
        {
            Debug.Log("DID IT");
            _lerpOrigin = transform.position;
			_lerping = true;
			//Find direction based on acceleration
			_lerpDirection = _verticalAcceleration > 0f ? 1 : -1;
            var vel = GetComponent<Rigidbody2D>().velocity;
            vel.y = 0f;
            GetComponent<Rigidbody2D>().velocity = vel;
        }

        _acceleration = Vector3.zero;
    }
    private void KeyboardUpdate()
	{
		//Set the acceleration relative to the input
		if (Input.GetKey(KeyCode.A))
		{
			_acceleration.x = -MovementSpeed * AccelterationMultiplier;
		}
		if (Input.GetKey(KeyCode.D))
		{
			_acceleration.x = MovementSpeed * AccelterationMultiplier;
		}
		//Vertical acceleration is additive and decays over time
		if (Input.GetKeyDown(KeyCode.W))
		{
			_verticalAcceleration += MovementSpeed * 2;
		}
		if (Input.GetKeyDown(KeyCode.S))
		{
			_verticalAcceleration -= MovementSpeed * 2;
		}
		//Reduce vertical acceleration over time
		var Decay = 1f;
		Decay -= Time.deltaTime * VerticalDecay;
		_verticalAcceleration *= Decay;

		//If the player is lerping do not adjust y (because lerping is fixed)
		if (_lerping)
			_verticalAcceleration = 0f;

		//Add the acceleration force to teh rigid body
		GetComponent<Rigidbody2D>().AddForce(_acceleration);
		//Clamp the velocity so the play is continuously gliding
		if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) <= Velocityclamp)
		{
			GetComponent<Rigidbody2D>().velocity = Vector3.zero;
		}

		if (Mathf.Abs(_verticalAcceleration) >= MovementSpeed && !_lerping && _lerpCooldownTimer.Equals(0f))
		{
			Debug.Log("DID IT");
			_lerpOrigin = transform.position;
			_lerping = true;
			//Find direction based on acceleration
            _lerpDirection = _verticalAcceleration > 0f ? 1 : -1;
			var vel = GetComponent<Rigidbody2D>().velocity;
			vel.y = 0f;
			GetComponent<Rigidbody2D>().velocity = vel;
		}

		_acceleration = Vector3.zero;
    }

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
