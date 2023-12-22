using UnityEngine;
using UnityEngine.SceneManagement;

namespace Elias.Scripts
{
    public class SceneChangeTimer : MonoBehaviour
    {
        public float sceneChangeDelay = 30f;
        private float _timer;

        private void Start()
        {
            _timer = 0f;
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= sceneChangeDelay)
            {
                ChangeScene();
            }
        }

        private void ChangeScene()
        {
            SceneManager.LoadScene("NomDeLaScene");
        }
    }
}