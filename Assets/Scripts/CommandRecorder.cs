using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpringTime
{
    public class CommandRecorder : MonoBehaviour
    {
        public static CommandRecorder CR;

        public GameObject PlayerShadow;
        public GameObject CurCommandContainer;
        public GameObject PreCommandContainer;
        public GameObject CommandVisualPrefab;

        private List<CommandType> _prevCmdList;
        private List<float> _prevCmdTimesList;

        private void Awake()
        {
            if (CR == null) CR = this;
            else if (CR != this) Destroy(gameObject);
            DontDestroyOnLoad(gameObject);

            _prevCmdList = new List<CommandType>();
            _prevCmdTimesList = new List<float>();
        }


        public void AddCommand(CommandType cmd, float time)
        {
            _prevCmdList.Add(cmd);
            _prevCmdTimesList.Add(time);
        }

        private void _generateLastCommandListVisual()
        {
            if (_prevCmdList.Count <= 0) return;
            // If there is cmd, then generate visuals;
            for (int i = 0; i < _prevCmdList.Count; i++)
            {
                GameObject go = Instantiate(CommandVisualPrefab);
                go.transform.SetParent(PreCommandContainer.transform, false);
                go.GetComponent<VisualCommandSetup>().Setup(_prevCmdList[i], _prevCmdTimesList[i]);
            }
            // After Generation, reinit both list
            _prevCmdList = new List<CommandType>();
            _prevCmdTimesList = new List<float>();
        }

        // This function is called each time player replay a level
        private void _onNewLevelLoaded(Scene scene, LoadSceneMode mode)
        {
            PreCommandContainer = GameObject.FindWithTag("PreCommand");
            CurCommandContainer = GameObject.FindWithTag("CurCommand");
            _generateLastCommandListVisual();
        }


        void OnEnable()
        {
            SceneManager.sceneLoaded += _onNewLevelLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= _onNewLevelLoaded;
        }
    }
}

