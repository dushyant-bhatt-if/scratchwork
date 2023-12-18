using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CustomDatePicker
{
    public class PopupSceneLoader : MonoBehaviour
    {
        [SerializeField] private bool loadPopupScene;
        private void OnEnable()
        {
            if (loadPopupScene && PopupManager.Instance == null)
            {
                SceneManager.LoadSceneAsync("PopupScene", LoadSceneMode.Additive);
            }
        }
        public void ShowPopup(string name)
        {
            PopupManager.Instance?.ShowPopup(name);
        }
        public void ShowPopupWithAnimation(string name)
        {
            PopupManager.Instance?.ShowPopup(name, true);
        }
        public void HidePopup(string name)
        {
            PopupManager.Instance?.HidePopup(name);
        }


    }

}
