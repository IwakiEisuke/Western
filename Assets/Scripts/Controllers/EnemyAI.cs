public class EnemyAI : IController
{
    Character _target;

    public EnemyAI(Character target)
    {
        _target = target;
    }

    public void HandleInput()
    {

    }

    public void Disable()
    {
        throw new System.NotImplementedException();
    }

    public void Enable()
    {
        throw new System.NotImplementedException();
    }
}
