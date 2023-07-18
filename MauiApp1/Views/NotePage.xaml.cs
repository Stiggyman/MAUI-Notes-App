using System.Security.Cryptography.X509Certificates;

namespace MauiApp1.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class NotePage : ContentPage 
{
	
	
	public NotePage()
	{
		InitializeComponent();

		string appDataPath = FileSystem.AppDataDirectory;
		string randomFileName = $"{Path.GetRandomFileName()}.notes.txt";
		
		LoadNote(Path.Combine(appDataPath, randomFileName));
	}
	public string ItemId
	{
        set
		{
            LoadNote(value);
        }
    }
	private async void SaveButton_Clicked(object sender, EventArgs e)
	{
		if (BindingContext is Models.Note note)
		{
			  File.WriteAllText(note.Filename, note.Text);
		}
        await Shell.Current.GoToAsync("..");
    }

	private async void DeleteButton_Clicked(object sender, EventArgs e)
	{
		if (BindingContext is Models.Note note)
		{
			if (File.Exists(note.Filename))
			{
				File.Delete(note.Filename);
			}
		
		}

		await Shell.Current.GoToAsync("..");
    }

	private void LoadNote(string fileName)
	{
		Models.Note noteModel = new Models.Note();
		noteModel.Filename = fileName;

		if (File.Exists(fileName))
		{
			noteModel.Date = File.GetCreationTime(fileName);
			noteModel.Text = File.ReadAllText(fileName);
		}

		BindingContext = noteModel;
	}
}