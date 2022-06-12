using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using wpfApp.Repository;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace wpfApp.ViewModel;

public class PeopleListVM
{
    public ObservableCollection<PersonVM> peopleVMColection = new ObservableCollection<PersonVM>();
    private IPeopleRepository _repository;
    private MainWindow _mainWindow;
    private bool IsChanged
    {
        set => SetButtonsStatus(value);
    }

    public PeopleListVM(IPeopleRepository peopleRepository, MainWindow mainWindow) 
    {
        _mainWindow = mainWindow;
        _repository = peopleRepository;
        ReloadCollection();
        RegisterHandlers();
    }
    public void ReloadCollection()
    {

        peopleVMColection.Clear();
        _repository.Load();
        foreach (var person in _repository.GetAllPeople())
        {
            peopleVMColection.Add(new PersonVM(person));
        }
        foreach (var personVm in peopleVMColection)
        {
            personVm.PropertyChanged += PersonVmOnPropertyChanged;
        }

        IsChanged = false;
    }
    private void RegisterHandlers()
    {
        peopleVMColection.CollectionChanged += CollectionChanged;
        _mainWindow.Save_btn.Click += Save_btn_Click;
        _mainWindow.Cancel_btn.Click += Cancel_btn_Click;
        _mainWindow.PeopleDG.CellEditEnding += PeopleDG_CellEditEnding;
        _mainWindow.PeopleDG.CurrentCellChanged += PeopleDG_CurrentCellChanged;
    }

    private void PeopleDG_CurrentCellChanged(object? sender, EventArgs e)
    {
        ((DataGrid)sender).CommitEdit(DataGridEditingUnit.Row, true);
    }

    private void PeopleDG_CellEditEnding(object? sender, DataGridCellEditEndingEventArgs e)
    {
        if (nameof(PersonVM.DateOfBirth).Equals(e.Column.SortMemberPath))
        {
            var inputValue = ((TextBox) e.EditingElement).Text;
            try
            {
                DateTime.Parse(inputValue);
            }
            catch (FormatException exception)
            {
                MessageBox.Show($"There was an error while parsing a value: '{inputValue}' as date. " +
                                $"\r\n",
                    "Date validation error", MessageBoxButton.OK);
                ((TextBox) e.EditingElement).Text =
                    ((PersonVM) ((DataGrid) sender).CurrentItem).DateOfBirth.ToString("yyyy-MM-dd");
            }
            
        }

        

    }

    private void Cancel_btn_Click(object? sender, RoutedEventArgs e)
    {
        
        ReloadCollection();
        

    }
    private void Save_btn_Click(object? sender, RoutedEventArgs e)
    {
        if (IsValid(peopleVMColection))
        {
            _repository.Save();
            IsChanged = false;
        }
        else
        {
            MessageBox.Show(_mainWindow,
                "There are still some errors in the data. \r\n Please correct the highlighted fields",
                "Validation error",
                MessageBoxButton.OK,
                MessageBoxImage.Exclamation);
        }
        
    }
    private void PersonVmOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        IsChanged = true;
    }

    private void CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add:
                CollectionChangedAdd(sender, e);
                break;
            case NotifyCollectionChangedAction.Remove:
                CollectionChangedRemoved(sender, e);
                break;
            default:
                break;
        }

    }

    
    private void CollectionChangedAdd(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        foreach (PersonVM NewItem in e.NewItems)
        {
            if(NewItem.Id!=0) continue;
            NewItem.PropertyChanged += PersonVmOnPropertyChanged;
            _repository.Insert(NewItem._person);
            IsChanged = true;
        }

    }
    private void CollectionChangedRemoved(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        foreach (PersonVM DeletedItem in e.OldItems)
        {
            DeletedItem.PropertyChanged -= PersonVmOnPropertyChanged;
            if (DeletedItem.Id != null) _repository.Delete(DeletedItem.Id.Value);
        }

        IsChanged = true;
    }

    private void SetButtonsStatus(bool isEnabled)
    {
        _mainWindow.Save_btn.IsEnabled = isEnabled;
        _mainWindow.Cancel_btn.IsEnabled = isEnabled;
    }

    bool IsValid(ObservableCollection<PersonVM> list)
    {

        return list.All(person =>
        {
            return Validator.TryValidateObject(person
                , new ValidationContext(person, null, null)
                , new List<ValidationResult>()
                , false);
        });

    }
    

}