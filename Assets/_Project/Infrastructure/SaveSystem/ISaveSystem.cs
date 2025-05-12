namespace Infrastructure.SaveSystem
{
    public interface ISaveSystem
    {
        void Save(SaveContainer container);
        SaveContainer Load();
    }

}