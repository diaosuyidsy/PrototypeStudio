using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpringTime
{
    public class PlantPartsControl : MonoBehaviour
    {
        private bool _hasBloomed;
        private Animator _animator;
        SpriteRenderer _sr;
        private float _bloomMinInterval = 0.2f;

        private void Start()
        {
            _sr = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_hasBloomed || !collision.CompareTag("Player")) return;
            _hasBloomed = true;
            //StartCoroutine(_startToBloom(1f));
            _bloom();
            GameManager.GM.AddToObjective(1);
        }

        private void _bloom()
        {
            // If this is the first time a flower has bloomed
            if (Mathf.Approximately(GameManager.GM.LastFlowerBloomTime, 0f))
            {
                StartCoroutine(_startToBloom(1f));
                GameManager.GM.LastFlowerBloomTime = Time.timeSinceLevelLoad + 1f;
            }
            else
            {
                // See the last time a flower has bloomed
                float lastbloomFinishedtime = GameManager.GM.LastFlowerBloomTime;
                // bloomInterval could possibly be a negative number
                float bloomInterval = Time.timeSinceLevelLoad - lastbloomFinishedtime;
                // If this collision is too close to the last collision,
                // Then delay it to the _bloommininterval
                if (bloomInterval < _bloomMinInterval)
                {
                    StartCoroutine(_startToBloom(_bloomMinInterval - bloomInterval));
                    GameManager.GM.LastFlowerBloomTime = Time.timeSinceLevelLoad + _bloomMinInterval - bloomInterval;
                }
                else
                {
                    // If this collision is relatively far away from the last collision, just bloom
                    StartCoroutine(_startToBloom(1f));
                    GameManager.GM.LastFlowerBloomTime = Time.timeSinceLevelLoad + 1f;
                }
            }
        }

        IEnumerator _startToBloom(float time)
        {
            yield return new WaitForSeconds(time);
            _animator.SetTrigger("Bloom");
        }

    }

}
