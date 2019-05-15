using UnityEngine;
using UnityEngine.UI;

public class KeyButton : MonoBehaviour
{
    [SerializeField] private KeyCode[] keys;

    private void Update()
    {
        foreach (var key in keys)
        {
            if (Input.GetKeyDown(key))
            {
                GetComponent<Button>().onClick.Invoke();
            }
        }
    }
}
