using UnityEngine;

public class Healthbar : MonoBehaviour
{
    private void OnMouseOver()
    {
        CharacterStats stats = GetComponent<CharacterStats>();
        GameManager.Instance.ShowHealthMenu(stats);
    }
    
    private void OnMouseExit()
    {
        GameManager.Instance.CloseHealthMenu();
    }

    private void OnDestroy()
    {
        GameManager.Instance.CloseHealthMenu();
    }
}
