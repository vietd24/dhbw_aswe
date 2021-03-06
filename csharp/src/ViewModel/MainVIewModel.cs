﻿using Microsoft.VisualStudio.PlatformUI;
using mvvm.Model;
using mvvm.Utility;
using mvvm.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace mvvm.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private UserControl SchoolSummaryView { get; }
        private UserControl ClassView { get; }
        private UserControl TeacherView { get; }
        private UserControl StudentView { get; }
        public UserControl CurrentView { get; set; }

        private SchoolSummaryViewModel SchoolSummaryViewModel;
        private ClassViewModel ClassViewModel;
        private TeacherViewModel TeacherViewModel;
        private StudentViewModel StudentViewModel;

        public ICommand HomeCommand { get; private set; }

        public ObservableCollection<ClassBook> ClassBooks { get; set; }
        public ObservableCollection<Teacher> Teachers { get; set; }
        public ObservableCollection<Student> Students { get; set; }

        public MainViewModel()
        {
            SchoolSummaryView = new SchoolSummaryView();
            ClassView = new ClassView();
            TeacherView = new TeacherView();
            StudentView = new StudentView();

            SchoolSummaryViewModel = (SchoolSummaryViewModel)SchoolSummaryView.DataContext;
            ClassViewModel = (ClassViewModel)ClassView.DataContext;
            TeacherViewModel = (TeacherViewModel)TeacherView.DataContext;
            StudentViewModel = (StudentViewModel)StudentView.DataContext;

            InitializeCommands();

            SchoolSummaryViewModel.MainViewModel = this;
            ClassViewModel.MainViewModel = this;

            var dummyData = StudentTestDataUtility.GetDummySchoolData();

            ClassBooks = new ObservableCollection<ClassBook>(dummyData.ClassBooks);
            Teachers = new ObservableCollection<Teacher>(dummyData.Teachers);
            Students = new ObservableCollection<Student>(dummyData.Students);

            OnSchoolSummaryView();
        }

        private void InitializeCommands()
        {
            HomeCommand = new DelegateCommand(OnSchoolSummaryView);
        }

        private void OnSchoolSummaryView()
        {
            SchoolSummaryViewModel.ClassBooks = ClassBooks;
            SchoolSummaryViewModel.Teachers = Teachers;
            SchoolSummaryViewModel.Update();
            CurrentView = SchoolSummaryView;
            OnPropertyChanged(nameof(CurrentView));
        }

        internal void OnStudentView(Student selectedStudent)
        {
            StudentViewModel.ClassBooks = new List<ClassBook>(ClassBooks);
            StudentViewModel.Student = selectedStudent;
            CurrentView = StudentView;
            OnPropertyChanged(nameof(CurrentView));
        }

        internal void OnClassView(ClassBook selectedClass)
        {
            ClassViewModel.ClassBook = selectedClass;
            ClassViewModel.Teachers = Teachers;
            CurrentView = ClassView;
            OnPropertyChanged(nameof(CurrentView));
        }

        internal void OnTeacherView(Teacher selectedTeacher)
        {
            TeacherViewModel.Teacher = selectedTeacher;
            CurrentView = TeacherView;
            OnPropertyChanged(nameof(CurrentView));
        }
    }
}
