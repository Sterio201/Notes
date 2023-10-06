using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Notes
{
    // Класс, представляющий модель данных заметок, для возможности сохранения в Json файлы проводится
    // его сериализация
    [Serializable]
    public class NoteModel
    {
        [JsonPropertyName("date")] // Указываем имя свойства в JSON
        public DateTime Date { get; set; }

        [JsonPropertyName("textNote")] // Указываем имя свойства в JSON
        public string TextNote { get; set; }

        public NoteModel() { }  

        public NoteModel(DateTime dateTime, string text) 
        {
            Date = dateTime;
            TextNote = text;
        }

        public override string ToString()
        {
            string title = (TextNote.Length > 10) ? TextNote.Substring(0, 10) : TextNote;
            return $"{Date:dd.MM.yyyy HH_mm_ss} {title}...";
        }
    }
}