using UnityEngine;

namespace ChironPE
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField]
        private string horizontalAxis = "Horizontal";
        [SerializeField]
        private string verticalAxis = "Vertical";

        [SerializeField]
        private float movementSpeed = 5f;

        private CharacterController controller = null;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            Vector2 input = new Vector2(Input.GetAxisRaw(horizontalAxis), Input.GetAxisRaw(verticalAxis));
            
            if(input != Vector2.zero)
            {
                if(input.sqrMagnitude > 1)
                {
                    input.Normalize();
                }

                Vector3 velocity = new Vector3(input.x, 0, input.y) * movementSpeed;
                controller.Move(velocity * Time.deltaTime);
            }
        }
    }
}
