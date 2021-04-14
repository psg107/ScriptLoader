using Prism.Commands;
using Prism.Mvvm;
using ScriptLoader.Entities;
using ScriptLoader.Helpers;
using ScriptLoader.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptLoader.ViewModels
{
    public class SettingWindowViewModel : BindableBase
    {
        #region property

        public Editor Editor
        {
            get => editor;
            set
            {
                SetProperty(ref editor, value);
                UpdateEditor();
            }
        }

        public string EditorCommand
        {
            get => editorCommand;
            set
            {
                SetProperty(ref editorCommand, value);
                UpdateEditorCommand();
            }
        }

        private Editor editor;
        private string editorCommand;

        private MessageBoxHelper messageBoxHelper;
        private NavigationService navigationService;
        private SettingRepository settingRepository; 

        #endregion

        #region command

        private void InitializeServices()
        {
            this.messageBoxHelper = new MessageBoxHelper(this);
            this.navigationService = new NavigationService(this);
            this.settingRepository = SettingRepository.Instance;
        }

        /// <summary>
        /// 
        /// </summary>
        public DelegateCommand LoadCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    InitializeServices();

                    var setting = settingRepository.GetSetting();
                    this.Editor = setting.Editor;
                    this.EditorCommand = setting.EditorCommand;
                });
            }
        }

        #endregion

        /// <summary>
        /// Editor 업데이트
        /// </summary>
        private void UpdateEditor()
        {
            if (this.settingRepository == null)
            {
                return;
            }

            var setting = settingRepository.GetSetting();
            setting.Editor = this.Editor;
            settingRepository.UpdateSetting(setting);
        }

        /// <summary>
        /// EditorCommand 업데이트
        /// </summary>
        private void UpdateEditorCommand()
        {
            if (this.settingRepository == null)
            {
                return;
            }

            var setting = settingRepository.GetSetting();
            setting.EditorCommand = this.EditorCommand;
            settingRepository.UpdateSetting(setting);
        }
    }
}
