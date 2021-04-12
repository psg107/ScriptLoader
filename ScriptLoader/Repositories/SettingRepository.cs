﻿using ScriptLoader.Context;
using ScriptLoader.Entities;
using System.Linq;

namespace ScriptLoader.Repositories
{
    public class SettingRepository
    {
        private readonly ScriptLoaderDataContext context;

        public SettingRepository()
        {
            this.context = new ScriptLoaderDataContext();
        }

        public Setting GetSetting()
        {
            return context.Setting.FirstOrDefault();
        }

        public void UpdateSetting(Setting setting)
        {
            context.Setting.Update(setting);
        }

        public int Save()
        {
            return context.SaveChanges();
        }
    }
}