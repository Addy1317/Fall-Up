using System.Collections;
using UnityEngine;

namespace SS.FallUp.Platforms
{
    public class VanishingPlatform : Platform
    {
        [Header("Vanishing Settings")]
        [SerializeField] private float vanishDelay = 3f;   
        [SerializeField] private float reappearDelay = 5f; 

        private Coroutine vanishCoroutine;

        protected override void Update()
        {
            base.Update();  
        }

        public override void Activate()
        {
            base.Activate();

            if (vanishCoroutine != null)
            {
                StopCoroutine(vanishCoroutine); 
            }
            vanishCoroutine = StartCoroutine(VanishingSequence());
        }

        private IEnumerator VanishingSequence()
        {
            yield return new WaitForSeconds(vanishDelay);

            gameObject.SetActive(false);

            yield return new WaitForSeconds(reappearDelay);

            gameObject.SetActive(true);
            Activate();
        }

        protected override PlatformType GetPlatformType()
        {
            return PlatformType.VanishingPlatform;
        }
    }
}

