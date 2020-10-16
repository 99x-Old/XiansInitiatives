namespace XiansInitiative.Configuration.Accessor
{
    public interface IConfigurationAccessor
    {
        string GetSetting(string key);

        string GetConnectionString(string stringName);
    }
}