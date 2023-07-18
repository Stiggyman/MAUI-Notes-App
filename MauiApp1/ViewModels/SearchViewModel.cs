using CommunityToolkit.Mvvm.ComponentModel;
using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiApp1.ViewModels
{
    internal class SearchViewModel : ObservableObject, IQueryAttributable
    {
        private string _searchKeyword = string.Empty;
        private ObservableCollection<Note> _notes = new ObservableCollection<Note>();

        public ICommand SelectNoteCommand { get; }
        public string SearchKeyword
        {
            get => _searchKeyword;
            set
            {
                if (_searchKeyword != value)
                {
                    _searchKeyword = value;
                        OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Note> Notes
        {
            get => _notes;
            set
            {
                if (_notes != value)
                {
                    _notes = value;
                    OnPropertyChanged();
                }
            }
        }
        public ICommand UpdateSearchResultsCommand => new Command(updateSearchResults);

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            throw new NotImplementedException();
        }

        public void updateSearchResults()
        {
            Notes.Clear();
            IEnumerable<Note> notes = Note.Search(SearchKeyword);
            foreach (Note note in notes)
            {
                Notes.Add(note);
            }
        }

        private async Task SelectNoteAsync(ViewModels.NoteViewModel note)
        {
            if (note != null)
            {
                await Shell.Current.GoToAsync($"{nameof(Views.NotePage)}?load={note.Identifier}");
            }
        }
    }
}
