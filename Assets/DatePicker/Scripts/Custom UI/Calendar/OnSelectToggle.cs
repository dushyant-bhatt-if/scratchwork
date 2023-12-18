using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CustomDatePicker
{
    public class OnSelectToggle : MonoBehaviour
    {
        [SerializeField] private GameObject target;

        public bool IsSelf = true;

        void Awake()
        {
            if (IsSelf) target = gameObject;
        }

        public void Toggle()
        {
            target.SetActive(!target.activeSelf);
        }


    }
}
