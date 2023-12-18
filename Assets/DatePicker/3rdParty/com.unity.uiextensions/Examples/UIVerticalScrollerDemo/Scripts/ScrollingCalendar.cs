
using System;
using CustomDatePicker;
using TMPro;

namespace UnityEngine.UI.Extensions.Examples
{
    public class ScrollingCalendar : MonoBehaviour
    {
        public RectTransform monthsScrollingPanel;
        public RectTransform yearsScrollingPanel;
        public RectTransform daysScrollingPanel;

        public ScrollRect monthsScrollRect;
        public ScrollRect yearsScrollRect;
        public ScrollRect daysScrollRect;

        public GameObject yearsButtonPrefab;
        public GameObject monthsButtonPrefab;
        public GameObject daysButtonPrefab;

        private GameObject[] monthsButtons;
        private GameObject[] yearsButtons;
        private GameObject[] daysButtons;

        public RectTransform monthCenter;
        public RectTransform yearsCenter;
        public RectTransform daysCenter;

        public int formatType = 1;

        UIVerticalScroller yearsVerticalScroller;
        UIVerticalScroller monthsVerticalScroller;
        UIVerticalScroller daysVerticalScroller;


        public TextMeshProUGUI dateText;
        private DateTime SelectedDateTime;
        private bool _vibrationInitalized;

        private int daysSet;
        private int monthsSet;
        private int yearsSet;

        private void InitializeYears()
        {
            int currentYear = int.Parse(System.DateTime.Now.ToString("yyyy"))+10;

            int[] arrayYears = new int[currentYear + 1 - 1950];

            yearsButtons = new GameObject[arrayYears.Length];

            for (int i = 0; i < arrayYears.Length; i++)
            {
                arrayYears[i] = 1950 + i;

                GameObject clone = Instantiate(yearsButtonPrefab, yearsScrollingPanel);
                clone.transform.localScale = new Vector3(1, 1, 1);
                clone.GetComponentInChildren<Text>().text = "" + arrayYears[i];
                clone.name = "Year_" + arrayYears[i];
                clone.AddComponent<CanvasGroup>();
                yearsButtons[i] = clone;

            }

        }

        //Initialize Months
        private void InitializeMonths()
        {
            int[] months = new int[12];

            monthsButtons = new GameObject[months.Length];
            for (int i = 0; i < months.Length; i++)
            {
                string month = "";
                months[i] = i;
                month = GetMonth(i);
                GameObject clone = Instantiate(monthsButtonPrefab, monthsScrollingPanel);
                clone.transform.localScale = new Vector3(1, 1, 1);

               

                clone.GetComponentInChildren<Text>().text = month;
                clone.name = "Month_" + months[i];
                clone.AddComponent<CanvasGroup>();
                monthsButtons[i] = clone;
            }
        }

        private string GetMonth(int i)
        {
            switch (i)
            {
                case 0:
                    return "Jan";
                case 1:
                    return "Feb";
                case 2:
                    return "March";
                case 3:
                    return "April";
                case 4:
                    return "May";
                case 5:
                    return "June";
                case 6:
                    return "July";
                case 7:
                    return "Aug";
                case 8:
                    return "Sep";
                case 9:
                    return "Oct";
                case 10:
                    return "Nov";
                case 11:
                    return "Dec";
                default:
                  return "";
            }
        }

        private void InitializeDays()
        {
            int[] days = new int[31];
            daysButtons = new GameObject[days.Length];

            for (var i = 0; i < days.Length; i++)
            {
                days[i] = i + 1;
                GameObject clone = Instantiate(daysButtonPrefab, daysScrollingPanel);
                clone.GetComponentInChildren<Text>().text = "" + days[i];
                clone.name = "Day_" + days[i];
                clone.AddComponent<CanvasGroup>();
                daysButtons[i] = clone;
            }
        }

        // Use this for initialization
        public void Awake()
        {
            InitializeYears();
            InitializeMonths();
            InitializeDays();

            //Yes Unity complains about this but it doesn't matter in this case.
            monthsVerticalScroller = new UIVerticalScroller(monthCenter, monthCenter, monthsScrollRect, monthsButtons);
            yearsVerticalScroller = new UIVerticalScroller(yearsCenter, yearsCenter, yearsScrollRect, yearsButtons);
            daysVerticalScroller = new UIVerticalScroller(daysCenter, daysCenter, daysScrollRect, daysButtons);


            monthsVerticalScroller.Start();
            yearsVerticalScroller.Start();
            daysVerticalScroller.Start();

            daysSet = DateTime.Today.Day-1;
            monthsSet = DateTime.Today.Month-1;
            yearsSet = DateTime.Today.Year-1950;
            SetDate();
        }

        public void SetDate()
        {
            daysVerticalScroller.SnapToElement(daysSet);
            monthsVerticalScroller.SnapToElement(monthsSet);
            yearsVerticalScroller.SnapToElement(yearsSet);
        }


        void Update()
        {
            monthsVerticalScroller.Update();
            yearsVerticalScroller.Update();
            daysVerticalScroller.Update();

            UpdateElementColor(monthsVerticalScroller);
            UpdateElementColor(yearsVerticalScroller);
            UpdateElementColor(daysVerticalScroller);

            int day = daysVerticalScroller.focusedElementIndex + 1;
            int month = monthsVerticalScroller.focusedElementIndex + 1;
            int year = int.Parse(yearsVerticalScroller.result);

            DateTime dateTime = new DateTime(year, month, day);

            if (_vibrationInitalized && !dateTime.Equals(SelectedDateTime))
            {
                Debug.Log("Date Changed");
                GetComponent<AudioSource>()?.Play();
            }
            SelectedDateTime = dateTime;
            if (formatType==1)
            {
                dateText.text = SelectedDateTime.ToString("MM-dd-yyyy");
            }
            else if(formatType == 2)
            {
                dateText.text =GetMonth(SelectedDateTime.Month-1)+","+ SelectedDateTime.Year;
                CalendarController.calendarInstance.MonthAndYearSelect(dateText.text);
            }
            _vibrationInitalized = true;
        }

        void UpdateElementColor(UIVerticalScroller scroller)
        {
            foreach (var element in scroller._arrayOfElements)
            {
                element.GetComponentInChildren<Text>().color=Color.gray;
            }

            scroller._arrayOfElements[scroller.focusedElementIndex].GetComponentInChildren<Text>().color=Color.black;

        }

    }
}