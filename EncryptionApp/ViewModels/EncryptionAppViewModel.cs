using System.Threading.Tasks;
using System.Windows;
using EncryptionApp.Commands;

namespace EncryptionApp.ViewModels
{
	internal class EncryptionAppViewModel : BaseViewModel
	{
		private BaseCipherViewModel selectedViewModel;
		private string inputText;
		private float progress;
		private double elapsedTime;

		public DelegateCommand UpdateViewCommand => new(UpdateView);
		public DelegateCommand EncryptCommand => new(Encrypt);
		public DelegateCommand DecryptCommand => new(Decrypt);

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

		public BaseCipherViewModel SelectedViewModel
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

		private void Encrypt(object obj)
		{
			if (string.IsNullOrEmpty(InputText))
			{
				MessageBox.Show(
					messageBoxText: "Enter something to encrypt first!",
					caption: "Nothing to encrypt",
					button: MessageBoxButton.OK,
					icon: MessageBoxImage.Error);
				return;
			}

			Task.Run(() =>
			{
				EncryptionResult = selectedViewModel.Encrypt(InputText);
			});
		}

		private void Decrypt(object obj)
		{
			if (string.IsNullOrEmpty(InputText))
			{
				MessageBox.Show(
					messageBoxText: "Enter something to decrypt first!",
					caption: "Nothing to encrypt",
					button: MessageBoxButton.OK,
					icon: MessageBoxImage.Error);
				return;
			}

			Task.Run(() =>
			{
				EncryptionResult = selectedViewModel.Decrypt(InputText);
			});
		}
	}
}