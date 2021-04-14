using AutoHotkey.Interop;
using Microsoft.WindowsAPICodePack.Dialogs;
using Prism.Commands;
using Prism.Mvvm;
using ScriptLoader.Entities;
using ScriptLoader.Helpers;
using ScriptLoader.Repositories;
using ScriptLoader.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using ToastNotifications.Messages;

namespace ScriptLoader.ViewModels
{
    public class MainViewModel : BindableBase
    {
        #region property

        /// <summary>
        /// 스크립트 경로
        /// </summary>
        public string ScriptDirectory
        {
            get => scriptDirectory;
            set => SetProperty(ref scriptDirectory, value);
        }

        /// <summary>
        /// 불러온 스크립트
        /// </summary>
        public ObservableCollection<string> Scripts
        {
            get => scripts;
            set => SetProperty(ref scripts, value);
        }

        /// <summary>
        /// 작동중인 스크립트
        /// </summary>
        public string ActivatedScript
        {
            get => activatedScript;
            set
            {
                SetProperty(ref activatedScript, value);
                RaisePropertyChanged(nameof(ActivatedScriptFileName));
            }
        }

        /// <summary>
        /// 작동중인 스크립트 파일명
        /// </summary>
        public string ActivatedScriptFileName
        {
            get => Path.GetFileName(this.ActivatedScript);
        }

        /// <summary>
        /// 하단에 표시할 상태메시지
        /// </summary>
        public string BottomStatusMessage
        {
            get => bottomStatusMessage;
            set => SetProperty(ref bottomStatusMessage, value);
        }

        /// <summary>
        /// 작동중
        /// </summary>
        public bool Running
        {
            get => running;
            set => SetProperty(ref running, value);
        }

        private string scriptDirectory;
        private ObservableCollection<string> scripts;
        private string bottomStatusMessage;
        private string activatedScript;
        private bool running;

        #endregion

        #region services

        /// <summary>
        /// 메시지박스
        /// </summary>
        private MessageBoxHelper messageBoxHelper;

        /// <summary>
        /// 네비게이션
        /// </summary>
        private NavigationService navigationService;

        /// <summary>
        /// 내부 설정
        /// </summary>
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

                    this.BottomStatusMessage = "대기 중..";

                    var setting = this.settingRepository.GetSetting();
                    this.ScriptDirectory = setting.ScriptDirectory;

                    if (!string.IsNullOrEmpty(this.ScriptDirectory))
                    {
                        ScanCommand.Execute();
                    }
                });
            }
        }

        /// <summary>
        /// 스크립트 경로 설정
        /// </summary>
        public DelegateCommand SetScriptDirectoryCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (Running)
                    {
                        return;
                    }

                    CommonOpenFileDialog dialog = new CommonOpenFileDialog();
                    dialog.IsFolderPicker = true;
                    if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                    {
                        this.ScriptDirectory = Path.Combine(dialog.FileName);

                        var setting = this.settingRepository.GetSetting();
                        setting.ScriptDirectory = this.ScriptDirectory;
                        this.settingRepository.UpdateSetting(setting);

                        ScanCommand.Execute();

                        var notifier = new ToastBuilder
                        {
                            TopMost = false,
                            LifeTime = 5,
                            Width = 600
                        }.BuildNotifier();
                        notifier.ShowInformation($"" +
                            $"스크립트 경로가 설정되었습니다.\n" +
                            $"{this.ScriptDirectory}");
                    }
                });
            }
        }

        /// <summary>
        /// 스크립트 검색
        /// </summary>
        public DelegateCommand ScanCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (Running)
                    {
                        return;
                    }

                    var notifier = new ToastBuilder
                    {
                        TopMost = false,
                        LifeTime = 5,
                        Width = 600
                    }.BuildNotifier();

                    if (string.IsNullOrEmpty(this.ScriptDirectory))
                    {
                        notifier.ShowError("스크립트 경로가 설정되지 않았습니다.");
                        return;
                    }

                    if (!Directory.Exists(this.ScriptDirectory))
                    {
                        notifier.ShowError("스크립트 경로가 존재하지 않습니다.");
                        this.ScriptDirectory = string.Empty;
                        return;
                    }

                    var scripts = Directory.GetFiles(this.ScriptDirectory, "*.ahk");
                    if (scripts.Length != 0)
                    {
                        this.Scripts = new ObservableCollection<string>(scripts);
                    }
                });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DelegateCommand LoadScriptCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (Running)
                    {
                        return;
                    }

                    Task.Run(() =>
                    {
#warning 무한 루프 스크립트를 열면 AHK.LoadFile 호출시 메인쓰레드가 멈추는 문제로 인해 별도의 task에서 로드 시도 후 결과와 상관없이 toast 표시
                        try
                        {
                            var ahk = AutoHotkeyEngine.Instance;
                            ahk.LoadFile(this.ActivatedScript);
                        }
                        catch (Exception ex)
                        {
                            messageBoxHelper.ShowMessage(ex.Message);
                        }
                    });

                    var notifier = new ToastBuilder
                    {
                        TopMost = false,
                        LifeTime = 1,
                        Width = 300
                    }.BuildNotifier();
                    notifier.ShowInformation($"{this.ActivatedScriptFileName} 로드!");

                    this.BottomStatusMessage = $"실행 중 스크립트 : {this.ActivatedScriptFileName}";

                    this.Running = true;
                });
            }
        }

        /// <summary>
        /// 프로그램 재실행
        /// </summary>
        public DelegateCommand ReloadCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
#warning 무한루프 스크립트 사용시 ahk reset이 안되는 문제로 스크립트를 변경하려면 프로그램을 재실행해야함
                    System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                    Application.Current.Shutdown();
                    return;
                });
            }
        }

        /// <summary>
        /// 스크립트 수정
        /// </summary>
        public DelegateCommand<string> EditScriptCommand
        {
            get
            {
                return new DelegateCommand<string>((obj) =>
                {
                    var scriptFilePath = obj;

                    if (File.Exists(scriptFilePath))
                    {
                        var setting = this.settingRepository.GetSetting();
                        var cmd = string.Empty;
                        switch (setting.Editor)
                        {
                            case Editor.Notepad:
                                cmd = "notepad";
                                break;

                            case Editor.VSCode:
                                cmd = "code";
                                break;

                            case Editor.Etc:
                                cmd = setting.EditorCommand;
                                break;

                            default:
                                cmd = "notepad.exe";
                                break;
                        }
                        Process.Start(cmd, scriptFilePath);
                    }
                });
            }
        }

        /// <summary>
        /// 설정창 열기
        /// </summary>
        public DelegateCommand OpenSettingWindowCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    navigationService.OpenDialogWindow<SettingWindow>();
                });
            }
        }

        /// <summary>
        /// 스크립트 추가 창 열기
        /// </summary>
        public DelegateCommand AddScriptWindowCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    messageBoxHelper.ShowMessage("지원 예정..");
                });
            }
        }

        #endregion
    }
}
