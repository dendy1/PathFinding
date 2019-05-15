using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject uiContainer;
    public static GameManager Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    public void KillObject(GameObject objectToKill)
    {
        var poolTest = objectToKill.GetComponent<PoolObject>();
        Interactable interactable = objectToKill.GetComponent<Interactable>();

        if (interactable)
        {
            interactable.OnDefocused(objectToKill.transform);
        }
        
        if (poolTest)
        {
            poolTest.ReturnToPool();
        }
        else
        {
            Destroy(objectToKill);
        }
    }

    public void ShowHealthMenu(CharacterStats target)
    {
        uiContainer.SetActive(true);
        
        Text enemyName = uiContainer.transform.GetChild(0).GetComponent<Text>();
        enemyName.text = target.transform.name;
        
        Image hbFiller = uiContainer.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>();
        hbFiller.fillAmount = target.CurrentHealth / 100f;
    }

    public void CloseHealthMenu()
    {
        if (uiContainer)
            uiContainer.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
