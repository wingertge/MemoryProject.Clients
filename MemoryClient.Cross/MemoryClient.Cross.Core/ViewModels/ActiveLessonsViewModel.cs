using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MemoryApi.HttpClient;
using MemoryCore.JsonModels;
using MvvmCross.Core.ViewModels;

namespace MemoryClient.Cross.Core.ViewModels
{
    public class ActiveLessonsViewModel : AppViewModel
    {
        private readonly ILessonsClient _api;
        private readonly IApplicationSettings _appSettings;

        private bool _isBusy;
        public bool IsBusy {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public bool ShowList => _lessons.Count > 0;
        
        private bool _showPronunciation;
        public bool ShowPronunciation {
            get => _showPronunciation;
            set => SetProperty(ref _showPronunciation, value);
        }

        private List<Language> _languages = new List<Language>();
        public List<Language> Languages {
            get => _languages;
            set => SetProperty(ref _languages, value);
        }

        private Language _selectedLanguage;
        public Language SelectedLanguage {
            get => _selectedLanguage;
            set => SetProperty(ref _selectedLanguage, value);
        }

        private List<Lesson> _lessons = new List<Lesson>();
        public List<Lesson> Lessons
        {
            get => _lessons;
            set
            {
                SetProperty(ref _lessons, value);
                RaisePropertyChanged(nameof(ShowList));
            }
        }

        private readonly Dictionary<string, List<Lesson>> _lessonCache = new Dictionary<string, List<Lesson>>();

        private MvxCommand _addLessonCmd;
        public MvxCommand AddLessonCmd =>_addLessonCmd ?? (_addLessonCmd = new MvxCommand(AddLesson));
 
        private void AddLesson()
        {
            throw new NotImplementedException();
        }

        private MvxCommand _deleteLessonCmd;
        public MvxCommand DeleteCmd =>_deleteLessonCmd ?? (_deleteLessonCmd = new MvxCommand(DeleteLesson));
 
        private void DeleteLesson()
        {
            throw new NotImplementedException();
        }

        private ICommand _languageChangedCmd;
        public ICommand LanguageChangedCmd =>_languageChangedCmd ?? (_languageChangedCmd = new MvxAsyncCommand(LanguageChanged));
 
        private async Task LanguageChanged()
        {
            if (SelectedLanguage == null) _lessons = new List<Lesson>();
            else
            {
                ShowPronunciation = LanguageUtils.HasPronunciation(SelectedLanguage);
                if (_lessonCache.ContainsKey(SelectedLanguage.IETFTag))
                {
                    _lessons = _lessonCache[SelectedLanguage.IETFTag];
                    return;
                }

                IsBusy = true;
                var newLessons = await _api.GetListAsync(_appSettings.AuthToken, SelectedLanguage.IETFTag);
                _lessonCache[SelectedLanguage.IETFTag] = newLessons;
                _lessons = newLessons;
                IsBusy = false;
            }
        }

        public ActiveLessonsViewModel(ILessonsClient api, IApplicationSettings appSettings)
        {
            _api = api;
            _appSettings = appSettings;
        }

        public async Task Init()
        {
            IsBusy = true;
            Languages = (await _api.GetLanguagesAsync(_appSettings.AuthToken)).ToList();
        }
    }
}