using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] string scenNmae = "AI enemy task";
    
    public void AIenemyTask()
    {
        SceneManager.LoadSceneAsync(scenNmae);
    }   
}
