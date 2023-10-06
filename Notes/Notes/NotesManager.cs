using Avalonia.Styling;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Notes
{
    public class NotesManager
    {
        string folderNotes;
        public void SetFolderNotes(string folder) {folderNotes = folder;}
        public string GetFolderNotes() { return folderNotes; }

        ObservableCollection<NoteModel> result = new ObservableCollection<NoteModel>();

        // Функция записи текущих экземпляров класса NoteModel в формат Json файлов
        public async Task SaveNotesJSON(ObservableCollection<NoteModel> notes) 
        {
            // Перебираем всю коллекцию _notes и сохраняем элемент каждой из них в файл формата Json
            foreach (var note in notes) 
            {
                using (FileStream fs = new FileStream(Path.Combine(folderNotes, note.Date.ToString("dd.MM.yyyy HH_mm_ss").Replace(":", "_") + ".json"), FileMode.OpenOrCreate))
                {
                    await JsonSerializer.SerializeAsync(fs, note); 
                }
            }
        }

        // Функция загрузки файлов из формата Json в формат NoteModel
        public ObservableCollection<NoteModel> LoadNotesJSON() 
        {
            // Загружаем в массив названия всех файлов формата Json
            string[] jsonFiles = Directory.GetFiles(folderNotes, "*.json");
            result = new ObservableCollection<NoteModel>();

            // Перебираем весь массив названий файлов и по ним производим создание новых экземпляров
            // класса NoteModel и их добавления в коллекцию 
            foreach (string jsonFile in jsonFiles)
            {
                using (FileStream fs = new FileStream(jsonFile, FileMode.OpenOrCreate))
                {
                    NoteModel newNote = new NoteModel();
                    newNote = JsonSerializer.Deserialize<NoteModel>(fs);
                    result.Add(newNote);
                }
            }

            return result;
        }

        // Удаление файла Json указанного экземпляра класса
        public void DeleteJson(NoteModel note) 
        {
            FileInfo fileInfo = new FileInfo(Path.Combine(folderNotes, note.Date.ToString("dd.MM.yyyy HH_mm_ss").Replace(":", "_") + ".json"));
            if (fileInfo.Exists) 
            {
                fileInfo.Delete();
            }
        }
    }
}