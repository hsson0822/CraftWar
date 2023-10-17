using UnityEngine;
using UnityEngine.SceneManagement;

namespace SimpleMobileInput.Demo
{
    public class SceneNavigation : MonoBehaviour
    {
        public void GoToSceneDemoSettings()
        {
            SceneManager.LoadScene("DemoSettings");
        }

        public void GoToSceneDemoFixedControls()
        {
            SceneManager.LoadScene("DemoFixedControls");
        }

        public void GoToSceneDemoMenu()
        {
            SceneManager.LoadScene("DemoMenu");
        }
    }
}