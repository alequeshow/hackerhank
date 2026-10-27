public class TimeMap {
    
    private readonly Dictionary<string, List<(int timestamp, string value)>> _store = [];

    public void Set(string key, string value, int timestamp) 
    {
        if (!_store.TryGetValue(key, out var list))
        {
            list = new List<(int, string)>();
            _store[key] = list;
        }
        
        list.Add((timestamp, value));
    }
    
    public string Get(string key, int timestamp) 
    {
        if (!_store.TryGetValue(key, out var list) || list.Count == 0)
        {
            return "";
        }

        if (timestamp < list[0].timestamp)
        {
            return "";
        }

        int left = 0;
        int right = list.Count - 1;
        
        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            
            if (list[mid].timestamp == timestamp)
            {
                return list[mid].value;
            }
            
            if (list[mid].timestamp < timestamp)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }

        return list[right].value;
    }
}