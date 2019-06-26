# liff-app-on-blazor
C# wrapper of LIFF client API for use in Blazor applications.

## Demo Site 
Published on Github Pages
https://pierre3.github.io/liff-app-on-blazor/

## Usage

```cs
public interface ILiff
{
    bool Initialized { get; }
    LiffData Data { get; }
    string Error { get; }
    Profile Profile { get; }

    event EventHandler<InitErrorEventArgs> InitError;
    event EventHandler<InitSuccessEventArgs> InitSuccess;

    Task CloseWindowAsync();
    Task<string> GetAccessTokenAsync();
    Task InitializeAsync(IJSRuntime jSRuntime);
    Task LoadProfileAsync();
    Task OpenWindowAsync(string url, bool external);
    void Reset();
    Task SendMessagesAsync(object messages);
}
```

```cs
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ILiff, Liff>();
    }

    public void Configure(IComponentsApplicationBuilder app)
    {
        app.AddComponent<App>("app");
    }
}
```

```cshtml
@page "/"
@inject ILiff Liff
@inject IJSRuntime JSRuntime

<div class="card" style="width: 20rem;">
    @if (Profile != null)
    {
    <img class="card-img" src="@Profile?.PictureUrl" />
    <div class="card-body">
        <h5 class="card-title">@Profile?.DisplayName</h5>
        <p class="card-text">@Profile?.StatusMessage</p>
    </div>
    }
    else
    {
    <div class="card-body">
        <button class="btn btn-info" onclick="@LoadProfileAsync">プロファイル読み込み</button>
    </div>
    }
    <ul class="list-group">
        <li class="list-group-item">Language: @Data?.Language</li>
        <li class="list-group-item">Type: @Data?.Context.Type</li>
        <li class="list-group-item">ViewType: @Data?.Context.ViewType</li>
        <li class="list-group-item">UserId: @Data?.Context.UserId</li>
        @if (@Data?.Context.Type == ContextType.Utou)
        {
        <li class="list-group-item">UtouId: @Data?.Context.UtouId</li>
        }
        else if (@Data?.Context.Type == ContextType.Room)
        {
        <li class="list-group-item">RoomId: @Data?.Context.RoomId</li>
        }
        else if (@Data?.Context.Type == ContextType.Group)
        {
        <li class="list-group-item">GroupId: @Data?.Context.GroupId</li>
        }
    </ul>
</div>

@functions{
    LiffData Data { get; set;}
    Profile Profile { get; set;}

    protected override async Task OnInitAsync()
    {
        Update();
        
        Liff.InitSuccess += async (_, __) =>
        {
            Update();
            await LoadProfileAsync();               
        };
 
        Liff.InitError += async (_, args) =>
            await JSRuntime.InvokeAsync<object>("liffInterop.alert", args.Error);
        
        await Liff.InitializeAsync(JSRuntime);
    }

    private async Task LoadProfileAsync()
    {
        try
        {
            await Liff.LoadProfileAsync();
        }
        catch (Exception e)
        {
            await JSRuntime.InvokeAsync<object>("liffInterop.alert", e.ToString());
        }
        Update();
    }

    private void Update()
    {
        Data = Liff.Data;
        Profile = Liff.Profile;
        StateHasChanged();
    }
}
```
