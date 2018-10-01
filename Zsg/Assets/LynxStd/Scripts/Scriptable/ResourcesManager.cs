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

        public MeshContainer[] meshContainers;
        Dictionary<string, int> meshDictionaries = new Dictionary<string, int>();

        public void Init()
        {
            InitWeapon();
            InitMeshContainer();
        }

        void InitWeapon()
        {
            for (int i = 0; i < allWeapons.Length; i++)
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

        void InitMeshContainer()
        {
            for (int i = 0; i < meshContainers.Length; i++)
            {
                if (meshDictionaries.ContainsKey(meshContainers[i].id))
                {

                }
                else
                {
                    meshDictionaries.Add(meshContainers[i].id, i);
                }
            }
        }

        public MeshContainer GetMesh(string id)
        {
            MeshContainer retVal = null;
            int index = -1;

            if (meshDictionaries.TryGetValue(id, out index))
            {
                retVal = meshContainers[index];
            }

            return retVal;
        }
    }
}
