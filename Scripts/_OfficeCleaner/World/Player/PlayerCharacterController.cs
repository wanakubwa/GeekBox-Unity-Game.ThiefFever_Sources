using StackSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float runThreshold = 2.5f;
    [SerializeField]
    private float curveAngleThreshold = 0.5f;
    [SerializeField]
    private float curveSpeedMultiply = 2f;
    [SerializeField]
    private float normalSpeedMultiply = 1.5f;
    [SerializeField]
    private PlayerAnimationController animatorController = new PlayerAnimationController();
    [SerializeField]
    private BubbleUIController bubbleUI;
    [SerializeField]
    private StackController stacker;

    private Rigidbody body;

    //Collision State
    private List<Collider> colliding = new List<Collider>();
    private Collider groundCollider = new Collider();
    private Rigidbody groundRigidbody = new Rigidbody();
    private Vector3 groundNormal = Vector3.down;
    private Vector3 groundContactPoint = Vector3.zero;
    private Vector3 groundVelocity = Vector3.zero;

    public float MoveSpeed { get => moveSpeed; }

    // Controllers.
    public PlayerAnimationController AnimatorController { get => animatorController; }
    public BubbleUIController BubbleUI { get => bubbleUI; }
    public StackController Stacker { get => stacker; }

    // Variables.
    public bool IsInputEnabled { get; set; } = true;
    public bool IsHide { get; set; } = false;
    private UserInputManager CachedInputManager { get; set; }
    private Vector3 InputMoveVector { get; set; } = Vector3.zero;
    private bool CanMove { get; set; } = false;

    public void SetVisible(bool isVisible)
    {
        gameObject.SetActive(isVisible);
        IsHide = !isVisible;

        if (Stacker.StackedItems.Count > Constants.DEFAULT_VALUE)
        {
            AnimatorController.RefreshAnimationState(PlayerAnimationController.AnimationType.STACK_RUN);
        }
    }

    public void AddToStack(StackableObject prefab, Vector3 startPosition)
    {
        if(Stacker.StackedItems.Count == Constants.DEFAULT_VALUE)
        {
            AnimatorController.RefreshAnimationState(PlayerAnimationController.AnimationType.STACK_RUN);
        }

        Stacker.AddToStack(prefab, startPosition);
    }

    public void RemoveFromStack(Action<StackableObject> onRemoveCallback = null)
    {
        Stacker.RemoveFromStack(onRemoveCallback);

        if (Stacker.StackedItems.Count == Constants.DEFAULT_VALUE)
        {
            AnimatorController.RefreshAnimationState(PlayerAnimationController.AnimationType.NORMAL);
        }
    }

    private void Awake()
    {
        CachedInputManager = UserInputManager.Instance;
    }

    //Initialize variables
    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    //Movement Handling
    private void FixedUpdate()
    {
        // Gracz nie dotyka ekranu.
        if (!IsInputEnabled) { return; }

        Vector3 movement = Vector3.zero;
        if (CanMove)
        {
            if (InputMoveVector != Vector3.zero)
            {
                // -45 = 180 - camera.y
                Vector3 fixedMoveVector = Quaternion.AngleAxis(-45f, Vector3.up) * InputMoveVector;
                Vector3 targetRotationPosition = transform.position + fixedMoveVector;
                transform.LookAt(targetRotationPosition);
            }

            movement = transform.right * MoveSpeed * Time.fixedDeltaTime;
        }

        body.velocity = movement;

        Debug.DrawLine(transform.position, transform.position + body.velocity, Color.red);
    }

    private void Update()
    {
        if (!CanMove)
        {
            AnimatorController.SetIdle(true);
        }
        else
        {
            AnimatorController.SetIdle(false);
        }

        if(IsInputEnabled == false)
        {
            AnimatorController.RunParticles.SetActive(false);
        }

        // Sprawdzenie czy postac biegnie.
        if (body.velocity.magnitude >= runThreshold)
        {
            AnimatorController.RunParticles.SetActive(true);
        }
        else
        {
            AnimatorController.RunParticles.SetActive(false);
        }
    }

    //Ground Collision Handling
    private void OnCollisionEnter(Collision collision)
    {
        colliding.Add(collision.collider);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.impulse.magnitude > float.Epsilon)
        {
            if (!colliding.Contains(collision.collider))
            {
                colliding.Add(collision.collider);
            }

            //Record ground telemetry
            for (int i = 0; i < collision.contacts.Length; i++)
            {
                if (Vector3.Dot(Vector3.up, collision.contacts[i].normal) > Vector3.Dot(Vector3.up, groundNormal))
                {
                    groundNormal = collision.contacts[i].normal;
                    groundCollider = collision.collider;
                    groundContactPoint = collision.contacts[i].point;
                    groundRigidbody = collision.rigidbody;
                    if (groundRigidbody != null && groundVelocity == Vector3.zero)
                    {
                        groundVelocity = groundRigidbody.GetPointVelocity(groundContactPoint);
                    }
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        colliding.Remove(collision.collider);
        if (colliding.Count == 0)
        {
            groundVelocity = Vector3.zero;
        }
    }

    private void OnEnable()
    {
        CachedInputManager.OnMouseHold += OnMousePressHandler;
        CachedInputManager.OnMouseRelease += OnMouseReleaseHandler;
    }

    private void OnDisable()
    {
        if (CachedInputManager != null)
        {
            CachedInputManager.OnMouseHold -= OnMousePressHandler;
            CachedInputManager.OnMouseRelease -= OnMouseReleaseHandler;
        }
    }

    // Handlers.
    private void OnMousePressHandler(Vector2 delta, Vector2 deltaAnchor)
    {
        if (IsInputEnabled == true)
        {
            InputMoveVector = new Vector3(-deltaAnchor.x, Constants.DEFAULT_VALUE, -deltaAnchor.y);
            CanMove = true;
        }
    }

    private void OnMouseReleaseHandler()
    {
        InputMoveVector = Vector3.zero;
        CanMove = false;
    }
}