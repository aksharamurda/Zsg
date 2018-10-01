using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LynxStd.UI
{
    public class UIElement : MonoBehaviour
    {
        UIUpdater uiUpdater;

        private void Awake()
        {
            uiUpdater = UIUpdater.singleton;

            if (uiUpdater != null)
                uiUpdater.elements.Add(this);
        }

        public virtual void Init()
        {

        }

        public virtual void Tick(float delta)
        {

        }
    }
}
