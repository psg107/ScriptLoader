using ScriptLoader.Entities;
using System.Collections.Generic;

namespace ScriptLoader.Context
{
    public static class ScriptLoaderDataContextSeed
    {
        public static List<Setting> GenerateSetting()
        {
            var settings = new List<Setting>
            {
                new Setting
                {
                    Id = 1,
                    ScriptDirectory = string.Empty
                }
            };

            return settings;
        }
    }
}
