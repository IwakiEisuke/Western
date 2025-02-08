using UnityEngine;

public abstract class Character : MonoBehaviour
{
    // �C���X�^���X���ɃR���g���[���[��ݒ肷��B
    // �R���g���[���[��MonoBehaviour�p�������ăR���|�[�l���g�ɂ��������ǂ��H
        // AI����鎞��MonoBehaviour�Ŏ���
    protected IController _controller;

    public void SetController(IController newController)
    {
        if (newController == null)
        {
            _controller.Disable();
        }
        else
        {
            _controller = newController;
            _controller.Enable();
        }
    }

    void Update()
    {
        _controller.HandleInput();
        UpdatePhysics();
    }

    public abstract void UpdatePhysics();
}
