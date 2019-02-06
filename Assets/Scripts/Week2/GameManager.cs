using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week2
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager GM;

        private void Awake()
        {
            GM = this;
        }

        // Update is called once per frame
        void Update()
        {
            _checkInput();
        }

        private void _checkInput()
        {
            if (Input.GetMouseButtonDown(0))
            {

            }
        }
    }
}

