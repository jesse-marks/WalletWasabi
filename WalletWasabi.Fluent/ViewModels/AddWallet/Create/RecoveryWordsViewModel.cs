using System.Collections.Generic;
using System.Reactive.Disposables;
using NBitcoin;
using ReactiveUI;
using WalletWasabi.Blockchain.Keys;
using WalletWasabi.Fluent.ViewModels.Navigation;

namespace WalletWasabi.Fluent.ViewModels.AddWallet.Create;

[NavigationMetaData(Title = "Recovery Words")]
public partial class RecoveryWordsViewModel : RoutableViewModel
{
	public RecoveryWordsViewModel(Mnemonic mnemonic, string walletName)
	{
		MnemonicWords = new List<RecoveryWordViewModel>();

		for (int i = 0; i < mnemonic.Words.Length; i++)
		{
			MnemonicWords.Add(new RecoveryWordViewModel(i + 1, mnemonic.Words[i]));
		}

		EnableBack = true;

		NextCommand = ReactiveCommand.Create(() => OnNext(mnemonic, walletName));

		CancelCommand = ReactiveCommand.Create(OnCancel);
	}

	public List<RecoveryWordViewModel> MnemonicWords { get; set; }

	private void OnNext(Mnemonic mnemonic, string walletName)
	{
		Navigate().To(new ConfirmRecoveryWordsViewModel(MnemonicWords, mnemonic, walletName));
	}

	private void OnCancel()
	{
		Navigate().Clear();
	}

	protected override void OnNavigatedTo(bool isInHistory, CompositeDisposable disposables)
	{
		var enableCancel = Services.WalletManager.HasWallet();
		SetupCancel(enableCancel: enableCancel, enableCancelOnEscape: enableCancel, enableCancelOnPressed: false);

		base.OnNavigatedTo(isInHistory, disposables);
	}
}
