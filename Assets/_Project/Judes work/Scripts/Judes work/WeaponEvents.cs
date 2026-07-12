using System;

namespace FPS.Weapons
{
    public static class WeaponEvents
    {
        public static event Action OnWeaponFired;
        public static event Action OnHeadshot;
        public static event Action<int> OnWeaponChanged;

        public static void WeaponFired()
        {
            OnWeaponFired?.Invoke();
        }

        public static void Headshot()
        {
            OnHeadshot?.Invoke();
        }

        public static void WeaponChanged(int weaponNumber)
        {
            OnWeaponChanged?.Invoke(weaponNumber);
        }
    }
}