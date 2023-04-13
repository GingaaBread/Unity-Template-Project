namespace Service
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine.Assertions;
    using UnityEngine;

    public class InputService : MonoBehaviour
    {

        private static InputService _instance = null;
        public static InputService Instance
        {
            get
            {
                if (_instance == null) throw new System.MissingFieldException("InputService singleton was called without InputService being set up (check that InputService is in the scene)");
                return _instance;
            }
            private set { _instance = value; }
        }

        public InputMaster inputMaster;


        private void Awake()
        {
            Assert.IsNull(_instance, "InputService singleton is already set. (check there is only one InputService in the scene)");
            Instance = this;
        }

    }
}
