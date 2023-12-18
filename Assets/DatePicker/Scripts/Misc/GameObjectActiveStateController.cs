using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CustomDatePicker
{
    public class GameObjectActiveStateController : MonoBehaviour
    {
        [SerializeField] private GameObject target;
        public bool isSelf = true;
        private void Start()
        {
            if (isSelf) target = gameObject;
        }
        public void ChangeState(bool state)
        {
            target.SetActive(state);
        }

    }
}
