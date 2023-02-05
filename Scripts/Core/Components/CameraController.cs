using GeekBox.Scripts.Generic;
using UnityEngine;

public class CameraController : SingletonBase<CameraController>
{
    #region Fields

    [SerializeField]
    private Camera currentCamera;
    [SerializeField]
    private float dampMoveTimeS = 0.1f;

    #endregion

    #region Propeties

    public Camera CurrentCamera {
        get => currentCamera;
    }

    private PlayerCharacterController Target { get; set; }
    private Vector3 XZMultiplier { get; set; } = Vector3.right + Vector3.forward;

    #endregion

    #region Methods

    private void Start()
    {
        Target = FindObjectOfType<PlayerCharacterController>();
    }

    private void OnEnable()
    {
        if(PlayerManager.Instance != null)
        {
            PlayerManager.Instance.OnCharacterSpawned += OnCharacterSpawned;
        }
    }
    private void OnDisable()
    {
        if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.OnCharacterSpawned -= OnCharacterSpawned;
        }
    }

    private void Update()
    {
        //transform.position = new Vector3(Target.transform.position.x, transform.position.y, Target.transform.position.z);

        // != null
        if (Target)
        {
            Vector3 targetXZ = Target.transform.position.Mul(XZMultiplier);
            Vector3 cameraXZ = transform.position.Mul(XZMultiplier);

            Vector3 delta = targetXZ - cameraXZ;
            Vector3 destination = transform.position + delta;

            Vector3 velocity = new Vector3();
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampMoveTimeS);
        }
    }

    private void OnCharacterSpawned(PlayerCharacterController player)
    {
        Target = player;
    }

    #endregion

    #region Enums



    #endregion
}
