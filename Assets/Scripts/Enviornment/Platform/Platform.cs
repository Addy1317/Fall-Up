#region Summary
/// <summary>
///  
/// </summary>
#endregion
using SS.FallUp.Mangers;
using SS.FallUp.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.FallUp.Platform
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 2f;
        [SerializeField] private float _boundY = 6f;

        [SerializeField] private bool _movingPlatformLeft;
        [SerializeField] private bool _movingPlatformRight;
        [SerializeField] private bool _isBreakable;
        [SerializeField] private bool _isSpike;
        [SerializeField] private bool _isPlatform;

        private Animator animator;
        private void Awake()
        {
            if (_isBreakable)
            {
                animator = GetComponent<Animator>();
            }
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            MovePlatform();
        }

        private void MovePlatform()
        {
            Vector2 temp = transform.position;
            temp.y += _moveSpeed * Time.deltaTime;
            transform.position = temp;

            if (temp.y >= _boundY)
            {
                gameObject.SetActive(false);
            }
        }

        private void BreakableDeactivate()
        {
            Invoke("DeactivateGameObject", 0.3f);
        }

        private void DeactivateGameObject()
        {
            SoundManager.instance.IceBreakClip();
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D target)
        {
            if(target.tag == "Player")
            {
                if(_isSpike)
                {
                    target.transform.position = new Vector2(1000f, 1000f);
                    GameManager.instance.RestartGame();
                    SoundManager.instance.GameOverSound();
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D target)
        {
            if(target.gameObject.tag == "Player")
            {
                if(_isBreakable)
                {
                    animator.Play("Break");
                    SoundManager.instance.LandSound();
                }

                if(_isPlatform)
                {
                    SoundManager.instance.LandSound();
                }
            }
        }

        private void OnCollisionStay2D(Collision2D target)
        {
            if(target.gameObject.tag =="Player")
            {
                if(_movingPlatformLeft)
                {
                    target.gameObject.GetComponent<PlayerController>().PlatformMove(-1f);
                } 

                if(_movingPlatformRight)
                {
                    target.gameObject.GetComponent<PlayerController>().PlatformMove(1f);
                }
            }
        }

       
    }
}
