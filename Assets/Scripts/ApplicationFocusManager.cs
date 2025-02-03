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
        // �t�H�[�J�X���Ȃ��ݒ�̏ꍇ�̓J�[�\����\�����Ȃ��瑀��ł���悤�ɂ���
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
