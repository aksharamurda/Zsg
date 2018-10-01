using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LynxStd
{
    public class Character : MonoBehaviour
    {

        public string outfitId;

        public bool isFemale;
        public SkinnedMeshRenderer bodyRenderer;

        public void LoadCharacter(ResourcesManager r)
        {
            MeshContainer m = r.GetMesh(outfitId);
            LoadMeshContainer(m);
        }

        public void LoadMeshContainer(MeshContainer m)
        {
            bodyRenderer.sharedMesh = (isFemale)? m.f_Mesh : m.m_Mesh;
            bodyRenderer.material = m.material;
        }
    }
}
