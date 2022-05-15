using Unity.Jobs;

namespace Fleetio.ECS
{

    public interface IJobDependency
    {
        JobHandle JobHandle { get; }

    }
    public interface ISystem
    {
        void Run();
    }
}