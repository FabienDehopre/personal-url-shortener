using Microsoft.AspNetCore.Components;

namespace PersonalUrlShortener.Client.Components;

public partial class TailwindTransitionalElement : TailwindTransitionalComponentBase
{
    [Parameter] public bool IsOpened { get; set; }

    [Parameter] public EventCallback<bool> IsOpenedChanged { get; set; }

    private bool _oldStatus;
    private readonly List<TailwindTransition> _transitions = new();

    private bool _isFirstRender = true;

    protected override async Task OnParametersSetAsync()
    {
        if (_oldStatus != IsOpened && !_isFirstRender)
        {
            _oldStatus = IsOpened;
            if (IsOpened)
            {
                SetHiddenClass();
            }
            await ToggleAsync();
        }
        else
        {
            SetHiddenClass();
        }
    }

    protected override void OnInitialized()
    {
        _oldStatus = IsOpened;
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            _isFirstRender = false;
        }
    }

    private string _hiddenClass = "";
    private bool IsTransitional => !string.IsNullOrWhiteSpace(Entering) || !string.IsNullOrWhiteSpace(Leaving);

    public async Task Toggle()
    {
        await InvokeAsync(async () =>
        {
            if (IsOpened)
            {
                await ShowAsync();
            }
            else
            {
                await HideAsync();
            }
        });
    }

    private async Task ToggleAsync()
    {
        var tasks = new List<Task>();
        if (IsTransitional)
        {
            tasks.Add(Toggle());
        }
        
        foreach (var item in _transitions)
        {
            tasks.Add(item.Toggle());
        }

        //Console.WriteLine(tasks.Count());
        // Run all the tasks
        await Task.WhenAll(tasks);

        await IsOpenedChanged.InvokeAsync(IsOpened);

        SetHiddenClass();
    }

    internal void AddTransition(TailwindTransition transition)
    {
        _transitions.Add(transition);
    }

    private void SetHiddenClass()
    {
        _hiddenClass = IsOpened ? "" : "hidden";
        StateHasChanged();
    }
}