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

                // Указывается папка для сохранения заметок
                string currentDirectory = Environment.CurrentDirectory;
                // Указываем для переменной класса NotesManager папку ддля загрузки и сохранения заметок
                MainWindow mainWindow = (MainWindow)desktop.MainWindow;
                string directoryNotes = Path.Combine(currentDirectory, "NotesList");
                mainWindow.notesManager.SetFolderNotes(directoryNotes);

                // В случае если папки не существует, создаем ее и добавляем первую заметку,
                // иначе производим выгрузку всех Json файлов заметок и их преобразование в класс NoteModel
                if (Directory.Exists(directoryNotes))
                {
                    mainWindow.notes = mainWindow.notesManager.LoadNotesJSON();
                }
                else 
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(directoryNotes);
                    directoryInfo.Create();

                    NoteModel firstNote = new NoteModel(DateTime.Now, "Первая заметка");
                    mainWindow.notes.Add(firstNote);
                }

                mainWindow.AddAllNotes();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}