using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LynxStd
{
    [CreateAssetMenu(menuName = "Single Instances/Player References", fileName = "Player References")]
    public class PlayerReferences : ScriptableObject
    {
        public IntVariable curAmmo;
        public IntVariable curCarrying;
        public IntVariable health;
        public FloatVariable targetSpread;

    }
}
