using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using System;
using System.IO;

namespace Notes
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();

                // ����������� ����� ��� ���������� �������
                string currentDirectory = Environment.CurrentDirectory;
                // ��������� ��� ���������� ������ NotesManager ����� ���� �������� � ���������� �������
                MainWindow mainWindow = (MainWindow)desktop.MainWindow;
                string directoryNotes = Path.Combine(currentDirectory, "NotesList");
                mainWindow.notesManager.SetFolderNotes(directoryNotes);

                // � ������ ���� ����� �� ����������, ������� �� � ��������� ������ �������,
                // ����� ���������� �������� ���� Json ������ ������� � �� �������������� � ����� NoteModel
                if (Directory.Exists(directoryNotes))
                {
                    mainWindow.notes = mainWindow.notesManager.LoadNotesJSON();
                }
                else 
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(directoryNotes);
                    directoryInfo.Create();

                    NoteModel firstNote = new NoteModel(DateTime.Now, "������ �������");
                    mainWindow.notes.Add(firstNote);
                }

                mainWindow.AddAllNotes();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}