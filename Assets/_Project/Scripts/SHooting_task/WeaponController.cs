using UnityEngine;
using FPS.Damage;

namespace FPS.Weapons
{
    public class WeaponController : MonoBehaviour
    {
        private enum WeaponType
        {
            Pistol = 1,
            GrenadeLauncher = 2
        }


        [SerializeField] private Camera playerCamera;
        [SerializeField] private GameObject pistolObject;
        [SerializeField] private GameObject grenadeLauncherObject;
        [SerializeField] private Transform pistolFirePoint;
        [SerializeField] private Transform grenadeFirePoint;
        [SerializeField] private GameObject pistolBulletPrefab;
        [SerializeField] private GameObject grenadeBulletPrefab;
        [SerializeField] private Transform targetpoint;


        [Header("Damage")]
        [SerializeField] private float pistolBodyDamage = 10f;
        [SerializeField] private float pistolHeadDamage = 50f;
        [SerializeField] private float grenadeBodyDamage = 30f;

        [Header("Raycast")]
        [SerializeField] private float shootRange = 100f;

        [Header("Recoil")]
        [SerializeField] private Transform weaponHolder;
        [SerializeField] private Vector3 recoilKick = new Vector3(0f, 0f, -0.25f);
        [SerializeField] private Vector3 recoilRotation = new Vector3(10f, -5f, 0f);
        [SerializeField] private float recoilSnappiness = 20f;
        [SerializeField] private float recoilReturnSpeed = 12f;


        private WeaponType currentWeapon = WeaponType.Pistol;
        private Vector3 originalWeaponPosition;
        private Quaternion originalWeaponRotation;

        [SerializeField] private int maxAmmo = 15;
        private int currentAmmo;

        [SerializeField] private TMPro.TMP_Text ammoText;
        [SerializeField] private TMPro.TMP_Text outOfAmmoText;

        private void Start()
        {
            originalWeaponPosition = weaponHolder.localPosition;
            originalWeaponRotation = weaponHolder.localRotation;
            SelectWeapon(WeaponType.Pistol);

            currentAmmo = maxAmmo;
            UpdateAmmoUI();

            if (outOfAmmoText != null)
                outOfAmmoText.gameObject.SetActive(false);

        }

        private void Update()
        {
            WeaponSelectionInput();

            if (Input.GetMouseButtonDown(1))
            {
                Shoot();
            }

            weaponHolder.localPosition = Vector3.Lerp(
            weaponHolder.localPosition,
             originalWeaponPosition,
             Time.deltaTime * recoilReturnSpeed
                );
            weaponHolder.localRotation = Quaternion.Slerp(weaponHolder.localRotation, originalWeaponRotation, Time.deltaTime * recoilReturnSpeed);
        }

        private void WeaponSelectionInput()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SelectWeapon(WeaponType.Pistol);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SelectWeapon(WeaponType.GrenadeLauncher);
            }
        }

        private void SelectWeapon(WeaponType weaponType)
        {
            currentWeapon = weaponType;

            pistolObject.SetActive(currentWeapon == WeaponType.Pistol);
            grenadeLauncherObject.SetActive(currentWeapon == WeaponType.GrenadeLauncher);

            WeaponEvents.WeaponChanged((int)currentWeapon);

            Debug.Log("Selected weapon: " + currentWeapon);
        }

        private void Shoot()
        {
            if (currentAmmo <= 0)
            {
                if (outOfAmmoText != null)
                    outOfAmmoText.gameObject.SetActive(true);

                return;
            }

            currentAmmo--;
            UpdateAmmoUI();

            if (currentAmmo == 0 && outOfAmmoText != null)
                outOfAmmoText.gameObject.SetActive(true);
            WeaponEvents.WeaponFired();
            ApplyRecoil();
            SpawnBulletVisual();
            ShootRaycast();
        }

        private void SpawnBulletVisual()
        {
            if (currentWeapon == WeaponType.Pistol && pistolBulletPrefab != null)
            {
                Vector3 shootDirection = (targetpoint.position - pistolFirePoint.position).normalized;
                GameObject Clone =  Instantiate(pistolBulletPrefab, pistolFirePoint.position, pistolFirePoint.rotation);
                Clone.GetComponent<Rigidbody>().AddForce(shootDirection * 3,ForceMode.Impulse);
            }
            else if (currentWeapon == WeaponType.GrenadeLauncher && grenadeBulletPrefab != null)
            {
                Vector3 shootDirection = (targetpoint.position - grenadeFirePoint.position).normalized;
                GameObject Clone = Instantiate(grenadeBulletPrefab, grenadeFirePoint.position, grenadeFirePoint.rotation);
                Clone.GetComponent<Rigidbody>().AddForce(shootDirection * 20f, ForceMode.Impulse);
            }
        }

        private void ShootRaycast()
        {
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, shootRange))
            {
                Debug.DrawRay(ray.origin, ray.direction * shootRange, Color.red, 1f);

                Hitbox hitbox = hit.collider.GetComponent<Hitbox>();

                if (hitbox == null)
                {
                    Debug.Log("Hit object without hitbox: " + hit.collider.name);
                    return;
                }

                ApplyDamage(hitbox);
            }
        }

        private void ApplyDamage(Hitbox hitbox)
        {
            if (currentWeapon == WeaponType.Pistol)
            {
                if (hitbox.Type == Hitbox.HitboxType.Head)
                {
                    hitbox.Health.TakeDamage(pistolHeadDamage);
                    WeaponEvents.Headshot();
                    Debug.Log("Pistol headshot: 50 damage");
                }
                else
                {
                    hitbox.Health.TakeDamage(pistolBodyDamage);
                    Debug.Log("Pistol body shot: 10 damage");
                }
            }
            else if (currentWeapon == WeaponType.GrenadeLauncher)
            {
                if (hitbox.Type == Hitbox.HitboxType.Head)
                {
                    hitbox.Health.Die();
                    WeaponEvents.Headshot();
                    Debug.Log("Grenade headshot: instant kill");
                }
                else
                {
                    hitbox.Health.TakeDamage(grenadeBodyDamage);
                    Debug.Log("Grenade body shot: 30 damage");
                }
            }
        }

        private void ApplyRecoil()
        {
            weaponHolder.localPosition = originalWeaponPosition + recoilKick;
            weaponHolder.localRotation = originalWeaponRotation * Quaternion.Euler(recoilRotation);
        }
        private void UpdateAmmoUI()
        {
            ammoText.text = "Bullets left: " + currentAmmo;
        }
    }
}


public class Weapon
{
    public string WeaponName;
    public string wWeaponType; //hitscan, projectile
    public int damage;
    public float fireRate;
    public float recoilRate;
    public float critMultiplayer;
}