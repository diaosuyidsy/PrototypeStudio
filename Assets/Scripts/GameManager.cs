using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpringTime
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager GM;

        private void Awake()
        {
            GM = this;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("Week1_SpringTime");
            }
        }
    }
}
