using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUpdater : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    private int _score;

    public int Score
    {
        get => _score;

        private set
        {
            _score = value;
            _scoreText.text = _score.ToString();
        }
    }

    public void IncreaseScore (int score) => Score += Mathf.Abs(score); //i won't pass negative numbers, but doing it just in case
}
