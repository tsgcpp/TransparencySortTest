using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TextVisualization
{
    public class MaterialText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        [SerializeField] private Material _material;

        private Color _color;
        private int _renderQueue;

        private void Start()
        {
            Apply();
        }

        private void LateUpdate()
        {
            if (ShouldSkip())
            {
                return;
            }

            Apply();
        }

        private void OnValidate()
        {
            Apply();
        }

        private void Apply()
        {
            _color = _material.color;
            _renderQueue = _material.renderQueue;

            _text.SetText(_renderQueue.ToString());
            _text.color = _color;
        }

        private bool ShouldSkip()
        {
            if (!_color.Equals(_material.color))
            {
                return false;
            }

            if (_renderQueue != _material.renderQueue)
            {
                return false;
            }

            return true;
        }
    }
}
