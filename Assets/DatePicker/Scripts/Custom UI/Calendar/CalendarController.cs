using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace CustomDatePicker
{
    public class CalendarController : MonoBehaviour
    {
        public GameObject _calendarPanel;
        public GameObject preButton;
        public GameObject nextButton;
        public TextMeshProUGUI monthAndYear;
        public GameObject gridView;
        public GameObject monthAndYearPicker;
        public DateTile _tile;
        public RectTransform layoutPanel;
        public List<DateTile> _dateItems = new List<DateTile>();
        public List<DateTile> _daysItem = new List<DateTile>();
        public TextMeshProUGUI date;
        public UnityAction<string> OnDateSelect;
        public Color onEditingDateTextColor = Color.green;
        private TextMeshProUGUI previousSelectedDateText;
        public float dayFontSize = 65;
        public Color dayFontColor = Color.black;
        public float dateTileFontSize = 65;
        public Color dateTileColor = Color.black;
        public Color currentDateTextColor = Color.green;
        public Color currentDateBackgroundColor = Color.white;
        public Color currentMonthTextColor = Color.gray;
        public Color currentMonthBackgroundColor = Color.white;
        public Color defaultBackgroundColor = Color.grey;
        public Color clickedDateTextColor = Color.black;
        public Color clickedDateBackgroundColor = Color.gray;
        public List<string> days = new List<string> { "S", "M", "T", "W", "T", "F", "S" };
        private List<string> months = new List<string> { "", "Jan", "Feb", "March", "Apr", "May", "June", "July", "Aug", "Sep", "Oct", "Nov", "Dec" };
        private List<string> years = new List<string>();
        private List<GameObject> monthPrefabs = new List<GameObject>();
        private List<GameObject> yearPrefabs = new List<GameObject>();
        const int _totalDateNum = 42;
        private DateTime _dateTime;
        private Image previousClickedImage;
        public static CalendarController calendarInstance;
        DateTime today;
        public int selectedDay, selectedMonth, selectedYear;
        private DateTile currentSelectedTile;
        void Start()
        {
            calendarInstance = this;
            _dateItems.Clear();
            _dateItems.Add(_tile);
            for (int i = 1; i < _totalDateNum + 8; i++)
            {
                DateTile obj = Instantiate(_tile);
                obj.transform.SetParent(gridView.transform, false);
                obj.calendarController = this;
                if (i < 8)
                {
                    if (i <= days.Count)
                    {
                        obj.name = "Tile:" + days[i - 1];
                        obj.text.text = days[i - 1];
                        obj.text.fontSize = dayFontSize;
                        obj.text.color = dayFontColor;
                        obj.text.fontStyle = FontStyles.Bold;
                        _daysItem.Add(obj);
                        obj.ChangeVisility(true);
                        obj.clickable = false;
                        obj.textContainer.GetComponent<Image>().color = Color.clear;
                    }
                    else
                    {
                        _daysItem.Add(obj);
                        obj.clickable = true;
                        obj.ChangeVisility(false);

                    }


                }
                else
                {
                    obj.name = "Tile:" + (i + 1 - 8).ToString();
                    _dateItems.Add(obj);
                }

            }
            _dateTime = DateTime.Now;
            today = DateTime.Today;
            if (date != null) date.text = months[today.Month] + " " + today.Day + "," + today.Year;

            CreateCalendar();
        }
        private void OnEnable()
        {
            calendarInstance = this;


            LoginManager.ins.DateField.captionText.text = today.Day.ToString("00");
            LoginManager.ins.MonthField.captionText.text = months[today.Month].ToString();
            LoginManager.ins.YearField.captionText.text = today.Year.ToString();



            if (date != null) date.color = onEditingDateTextColor;
        }
        private void OnDisable()
        {
            if (date != null) date.color = Color.black;
        }
        // Update is called once per frame

        void CreateCalendar()
        {

            DateTime firstDay = _dateTime.AddDays(-(_dateTime.Day - 1));
            int index = GetDays(firstDay.DayOfWeek);

            int date = 0;
            for (int i = 0; i < _totalDateNum; i++)
            {
                _dateItems[i].ChangeVisility(false);

                if (i > index)
                {

                    DateTime thatDay = firstDay.AddDays(date);
                    if (thatDay.Month == firstDay.Month)
                    {
                        _dateItems[i].ChangeVisility(true);
                        _dateItems[i].text.text = (date + 1).ToString();
                        _dateItems[i].text.fontSize = dateTileFontSize;
                        _dateItems[i].text.color = dateTileColor;

                        if (_dateTime.Month == today.Month && date == today.Day - 1 && _dateTime.Year == today.Year)
                        {
                            _dateItems[i].text.color = currentDateTextColor;
                            _dateItems[i].text.fontStyle = FontStyles.Bold;
                            _dateItems[i].textContainer.GetComponent<Image>().color = currentDateBackgroundColor;
                        }
                        else
                        {
                            _dateItems[i].text.fontStyle = FontStyles.Normal;
                            _dateItems[i].textContainer.GetComponent<Image>().color = Color.clear;
                        }
                        if(date==selectedDay-1&&selectedMonth== _dateTime.Month&&selectedYear== _dateTime.Year)
                        {
                            _dateItems[i].text.color = clickedDateTextColor;
                            _dateItems[i].textContainer.GetComponent<Image>().color = clickedDateBackgroundColor;
                        }
                      


                        date++;
                    }
                }
                else
                {

                }
            }
            //if (this.date != null) this.date.text = months[_dateTime.Month] + " " + _dateTime.Day + "," + _dateTime.Year;
            monthAndYear.text = months[_dateTime.Month] + ", " + _dateTime.Year.ToString();
        }
        int GetDays(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Monday: return 1;
                case DayOfWeek.Tuesday: return 2;
                case DayOfWeek.Wednesday: return 3;
                case DayOfWeek.Thursday: return 4;
                case DayOfWeek.Friday: return 5;
                case DayOfWeek.Saturday: return 6;
                case DayOfWeek.Sunday: return 0;
            }

            return 0;
        }


        protected void button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            // identify which button was clicked and perform necessary actions
        }
        public void showMonthAndYearPicker()
        {
            if (monthAndYearPicker.activeSelf)
            {
                monthAndYearPicker.SetActive(false);
                preButton.SetActive(true);
                nextButton.SetActive(true);
            }
            else
            {
                monthAndYearPicker.SetActive(true);
                preButton.SetActive(false);
                nextButton.SetActive(false);
            }
            if (layoutPanel != null)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(layoutPanel);
            }

        }

        public void YearPrev()
        {
            _dateTime = _dateTime.AddYears(-1);
            CreateCalendar();
        }

        public void YearNext()
        {
            _dateTime = _dateTime.AddYears(1);
            CreateCalendar();
        }

        public void MonthPrev()
        {
            _dateTime = _dateTime.AddMonths(-1);
            CreateCalendar();
        }

        public void MonthNext()
        {
            _dateTime = _dateTime.AddMonths(1);
            CreateCalendar();
        }

        public void ShowCalendar(Text target)
        {
            _calendarPanel.SetActive(true);
            _calendarPanel.transform.position = Input.mousePosition - new Vector3(0, 120, 0);
        }

        public void OnDateItemClick(string day, DateTile dateTile)
        {
            string[] str = this.monthAndYear.text.Split(',');
            selectedDay = int.Parse(day);
            selectedMonth =months.IndexOf(str[0].Replace(" ", ""));
            selectedYear = int.Parse(str[1].Replace(" ",""));
            Debug.Log(today.Day + ".. " + today.Month + "..." + today.Year);
            if (selectedYear > today.Year)
            {
                LoginManager.ins.showToast("Can't Select Future Date!", 3);
                Handheld.Vibrate();
                LoginManager.ins.shakeDuration = 2;
                Debug.LogError("cant able to select future date");
                //if (_calendarPanel != null) _calendarPanel.SetActive(false);

            }
            else if(selectedYear >= today.Year && selectedMonth > today.Month )
            {
                LoginManager.ins.showToast("Can't Select Future Date!", 3);
                Handheld.Vibrate();
                LoginManager.ins.shakeDuration = 2;
                Debug.LogError("cant able to select future date");
            }
            else if (selectedYear >= today.Year && selectedMonth >= today.Month && selectedDay > today.Day)
            {
                LoginManager.ins.showToast("Can't Select Future Date!", 3);
                Handheld.Vibrate();
                LoginManager.ins.shakeDuration = 2;
                Debug.LogError("cant able to select future date");
            }
            else
            {
                Debug.Log(selectedDay + " .. " + str[0] + "..." + selectedYear);
                LoginManager.ins.DateField.captionText.text = selectedDay.ToString("00");
                LoginManager.ins.MonthField.captionText.text = str[0].ToString();
                LoginManager.ins.YearField.captionText.text = selectedYear.ToString();
                if (date != null)
                {
                    date.text = str[0] + " " + selectedDay + ", " + selectedYear;
                    OnDateSelect?.Invoke(date.text);
                }
                if (previousClickedImage != null)
                {
                    previousClickedImage.color = Color.clear;
                    previousSelectedDateText.color = dayFontColor;
                }

                dateTile.text.color = clickedDateTextColor;
                dateTile.textContainer.GetComponent<Image>().color = clickedDateBackgroundColor;
                currentSelectedTile = dateTile;
                previousClickedImage = dateTile.image;

                previousSelectedDateText = dateTile.text;
                if (_calendarPanel != null) _calendarPanel.SetActive(false);
            }           
        }
        public void MonthAndYearSelect(string text)
        {
            string[] str = text.Split(',');
            int monthIndex = months.IndexOf(str[0]);
            int yearValue = Int32.Parse(str[1]);
            _dateTime = new DateTime(yearValue, monthIndex, _dateTime.Day);
            CreateCalendar();
        }
        public void CalendarActiveDeactiveToggle()
        {
            if (_calendarPanel.activeSelf)
            {
                _calendarPanel.SetActive(false);
            }
            else
            {
                _calendarPanel.SetActive(true);
            }
        }


    }
}
