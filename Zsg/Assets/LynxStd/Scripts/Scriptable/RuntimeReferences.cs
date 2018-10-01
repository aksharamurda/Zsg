using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LynxStd
{
    [CreateAssetMenu(menuName = "Single Instances/Runtime References", fileName = "Runtime References")]
    public class RuntimeReferences : ScriptableObject
    {
        public List<RuntimeWeapon> runtimeWeapons = new List<RuntimeWeapon>();

        public void Init()
        {
            runtimeWeapons.Clear();
        }

        public RuntimeWeapon WeaponToRuntimeWeapon(Weapon w)
        {
            RuntimeWeapon rw = new RuntimeWeapon();
            rw.w_actual = w;
            rw.curAmmo = w.magazineAmmo;
            rw.curCarryingAmmo = w.maxAmmo;

            runtimeWeapons.Add(rw);

            return rw;
        }

        public void RemoveRuntimeWeapon(RuntimeWeapon rw)
        {
            if (rw.m_instance)
                Destroy(rw.m_instance);

            if (runtimeWeapons.Contains(rw))
                runtimeWeapons.Remove(rw);
        }
    }

    [System.Serializable]
    public class RuntimeWeapon
    {
        public int curAmmo;
        public int curCarryingAmmo;
        public float lastFired;
        public GameObject m_instance;
        public WeaponHook w_hook;
        public Weapon w_actual;

        public void ShootWeapon()
        {
            w_hook.Shoot();
            curAmmo--;

            Debug.Log("Shoot Weapon Working.");
        }
    }
}
