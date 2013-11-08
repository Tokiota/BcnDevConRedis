namespace POC.Data
{
    public interface IRepository<in T>
    {
        void Add(T newEntity);
    }
}