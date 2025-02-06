using UnityEngine;

public abstract class Character : MonoBehaviour
{
    // インスタンス毎にコントローラーを設定する。
    // コントローラーをMonoBehaviour継承させてコンポーネントにした方が良い？
        // AIを作る時はMonoBehaviourで実装
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
