using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;

namespace Notes;

public partial class NoteEdit : Window
{
    MainWindow mainWindow;

    public NoteEdit(MainWindow main)
    {
        InitializeComponent();

        mainWindow = main;

        if (main.choiseNote != null) 
        {
            TextNote.Text = main.choiseNote.TextNote;
        }

        SaveButton.Click += SaveButton_Click;
        CloseButton.Click += CloseButton_Click;
    }

    // —охранение нового экземпл€ра заметки или редактирование уже созданной
    private void SaveButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (TextNote.Text != null)
        {
            // ¬ случае если редактируетс€ уже созданна€ заметка, происходит ее замена в коллекци€х данных 
            // и ListBox в интерфейсе. »наче если создаетс€ нова€ заметка, то соответственно новый экземпл€р 
            // заметки попадает в коллекцию данных и интерфейса.
            if (mainWindow.choiseNote != null)
            {
                int id = mainWindow.notes.IndexOf(mainWindow.choiseNote);
                mainWindow.choiseNote = new NoteModel(mainWindow.choiseNote.Date, TextNote.Text);
                mainWindow.notes[id] = mainWindow.choiseNote;
                mainWindow.AllNotes.Items[id] = mainWindow.choiseNote;
            }
            else 
            {
                NoteModel newNote = new NoteModel(DateTime.Now, TextNote.Text);
                mainWindow.notes.Add(newNote);
                mainWindow.AllNotes.Items.Add(newNote);
            }
        }

        Close();
    }

    private void CloseButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Close();
    }
}