using System.Linq;
using System.Text;
using System.Threading.Tasks;

interface ISkill
{
    SkillDef SkillId { get; }
    void Setup();
    void Update();
    void Levelup();
}

public enum SkillDef
{
    Invalid = 0,
    HomingBullet = 1,
    AreaAttackResporn = 2,
}