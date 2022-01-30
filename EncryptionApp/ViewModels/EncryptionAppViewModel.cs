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
			get => elapsedTime;
			set
			{
				elapsedTime = value;
				OnPropertyChanged(nameof(ElapsedTime));
			}
		}

		public float Progress
		{
			get => progress;
			set
			{
				progress = value;
				OnPropertyChanged(nameof(Progress));
			}
		}

		public BaseCipherViewModel SelectedViewModel
		{
			get => selectedViewModel;
			set
			{
				selectedViewModel = value;
				OnPropertyChanged(nameof(SelectedViewModel));
			}
		}

		public string InputText
		{
			get => inputText;
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
			SelectedViewModel = parameter.ToString()! switch
			{
				"Caesar" => new CaesarCipherViewModel(this),
				"Symmetric" => new SymmetricKeyViewModel(this),
				"Substitution" => new SubstitutionCipherViewModel(this),
				_ => SelectedViewModel
			};
		}

		private void Encrypt(object obj)
		{
			if (string.IsNullOrEmpty(InputText))
			{
				MessageBox.Show(
					"Enter something to encrypt first!",
					"Nothing to encrypt",
					MessageBoxButton.OK,
					MessageBoxImage.Error);
				return;
			}

			Task.Run(() =>
			{
				CipherResult = selectedViewModel.Encrypt(InputText);
			});
		}

		private void Decrypt(object obj)
		{
			if (string.IsNullOrEmpty(InputText))
			{
				MessageBox.Show(
					"Enter something to decrypt first!",
					"Nothing to encrypt",
					MessageBoxButton.OK,
					MessageBoxImage.Error);
				return;
			}

			Task.Run(() =>
			{
				CipherResult = selectedViewModel.Decrypt(InputText);
			});
		}
	}
}