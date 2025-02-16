using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private int roadWidht;

    [Header("Control")]
    [SerializeField] private float slideSpeed;
    private Vector3 clickScreenPosition;
    private Vector3 clickPlayerPosition;
    [SerializeField] private CrowdSystem сrowdSystem;

    private void Update()
    {
        MoveSpeedForward();
        ManageControl();
    }

    private void MoveSpeedForward()
    {
        transform.position += Vector3.forward * Time.deltaTime * moveSpeed;
    }

    private void ManageControl()
    {
        if(Input.GetMouseButtonDown(0))
        {
            clickScreenPosition = Input.mousePosition;
            clickPlayerPosition = transform.position;
        }
        else if(Input.GetMouseButton(0))
        {
            float xScreenDifference = Input.mousePosition.x - clickScreenPosition.x;
            xScreenDifference /= Screen.width;
            xScreenDifference *= slideSpeed;
            
            Vector3 position = transform.position;
            position.x = clickPlayerPosition.x + xScreenDifference;

            position.x = Mathf.Clamp(position.x, - roadWidht / 2 + сrowdSystem.GetCrowdRadius(), roadWidht / 2 - сrowdSystem.GetCrowdRadius());

            transform.position = position;

            //transform.position = clickPlayerPosition + Vector3.right * xScreenDifference;
        }
    }
}
