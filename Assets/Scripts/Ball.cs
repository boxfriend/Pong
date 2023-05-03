using System.Collections;
using UnityEngine;

namespace Boxfriend
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private AudioSource _audio;
        [SerializeField] private Rigidbody2D _body;
        [SerializeField] private float _moveSpeed = 1;
        [SerializeField] private float _moveMultiplier = 0.33f;

        private float _currentMoveSpeed;
        private Vector2 _velocity;

        private void Awake ()
        {
            StartCoroutine(Restart());
        }

        private void OnTriggerEnter2D (Collider2D collision)
        {
            if(collision.TryGetComponent(out ScoreUpdater scoreUpdater))
            {
                scoreUpdater.IncreaseScore(1);
            }
            _audio.Play();
            StartCoroutine(Restart());
        }

        private void OnCollisionEnter2D (Collision2D collision)
        {
            var collisionNormal = collision.GetContact(0).normal;
            var newDirection = Vector2.Reflect(_velocity, collisionNormal);

            if(collision.collider.CompareTag("Paddle"))
                _currentMoveSpeed += _moveSpeed * _moveMultiplier;

            _body.velocity = newDirection.normalized * _currentMoveSpeed;
            _velocity = _body.velocity;
            _audio.Play();
        }

        private IEnumerator Restart()
        {
            _body.velocity = Vector2.zero;
            _body.position = Vector2.zero;
            _currentMoveSpeed = _moveSpeed;

            yield return new WaitForSeconds(2);

            var randomDir = RandomStartAngle() * _currentMoveSpeed;
            randomDir = RotateRandomAngle(randomDir, 10);
            _body.velocity = randomDir;
            _velocity = _body.velocity;
        }

        private Vector2 RandomStartAngle()
        {
            var direction = Random.value > 0.5f ? Vector2.right : Vector2.left;
            return RotateRandomAngle(direction, 45);
        }

        private Vector2 RotateRandomAngle(Vector2 toRotate, float maxAngleDiff)
        {
            var angle = Random.Range(-maxAngleDiff, maxAngleDiff);
            return (Vector2)(Quaternion.Euler(0, 0, angle) * toRotate);
        }
    }
}
