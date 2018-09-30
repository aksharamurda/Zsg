using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LynxStd
{
    [System.Serializable]
    public class WeaponManager
    {
        public string mainWeaponID;
        public string secondWeaponID;

        public RuntimeWeapon m_Weapon;
        public RuntimeWeapon s_Weapon;
    }
}
