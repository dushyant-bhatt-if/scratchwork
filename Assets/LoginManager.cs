using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class LoginManager : MonoBehaviour
{

    public static LoginManager ins;
    [Header("==Login Fields==")]
    public InputField PasswordField;

    [SerializeField]
    public TMP_InputField _PinField;
    [SerializeField]
    private TMP_InputField _PinField2;
    [Header("== SignUp Fields==")]
    [SerializeField] public InputField MemberCodeField;
    [SerializeField] public InputField NewPasswordField;
    [SerializeField] public InputField ConfirmPasswordField;

    [SerializeField] public Dropdown DateField;
    [SerializeField] public Dropdown MonthField;
    [SerializeField] public Dropdown YearField;
   // [SerializeField] public TMP_InputField DoB_Text;



    private void Awake()
    {
        if (ins == null)
            ins = this;
    }
    // Start is called before the first frame update 
    void Start()
    {
        clr = false;
        _PinField.DeactivateInputField();
        _PinField.ActivateInputField();
      //  _PinField.text = transform.GetComponent<Firebasedata>().GenerateCode();
        //PasswordField.text = transform.GetComponent<Firebasedata>().GenerateCode();

    }


    public void checkPin()
    {
        string checkCode = _PinField.text;
        screen = UImanager.ins.loginScreen1.transform;
        StartCoroutine(apiManager.ins.checkMembercode());

        //••••
        /*bool isValid = false;
        for (int i = 0; i < transform.GetComponent<Firebasedata>().CurrentUser.Count; i++)
        {
            if (checkCode == transform.GetComponent<Firebasedata>().CurrentUser[i].Membercode)
            {
                isValid = true;
                Debug.Log("yes code is  avaialble .." + checkCode);
            }
        }*/
        if(!apiManager.ins.isValid)
        {
            showToast("Please check entered Code!", 3);
            Handheld.Vibrate();
            shakeDuration = 2;
        }
        else if (checkCode.Length < 3)
        { showToast("Insert 4 digit code!", 3);
            Handheld.Vibrate();
            shakeDuration = 2; 
        }
        else
        {
            UImanager.ins.loginScreen1.SetActive(false);
            UImanager.ins.loginScreen2.SetActive(true);
        }
    }
    public void ClickOnLogin()
    {
        string checkCode = _PinField.text;
        string Passcode = PasswordField.text;
        screen = UImanager.ins.loginScreen2.transform;

        for(int i=0;i< transform.GetComponent<Firebasedata>().CurrentUser.Count;i++)
        {
            if(transform.GetComponent<Firebasedata>().CurrentUser[i].Membercode == checkCode
                &&
                transform.GetComponent<Firebasedata>().CurrentUser[i].PasswordField == Passcode)
            {
                clr = true;
                showToast("Login success!", 2);
                UImanager.ins.GetOnloginSuccess(checkCode);
                break;
            }
            else
            {
                showToast("Please check login details!", 3);
                Handheld.Vibrate();
                shakeDuration = 2;
            }
        }
    }

    public void ClickOnSignUp()
    {
        string _mCode = MemberCodeField.text;
        if (_mCode == "")
        {
            showToast("Please enter unique code first!", 3);
            Handheld.Vibrate();
            shakeDuration = 2;
            return;
        }
            string _password = NewPasswordField.text;
        string _cPassword = ConfirmPasswordField.text;
        screen = transform.GetComponent<UImanager>().SignUpScreen.transform;
        bool isValid = false;
        for (int i = 0; i < transform.GetComponent<Firebasedata>().CurrentUser.Count; i++)
        {
            if (_mCode == transform.GetComponent<Firebasedata>().CurrentUser[i].Membercode)
            {
                isValid = true;
                Debug.Log("yes code is already avaialble .." + _mCode);
            } 
        }
      
        if(!isValid)
        { 
                if (_password != "")
                {
                    if (_password == _cPassword)
                    {
                        transform.GetComponent<Firebasedata>().AddUser();
                    }
                    else
                    {
                        showToast("Please check confirm password!", 3);
                        Handheld.Vibrate();
                        shakeDuration = 2;
                    }
                }
                else
                {
                    showToast("Password can't be empty!", 3);
                    Handheld.Vibrate();
                    shakeDuration = 2;
                };

            }
            else
            {
                showToast("Please use unique code!", 3);
                Handheld.Vibrate();
                shakeDuration = 2;

            }         
    }

    public Transform screen;

    // Desired duration of the shake effect
    public float shakeDuration = 0f;

    // A measure of magnitude for the shake. Tweak based on your preference
    private float shakeMagnitude = 1.1f;

    // A measure of how quickly the shake effect should evaporate
    private float dampingSpeed = 1.0f;

    // The initial position of the GameObject
    Vector3 initialPosition;
    // Update is called once per frame
    void Update()
    {
        if (shakeDuration > 0)
        {
            screen.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            screen.localPosition = initialPosition;
        }
        _PinField.textComponent.GetComponent<RectTransform>().localPosition = Vector2.zero;
    }

    public Text txt;
   public void showToast(string text,
        int duration)
    {
        StartCoroutine(showToastCOR(text, duration));
    }
    public bool clr;
    private IEnumerator showToastCOR(string text,
        int duration)
    {
        Color orginalColor = Color.red;
        if (clr)
        { orginalColor = Color.green; }
        else
        { orginalColor = Color.red; }

        txt.text = text;
        txt.enabled = true;
        //Fade in
        yield return fadeInAndOut(txt, true, 0.5f);

        //Wait for the duration
        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            yield return null;
        }

        //Fade out
        yield return fadeInAndOut(txt, false, 0.5f);

        txt.enabled = false;
        txt.color = orginalColor;
        clr = false;

    }

    IEnumerator fadeInAndOut(Text targetText, bool fadeIn, float duration)
    {
        //Set Values depending on if fadeIn or fadeOut
        float a, b;
        if (fadeIn)
        {
            a = 0f;
            b = 1f;
        }
        else
        {
            a = 1f;
            b = 0f;
        }
       

        Color currentColor = Color.red;

        if (clr)
        { currentColor = Color.green; }
        else
        { currentColor = Color.red; }
        float counter = 0f;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(a, b, counter / duration);

            targetText.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            yield return null;
        }
    }

    public void OnBackKeyClicked()
    {

        _PinField.text = "";
        PasswordField.text = "";
        MemberCodeField.text= NewPasswordField.text = ConfirmPasswordField.text = "";
        UImanager.ins.loginScreen1.SetActive(true);
        UImanager.ins.loginScreen2.SetActive(false);
    }

    public void OnPinAdding()
    {
        _PinField.textComponent.GetComponent<RectTransform>().localPosition = Vector2.zero;
    }

    public void dummy_PIN()
    {
        string a  = "•";
        string b = "";
        for(int i = 0; i < _PinField.text.Length; i++) {
            b += a;
        }
        _PinField2.text = b;
       
    }
    
}
