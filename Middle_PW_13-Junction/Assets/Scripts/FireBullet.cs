using Fusion;
using UnityEngine;
using UnityEngine.EventSystems;

public class FireBullet : NetworkBehaviour
{
    [SerializeField] private GameObject bullet;

    //[SerializeField] private LineRenderer lineRenderer;

    [SerializeField] private float rayDistance = 200f;

    [SerializeField] private float speedBullet = 20f;

    private InputData inputData;

    private MoveBullet moveBullet;

    private RaycastHit hit;

    private float currentTime;

    private float timeToAttack = 1f;

    private bool isTime = false;

    public override void Spawned()
    {
        inputData = GetComponent<InputData>();

        moveBullet = GetComponent<MoveBullet>();

        currentTime = timeToAttack;
    }

    //void Start()
    //{
    //    moveBullet = GetComponent<MoveBullet>();

    //    inputData = GetComponent<InputData>();

    //    currentTime = timeToAttack;
    //}

    public override void FixedUpdateNetwork()
    {
        //RaycastRender();

        if (currentTime > 0f && !isTime)
        {
            currentTime -= Runner.DeltaTime;
        }
        else
        {
            isTime = true;

            currentTime = timeToAttack;
        }

        if (inputData.isAttackBegin && isTime)
        {
            Shoot();

            isTime = false;
        }
    }

    /// <summary>
    /// Создание луча и его визуализация
    /// </summary>
    //void RaycastRender()
    //{
    //    Physics.Raycast(transform.position, transform.forward, out hit, rayDistance);

    //    //lineRenderer.enabled = true;

    //    //lineRenderer.SetPosition(0, transform.position);

    //    //lineRenderer.SetPosition(1, hit.point);
    //}

    /// <summary>
    /// Выстрел в направлении луча
    /// </summary>
    void Shoot()
    {
        Debug.Log("Выстрел.");

        // Создаем экземпляр и разворачиваем его в направлении луча
        //var fire = Instantiate(bullet, transform.position, Quaternion.LookRotation(hit.normal));
        //var fire = Runner.Spawn(bullet, transform.position, Quaternion.LookRotation(hit.normal));

        //Vector3 localDirection = Vector3.forward;

        //Vector3 worldDirection = transform.TransformDirection(localDirection);

        var fire = Runner.Spawn(bullet, transform.position, Quaternion.LookRotation(transform.forward), Object.InputAuthority);

        var direction = (transform.up + transform.right).normalized;

        fire.GetComponent<MoveBullet>().Direction = transform.up.normalized;

        //moveBullet.Direction = worldDirection.normalized;

        //moveBullet.Direction = transform.forward;

        // Двигаем вперед используя локальные координаты
        //fire.GetComponent<Rigidbody>().linearVelocity += transform.up * speedBullet;
        //fire.GetComponent<Rigidbody>().AddForce(transform.forward * speedBullet, ForceMode.Acceleration);
        //transform.position = transform.position + transform.up.normalized * speedBullet * Runner.DeltaTime;
    }
}
