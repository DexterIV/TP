
using System;
using System.Collections.ObjectModel;
using System.Windows;
using TP.MVVMExample.Model;
using TP.MVVMExample.MVVMLight;

namespace TP.MVVMExample.ViewModel
{
  /// <summary>
  /// This class contains properties that the <see cref="MainWindow"/> can data bind to.
  /// </summary>
  internal class MainViewModel : ViewModelBase
  {

    #region constructors
    /// <summary>
    /// Initializes a new instance of the MainViewModel class.
    /// </summary>
    public MainViewModel()
    {
      DataLayer = new DataLayer();
      MyCommand = new RelayCommand(ShowPopupWindow, () => !string.IsNullOrEmpty(m_ActionText));
      m_ActionText = "Text to be displayed on the popup";
    }
    #endregion

    #region ViewModel API
    public ObservableCollection<User> Users
    {
      get { return m_Users; }
      set
      {
        m_Users = value;
        RaisePropertyChanged();
      }
    }
    public User CurrentUser
    {
      get
      {
        return m_CurrentUser;
      }
      set
      {
        m_CurrentUser = value;
        RaisePropertyChanged();
      }
    }
    public string ActionText
    {
      get { return m_ActionText; }
      set
      {
        m_ActionText = value;
        MyCommand.RaiseCanExecuteChanged();
        RaisePropertyChanged();
      }
    }
    public RelayCommand MyCommand
    {
      get;
      private set;
    }
    #endregion

    #region Unit test instrumentation
    /// <summary>
    /// Gets or sets the message box show delegate.
    /// </summary>
    /// <remarks>
    /// It is to be used by unit test to override default popup. Limited access ability is addressed by explicate allowing unit test assembly to access internals 
    /// using <see cref="System.Runtime.CompilerServices.InternalsVisibleToAttribute"/>.
    /// </remarks>
    /// <value>The message box show delegate.</value>
    internal Func<string, string, MessageBoxButton, MessageBoxImage, MessageBoxResult> MessageBoxShowDelegate { get; set; } = MessageBox.Show;
    internal DataLayer DataLayer
    {
      get { return m_DataLayer; }
      set
      {
        m_DataLayer = value; Users = new ObservableCollection<User>(value.User);
      }
    } 
    #endregion

    #region Private stuff
    private DataLayer m_DataLayer;
    private User m_CurrentUser;
    private string m_ActionText;
    private ObservableCollection<User> m_Users;
    private void ShowPopupWindow()
    {
      MessageBoxShowDelegate(ActionText, "Button interaction", MessageBoxButton.OK, MessageBoxImage.Information);
    }
    #endregion

  }
}