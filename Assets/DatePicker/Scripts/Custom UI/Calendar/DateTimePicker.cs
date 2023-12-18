using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace CustomDatePicker
{
    public class DateTimePicker : MonoBehaviour
    {
        public TMP_InputField timeInputField;
        public TextMeshProUGUI timeLabel;
        [SerializeField] Color activeTextColor;
        private int previousLength = 0;
        private string meridiem = "AM";
        private string validTime = "";
        private Color activeColor = Color.green;
        private int hours = 0;
        private int minutes = 0;
        // Start is called before the first frame update
        void Start()
        {

        }

        private void OnEnable()
        {
            timeLabel.color = activeTextColor;
        }
        private void OnDisable()
        {
            timeLabel.color = Color.black;
        }
        // Update is called once per frame
        void Update()
        {

        }
        public void onValueChange(string time)
        {
            string pattern = "^(([0-9]{0,2}):?([0-9]{0,2})){1}$";
            Regex rgx = new Regex(pattern);
            if (!rgx.IsMatch(time))
            {
                if (time.Length > 0) timeInputField.text = time.Substring(0, time.Length - 1);
            }
            if (time.Length > previousLength && time.Length == 2)
            {
                timeInputField.text = timeInputField.text + ":";
            }
            previousLength = time.Length;
            timeInputField.stringPosition = timeInputField.text.Length;
        }
        public void pickTime(string time)
        {

            validTime = time;
            string[] str_array = { "", "" };
            if (validTime.Contains(":"))
            {
                str_array = validTime.Split(char.Parse(":"));
            }
            else if (validTime.Length > 0)
            {
                if (validTime.Length > 1 && validTime.Length <= 2)
                {
                    str_array[0] = validTime[0] + "";
                    if (int.Parse(validTime) > 11) str_array[1] = validTime[1] + "0";
                    else str_array[0] = validTime.Substring(0, 2) + "";
                }
                else if (validTime.Length > 2 && validTime.Length <= 3)
                {
                    str_array[0] = validTime.Substring(0, 2) + "";
                    if (int.Parse(str_array[0]) > 11)
                    {
                        str_array[0] = validTime[0] + "";
                        str_array[1] = validTime.Substring(1, 2);
                    }
                    else
                    {
                        str_array[1] = validTime[2] + "0";
                    }

                }
                else if (validTime.Length > 3 && validTime.Length <= 4)
                {
                    str_array[0] = validTime.Substring(0, 2) + "";
                    Debug.Log(str_array[0]);
                    str_array[1] = validTime.Substring(2, 2) + "";
                }
                else
                {
                    str_array[0] = validTime[0] + "";
                }
            }
            if (str_array.Length > 0 && str_array[0].Length > 0)
            {
                hours = int.Parse(str_array[0]) % 12;
            }
            if (str_array.Length > 1 && str_array[1].Length > 0)
            {
                if ((int.Parse(str_array[1]) / 60) > 0) hours += 1;
                minutes = int.Parse(str_array[1]) % 60;
            }

            validTime = $"{hours:00}:{minutes:00} {meridiem}";
            timeInputField.text = validTime;
            timeInputField.caretPosition = time.Length + 1;
            if (timeLabel != null) timeLabel.text = validTime;
        }
        public void chooseMeridiem(bool value)
        {

            meridiem = value ? "AM" : "PM";

            validTime = $"{hours:00}:{minutes:00} {meridiem}";
            if (timeLabel != null) timeLabel.text = validTime;
        }





    }
}
