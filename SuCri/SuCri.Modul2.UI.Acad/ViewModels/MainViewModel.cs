using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using MaterialDesignThemes.Wpf;
using SuCri.Modul2.UI.Acad.Utils;
using SuCri.Modul2.UI.Acad.Views;

namespace SuCri.Modul2.UI.Acad.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        //Fields
        //private UserAccountModel _currentUserAccount;
        private ViewModelBase _currentChildView;
        private string _caption;
        private PackIconKind _icon;
        public CultureInfo Culture { get; set; }

        //private IUserRepository _userRepository;

        //Properties
        //public UserAccountModel CurrentUserAccount
        //{
        //    get => _currentUserAccount;
        //    set
        //    {
        //        _currentUserAccount = value;
        //        OnPropertyChanged(nameof(CurrentUserAccount));
        //    }
        //}

        public ViewModelBase CurrentChildView
        {
            get => _currentChildView;
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }

        }
        public string Caption
        {
            get => _caption;
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }
        public PackIconKind Icon
        {
            get => _icon;
            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }


        //Commands

        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowReportsViewCommand { get; }
        public ICommand ShowSUCreationViewCommand { get; }

        public ICommand ShowPropertiesViewCommand { get; }
        public ICommand ShowAutoFeaturesViewCommand { get; }
        public ICommand ShowSettingsViewCommand { get; }
        public ICommand ShowNotificationViewCommand { get; }
        public ICommand ShowMailViewCommand { get; }
        public ICommand ShowDocumentationsViewCommand { get; }

        //public ICommand TestCommand { get; }



        //public string Title => _resManager.GetString("Title", Culture);

        //public string SubTitle1 => _resManager.GetString("SubTitle1", Culture);

        //public string SubTitle1But1 => _resManager.GetString("SubTitle1But1", Culture);
        //public string SubTitle1But2 => _resManager.GetString("SubTitle1But2", Culture);
        //public string SubTitle1But3 => _resManager.GetString("SubTitle1But3", Culture);
        //public string SubTitle1But4 => _resManager.GetString("SubTitle1But4", Culture);
        //public string SubTitle1But5 => _resManager.GetString("SubTitle1But5", Culture);
        //public string SubTitle2 => _resManager.GetString("SubTitle2", Culture);
        //public string SubTitle2But1 => _resManager.GetString("SubTitle2But1", Culture);
        //public string SubTitle2But2 => _resManager.GetString("SubTitle2But2", Culture);
        //public string SubTitle3 => _resManager.GetString("SubTitle3", Culture);
        //public string SubTitle3But1 => _resManager.GetString("SubTitle3But1", Culture);
        //public string SubTitle3But2 => _resManager.GetString("SubTitle3But2", Culture);
        //public string SubTitle4 => _resManager.GetString("SubTitle4", Culture);
        //public string SubTitle4But1 => _resManager.GetString("SubTitle4But1", Culture);
        //public string SubTitle5 => _resManager.GetString("SubTitle5", Culture);
        //public string SubTitle5But1 => _resManager.GetString("SubTitle5But1", Culture);
        //public string SubTitle5But2 => _resManager.GetString("SubTitle5But2", Culture);
        //public string SubTitle5But3 => _resManager.GetString("SubTitle5But3", Culture);
        //public string SubTitle5But4 => _resManager.GetString("SubTitle5But4", Culture);
        //public string SubTitle5But5 => _resManager.GetString("SubTitle5But5", Culture);
        //public string SubTitle5But6 => _resManager.GetString("SubTitle5But6", Culture);

        //public string Description => _resManager.GetString("Description", Culture);
        //public string Description1 => _resManager.GetString("Description1", Culture);
        //public string Description2 => _resManager.GetString("Description2", Culture);
        //public string Description3 => _resManager.GetString("Description3", Culture);

        //public string Description4 => _resManager.GetString("Description4", Culture);
        //public string NumComp => _resManager.GetString("NumComp", Culture);

        public string MenuButton1 => _resManager.GetString("MenuButton1", Culture);

        public string MenuButton2 => _resManager.GetString("MenuButton2", Culture);
        public string MenuButton3 => _resManager.GetString("MenuButton3", Culture);
        public string MenuButton4 => _resManager.GetString("MenuButton4", Culture);
        public string MenuButton5 => _resManager.GetString("MenuButton5", Culture);
        public string MenuButton6 => _resManager.GetString("MenuButton6", Culture);
        public string MenuButton7 => _resManager.GetString("MenuButton7", Culture);


        private ResourceManager _resManager = Languages.UILanguage.ResourceManager;
        public double WinTopPosition
        {
            get => Properties.Settings.Default.WinTopPosition;
            set { Properties.Settings.Default.WinTopPosition = value; }
        }

        public double WinLeftPosition
        {
            get => Properties.Settings.Default.WinLeftPosition;
            set { Properties.Settings.Default.WinLeftPosition = value; }
        }


        public MainViewModel()
        {
            //_userRepository = new UserRepository();
            //CurrentUserAccount = new UserAccountModel();

            //Initialize Commands
            

            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowReportsViewCommand = new ViewModelCommand(ExecuteShowReportsViewCommand);
            ShowSUCreationViewCommand = new ViewModelCommand(ExecuteShowSUCreationViewCommand);
            ShowPropertiesViewCommand = new ViewModelCommand(ExecuteShowPropertiesViewCommand);
            ShowAutoFeaturesViewCommand = new ViewModelCommand(ExecuteShowAutoFeaturesViewCommand);
            ShowSettingsViewCommand = new ViewModelCommand(ExecuteShowSettingsViewCommand);
            ShowNotificationViewCommand = new ViewModelCommand(ExecuteShowNotificationViewCommand);
            ShowMailViewCommand = new ViewModelCommand(ExecuteShowMailViewCommand);
            ShowDocumentationsViewCommand = new ViewModelCommand(ExecuteShowDocumentationsViewCommand);
            //TestCommand = new ViewModelCommand(ExecuteTestCommand);
            //Default View
            ExecuteShowHomeViewCommand(null);

            Culture = CultureInfo.CreateSpecificCulture(Language.Languages[Properties.Settings.Default.Language]);




            //LoadCurrentUserData();

        }

        //private void ExecuteTestCommand(object obj)
        //{
        //    if (Properties.Settings.Default.Language == "English")
        //    {
        //        Properties.Settings.Default.Language = "German";
        //    }

        //    else
        //    {
        //        Properties.Settings.Default.Language = "English";
        //    }
        //}

        private void ExecuteShowHomeViewCommand(object obj)
        {
            CurrentChildView = new HomeViewModel();
            Caption = MenuButton1;
            Icon = PackIconKind.HomeOutline; ;


        }

        private void ExecuteShowSUCreationViewCommand(object obj)
        {
            CurrentChildView = new SUCreationViewModel();
            Caption = MenuButton2;
            Icon = PackIconKind.Bench;
        }

        private void ExecuteShowReportsViewCommand(object obj)
        {
            CurrentChildView = new ReportsViewModel();
            Caption = MenuButton3;
            Icon = PackIconKind.ChartArc;
        }

        private void ExecuteShowPropertiesViewCommand(object obj)
        {
            CurrentChildView = new PropertiesViewModel();
            Caption = MenuButton4;
            Icon = PackIconKind.PencilOutline;
        }

        private void ExecuteShowAutoFeaturesViewCommand(object obj)
        {
            CurrentChildView = new AutoFeaturesViewModel();
            Caption = MenuButton5;
            Icon = PackIconKind.RobotOutline;
        }

        private void ExecuteShowSettingsViewCommand(object obj)
        {
            CurrentChildView = new SettingsViewModel();
            Caption = MenuButton6;
            Icon = PackIconKind.CogOutline;
        }

        private void ExecuteShowNotificationViewCommand(object obj)
        {
            CurrentChildView = new NotificationViewModel();
            Caption = "Notifications";
            Icon = PackIconKind.BellOutline;
        }

        private void ExecuteShowMailViewCommand(object obj)
        {
        }

        private void ExecuteShowDocumentationsViewCommand(object obj)
        {
            CurrentChildView = new DocumentationsViewModel();
            Caption = MenuButton7;
            Icon = PackIconKind.FileDocument;
        }


        //private void LoadCurrentUserData()
        //{
        //    var user = _userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
        //    if (user != null)
        //    {


        //        CurrentUserAccount.Username = user.Username;
        //        CurrentUserAccount.DisplayName = $"{user.Name} {user.LastName}";
        //        CurrentUserAccount.ProfilePicture = null;

        //    }
        //    else
        //    {
        //        CurrentUserAccount.DisplayName = "Invalid user, not logged in";
        //        //Hide Child Views

        //    }
        //}

        public bool IsEnglish
        {
            get => Properties.Settings.Default.Language != Language.English;
            set
            {
                Properties.Settings.Default.Language = value ? Language.German : Language.English;
                Culture = CultureInfo.CreateSpecificCulture(Language.Languages[Properties.Settings.Default.Language]);
                CultureChanged?.Invoke();
                Caption = _resManager.GetString("MenuButton1", Culture);

                OnPropertyChanged(nameof(IsEnglish));
            }
        }

        public bool IsRegisterDone
        {
            get => Properties.Settings.Default.IsRegisterCmdDone;
            set
            {
                OnPropertyChanged(nameof(IsRegisterDone));
            }
        }

        public static Action CultureChanged;

    }
}
