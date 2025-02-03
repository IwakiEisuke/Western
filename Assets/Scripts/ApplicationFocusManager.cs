using Unity.Cinemachine;
using UnityEngine;

public class ApplicationFocusManager : MonoBehaviour
{
    [SerializeField] bool canFocus = true;
    [SerializeField] CinemachineInputAxisController inputAxisController;

    public static bool IsFocus { get; private set; }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Focus(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Focus(false);
        }
    }

    public void Focus(bool focus)
    {
        // フォーカスしない設定の場合はカーソルを表示しながら操作できるようにする
        if (!canFocus)
        {
            IsFocus = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            inputAxisController.enabled = true;
            return;
        }


        IsFocus = focus;

        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            inputAxisController.enabled = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            inputAxisController.enabled = false;
        }
    }
}
