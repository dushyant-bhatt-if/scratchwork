using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace CustomDatePicker
{
    public class DatePickController : MonoBehaviour
    {
        public TextMeshProUGUI date;
        void Start()
        {
            CalendarController.calendarInstance.OnDateSelect += (string dateString) =>
            {
                ChooseDate(dateString);
            };
        }

        public void ChooseDate(string dateString)
        {
            date.text = dateString;
        }

    }
}
