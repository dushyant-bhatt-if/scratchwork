using UnityEngine;
namespace CustomDatePicker
{
    public class DontDestroyStatic : MonoBehaviour
    {
        public static DontDestroyStatic Instance;
        private void Awake()
        {
            //if (Instance == null)
            //{
            //    Instance = this;
            //    DontDestroyOnLoad(gameObject);
            //}
            //else
            //{
            //    Destroy(gameObject);
            //}
            DontDestroyOnLoad(gameObject);

        }
        void Start()
        {



        }



    }
}
