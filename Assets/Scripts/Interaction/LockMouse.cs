using UnityEngine;

namespace IP1.Interaction
{
    public class LockMouse : MonoBehaviour
    {
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }
    }
}
