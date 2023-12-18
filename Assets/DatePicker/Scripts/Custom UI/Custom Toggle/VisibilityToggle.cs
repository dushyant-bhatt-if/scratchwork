using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CustomDatePicker
{
    public class VisibilityToggle : MonoBehaviour
    {
        public GameObject target;
        public void Toggle()
        {
            if (target.activeSelf)
            {
                target.SetActive(false);
            }
            else
            {
                target.SetActive(true);
            }
        }
    }
}
