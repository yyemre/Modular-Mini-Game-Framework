using MiniGames.EndlessRunner;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RunnerCharacterController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float jumpLength = 2.0f;
    [SerializeField] private float jumpHeight = 1.2f;
    [SerializeField] private float laneOffset = 1.0f;
    [SerializeField] private float laneChangeSpeed = 1.0f;
    [SerializeField] private float slideLength = 2.0f;

    [SerializeField] private float minSpeed = 5.0f;
    [SerializeField] private float maxSpeed = 10.0f;
    [SerializeField] private float acceleration = .1f;

    private int _currentLane = 1;
    private Vector3 _targetPosition;
    private float _speed;
    
    private bool _isMoving;
    private bool _isRunning;
    private bool _isJumping;
    private float _jumpStartZ;

    private bool _isSliding;
    private float _slideStartZ;
    private bool _isSwiping;
    private Vector2 _touchStart;

    private CharacterController _controller;

    public float Speed => _speed;
    public float SpeedRatio => (_speed - minSpeed) / (maxSpeed - minSpeed);
    public bool IsJumping => _isJumping;
    public bool IsSliding => _isSliding;
    public bool IsMoving => _isMoving;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        ResetSpeed();
        _targetPosition = transform.position;
    }

    private void Update()
    {
        if (!_isMoving) return;

        HandleSwipeInput();
        MoveCharacter();
        UpdateJump();
        UpdateSlide();
    }

    private void MoveCharacter()
    {
        float moveStep = _speed * Time.deltaTime;
        Vector3 current = transform.position;
        float targetX = Mathf.Lerp(current.x, _targetPosition.x, laneChangeSpeed * Time.deltaTime);
        Vector3 desired = new Vector3(targetX, current.y, current.z + moveStep);

        _controller.Move(desired - current);
        _speed = Mathf.Min(_speed + acceleration * Time.deltaTime, maxSpeed);
    }

    private void UpdateJump()
    {
        if (!_isJumping) return;

        float progress = (transform.position.z - _jumpStartZ) / (jumpLength * (1.0f + SpeedRatio));
        if (progress >= 1f)
        {
            _isJumping = false;
            return;
        }

        float yOffset = Mathf.Sin(Mathf.PI * progress) * jumpHeight;
        var pos = transform.position;
        transform.position = new Vector3(pos.x, yOffset, pos.z);
    }

    private void UpdateSlide()
    {
        if (_isSliding && transform.position.z - _slideStartZ > slideLength)
        {
            StopSliding();
        }
    }

    private void HandleSwipeInput()
    {
        if (Input.touchCount != 1) return;

        var touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            _touchStart = touch.position;
            _isSwiping = true;
        }

        if (touch.phase == TouchPhase.Ended)
        {
            _isSwiping = false;
        }

        if (_isSwiping)
        {
            Vector2 delta = (touch.position - _touchStart) / Screen.width;
            if (delta.magnitude < 0.01f) return;

            _isSwiping = false;

            if (Mathf.Abs(delta.y) > Mathf.Abs(delta.x))
            {
                if (delta.y > 0) Jump();
                else Slide();
            }
            else
            {
                if (delta.x > 0) ChangeLane(1);
                else ChangeLane(-1);
            }
        }
    }

    public void Jump()
    {
        if (!_isRunning || _isJumping) return;

        StopSliding();
        _isJumping = true;
        _jumpStartZ = transform.position.z;
    }

    public void Slide()
    {
        if (!_isRunning || _isSliding) return;

        StopJumping();
        _isSliding = true;
        _slideStartZ = transform.position.z;
        AdjustCollider(forSlide: true);
    }

    public void StopJumping() => _isJumping = false;

    public void StopSliding()
    {
        _isSliding = false;
        AdjustCollider(forSlide: false);
    }

    public void ChangeLane(int direction)
    {
        if (!_isRunning) return;

        int targetLane = _currentLane + direction;
        if (targetLane < 0 || targetLane > 2) return;

        _currentLane = targetLane;
        _targetPosition = new Vector3((_currentLane - 1) * laneOffset, 0, 0);
    }

    private void AdjustCollider(bool forSlide)
    {
        _controller.height = forSlide ? 1.0f : 2.0f;
        _controller.center = new Vector3(0, forSlide ? 0.5f : 1.0f, 0);
    }

    public void EnableControl()
    {
        _isMoving = true;
        _isRunning = true;
    }

    public void DisableControl()
    {
        _isMoving = false;
        _isRunning = false;
        StopJumping();
        StopSliding();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Obstacle"))
        {
            FindObjectOfType<RunnerGame>()?.OnDeath(); // TODO: Event'e dönüştür
        }
        else if (hit.gameObject.TryGetComponent(out Coin coin))
        {
            coin.Collect();
        }
    }

    public void ResetSpeed() => _speed = minSpeed;

    public void ResetPosisition() => transform.position = Vector3.zero;
}
