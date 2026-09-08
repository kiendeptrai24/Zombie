public class ZombieAnimationTrigger : KienMonoBehaviour
{
    protected ZombieController m_zombie;
    protected IStateMachine m_zombieSM;
    protected override void Awake()
    {
        m_zombie = GetComponentInParent<ZombieController>();
    }
    override protected void Start()
    {
        base.Start();
        m_zombieSM = m_zombie.machine;
    }
    public virtual void Animtiontrigger()
    {
        m_zombieSM.GetFeature<IAnimationTrigger>()?.ActiveTrigger();
    }
    public virtual void ActiveSkill()
    {
        m_zombieSM.GetFeature<ISkillTrigger>()?.ActiveSkill();
    }
}