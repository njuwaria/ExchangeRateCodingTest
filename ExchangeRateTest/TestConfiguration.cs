using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExchangeRateTest
{
    public class TestConfiguration : IConfiguration
    {
        private readonly Dictionary<string, string> _configurations;

        public TestConfiguration(Dictionary<string, string> configurations)
        {
            _configurations = configurations;
        }

        public string this[string key]
        {
            get
            {
                _configurations.TryGetValue(key, out var value);
                return value;
            }
            set
            {
                // Implement if needed
            }
        }

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            return Enumerable.Empty<IConfigurationSection>();
        }

        public IChangeToken GetReloadToken()
        {
            throw new NotImplementedException();
        }

        public IConfigurationSection GetSection(string key)
        {
            throw new NotImplementedException();
        }

        public T GetValue<T>(string key)
        {
            if (_configurations.TryGetValue(key, out var value))
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            return default;
        }
    }
    public class ConfigurationSection : IConfigurationSection
    {
        public string this[string key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Key { get; set; }

        public string Value { get; set; }

        public string Path => throw new NotImplementedException();

        public string SectionKey => throw new NotImplementedException();

        public IChangeToken GetReloadToken()
        {
            throw new NotImplementedException();
        }

        public IConfigurationSection GetSection(string key)
        {
            return this;
        }

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            throw new NotImplementedException();
        }
    }
}
