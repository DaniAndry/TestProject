using System;
using TMPro;
using UnityEngine;

public class PointCounter : MonoBehaviour
{
    [SerializeField] private Player _player;
    private TextMeshProUGUI _points;

    private void Start()
    {
        _points = GetComponent<TextMeshProUGUI>();
        _player.PointsChanged += DisplayPoints;
    }

    private void DisplayPoints()
    {
        _points.text = Convert.ToString(_player.Points);
    }

    private void OnDisable()
    {
        _player.PointsChanged -= DisplayPoints;
    }
}
