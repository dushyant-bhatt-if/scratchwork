using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace CustomDatePicker
{
    public class BaseToggleModifier : MonoBehaviour
    {
        public bool changeImageColor;
        public bool changeTextColor;
        public UnityEvent OnClick;
        protected Toggle toggle;
        protected RectTransform toggleTransform;
        protected Color selectedColor;


        private void Awake()
        {
            toggle = GetComponent<Toggle>();
            toggleTransform = GetComponent<RectTransform>();
        }


        void Start()
        {
            selectedColor = Color.black;


            ToggleValueChange();


            toggle.onValueChanged.AddListener((a) =>
            {
                ToggleValueChange();
            });
        }

        public virtual void ToggleValueChange()
        {
            Debug.Log("i am called");
            if (toggle.isOn) OnClick?.Invoke();
            if (changeImageColor)
                toggleTransform.GetComponentInChildren<Image>().color = !toggle.isOn ? Color.white : selectedColor;
            if (changeTextColor)
                toggleTransform.GetComponentInChildren<TextMeshProUGUI>().color = !toggle.isOn ? selectedColor : Color.white;

        }
    }
}
