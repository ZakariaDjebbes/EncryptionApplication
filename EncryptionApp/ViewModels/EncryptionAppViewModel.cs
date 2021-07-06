using EncryptionApp.Commands;

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

		public DelegateCommand UpdateViewCommand => new(UpdateView);

		public EncryptionAppViewModel()
		{
			SelectedViewModel = new CaesarCipherViewModel(this);
		}

		private void UpdateView(object parameter)
		{
			if (parameter.ToString().Equals("Caesar"))
			{
				SelectedViewModel = new CaesarCipherViewModel(this);
			}

			if (parameter.ToString().Equals("Symmetric"))
			{
				SelectedViewModel = new SymmetricKeyViewModel();
			}

			if (parameter.ToString().Equals("Substitution"))
			{
				SelectedViewModel = new SubstitutionCipherViewModel(this);
			}
		}
	}
}