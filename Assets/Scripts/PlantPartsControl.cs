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

        private void Start()
        {
            _sr = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_hasBloomed || !collision.CompareTag("Player")) return;
            _hasBloomed = true;
            StartCoroutine(_startToBloom(1f));
        }

        IEnumerator _startToBloom(float time)
        {
            yield return new WaitForSeconds(time);
            _animator.SetTrigger("Bloom");
        }

    }

}
