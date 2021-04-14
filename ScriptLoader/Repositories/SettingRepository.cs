using ScriptLoader.Context;
using ScriptLoader.Entities;
using System.Linq;

namespace ScriptLoader.Repositories
{
    public class SettingRepository
    {
        public static SettingRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SettingRepository();
                }
                return instance;
            }
        }
        private static SettingRepository instance;

        private readonly ScriptLoaderDataContext context;

        private SettingRepository()
        {
            this.context = new ScriptLoaderDataContext();
        }

        public Setting GetSetting()
        {
            return context.Setting.FirstOrDefault();
        }

        public int UpdateSetting(Setting setting)
        {
            context.Setting.Update(setting);
            return context.SaveChanges();
        }
    }
}
