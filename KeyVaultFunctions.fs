namespace Test

module KeyVaultFunctions =
    open Microsoft.Azure.KeyVault
    open Microsoft.Azure.Services.AppAuthentication
    open System.Threading.Tasks

    let tokenProvider = new AzureServiceTokenProvider()

    let callback a b c =
        printfn "In callback with %A %A %A!" a b c
        let callbackResult = tokenProvider.KeyVaultTokenCallback.Invoke(a, b, c)
        printfn "Callback finished"
        callbackResult

    let KeyVaultTest =
        let kvc = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(callback))
        let secret = kvc.GetSecretAsync("https://SomeKeyVault.vault.azure.net/Secrets/SomeSecret").GetAwaiter().GetResult().Value
        secret
