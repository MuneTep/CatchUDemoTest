using DG.Tweening;
using System.Collections;
using UnityEngine;

public class StuffReveralCameraMove : MonoBehaviour
{    
        public Camera mainCam;
        public Camera revealCam;
        public float revealDuration = 2.5f;

        private void Start()
        {
            mainCam.enabled = true;
            revealCam.enabled = false;
        }

        public void StartReveal(System.Action onComplete = null)
        {
            StartCoroutine(RevealRoutine(onComplete));
        }

        private IEnumerator RevealRoutine(System.Action onComplete)
        {
            //mainCam.enabled = false;
            //revealCam.enabled = true;
            
            mainCam.transform.DOMove(revealCam.transform.position, 1f);

            Time.timeScale = 0.25f;
            yield return new WaitForSecondsRealtime(revealDuration);  // Ω√∞£ ∏ÿ√„ Ω√ WaitForSecondsRealtime∑Œ

            Time.timeScale = 1f;

            //revealCam.enabled = false;
            //mainCam.enabled = true;

            onComplete?.Invoke();
        }
}
