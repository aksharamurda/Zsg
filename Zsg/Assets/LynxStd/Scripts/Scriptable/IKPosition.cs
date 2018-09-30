﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LynxStd
{
    [CreateAssetMenu(menuName = "Weapons/IKPosition", fileName = "IKPosition")]
    public class IKPosition : ScriptableObject
    {
        public Vector3 pos;
        public Vector3 rot;
    }
}
