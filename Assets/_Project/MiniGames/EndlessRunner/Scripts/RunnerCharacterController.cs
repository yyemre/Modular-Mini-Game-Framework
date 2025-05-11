using MiniGames.EndlessRunner;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class RunnerCharacterController : MonoBehaviour
{
    [Header("Controls")]
    public float jumpLength = 2.0f; 
    public float jumpHeight = 1.2f; 
    public float laneOffset = 1.0f; 
    public float laneChangeSpeed = 1.0f;
    public float slideLength = 2.0f;
    
    public float minSpeed = 5.0f;
    public float maxSpeed = 10.0f;
    public float acceleration = .1f;
    
    protected int m_CurrentLane = 1;
    protected Vector3 m_TargetPosition = Vector3.zero;

    protected float m_Speed;

    protected bool m_Jumping;
    protected float m_JumpStart;

    protected bool m_Sliding;
    protected float m_SlideStart;

    protected bool m_IsMoving;
    protected bool m_IsRunning;
    protected bool m_IsInvincible;

    protected Vector3 m_MoveDirection = Vector3.zero;
    protected UnityEngine.CharacterController m_Controller;

    protected Vector2 m_StartingTouch;
    protected bool m_IsSwiping = false;

    public float speed => m_Speed;
    public float speedRatio => (m_Speed - minSpeed) / (maxSpeed - minSpeed);
    public bool isJumping => m_Jumping;
    public bool isSliding => m_Sliding;
    public bool isMoving => m_IsMoving;

    private void Awake()
    {
        m_Controller = GetComponent<UnityEngine.CharacterController>();
    }
    private void Start()
    {
        m_Speed = minSpeed;
    }
    private void Update()
    {
        if (!m_IsMoving) return;

        HandleInput();

        float scaledSpeed = m_Speed * Time.deltaTime;
        Vector3 currentPosition = transform.position;
        
        float targetX = m_TargetPosition.x;
        float smoothedX = Mathf.Lerp(currentPosition.x, targetX, Time.deltaTime * laneChangeSpeed);
        
        Vector3 desiredPosition = new Vector3(smoothedX, currentPosition.y, currentPosition.z + scaledSpeed);
        
        if (m_Jumping)
        {
            float jumpProgress = (currentPosition.z - m_JumpStart) / (jumpLength * (1.0f + speedRatio));
            if (jumpProgress >= 1.0f)
            {
                StopJumping();
            }
            else
            {
                desiredPosition.y = Mathf.Sin(Mathf.PI * jumpProgress) * jumpHeight;
            }
        }
        
        if (m_Sliding && currentPosition.z - m_SlideStart > slideLength)
        {
            StopSliding();
        }
        
        m_Controller.Move(desiredPosition - currentPosition);
        
        m_Speed += acceleration * Time.deltaTime;
    }
    private void HandleInput()
    {
        if (Input.touchCount == 1)
        {
            if (m_IsSwiping)
            {
                Vector2 diff = Input.GetTouch(0).position - m_StartingTouch;
                diff = new Vector2(diff.x / Screen.width, diff.y / Screen.width);

                if (diff.magnitude > 0.01f)
                {
                    if (Mathf.Abs(diff.y) > Mathf.Abs(diff.x))
                    {
                        if (diff.y < 0) Slide();
                        else Jump();
                    }
                    else
                    {
                        if (diff.x < 0) ChangeLane(-1);
                        else ChangeLane(1);
                    }

                    m_IsSwiping = false;
                }
            }

            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                m_StartingTouch = Input.GetTouch(0).position;
                m_IsSwiping = true;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                m_IsSwiping = false;
            }
        }
    }
    public void Jump()
    {
        Debug.Log("Jump");
        if (!m_IsRunning || m_Jumping) return;

        if (m_Sliding) StopSliding();

        float correctJumpLength = jumpLength * (1.0f + speedRatio);
        m_JumpStart = transform.position.z;
        m_Jumping = true;
    }
    public void Slide()
    {
        Debug.Log("Slide");
        if (!m_IsRunning || m_Sliding) return;

        if (m_Jumping) StopJumping();

        m_Sliding = true;
        m_SlideStart = transform.position.z;
        HandleCollider();
    }
    public void ChangeLane(int direction)
    {
        Debug.Log(direction == 1 ? "Right" : "Left");
        Debug.Log("target lane: " + (m_CurrentLane + direction));
        
        if (!m_IsRunning) return;

        int targetLane = m_CurrentLane + direction;
        if (targetLane < 0 || targetLane > 2) return;

        m_CurrentLane = targetLane;
        m_TargetPosition = new Vector3((m_CurrentLane - 1) * laneOffset, 0, 0);
    }
    public void StopJumping() => m_Jumping = false;

    public void StopSliding()
    {
        m_Sliding = false;
        HandleCollider();
    }

    private void HandleCollider()
    {
        if (m_Sliding)
        {
            m_Controller.height = 1.0f;
            m_Controller.center = new Vector3(0, 0.5f, 0);
        }
        else
        {
            m_Controller.height = 2.0f;
            m_Controller.center = new Vector3(0, 1f, 0);
        }
    }

    public void EnableControl()
    {
        m_IsMoving = true;
        m_IsRunning = true;
    }

    public void DisableControl()
    {
        m_IsMoving = false;
        m_IsRunning = false;
        StopJumping();
        StopSliding();
    }
    
    // TODO: Event system
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!m_IsInvincible && hit.gameObject.CompareTag("Obstacle"))
        {
            FindObjectOfType<RunnerGame>()?.OnDeath();
        }
        if (hit.gameObject.TryGetComponent(out Coin coin))
        {
            coin.Collect();
        }
    }
    public void ResetSpeed()
    {
        m_Speed = minSpeed;
    }

    public void ResetPosisition()
    {
        transform.position = new Vector3(0, 0, 0);
    }
}
