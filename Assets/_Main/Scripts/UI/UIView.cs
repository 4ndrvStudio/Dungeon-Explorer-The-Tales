using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DE
{
    public class UIView : MonoBehaviour
    {

        [SerializeField] private ViewName _viewName = ViewName.None;
        public ViewName ViewName => _viewName;

        public virtual void Show() => gameObject.SetActive(true);

        public virtual void Hide() => gameObject.SetActive(false);
    }

}
