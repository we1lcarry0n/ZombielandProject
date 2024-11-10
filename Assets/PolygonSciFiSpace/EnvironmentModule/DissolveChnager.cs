using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveChnager : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] _meshrenderers;
    [SerializeField] private float _dissolveChangeSpeed;
    private bool _isHiddden = false;
    [SerializeField] private float _minDissolve = 6.5f;
    [SerializeField] private float _maxDissolve = 12f;
    private float _currentDissolveValue;

    private void Start()
    {
        _currentDissolveValue = _maxDissolve;    
    }

    private void Update()
    {
        if (!_isHiddden && _currentDissolveValue >= _maxDissolve)
        {
            return;
        }
        if (_isHiddden && _currentDissolveValue <= _minDissolve)
        {
            return;
        }

        _currentDissolveValue += _isHiddden ? -_dissolveChangeSpeed * Time.deltaTime : _dissolveChangeSpeed * Time.deltaTime;
        foreach (var renderer in _meshrenderers)
        {
            renderer.material.SetFloat("_CutoffHeight", _currentDissolveValue);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("test");
            _isHiddden = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isHiddden = false;
        }
    }
}
