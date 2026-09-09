using UnityEngine;

public class WeaponController : KienMonoBehaviour
{
    [SerializeField] private int baseWeapon;
    [SerializeField] private GameObject bombPrefab;
    [Header("Weapons")]
    [SerializeField] private WeaponBase[] weapons;
    private ZombieDetector zombieDetector;

    private WeaponBase currentWeapon;
    private int currentWeaponIndex;
    [SerializeField] private float interval = 1;
    private float timer;
    protected override void Awake()
    {
        base.Awake();
        zombieDetector = GetComponent<ZombieDetector>();
    }
    protected override void Start()
    {
        EquipWeapon(baseWeapon);
    }

    public void Shoot()
    {
        if (currentWeapon == null)
            return;

        currentWeapon.Shoot();
    }
    void Update()
    {
        if (zombieDetector.NearestZombie != null)
        {
            Shoot();
        }
        if (Time.time > timer + interval)
        {
            timer = Time.time;
            ThrowBomb(zombieDetector.FindBestBombPosition());
        }

    }
    public void SwitchWeapon()
    {
        int nextIndex = (currentWeaponIndex + 1) % weapons.Length;

        EquipWeapon(nextIndex);
    }

    private void EquipWeapon(int index)
    {
        if (index < 0 || index >= weapons.Length)
            return;

        // Tắt súng hiện tại
        if (currentWeapon != null)
        {
            currentWeapon.gameObject.SetActive(false);
        }

        // Đổi súng
        currentWeaponIndex = index;
        currentWeapon = weapons[index];

        // Bật súng mới
        currentWeapon.gameObject.SetActive(true);
    }
    public void ThrowBomb(Vector3 targetPosition)
    {
        Vector3 throwPoint = transform.position + Vector3.up;
        GameObject bomb = ObjectPool.Instance.GetObject(
            bombPrefab,
            throwPoint + Vector3.up,
            Quaternion.identity
        );
        bomb.GetComponent<BombController>().ResetBomb();
        Rigidbody rb = bomb.GetComponent<Rigidbody>();

        Vector3 velocity = CalculateThrowVelocity(
            throwPoint,
            targetPosition,
            .5f
        );

        rb.linearVelocity = velocity;
    }
    private Vector3 CalculateThrowVelocity(
        Vector3 start,
        Vector3 target,
        float flightTime)
    {
        Vector3 distance = target - start;

        Vector3 velocity = new Vector3(
            distance.x / flightTime,
            0f,
            distance.z / flightTime
        );

        velocity.y =
            (distance.y - 0.5f * Physics.gravity.y * flightTime * flightTime)
            / flightTime;

        return velocity;
    }
}