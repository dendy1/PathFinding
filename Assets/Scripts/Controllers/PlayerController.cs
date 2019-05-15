using UnityEngine;

public class PlayerController : BaseController
{
    [SerializeField] private bool autoClick;

    private void Update()
    {      
        bool leftButton, rightButton;
        leftButton = autoClick ? Input.GetMouseButton(0) : Input.GetMouseButtonDown(0);
        rightButton = autoClick ? Input.GetMouseButton(1) : Input.GetMouseButtonDown(1);

        if (leftButton && !PlayerManager.Instance.Controlling)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Interactable newTarget = hit.collider.GetComponent<Interactable>();
                
                if (newTarget)
                {
                    EventManager.InvokeEvent("PlayerAttacked", this, new PlayerAttackedEA(transform, newTarget));
                    SetFocus(newTarget);
                }
                else
                {
                    MoveTo(hit.point);
                    RemoveFocus();
                }
            }
        }

        if (NavMeshTarget)
        {
            MoveTo(NavMeshTarget.position);
        }
    }
}
