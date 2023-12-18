using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace CustomDatePicker
{
    public class GenericPopupController : MonoBehaviour
    {
        public static GenericPopupController Instance;
        [SerializeField] Button button1;
        [SerializeField] Button button2;
        public UnityEvent onButton1;
        public UnityEvent onButton2;
        void Awake()
        {
            Instance = this;
        }
        void Start()
        {
            button1.onClick.AddListener(onButton1.Invoke);
            button2.onClick.AddListener(onButton2.Invoke);

            button1.onClick.AddListener(Hide);
            button2.onClick.AddListener(Hide);
        }

        public void Show()
        {
            PopupManager.Instance.ShowPopup("highlight_item_delete_popup", true);

        }
        public void Hide()
        {
            PopupManager.Instance.HidePopup("highlight_item_delete_popup", true);
        }
    }
}

