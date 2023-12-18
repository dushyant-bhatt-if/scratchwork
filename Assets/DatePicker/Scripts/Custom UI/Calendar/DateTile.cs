using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace CustomDatePicker
{
    public class DateTile : MonoBehaviour
    {
        public GameObject textContainer;
        public bool clickable = false;
        public TextMeshProUGUI text;
        [HideInInspector]
        public Image image;
        public CalendarController calendarController;
        void Start()
        {
            image = textContainer.GetComponent<Image>();
        }
        public void ChangeVisility(bool active)
        {
            if (!active) clickable = false;
            else clickable = true;
            textContainer.SetActive(active);
        }
        public void OnClick()
        {

            if (clickable)
            {
                calendarController.OnDateItemClick(text.text, this);
            }

        }
    }
}
