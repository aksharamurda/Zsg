using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LynxStd
{
    [CreateAssetMenu(menuName = "Characters/Mesh Container", fileName = "Mesh Container")]
    public class MeshContainer : ScriptableObject
    {
        public string id;
        public Mesh m_Mesh; //Male
        public Mesh f_Mesh; //Female
        public Material material;
    }
}
