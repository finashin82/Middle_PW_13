using Fusion;
using UnityEngine;
using UnityEngine.EventSystems;

public class FireBullet : NetworkBehaviour
{
    [SerializeField] private GameObject bullet;

    private InputData inputData;

    private float currentTime;

    private float timeToAttack = 1f;

    private bool isTime = false;

    public override void Spawned()
    {
        inputData = GetComponent<InputData>();

        currentTime = timeToAttack;
    }

    public override void FixedUpdateNetwork()
    {
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
    /// Выстрел по направлению оружия
    /// </summary>
    void Shoot()
    {
        var fire = Runner.Spawn(bullet, transform.position, Quaternion.LookRotation(transform.forward));

        var direction = (transform.up + transform.right).normalized;

        fire.GetComponent<MoveBullet>().Direction = transform.up.normalized;
    }
}
