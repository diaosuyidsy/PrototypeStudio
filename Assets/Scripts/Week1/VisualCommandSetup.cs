using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpringTime
{
    public class VisualCommandSetup : MonoBehaviour
    {
        public Image CommandImage;
        public Text CommandTimeText;

        // Empty command should not be recorded
        public void Setup(CommandType cmd, float time)
        {
            Setup(cmd);
            Setup(time);
        }

        public void Setup(CommandType cmd)
        {
            switch (cmd)
            {
                case CommandType.Down:
                    CommandImage.rectTransform.localEulerAngles = new Vector3(0f, 0f, -90f);
                    break;
                case CommandType.Up:
                    CommandImage.rectTransform.localEulerAngles = new Vector3(0f, 0f, 90f);
                    break;
                case CommandType.Left:
                    CommandImage.rectTransform.localEulerAngles = new Vector3(0f, 0f, 180f);
                    break;
            }
        }

        public void Setup(float time)
        {
            CommandTimeText.text = time.ToString("F1");
        }
    }
}

