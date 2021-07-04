using System.Windows.Input;
using DirectionClient.Commands;

namespace EncryptionApp.ViewModels
{
	internal class EncryptionAppViewModel : BaseViewModel
	{
		private BaseViewModel selectedViewModel;
		private string inputText;
		private float progress;
		private double elapsedTime;

		public double ElapsedTime
		{
			get { return elapsedTime; }
			set
			{
				elapsedTime = value;
				OnPropertyChanged(nameof(ElapsedTime));
			}
		}

		public float Progress
		{
			get { return progress; }
			set
			{
				progress = value;
				OnPropertyChanged(nameof(Progress));
			}
		}

		public BaseViewModel SelectedViewModel
		{
			get { return selectedViewModel; }
			set
			{
				selectedViewModel = value;
				OnPropertyChanged(nameof(SelectedViewModel));
			}
		}

		public string InputText
		{
			get { return inputText; }
			set
			{
				inputText = value;
				OnPropertyChanged(nameof(InputText));
			}
		}

		public ICommand UpdateViewCommand { get; set; }

		public EncryptionAppViewModel()
		{
			UpdateViewCommand = new UpdateViewCommand(this);
			SelectedViewModel = new CaesarAlgorithmViewModel(this);
		}
	}
}