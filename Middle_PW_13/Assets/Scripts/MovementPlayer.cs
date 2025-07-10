using Fusion;
using Fusion.Addons.Physics;
using Unity.Cinemachine;
using UnityEngine;

public class MovementPlayer : NetworkBehaviour
{
    public Camera Camera;

    private InputData inputData;

    [SerializeField] private float speed;

    [SerializeField] private float playerRadius;

    [SerializeField] private LayerMask collisionMask;

    private CinemachineCamera freeLookCamera;

    private Rigidbody rb;

    //private Animator animator;

    public override void Spawned()
    {
        if (HasStateAuthority)
        {
            inputData = GetComponent<InputData>();

            Camera = Camera.main;

            freeLookCamera = FindFirstObjectByType<CinemachineCamera>();

            freeLookCamera.Follow = transform;

            rb = GetComponent<Rigidbody>();
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (!HasStateAuthority) return;

        // �������� ����������� ������
        Vector3 cameraForward = freeLookCamera.transform.forward;
        Vector3 cameraRight = freeLookCamera.transform.right;

        // ���������� ������������ ������������ (������ ������ �����/����)
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // ������������ ������ �� ����������� ������, ����� �� ��������� ������� ������
        //Vector3 dir = new Vector3(cameraForward.x, 0, cameraForward.z);
        //transform.rotation = Quaternion.LookRotation(dir);

        // ��������� ����������� �������� ������������ ������
        Vector3 moveDirection = (cameraForward * inputData.inputVector.y + cameraRight * inputData.inputVector.x).normalized;

        // ����������� �� ���� ��������
        transform.LookAt(transform.position + moveDirection);

        //if (isAttackBegin)
        //{
        //    animator.SetBool("isAttack", true);
        //}
        //else
        //{
        //    animator.SetBool("isAttack", false);
        //}

        if (inputData.inputVector != Vector2.zero)
        {
            // ����������� ��������� � ����������� ������
            //rb.MovePosition(rb.position + moveDirection * speed * Runner.DeltaTime);

            var newPosition = transform.position + moveDirection * speed * Runner.DeltaTime;

            if (!Physics.CheckSphere(newPosition, playerRadius, collisionMask))
            {
                transform.position = newPosition;
            }
            else
            {
                Debug.Log("������������!");
            }

            //transform.position = newPosition;

            //var velocity = moveDirection * speed;
            // ��������� ��������
            //rb.linearVelocity += Vector3.Lerp(rb.linearVelocity, velocity, 0.8f * Runner.DeltaTime);

            //animator.SetBool("isWalk", true);

            // ������� �� ���
            //if (isSprint)
            //{
            //    animator.SetBool("isRun", true);
            //}
            //else
            //{
            //    animator.SetBool("isRun", false);
            //}
        }
        else
        {
            //animator.SetBool("isWalk", false);
        }
    }
}
