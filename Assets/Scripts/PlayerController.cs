using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public CharacterController charCon;
    public float moveSpeed;

    public InputActionReference moveAction;

    private Vector3 currentMovement;

    public InputActionReference lookAction;
    private Vector2 rotStore;
    public float lookSpeed;

    public Camera playerCamera;

    public float minViewAngel;
    public float maxViewAngel;

    public InputActionReference jumpAction;
    public float jumpPower;
    public float gravityModifier = 4f;

    public InputActionReference sprintAction;
    public float sprintSpeed;

    public float camZoomNormal;
    public float camZoomOut;
    public float camZoomSpeed;

    public WeaponsController weaponCon;
    public InputActionReference shootAction;

    public InputActionReference reloadAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float yStore = currentMovement.y;

        Vector2 moveInput = moveAction.action.ReadValue<Vector2>();

        Vector3 moveForward = transform.forward * moveInput.y;
        Vector3 moveSides = transform.right * moveInput.x;

        //冲刺
        if (sprintAction.action.IsPressed())
        {
            currentMovement = (moveForward + moveSides) * sprintSpeed;

            if (currentMovement != Vector3.zero)
            {
                playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, camZoomOut, Time.deltaTime * camZoomSpeed);
            }
        }
        else
        {
            //currentMovement是速度，有大小和方向
            currentMovement = (moveForward + moveSides) * moveSpeed;
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, camZoomNormal, Time.deltaTime * camZoomSpeed);
        }

        //重力
        if (charCon.isGrounded)
        {
            yStore = 0f;
        }

        currentMovement.y = yStore + (Physics.gravity.y * Time.deltaTime * gravityModifier);

        //跳跃
        if (jumpAction.action.WasPressedThisFrame() && charCon.isGrounded)
        {
            currentMovement.y = jumpPower;
        }

        charCon.Move(currentMovement * Time.deltaTime);

        //处理视角移动
        Vector2 lookInput = lookAction.action.ReadValue<Vector2>();
        lookInput.y = -lookInput.y;

        rotStore = rotStore + (lookInput * lookSpeed * Time.deltaTime);

        rotStore.y = Mathf.Clamp(rotStore.y, minViewAngel, maxViewAngel);

        transform.rotation = Quaternion.Euler(0f, rotStore.x, 0f);
        playerCamera.transform.localRotation = Quaternion.Euler(rotStore.y, 0f, 0f);

        //开火
        if (shootAction.action.WasPressedThisFrame())
        {
            weaponCon.Shoot();
        }

        if (shootAction.action.IsPressed())
        {
            weaponCon.ShootHeld();
        }

        if (reloadAction.action.WasPressedThisFrame())
        {
            weaponCon.Reload();
        }


    }

}
