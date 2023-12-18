using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
namespace CustomDatePicker
{
    public class PopupManager : MonoBehaviour
    {
        public static PopupManager Instance;
        [SerializeField]
        private GameObject popupContainer;
        [SerializeField]
        public CanvasGroup blurBackground;
        [SerializeField]
        private List<Popup> popupList = new List<Popup>();
        public List<Popup> currentOpenPopup = new List<Popup>();
        public bool openBottonMenu = false;
        public UnityEvent OnAnimComplete;
        private void Awake()
        {
            Instance = this;
            foreach (Popup popup in popupList)
            {
                popup.gameObject.SetActive(true);
            }

        }
        void Start()
        {
            popupContainer.SetActive(true);
            blurBackground.gameObject.SetActive(false);
        }
        public void ShowBottomCircularMenu(bool animate = true)
        {

            string name = "bottom_circular_menu";
            Popup popup = popupList.First(r => r.popupName == name);

            popup.gameObject.SetActive(true);
            if (animate)
            {

                popup.ShowPopup();
                currentOpenPopup.Remove(popup);
            }
            else
            {
                popup.ShowPopupImmediate();
            }
        }
        public void Register(Popup popup)
        {
            if (!popupList.Contains(popup)) popupList.Add(popup);
            popup.gameObject.SetActive(false);
        }
        public void ShowPopup(string name)
        {
            ShowPopup(name, true, true);
        }
        public void ShowPopup(string name, bool animate = false, bool backgroundBlur = true)
        {

            if (backgroundBlur)
            {
                blurBackground.gameObject.SetActive(true);
                blurBackground.alpha = 1;
            }

            Popup popup = popupList.First(r => r.popupName == name);
            popup.gameObject.SetActive(true);
            if (animate)
            {
                popup.ShowPopup();
            }
            else
            {
                currentOpenPopup.Add(popup);
            }
        }
        public void HidePopup(string name, bool animate = false)
        {
            if (popupList.Count == 0) return;
            Popup popup = popupList.First(r => r.popupName == name);

            if (animate)
            {
                popup.HidePopup();
            }
            else
            {
                popup.gameObject.SetActive(false);
                currentOpenPopup.Remove(popup);
                if (currentOpenPopup.Count == 0) blurBackground.gameObject.SetActive(false);
            }

        }
        public void HidePopupWithAnimation()
        {

            if (currentOpenPopup.Count > 0) currentOpenPopup[currentOpenPopup.Count - 1].HidePopup();
            if (currentOpenPopup.Count == 0) blurBackground.gameObject.SetActive(false);
        }
        public void HidePopupImmediate()
        {
            blurBackground.gameObject.SetActive(false);
            if (currentOpenPopup.Count > 0) currentOpenPopup[currentOpenPopup.Count - 1].gameObject.SetActive(false);
        }
    }

}
