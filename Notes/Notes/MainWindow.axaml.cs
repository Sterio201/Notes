using Avalonia.Controls;
using System;
using System.Collections.ObjectModel;

namespace Notes
{
    public partial class MainWindow : Window
    {
        public NotesManager notesManager;
        public ObservableCollection<NoteModel> notes;
        public NoteModel choiseNote;

        public MainWindow()
        {
            notes = new ObservableCollection<NoteModel>();

            InitializeComponent();
            
            this.DataContext = this;

            AddNote.Click += AddNote_Click;
            DeleteNote.Click += DeleteNote_Click;
            EditNote.Click += EditNote_Click;
            AllNotes.SelectionChanged += AllNotes_SelectionChanged;

            this.Closing += MainWindow_Closing;

            notesManager = new NotesManager();
        }

        private void AddNote_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            DeleteNote.IsEnabled = false;
            EditNote.IsEnabled = false;
            choiseNote = null;
            var noteEdit = new NoteEdit(this);
            noteEdit.ShowDialog(this);
        }

        private void DeleteNote_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            notesManager.DeleteJson(choiseNote);

            notes.Remove(choiseNote);
            AllNotes.Items.Remove(choiseNote);

            DeleteNote.IsEnabled = false;
            EditNote.IsEnabled = false;
        }

        private void EditNote_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            DeleteNote.IsEnabled = false;
            EditNote.IsEnabled = false;
            var noteEdit = new NoteEdit(this);
            noteEdit.ShowDialog(this);
        }

        private void AllNotes_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            DeleteNote.IsEnabled = true;
            EditNote.IsEnabled = true;
            choiseNote = (NoteModel)AllNotes.SelectedItem;
        }

        private async void MainWindow_Closing(object? sender, WindowClosingEventArgs e)
        {
            if (notes.Count > 0) 
            {
                await notesManager.SaveNotesJSON(notes);
            }
        }

        public void AddAllNotes() 
        {
            if (notes.Count > 0)
            {
                foreach (NoteModel note in notes)
                {
                    AllNotes.Items.Add(note);
                }
            }
        }
    }
}