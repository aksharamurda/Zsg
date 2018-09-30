using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LynxStd
{
    [CreateAssetMenu(menuName = "Single Instances/Resources", fileName = "Resources")]
    public class ResourcesManager : ScriptableObject
    {
        public RuntimeReferences runtime;
        public Weapon[] allWeapons;
        Dictionary<string, int> weaponDictionaries = new Dictionary<string, int>();

        public void Init()
        {
            for (int i=0;i< allWeapons.Length;i++)
            {
                if (weaponDictionaries.ContainsKey(allWeapons[i].id))
                {

                }
                else
                {
                    weaponDictionaries.Add(allWeapons[i].id, i);
                }
            }
        }

        public Weapon GetWeapon(string id)
        {
            Weapon retVal = null;
            int index = -1;

            if (weaponDictionaries.TryGetValue(id, out index))
            {
                retVal = allWeapons[index];
            }

            return retVal;
        }
    }
}
