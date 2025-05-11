namespace Core
{
    public interface ISettingsSystem
    {
        void SetFloat(string key, float value);
        float GetFloat(string key, float defaultValue = 1f);
        void SetBool(string key, bool value);
        bool GetBool(string key, bool defaultValue = true);
    }
}