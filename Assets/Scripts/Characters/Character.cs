using UnityEngine;

public abstract class Character : MonoBehaviour
{
    // �C���X�^���X���ɃR���g���[���[��ݒ肷��B
    // �R���g���[���[��MonoBehaviour�p�������ăR���|�[�l���g�ɂ��������ǂ��H
        // AI����鎞��MonoBehaviour�Ŏ���
    protected IController _controller;

    public void SetController(IController newController)
    {
        _controller = newController;
    }
    void Update()
    {
        _controller.HandleInput();
        UpdatePhysics();
    }

    public abstract void UpdatePhysics();
}
