using UnityEngine;

public class WeaponController : KienMonoBehaviour
{
    [SerializeField] private int baseWeapon;
    [Header("Weapons")]
    [SerializeField] private WeaponBase[] weapons;
    private ZombieDetector zombieDetector;
    private WeaponBase currentWeapon;
    private int currentWeaponIndex;
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
    
}