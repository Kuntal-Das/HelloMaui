using Newtonsoft.Json;

namespace HelloMaui.UnitTest.Mocks;

public class MockPreferences : IPreferences
{
    private Dictionary<string, string> _dictionary = new();

    public bool ContainsKey(string key, string? sharedName = null)
    {
        return _dictionary.ContainsKey(key);
    }

    public void Remove(string key, string? sharedName = null)
    {
        _dictionary.Remove(key);
    }

    public void Clear(string? sharedName = null)
    {
        _dictionary.Clear();
    }

    public void Set<T>(string key, T value, string? sharedName = null)
    {
        _dictionary[key] = JsonConvert.SerializeObject(value);
    }

    public T Get<T>(string key, T defaultValue, string? sharedName = null)
    {
        if (!_dictionary.TryGetValue(key, out var value)) return defaultValue;
        return JsonConvert.DeserializeObject<T>(value) ?? defaultValue;
    }
}