using System;
using System.Collections.Generic;
using MemoryCore.JsonModels;
using MvvmCross.Core.ViewModels;

namespace MemoryClient.Cross.Core.ViewModels
{
    public class ActiveLessonsViewModel : AppViewModel
    {
        private List<Language> _languages;
        public List<Language> Languages {
            get => _languages;
            set => SetProperty(ref _languages, value);
        }

        private Language _selectedLanguage;
        public Language SelectedLanguage {
            get => _selectedLanguage;
            set => SetProperty(ref _selectedLanguage, value);
        }

        private List<Lesson> _lessons;
        public List<Lesson> Lessons {
            get => _lessons;
            set => SetProperty(ref _lessons, value);
        }

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
    }
}