using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SpringTime
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager GM;
        public Text CountdownText;
        public Text ResultText;
        [HideInInspector]
        public float LastFlowerBloomTime;

        private int _objectiveAmount;
        private int _finishedObjectiveAmount;


        public enum GameState
        {
            Prepare,
            Record,
            Start,
            End
        }
        public GameState State;

        private void Awake()
        {
            GM = this;
            State = GameState.Prepare;
            _objectiveAmount = GameObject.FindGameObjectsWithTag("Objective").Length;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("Week1_SpringTime");
            }
        }

        public void OnCalculateScore()
        {
            if (State != GameState.End)
            {
                State = GameState.End;
                StartCoroutine(_calculating(3f));
            }
        }

        IEnumerator _calculating(float time)
        {
            yield return new WaitForSeconds(2f);
            float elapsedTime = 0f;
            float targetPercentage = 100f * _finishedObjectiveAmount / _objectiveAmount;

            float curPercentage = 0f;
            while (elapsedTime < time)
            {
                elapsedTime += Time.deltaTime;
                curPercentage = Mathf.Lerp(0f, targetPercentage, elapsedTime / time);
                ResultText.text = curPercentage.ToString("F0") + "%";
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene("Week1_SpringTime");
        }

        public void AddToObjective(int amount)
        {
            _finishedObjectiveAmount += amount;
        }
    }
}
